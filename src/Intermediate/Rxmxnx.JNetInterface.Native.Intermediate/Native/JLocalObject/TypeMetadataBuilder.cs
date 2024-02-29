namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	internal ref struct TypeMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		private readonly JTypeKind _kind;
		/// <summary>
		/// Interface types.
		/// </summary>
		private readonly ISet<Type> _interfaceTypes;

		/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
		private HashSet<JInterfaceTypeMetadata>? _interfaces;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		public ReadOnlySpan<Byte> DataTypeName { get; }
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		public ReadOnlySpan<Byte> Signature { get; private set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		/// <param name="kind">Java datatype kind.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		public TypeMetadataBuilder(ReadOnlySpan<Byte> dataTypeName, JTypeKind kind, ISet<Type> interfaceTypes)
		{
			this.DataTypeName = dataTypeName;
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
			this.Signature = signature;
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
					this.DataTypeName, this._kind is not JTypeKind.Interface);

			foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			{
				if (!this._interfaceTypes.Contains(interfaceMetadata.InterfaceType))
					NativeValidationUtilities.ThrowInvalidImplementation<TInterface>(
						this.DataTypeName, this._kind is not JTypeKind.Interface);
			}
			this._interfaces ??= [];
			this._interfaces.Add(metadata);
		}
		/// <summary>
		/// Creates a metadata interfaces set for current datatype.
		/// </summary>
		/// <returns>A set with current datatype interfaces.</returns>
		public IReadOnlySet<JInterfaceTypeMetadata> GetInterfaceSet()
			=> this._interfaces ?? (IReadOnlySet<JInterfaceTypeMetadata>)ImmutableHashSet<JInterfaceTypeMetadata>.Empty;
	}

	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	protected ref partial struct TypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass>
		where TClass : JLocalObject, IClassType<TClass>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;
		/// <summary>
		/// A <see cref="TypeMetadataBuilder"/> instance.
		/// </summary>
		private TypeMetadataBuilder _builder;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type metadata of current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private TypeMetadataBuilder(ReadOnlySpan<Byte> className, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata, ISet<Type> interfaceTypes)
		{
			NativeValidationUtilities.ThrowIfInvalidTypeBuilder(className, TClass.FamilyType);
			this._builder = new(className, JTypeKind.Class, interfaceTypes);
			this._baseMetadata = baseMetadata;
			this._modifier = modifier;
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public TypeMetadataBuilder<TClass> Implements<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this._builder.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JClassTypeMetadata<TClass> Build()
			=> new ClassTypeMetadata(this._builder, this._modifier, this._baseMetadata);

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		internal TypeMetadataBuilder<TClass> WithSignature(CString signature)
		{
			this._builder.WithSignature(signature);
			return this;
		}

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TClass}"/> instance.</returns>
		public static TypeMetadataBuilder<TClass> Create(ReadOnlySpan<Byte> className,
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
		/// <returns>A new <see cref="TypeMetadataBuilder{TClass}"/> instance.</returns>
		public static TypeMetadataBuilder<TObject>
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

		/// <summary>
		/// Creates a <see cref="JClassTypeMetadata{TClass}"/> instance.
		/// </summary>
		/// <param name="builder">A <see cref="TypeMetadataBuilder"/> instance.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type metadata of current type.</param>
		/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
		internal static JClassTypeMetadata<TClass> Build(TypeMetadataBuilder builder, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata)
			=> new ClassTypeMetadata(builder, modifier, baseMetadata);
		/// <summary>
		/// Creates a <see cref="JClassTypeMetadata{TClass}"/> instance for <paramref name="primitiveMetadata"/>
		/// wrapper class.
		/// </summary>
		/// <param name="primitiveMetadata">A <see cref="JPrimitiveTypeMetadata"/> instance.</param>
		/// <param name="baseMetadata">Base type metadata of current type.</param>
		/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
		internal static JClassTypeMetadata<TClass> Build(JPrimitiveTypeMetadata primitiveMetadata,
			JClassTypeMetadata? baseMetadata = default)
			=> new ClassTypeMetadata(primitiveMetadata.WrapperInformation, primitiveMetadata.SizeOf == 0, baseMetadata);
	}
}