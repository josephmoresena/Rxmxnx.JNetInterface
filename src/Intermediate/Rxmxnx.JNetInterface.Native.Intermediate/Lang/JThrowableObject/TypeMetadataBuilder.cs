namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> throwable builder.
	/// </summary>
	/// <typeparam name="TThrowable">Type of <c/>java.lang.Throwable<c/> class.</typeparam>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	protected new ref struct TypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable>
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;
		/// <summary>
		/// A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.
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
			this._builder = new(className, JTypeKind.Class, interfaceTypes);
			this._baseMetadata = baseMetadata;
			this._modifier = modifier;
		}

		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public TypeMetadataBuilder<TThrowable> Implements<
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
		public readonly JThrowableTypeMetadata<TThrowable> Build()
		{
			JClassTypeMetadata<TThrowable> classMetadata =
				JLocalObject.TypeMetadataBuilder<TThrowable>.Build(this._builder, this._modifier, this._baseMetadata);
			return new(classMetadata);
		}

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TThrowable}"/> instance.</returns>
		public static TypeMetadataBuilder<TThrowable> Create(ReadOnlySpan<Byte> className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			CommonValidationUtilities.ValidateNotEmpty(className);
			JClassTypeMetadata baseMetadata = typeof(TThrowable) != typeof(JThrowableObject) ?
				IClassType.GetMetadata<JThrowableObject>() :
				IClassType.GetMetadata<JLocalObject>();
			return !IVirtualMachine.MetadataValidationEnabled ?
				new(className, modifier, baseMetadata, ImmutableHashSet<Type>.Empty) :
				TypeMetadataBuilder<TThrowable>.CreateWithValidation(className, baseMetadata, modifier);
		}
		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TThrowable}"/> instance.</returns>
		public static TypeMetadataBuilder<TObject>
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
				ReadOnlySpan<Byte> className, JTypeModifier modifier = JTypeModifier.Extensible)
			where TObject : TThrowable, IThrowableType<TObject>
		{
			CommonValidationUtilities.ValidateNotEmpty(className);
			NativeValidationUtilities.ThrowIfSameType(className, typeof(TThrowable), typeof(TObject));
			return !IVirtualMachine.MetadataValidationEnabled ?
				new(className, modifier, IClassType.GetMetadata<TThrowable>(), ImmutableHashSet<Type>.Empty) :
				TypeMetadataBuilder<TThrowable>.CreateWithValidation<TObject>(className, modifier);
		}

		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder{TObject}"/> instance with validation.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TObject}"/> instance.</returns>
		private static TypeMetadataBuilder<TObject> CreateWithValidation<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
			ReadOnlySpan<Byte> className, JTypeModifier modifier) where TObject : TThrowable, IThrowableType<TObject>
		{
			IReadOnlySet<Type> baseTypes = IClassType<TObject>.TypeBaseTypes;
			IReadOnlySet<Type> baseBaseTypes = IClassType<TThrowable>.TypeBaseTypes;
			NativeValidationUtilities.ValidateBaseTypes(className, baseTypes, baseBaseTypes);
			IReadOnlySet<Type> interfaceTypes = IReferenceType<TObject>.TypeInterfaces;
			return new(className, modifier, IClassType.GetMetadata<TThrowable>(), interfaceTypes);
		}
		/// <summary>
		/// Creates a new <see cref="TypeMetadataBuilder{TThrowable}"/> instance with validation.
		/// </summary>
		/// <param name="className">Class name of the current type.</param>
		/// <param name="baseMetadata">Base metadata.</param>
		/// <param name="modifier">Modifier of the current type.</param>
		/// <returns>A new <see cref="TypeMetadataBuilder{TThrowable}"/> instance.</returns>
		private static TypeMetadataBuilder<TThrowable> CreateWithValidation(ReadOnlySpan<Byte> className,
			JClassTypeMetadata? baseMetadata, JTypeModifier modifier)
		{
			IReadOnlySet<Type> interfaceTypes = IReferenceType<TThrowable>.TypeInterfaces;
			return new(className, modifier, baseMetadata, interfaceTypes);
		}
	}
}