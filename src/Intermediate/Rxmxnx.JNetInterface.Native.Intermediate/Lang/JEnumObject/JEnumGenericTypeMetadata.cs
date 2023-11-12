namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	protected sealed partial class JTypeMetadataBuilder<TEnum>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IEnumType"/> type.
		/// </summary>
		internal sealed record JEnumGenericTypeMetadata : JEnumTypeMetadata
		{
			/// <inheritdoc cref="JEnumTypeMetadata.Fields"/>
			private readonly IEnumFieldList _fields;
			/// <inheritdoc cref="JDataTypeMetadata.Interfaces"/>
			private readonly IImmutableSet<JInterfaceTypeMetadata> _interfaces;

			/// <inheritdoc/>
			public override Type Type => typeof(TEnum);
			/// <inheritdoc/>
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;
			/// <inheritdoc/>
			public override JClassTypeMetadata BaseMetadata => JEnumObject.JEnumClassMetadata;
			/// <inheritdoc/>
			public override IEnumFieldList Fields => this._fields;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="enumName">Interface name of current type.</param>
			/// <param name="fields">Enum field list.</param>
			/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
			/// <param name="signature">JNI signature for current type.</param>
			/// <param name="arraySignature">Array JNI signature for current type.</param>
			internal JEnumGenericTypeMetadata(CString enumName, IEnumFieldList fields,
				IImmutableSet<JInterfaceTypeMetadata> interfaces, CString? signature, CString? arraySignature) : base(
				enumName, signature, arraySignature)
			{
				this._fields = fields;
				this._interfaces = interfaces;
			}

			/// <inheritdoc/>
			internal override IDataType? ParseInstance(JObject? jObject) => jObject as TEnum ?? TEnum.Create(jObject);
		}
	}
}