namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	protected abstract class JTypeMetadataBuilder
	{
		/// <summary>
		/// Base types.
		/// </summary>
		private readonly ISet<Type> _baseTypes;
		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private readonly CString _dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.Interfaces"/>
		private readonly HashSet<JInterfaceTypeMetadata> _interfaces = new();
		/// <summary>
		/// Interface types.
		/// </summary>
		private readonly ISet<Type> _interfaceTypes;

		/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
		private CString? _arraySignature;
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private CString? _signature;

		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		protected CString DataTypeName => this._dataTypeName;
		/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
		protected CString? ArraySignature => this._arraySignature;
		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		protected CString? Signature => this._signature;
		/// <inheritdoc cref="JDataTypeMetadata.BaseTypes"/>
		protected ISet<Type> BaseTypes => this._baseTypes;

		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		protected abstract JTypeKind Kind { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		/// <param name="baseTypes">Base types.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		internal JTypeMetadataBuilder(CString dataTypeName, ISet<Type> baseTypes, ISet<Type> interfaceTypes)
		{
			this._dataTypeName = dataTypeName;
			this._baseTypes = baseTypes;
			this._interfaceTypes = interfaceTypes;
		}

		/// <summary>
		/// Creates a metadata interfaces set for current datatype.
		/// </summary>
		/// <returns>A set with current datatype interfaces.</returns>
		protected IImmutableSet<JInterfaceTypeMetadata> CreateInterfaceSet() => this._interfaces.ToImmutableHashSet();

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		protected void WithSignature(CString signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, false);
			this._signature = signature;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		protected void WithArraySignature(CString arraySignature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(arraySignature, false);
			this._arraySignature = arraySignature;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		protected void AppendInterface<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			if (!this._interfaceTypes.Contains(this.GetImplementingType<TInterface>()))
				ValidationUtilities.ThrowInvalidImplementation<TInterface>(
					this.DataTypeName, this.Kind != JTypeKind.Interface);

			JInterfaceTypeMetadata metadata = IInterfaceType.GetMetadata<TInterface>();
			foreach (JInterfaceTypeMetadata interfaceMetadata in metadata.Interfaces)
			{
				if (!this._interfaceTypes.Contains(this.GetImplementingType(interfaceMetadata)))
					ValidationUtilities.ThrowInvalidImplementation<TInterface>(
						this.DataTypeName, this.Kind != JTypeKind.Interface);
			}

			this._interfaces.Add(metadata);
		}

		/// <summary>
		/// Retrieves the CLR type of implementation of <typeparamref name="TInterface"/> in current type.
		/// </summary>
		/// <typeparam name="TInterface">Type of <see cref="IInterfaceType{TInterface}"/>.</typeparam>
		/// <returns>The CLR type of implementation of <typeparamref name="TInterface"/> in current type.</returns>
		protected abstract Type GetImplementingType<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>;

		/// <summary>
		/// Retrieves the CLR type of implementation of the interface in <paramref name="interfaceMetadata"/> in current type.
		/// </summary>
		/// <param name="interfaceMetadata">Interface type metadata.</param>
		/// <returns>The CLR type of implementation of the interface in <paramref name="interfaceMetadata"/> in current type.</returns>
		protected abstract Type GetImplementingType(JInterfaceTypeMetadata interfaceMetadata);

		/// <summary>
		/// Indicates whether <typeparamref name="TClass"/> type implements <typeparamref name="TInterface"/>.
		/// </summary>
		/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
		/// <typeparam name="TInterface">Type of java interface.</typeparam>
		/// <returns></returns>
		protected static Boolean
			HasInterface<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass, TInterface>()
			where TClass : JLocalObject, IReferenceType<TClass>
			where TInterface : JReferenceObject, IInterfaceType<TInterface>
		{
			Type typeofT = typeof(TClass);
			return typeofT.GetInterfaces()
			              .Any(interfaceType => interfaceType == typeof(IDerivedType<TClass, TInterface>));
		}
	}

	/// <summary>
	/// <see cref="JClassTypeMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
	protected sealed partial class JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : JTypeMetadataBuilder
		where TClass : JLocalObject, IClassType<TClass>
	{
		/// <inheritdoc cref="JDataTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;

		/// <inheritdoc cref="JDataTypeMetadata.Kind"/>
		protected override JTypeKind Kind => JTypeKind.Class;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type metadata of current type.</param>
		/// <param name="baseTypes">Base types.</param>
		/// <param name="interfaceTypes">Interface types.</param>
		private JTypeMetadataBuilder(CString className, JTypeModifier modifier, JClassTypeMetadata? baseMetadata,
			ISet<Type> baseTypes, ISet<Type> interfaceTypes) : base(className, baseTypes, interfaceTypes)
		{
			this._modifier = modifier;
			this._baseMetadata = baseMetadata;
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TClass> WithSignature(CString signature)
		{
			base.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TClass> WithArraySignature(CString arraySignature)
		{
			base.WithArraySignature(arraySignature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TClass> AppendInterface<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			base.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JClassTypeMetadata Build()
			=> new JClassGenericTypeMetadata(this.DataTypeName, this._modifier, this.CreateInterfaceSet(),
			                                 this._baseMetadata, this.BaseTypes, this.Signature, this.ArraySignature);

		/// <inheritdoc/>
		protected override Type GetImplementingType<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			=> typeof(IDerivedType<TClass, TInterface>);
		/// <inheritdoc/>
		protected override Type GetImplementingType(JInterfaceTypeMetadata interfaceMetadata)
			=> interfaceMetadata.GetImplementingType<TClass>();

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TClass}"/> instance.</returns>
		public static JTypeMetadataBuilder<TClass> Create(CString className,
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
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(CString className,
				JTypeModifier modifier = JTypeModifier.Extensible) where TObject : TClass, IClassType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ValidationUtilities.ThrowIfSameType<TClass, TObject>(className);
			ISet<Type> baseTypes = ValidationUtilities.ValidateBaseTypes<TClass, TObject>(className);
			ISet<Type> interfaceTypes = IReferenceType<TObject>.GetInterfaceTypes().ToHashSet();
			return new(className, modifier, IClassType.GetMetadata<TClass>(), baseTypes, interfaceTypes);
		}
	}
}