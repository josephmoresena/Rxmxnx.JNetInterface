namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
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
		/// <summary>
		/// Indicates whether current type is annotation.
		/// </summary>
		public readonly Boolean IsAnnotation => this._kind is JTypeKind.Annotation;

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		public void AppendInterface<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			this.AppendInterface(metadata);
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <param name="baseMetadata">Current type base definition.</param>
		/// <typeparam name="TInterface"></typeparam>
		public void AppendInterface<TInterface>(JClassTypeMetadata? baseMetadata)
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			if (baseMetadata is not null && baseMetadata.Interfaces.Contains(metadata)) return;
			this.AppendInterface(metadata);
			if (this._interfaces is not null)
				metadata.Interfaces.ForEach(
					new SuperInterfaceImplementationState
					{
						BaseInterfaces = baseMetadata?.BaseMetadata?.Interfaces ?? InterfaceSet.Empty,
						Interfaces = this._interfaces!,
					}, TypeMetadataBuilder.AppendSuperinterface);
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <param name="metadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		private void AppendInterface(JInterfaceTypeMetadata metadata)
		{
			if (IVirtualMachine.MetadataValidationEnabled)
				NativeValidationUtilities.ThrowIfAnnotation(this.DataTypeName, metadata, this.IsAnnotation);
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
		/// Checks implementation of the superinterface type from <paramref name="interfaceMetadata"/>.
		/// </summary>
		/// <param name="state">A <see cref="SuperInterfaceValidationState"/> instance.</param>
		/// <param name="interfaceMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		private static void ValidateSuperinterface(SuperInterfaceValidationState state,
			JInterfaceTypeMetadata interfaceMetadata)
		{
			if (!state.Interfaces.Contains(interfaceMetadata.InterfaceType))
				state.NotContained.Add(interfaceMetadata.ClassName);
		}
		/// <summary>
		/// Appends a superinterface to current type definition.
		/// </summary>
		/// <param name="state">A <see cref="SuperInterfaceImplementationState"/> instance.</param>
		/// <param name="metadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		private static void AppendSuperinterface(SuperInterfaceImplementationState state,
			JInterfaceTypeMetadata metadata)
		{
			if (!state.BaseInterfaces.Contains(metadata))
				state.Interfaces.Add(metadata);
		}
	}

	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	protected ref struct TypeMetadataBuilder<
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
			if (IVirtualMachine.MetadataValidationEnabled)
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
			this._builder.AppendInterface<TInterface>(this._baseMetadata);
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public readonly JClassTypeMetadata<TClass> Build()
			=> new ClassTypeMetadata<TClass>(this._builder, this._modifier, this._baseMetadata);

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
			return !IVirtualMachine.MetadataValidationEnabled ?
				new(className, modifier, IClassType.GetMetadata<TClass>(), ImmutableHashSet<Type>.Empty) :
				TypeMetadataBuilder<TClass>.CreateWithValidation<TObject>(className, modifier);
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
			=> new ClassTypeMetadata<TClass>(builder, modifier, baseMetadata);

		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder{TObject}"/> instance with validation.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TObject}"/> instance.</returns>
		private static TypeMetadataBuilder<TObject> CreateWithValidation<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
			ReadOnlySpan<Byte> className, JTypeModifier modifier) where TObject : TClass, IClassType<TObject>
		{
			IReadOnlySet<Type> baseTypes = IClassType<TObject>.TypeBaseTypes;
			IReadOnlySet<Type> baseBaseTypes = IClassType<TClass>.TypeBaseTypes;
			NativeValidationUtilities.ValidateBaseTypes(className, baseTypes, baseBaseTypes);
			IReadOnlySet<Type> interfaceTypes = IReferenceType<TObject>.TypeInterfaces;
			return new(className, modifier, IClassType.GetMetadata<TClass>(), interfaceTypes);
		}
	}
}