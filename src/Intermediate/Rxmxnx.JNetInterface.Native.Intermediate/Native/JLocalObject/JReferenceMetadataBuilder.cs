namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// <see cref="JReferenceMetadata"/> class builder.
	/// </summary>
	protected sealed record JMetadataBuilder
	{
		/// <inheritdoc cref="JReferenceMetadata.BaseMetadata"/>
		private readonly JReferenceMetadata? _baseMetadata;
		/// <inheritdoc cref="JDataTypeMetadata.ClassName"/>
		private readonly CString _className;
		/// <inheritdoc cref="JReferenceMetadata.Interfaces"/>
		private readonly HashSet<JDataTypeMetadata> _interfaces = new();
		/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
		private readonly JTypeModifier _modifier;
		/// <inheritdoc cref="JDataTypeMetadata.Type"/>
		private readonly Type _type;
		/// <inheritdoc cref="JDataTypeMetadata.ArraySignature"/>
		private CString? _arraySignature;

		/// <inheritdoc cref="JDataTypeMetadata.Signature"/>
		private CString? _signature;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="type">CLR type current type.</param>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <param name="baseMetadata">Base type of current type metadata.</param>
		private JMetadataBuilder(Type type, CString className, JTypeModifier modifier, JReferenceMetadata? baseMetadata)
		{
			this._type = type;
			this._className = className;
			this._modifier = modifier;
			this._baseMetadata = baseMetadata;
		}

		/// <summary>
		/// Sets the type signature.
		/// </summary>
		/// <param name="signature">Type signature.</param>
		/// <returns>Current instance.</returns>
		public JMetadataBuilder WithSignature(CString signature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(signature, false);
			this._signature = signature;
			return this;
		}
		/// <summary>
		/// Sets the array signature.
		/// </summary>
		/// <param name="arraySignature">Array signature.</param>
		/// <returns>Current instance.</returns>
		public JMetadataBuilder WithArraySignature(CString arraySignature)
		{
			ValidationUtilities.ThrowIfInvalidSignature(arraySignature, false);
			this._arraySignature = arraySignature;
			return this;
		}
		/// <summary>
		/// Appends an interface to current type definition.
		/// </summary>
		/// <typeparam name="TInterface"><see cref="IDataType"/> interface type.</typeparam>
		/// <returns>Current instance.</returns>
		public JMetadataBuilder AppendInterface<TInterface>() where TInterface : JLocalObject, IDataType<TInterface>
		{
			this._interfaces.Add(IDataType.GetMetadata<TInterface>());
			return this;
		}
		/// <summary>
		/// Creates the <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <returns>A new <see cref="JDataTypeMetadata"/> instance.</returns>
		public JReferenceMetadata Build()
			=> new(this._type, this._className, this._modifier, this._interfaces.ToImmutableHashSet(),
			       this._baseMetadata, this._signature, this._arraySignature);

		/// <summary>
		/// Creates a new <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject"><see cref="IDataType"/> type.</typeparam>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JMetadataBuilder"/> instance.</returns>
		public static JMetadataBuilder Create<TObject>(CString className,
			JTypeModifier modifier = JTypeModifier.Extensible) where TObject : JLocalObject, IDataType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			Type type = typeof(TObject);
			JReferenceMetadata? baseMetadata = type != typeof(JLocalObject) ?
				IDataType.GetMetadata<JLocalObject>() as JReferenceMetadata :
				default;
			return new(type, className, modifier, baseMetadata);
		}
		/// <summary>
		/// Creates a new <see cref="JReferenceMetadata"/> instance.
		/// </summary>
		/// <typeparam name="TObject"><see cref="IDataType"/> type.</typeparam>
		/// <typeparam name="TBase"><see cref="IDataType"/> base type.</typeparam>
		/// <param name="className">Class name of current type.</param>
		/// <param name="modifier">Modifier of current type.</param>
		/// <returns>A new <see cref="JMetadataBuilder"/> instance.</returns>
		public static JMetadataBuilder Create<TObject, TBase>(CString className,
			JTypeModifier modifier = JTypeModifier.Extensible) where TObject : TBase, IDataType<TObject>
			where TBase : JLocalObject, IDataType<TObject>
		{
			ValidationUtilities.ValidateNotEmpty(className);
			Type type = typeof(TObject);
			JReferenceMetadata? baseMetadata = IDataType.GetMetadata<TBase>() as JReferenceMetadata;
			return new(type, className, modifier, baseMetadata);
		}
	}
}