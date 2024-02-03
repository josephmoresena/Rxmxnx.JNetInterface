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
			private readonly IReadOnlySet<JInterfaceTypeMetadata> _interfaces;

			/// <inheritdoc/>
			public override Type Type => typeof(TEnum);
			/// <inheritdoc/>
			public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces => this._interfaces;
			/// <inheritdoc/>
			public override JClassTypeMetadata BaseMetadata => JEnumObject.enumClassMetadata;
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TEnum>();
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
				this._interfaces =
					InterfaceSet.GetClassInterfaces(JEnumObject.enumClassMetadata, builder.GetInterfaceSet());
			}

			/// <inheritdoc/>
			public override String ToString()
				=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
			{
				IEnvironment env = jClass.Environment;
				JClassObject enumClass = env.ClassFeature.GetClass<TEnum>();
				return TEnum.Create(new IReferenceType.ClassInitializer
				{
					Class = enumClass, LocalReference = localRef, RealClass = true,
				});
			}
			/// <inheritdoc/>
			internal override TEnum? ParseInstance(JLocalObject? jLocal)
			{
				switch (jLocal)
				{
					case null:
						return default;
					case TEnum result:
						return result;
					default:
						JLocalObject.Validate<TEnum>(jLocal);
						return TEnum.Create(jLocal);
				}
			}
			/// <inheritdoc/>
			internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(this.ClassName))
					JLocalObject.Validate<TEnum>(jGlobal, env);
				return TEnum.Create(new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
			}
			/// <inheritdoc/>
			internal override JFunctionDefinition<TEnum> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
				JArgumentMetadata[] metadata)
				=> JFunctionDefinition<TEnum>.Create(functionName, metadata);
			/// <inheritdoc/>
			internal override JFieldDefinition<TEnum> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
				=> new(fieldName);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TEnum>();
		}
	}
}