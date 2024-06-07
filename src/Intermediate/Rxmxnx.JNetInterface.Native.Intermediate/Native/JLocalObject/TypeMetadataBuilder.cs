namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	/// <remarks>
	/// Constructor.
	/// </remarks>
	/// <param name="dataTypeName">Datatype name.</param>
	/// <param name="kind">Java datatype kind.</param>
	/// <param name="interfaceTypes">Interface types.</param>
	internal ref struct TypeMetadataBuilder(
		ReadOnlySpan<Byte> dataTypeName,
		JTypeKind kind,
		IReadOnlySet<Type> interfaceTypes)
	{
		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		private readonly JTypeKind _kind = kind;
		/// <summary>
		/// Interface types.
		/// </summary>
		private readonly IReadOnlySet<Type> _interfaceTypes = interfaceTypes;

		/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
		private HashSet<JInterfaceTypeMetadata>? _interfaces;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		public ReadOnlySpan<Byte> DataTypeName { get; } = dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		public ReadOnlySpan<Byte> Signature { get; private set; }
		/// <summary>
		/// Indicates whether current type is annotation.
		/// </summary>
		public readonly Boolean IsAnnotation => this._kind is JTypeKind.Annotation;

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public void WithSignature(ReadOnlySpan<Byte> signature)
		{
			CommonValidationUtilities.ThrowIfInvalidSignature(signature, false);
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
			if (IVirtualMachine.MetadataValidationEnabled)
				NativeValidationUtilities.ThrowIfAnnotation(this.DataTypeName, IInterfaceType.GetMetadata<TInterface>(),
				                                            this.IsAnnotation);
			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			if (IVirtualMachine.MetadataValidationEnabled)
			{
				// Validates current interface.
				if (!this._interfaceTypes.Contains(metadata.InterfaceType))
					NativeValidationUtilities.ThrowInvalidImplementation(this.DataTypeName, metadata,
					                                                     this._kind is not JTypeKind.Interface);

				// Validates superinterfaces from current interface.
				SuperInterfaceValidationState state = new() { Interfaces = this._interfaceTypes, NotContained = [], };
				metadata.Interfaces.ForEach(state, TypeMetadataBuilder.ValidateSuperinterface);
				NativeValidationUtilities.ThrowIfInvalidImplementation(this.DataTypeName, metadata, state.NotContained,
				                                                       this._kind is not JTypeKind.Interface);
			}
			this._interfaces ??= [];
			this._interfaces.Add(metadata);
		}

		/// <summary>
		/// Creates a metadata interfaces set for the current datatype.
		/// </summary>
		/// <returns>A set with current datatype interfaces.</returns>
		public readonly IReadOnlySet<JInterfaceTypeMetadata> GetInterfaceSet()
			=> this._interfaces ?? (IReadOnlySet<JInterfaceTypeMetadata>)ImmutableHashSet<JInterfaceTypeMetadata>.Empty;

		/// <summary>
		/// Checks implementation of a the superinterface type from <paramref name="interfaceMetadata"/>.
		/// </summary>
		/// <param name="state">A <see cref="SuperInterfaceValidationState"/> instance.</param>
		/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		private static void ValidateSuperinterface(SuperInterfaceValidationState state,
			JInterfaceTypeMetadata interfaceMetadata)
		{
			if (!state.Interfaces.Contains(interfaceMetadata.InterfaceType))
				state.NotContained.Add(interfaceMetadata.ClassName);
		}
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
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <param name="baseMetadata">Base type metadata of the current type.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private TypeMetadataBuilder(ReadOnlySpan<Byte> className, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata, IReadOnlySet<Type> interfaceTypes)
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
		public TypeMetadataBuilder<TClass> Implements<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this._builder.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public readonly JClassTypeMetadata<TClass> Build()
			=> new ClassTypeMetadata(this._builder, this._modifier, this._baseMetadata);

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		internal TypeMetadataBuilder<TClass> WithSignature(ReadOnlySpan<Byte> signature)
		{
			this._builder.WithSignature(signature);
			return this;
		}

		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder{TClass}"/> instance.
		/// </summary>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TClass}"/> instance.</returns>
		public static TypeMetadataBuilder<TClass> Create(ReadOnlySpan<Byte> className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			CommonValidationUtilities.ValidateNotEmpty(className);
			JClassTypeMetadata? baseMetadata = !JLocalObject.IsObjectType<TClass>() ?
				IClassType.GetMetadata<JLocalObject>() :
				default;
			return !IVirtualMachine.MetadataValidationEnabled ?
				new(className, modifier, baseMetadata, ImmutableHashSet<Type>.Empty) :
				new(className, modifier, baseMetadata, IReferenceType<TClass>.TypeInterfaces);
		}
		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder{TOBject}"/> instance.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TObject}"/> instance.</returns>
		public static TypeMetadataBuilder<TObject>
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
				ReadOnlySpan<Byte> className, JTypeModifier modifier = JTypeModifier.Extensible)
			where TObject : TClass, IClassType<TObject>
		{
			CommonValidationUtilities.ValidateNotEmpty(className);
			NativeValidationUtilities.ThrowIfSameType(className, typeof(TClass), typeof(TObject));
			if (!IVirtualMachine.MetadataValidationEnabled)
				return new(className, modifier, IClassType.GetMetadata<TClass>(), ImmutableHashSet<Type>.Empty);
			NativeValidationUtilities.ValidateBaseTypes(className, IClassType<TObject>.TypeBaseTypes,
			                                            IClassType<TClass>.TypeBaseTypes);
			return new(className, modifier, IClassType.GetMetadata<TClass>(), IReferenceType<TObject>.TypeInterfaces);
		}

		/// <summary>
		/// Creates a <see cref="JClassTypeMetadata{TClass}"/> instance.
		/// </summary>
		/// <param name="builder">A <see cref="TypeMetadataBuilder"/> instance.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <param name="baseMetadata">Base type metadata of the current type.</param>
		/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
		internal static JClassTypeMetadata<TClass> Build(TypeMetadataBuilder builder, JTypeModifier modifier,
			JClassTypeMetadata? baseMetadata)
			=> new ClassTypeMetadata(builder, modifier, baseMetadata);
		/// <summary>
		/// Creates a <see cref="JClassTypeMetadata{TClass}"/> instance for <paramref name="primitiveMetadata"/>
		/// wrapper class.
		/// </summary>
		/// <param name="primitiveMetadata">A <see cref="JPrimitiveTypeMetadata"/> instance.</param>
		/// <param name="baseMetadata">Base type metadata of the current type.</param>
		/// <returns>A <see cref="JClassTypeMetadata{TClass}"/> instance.</returns>
		internal static JClassTypeMetadata<TClass> Build(JPrimitiveTypeMetadata primitiveMetadata,
			JClassTypeMetadata? baseMetadata = default)
			=> new ClassTypeMetadata(primitiveMetadata.WrapperInformation, primitiveMetadata.SizeOf == 0,
			                         baseMetadata ?? IClassType.GetMetadata<JLocalObject>());
	}
}