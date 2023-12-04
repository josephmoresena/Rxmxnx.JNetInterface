namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	protected sealed partial class JTypeMetadataBuilder<TThrowable>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IClassType"/> type.
		/// </summary>
		internal sealed record JThrowableGenericTypeMetadata : JThrowableTypeMetadata
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
			private readonly JClassTypeMetadata? _baseMetadata;
			/// <inheritdoc cref="JDataTypeMetadata.BaseTypes"/>
			private readonly ISet<Type> _baseTypes;
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IImmutableSet<JInterfaceTypeMetadata> _interfaces;
			/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
			private readonly JTypeModifier _modifier;

			/// <inheritdoc/>
			public override Type Type => typeof(TThrowable);
			/// <inheritdoc/>
			public override JClassTypeMetadata? BaseMetadata => this._baseMetadata;
			/// <inheritdoc/>
			public override JTypeModifier Modifier => this._modifier;
			/// <inheritdoc/>
			public override IReadOnlySet<Type> BaseTypes => (IReadOnlySet<Type>)this._baseTypes;
			/// <inheritdoc/>
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="className">Class name of current type.</param>
			/// <param name="modifier">Modifier of current type.</param>
			/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
			/// <param name="baseTypes">Base types set.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			/// <param name="signature">JNI signature for current type.</param>
			/// <param name="arraySignature">Array JNI signature for current type.</param>
			internal JThrowableGenericTypeMetadata(CString className, JTypeModifier modifier,
				IImmutableSet<JInterfaceTypeMetadata> interfaces, JClassTypeMetadata? baseMetadata,
				ISet<Type> baseTypes, CString? signature, CString? arraySignature) : base(
				className, signature, arraySignature)
			{
				this._modifier = modifier;
				this._interfaces = interfaces;
				this._baseMetadata = baseMetadata;
				this._baseTypes = baseTypes;
			}

			/// <inheritdoc/>
			internal override TThrowable? ParseInstance(JLocalObject? jLocal)
				=> jLocal as TThrowable ?? TThrowable.Create(jLocal);
			/// <inheritdoc/>
			internal override JThrowableException CreateException(JGlobalBase jGlobalThrowable)
				=> throw new NotImplementedException();
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TThrowable>();
		}
	}
}