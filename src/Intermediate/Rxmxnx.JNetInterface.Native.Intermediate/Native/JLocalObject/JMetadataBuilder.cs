namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JClassMetadata"/> class builder.
	/// </summary>
	protected abstract class JMetadataBuilder
	{
		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private readonly CString _dataTypeName;
		/// <inheritdoc cref="JReferenceMetadata.Interfaces"/>
		private readonly HashSet<JInterfaceMetadata> _interfaces = new();

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

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dataTypeName">Datatype name.</param>
		internal JMetadataBuilder(CString dataTypeName) => this._dataTypeName = dataTypeName;

		/// <summary>
		/// Creates a metadata interfaces set for current datatype.
		/// </summary>
		/// <returns>A set with current datatype interfaces.</returns>
		protected IImmutableSet<JInterfaceMetadata> CreateInterfaceSet() => this._interfaces.ToImmutableHashSet();

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
		protected void AppendInterface<TInterface>() where TInterface : JInterfaceObject, IInterfaceType<TInterface>
		{
			this._interfaces.Add(IInterfaceType.GetMetadata<TInterface>());
		}

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
	/// <see cref="JClassMetadata"/> class builder.
	/// </summary>
	/// <typeparam name="TClass">Type of <c/>java.lang.Object<c/> class.</typeparam>
	protected sealed partial class JMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : JMetadataBuilder
		where TClass : JLocalObject, IClassType<TClass>
	{
		/// <inheritdoc cref="JReferenceMetadata.BaseMetadata"/>
		private readonly JClassMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type of current type metadata.</param>
		private JMetadataBuilder(CString className, JTypeModifier modifier, JClassMetadata? baseMetadata) :
			base(className)
		{
			this._modifier = modifier;
			this._baseMetadata = baseMetadata;
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public new JMetadataBuilder<TClass> WithSignature(CString signature)
		{
			base.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public new JMetadataBuilder<TClass> WithArraySignature(CString arraySignature)
		{
			base.WithArraySignature(arraySignature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public new JMetadataBuilder<TClass> AppendInterface<
			[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TInterface>()
			where TInterface : JInterfaceObject, IInterfaceType<TInterface>
		{
			if (!IReferenceType<TClass>.IsDerivedFrom<TInterface>() &&
			    JMetadataBuilder.HasInterface<TClass, TInterface>())
				ValidationUtilities.ThrowInvalidImplementation<TInterface>(this.DataTypeName);
			base.AppendInterface<TInterface>();
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JClassMetadata Build()
			=> new JClassGenericMetadata(this.DataTypeName, this._modifier, this.CreateInterfaceSet(),
			                             this._baseMetadata, this.Signature, this.ArraySignature);

		/// <summary>
		/// Creates a new <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JMetadataBuilder{TClass}"/> instance.</returns>
		public static JMetadataBuilder<TClass> Create(CString className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			JClassMetadata? baseMetadata = typeof(TClass) != typeof(JLocalObject) ?
				IClassType.GetMetadata<JLocalObject>() :
				default;
			return new(className, modifier, baseMetadata);
		}
		/// <summary>
		/// Creates a new <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JMetadataBuilder{TObject}"/> instance.</returns>
		public static JMetadataBuilder<TObject>
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(CString className,
				JTypeModifier modifier = JTypeModifier.Extensible) where TObject : TClass, IClassType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			JClassMetadata baseMetadata = IClassType.GetMetadata<TClass>();
			return new(className, modifier, baseMetadata);
		}
	}
}