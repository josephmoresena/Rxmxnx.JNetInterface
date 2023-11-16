namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> throwable builder.
	/// </summary>
	/// <typeparam name="TThrowable">Type of <c/>java.lang.Throwable<c/> class.</typeparam>
	protected new sealed partial class JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable> : JTypeMetadataBuilder
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
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
		public new JTypeMetadataBuilder<TThrowable> WithSignature(CString signature)
		{
			base.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public new JTypeMetadataBuilder<TThrowable> WithArraySignature(CString arraySignature)
		{
			base.WithArraySignature(arraySignature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TThrowable> Implements<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JThrowableTypeMetadata Build()
			=> new JThrowableGenericTypeMetadata(this.DataTypeName, this._modifier, this.CreateInterfaceSet(),
			                                     this._baseMetadata, this.BaseTypes, this.Signature,
			                                     this.ArraySignature);

		/// <inheritdoc/>
		protected override Type GetImplementingType<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			=> typeof(IDerivedType<TThrowable, TInterface>);
		/// <inheritdoc/>
		protected override Type GetImplementingType(JInterfaceTypeMetadata interfaceMetadata)
			=> interfaceMetadata.GetImplementingType<TThrowable>();

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TClass}"/> instance.</returns>
		public static JTypeMetadataBuilder<TThrowable> Create(CString className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> baseTypes = IReferenceType<TThrowable>.GetBaseTypes().ToHashSet();
			ISet<Type> interfaceTypes = IReferenceType<TThrowable>.GetInterfaceTypes().ToHashSet();
			JClassTypeMetadata? baseMetadata = typeof(TThrowable) != typeof(JLocalObject) ?
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
				JTypeModifier modifier = JTypeModifier.Extensible) where TObject : TThrowable, IThrowableType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			NativeValidationUtilities.ThrowIfSameType<TThrowable, TObject>(className);
			ISet<Type> baseTypes = NativeValidationUtilities.ValidateBaseTypes<TThrowable, TObject>(className);
			ISet<Type> interfaceTypes = IReferenceType<TObject>.GetInterfaceTypes().ToHashSet();
			return new(className, modifier, IClassType.GetMetadata<TThrowable>(), baseTypes, interfaceTypes);
		}
	}
}