namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	internal ref struct JTypeMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private readonly ReadOnlySpan<Byte> _dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		private readonly JTypeKind _kind;
		/// <summary>
		/// Interface types.
		/// </summary>
		private readonly ISet<Type> _interfaceTypes;
		/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
		private readonly HashSet<JInterfaceTypeMetadata> _interfaces = [];

		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private ReadOnlySpan<Byte> _signature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		public ReadOnlySpan<Byte> DataTypeName => this._dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		public ReadOnlySpan<Byte> Signature => this._signature;
		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		public JTypeKind Kind => this._kind;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		/// <param name="kind">Java datatype kind.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		public JTypeMetadataBuilder(ReadOnlySpan<Byte> dataTypeName, JTypeKind kind, ISet<Type> interfaceTypes)
		{
			this._dataTypeName = dataTypeName;
			this._interfaceTypes = interfaceTypes;
			this._kind = kind;
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public void WithSignature(ReadOnlySpan<Byte> signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, false);
			this._signature = signature;
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public void AppendInterface<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			if (!this._interfaceTypes.Contains(metadata.InterfaceType))
				NativeValidationUtilities.ThrowInvalidImplementation<TInterface>(
					this._dataTypeName, this._kind != JTypeKind.Interface);

			foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			{
				if (!this._interfaceTypes.Contains(interfaceMetadata.InterfaceType))
					NativeValidationUtilities.ThrowInvalidImplementation<TInterface>(
						this._dataTypeName, this._kind != JTypeKind.Interface);
			}
			this._interfaces.Add(metadata);
		}
		/// <summary>
		/// Creates a metadata interfaces set for current datatype.
		/// </summary>
		/// <returns>A set with current datatype interfaces.</returns>
		public IImmutableSet<JInterfaceTypeMetadata> CreateInterfaceSet() => this._interfaces.ToImmutableHashSet();
	}

	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
	protected ref partial struct JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>
		where TClass : JLocalObject, IClassType<TClass>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;
		/// <summary>
		/// A <see cref="JTypeMetadataBuilder"/> instance.
		/// </summary>
		private JTypeMetadataBuilder _builder;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type metadata of current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(ReadOnlySpan<Byte> className, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata, ISet<Type> interfaceTypes)
		{
			this._builder = new(className, JTypeKind.Class, interfaceTypes);
			this._baseMetadata = baseMetadata;
			this._modifier = modifier;
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TClass> Implements<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this._builder.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JClassTypeMetadata Build()
			=> new JClassGenericTypeMetadata(this._builder, this._modifier, this._baseMetadata);

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		internal JTypeMetadataBuilder<TClass> WithSignature(CString signature)
		{
			this._builder.WithSignature(signature);
			return this;
		}

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TClass}"/> instance.</returns>
		public static JTypeMetadataBuilder<TClass> Create(ReadOnlySpan<Byte> className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> interfaceTypes = IReferenceType<TClass>.GetInterfaceTypes().ToHashSet();
			JClassTypeMetadata? baseMetadata = !JLocalObject.IsObjectType<TClass>() ?
				IClassType.GetMetadata<JLocalObject>() :
				default;
			return new(className, modifier, baseMetadata, interfaceTypes);
		}
		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TClass}"/> instance.</returns>
		public static JTypeMetadataBuilder<TObject>
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
				ReadOnlySpan<Byte> className, JTypeModifier modifier = JTypeModifier.Extensible)
			where TObject : TClass, IClassType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			NativeValidationUtilities.ThrowIfSameType<TClass, TObject>(className);
			NativeValidationUtilities.ValidateBaseTypes<TClass, TObject>(className);
			ISet<Type> interfaceTypes = IReferenceType<TObject>.GetInterfaceTypes().ToHashSet();
			return new(className, modifier, IClassType.GetMetadata<TClass>(), interfaceTypes);
		}
	}
}