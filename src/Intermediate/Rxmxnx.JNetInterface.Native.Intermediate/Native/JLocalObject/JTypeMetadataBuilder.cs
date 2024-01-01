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
		/// Base types.
		/// </summary>
		private readonly ISet<Type> _baseTypes;
		/// <summary>
		/// Interface types.
		/// </summary>
		private readonly ISet<Type> _interfaceTypes;
		/// <summary>
		/// Function to retrieve implementing type.
		/// </summary>
		private readonly Func<JInterfaceTypeMetadata, Type> _getImplementingType;
		/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
		private readonly HashSet<JInterfaceTypeMetadata> _interfaces = [];

		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private ReadOnlySpan<Byte> _signature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		public ReadOnlySpan<Byte> DataTypeName => this._dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		public ReadOnlySpan<Byte> Signature => this._signature;
		/// <inheritdoc cref="JDataTypeMetadata.BaseTypes"/>
		public ISet<Type> BaseTypes => this._baseTypes;
		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		public JTypeKind Kind => this._kind;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		/// <param name="kind">Java datatype kind.</param>
		/// <param name="getImplementingType">Delegate for retrieve implementing type.</param>
		/// <param name="baseTypes">Base types.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		public JTypeMetadataBuilder(ReadOnlySpan<Byte> dataTypeName, JTypeKind kind,
			Func<JInterfaceTypeMetadata, Type> getImplementingType, ISet<Type> baseTypes, ISet<Type> interfaceTypes)
		{
			this._dataTypeName = dataTypeName;
			this._baseTypes = baseTypes;
			this._interfaceTypes = interfaceTypes;
			this._kind = kind;
			this._getImplementingType = getImplementingType;
		}
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		/// <param name="kind">Java datatype kind.</param>
		/// <param name="getImplementingType">Delegate for retrieve implementing type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		public JTypeMetadataBuilder(ReadOnlySpan<Byte> dataTypeName, JTypeKind kind,
			Func<JInterfaceTypeMetadata, Type> getImplementingType, ISet<Type> interfaceTypes)
		{
			this._dataTypeName = dataTypeName;
			this._baseTypes = ImmutableHashSet<Type>.Empty;
			this._interfaceTypes = interfaceTypes;
			this._kind = kind;
			this._getImplementingType = getImplementingType;
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
		public void AppendInterface<TInterface>(Type implementingType)
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			if (!this._interfaceTypes.Contains(implementingType))
				NativeValidationUtilities.ThrowInvalidImplementation<TInterface>(
					this._dataTypeName, this._kind != JTypeKind.Interface);

			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			{
				if (!this._interfaceTypes.Contains(this._getImplementingType(interfaceMetadata)))
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

		/// <summary>
		/// Retrieves implementing type of <typeparamref name="TInterface"/> in <typeparamref name="TReference"/>.
		/// </summary>
		/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
		/// <typeparam name="TInterface">A <see cref="IInterfaceType{TInterface}"/> type.</typeparam>
		/// <returns>A <see cref="Type"/> instance.</returns>
		public static Type GetImplementingType<TReference, TInterface>()
			where TReference : JLocalObject, IReferenceType<TReference>
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
			=> typeof(IDerivedType<TReference, TInterface>);
		/// <summary>
		/// Retrieves implementing type of <paramref name="interfaceMetadata"/> in <typeparamref name="TReference"/>.
		/// </summary>
		/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
		/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		/// <returns>A <see cref="Type"/> instance.</returns>
		public static Type GetImplementingType<TReference>(JInterfaceTypeMetadata interfaceMetadata)
			where TReference : JLocalObject, IReferenceType<TReference>
			=> interfaceMetadata.GetImplementingType<TReference>();
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
		/// <param name="baseTypes">Base types.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(ReadOnlySpan<Byte> className, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata, ISet<Type> baseTypes, ISet<Type> interfaceTypes)
		{
			this._builder = new(className, JTypeKind.Class, JTypeMetadataBuilder.GetImplementingType<TClass>, baseTypes,
			                    interfaceTypes);
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
			this._builder.AppendInterface<TInterface>(JTypeMetadataBuilder.GetImplementingType<TClass, TInterface>());
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
			ISet<Type> baseTypes = IReferenceType<TClass>.GetBaseTypes().ToHashSet();
			ISet<Type> interfaceTypes = IReferenceType<TClass>.GetInterfaceTypes().ToHashSet();
			JClassTypeMetadata? baseMetadata = typeof(TClass) != typeof(JLocalObject) ?
				IClassType.GetMetadata<JLocalObject>() :
				default;
			return new(className, modifier, baseMetadata, baseTypes, interfaceTypes);
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
			ISet<Type> baseTypes = NativeValidationUtilities.ValidateBaseTypes<TClass, TObject>(className);
			ISet<Type> interfaceTypes = IReferenceType<TObject>.GetInterfaceTypes().ToHashSet();
			return new(className, modifier, IClassType.GetMetadata<TClass>(), baseTypes, interfaceTypes);
		}
	}
}