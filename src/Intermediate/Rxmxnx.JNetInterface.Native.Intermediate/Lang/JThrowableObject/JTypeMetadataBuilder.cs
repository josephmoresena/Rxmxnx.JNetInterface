namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// <see cref="JClassTypeMetadata"/> throwable builder.
	/// </summary>
	/// <typeparam name="TThrowable">Type of <c/>java.lang.Throwable<c/> class.</typeparam>
	protected new ref partial struct JTypeMetadataBuilder<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable>
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
		private readonly JClassTypeMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;
		/// <summary>
		/// A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.
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
			this._builder = new(className, JTypeKind.Class, JTypeMetadataBuilder.GetImplementingType<TThrowable>,
			                    interfaceTypes);
			this._baseMetadata = baseMetadata;
			this._modifier = modifier;
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TThrowable> WithSignature(CString signature)
		{
			this._builder.WithSignature(signature);
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JTypeMetadataBuilder<TThrowable> Implements<TInterface>()
			where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
		{
			this._builder.AppendInterface<TInterface>(
				JTypeMetadataBuilder.GetImplementingType<TThrowable, TInterface>());
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JThrowableTypeMetadata Build()
			=> new JThrowableGenericTypeMetadata(this._builder, this._modifier, this._baseMetadata);

		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TThrowable}"/> instance.</returns>
		public static JTypeMetadataBuilder<TThrowable> Create(ReadOnlySpan<Byte> className,
			JTypeModifier modifier = JTypeModifier.Extensible)
		{
			ValidationUtilities.ValidateNotEmpty(className);
			ISet<Type> interfaceTypes = IReferenceType<TThrowable>.GetInterfaceTypes().ToHashSet();
			JClassTypeMetadata? baseMetadata = typeof(TThrowable) != typeof(JThrowableObject) ?
				IClassType.GetMetadata<JThrowableObject>() :
				IClassType.GetMetadata<JLocalObject>();
			return new(className, modifier, baseMetadata, interfaceTypes);
		}
		/// <summary>
		/// Creates a new <see cref="JReferenceTypeMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject">Extension type <see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JTypeMetadataBuilder{TThrowable}"/> instance.</returns>
		public static JTypeMetadataBuilder<TObject>
			Create<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TObject>(
				ReadOnlySpan<Byte> className, JTypeModifier modifier = JTypeModifier.Extensible)
			where TObject : TThrowable, IThrowableType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			NativeValidationUtilities.ThrowIfSameType<TThrowable, TObject>(className);
			NativeValidationUtilities.ValidateBaseTypes<TThrowable, TObject>(className);
			ISet<Type> interfaceTypes = IReferenceType<TObject>.GetInterfaceTypes().ToHashSet();
			return new(className, modifier, IClassType.GetMetadata<TThrowable>(), interfaceTypes);
		}
	}
}