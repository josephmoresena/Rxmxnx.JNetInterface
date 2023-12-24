namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	protected new ref partial struct JTypeMetadataBuilder<TEnum>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IEnumType"/> type.
		/// </summary>
		internal sealed record JEnumGenericTypeMetadata : JEnumTypeMetadata
		{
			/// <inheritdoc cref="JEnumTypeMetadata.Fields"/>
			private readonly IEnumFieldList _fields;
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
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
			/// <param name="builder">A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.</param>
			/// <param name="fields">Enum field list.</param>
			public JEnumGenericTypeMetadata(JTypeMetadataBuilder builder, IEnumFieldList fields) : base(
				builder.DataTypeName, builder.Signature)
			{
				this._fields = fields;
				this._interfaces = builder.CreateInterfaceSet();
			}

			/// <inheritdoc/>
			internal override TEnum? ParseInstance(JLocalObject? jLocal) => jLocal as TEnum ?? TEnum.Create(jLocal);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TEnum>();
		}
	}
}