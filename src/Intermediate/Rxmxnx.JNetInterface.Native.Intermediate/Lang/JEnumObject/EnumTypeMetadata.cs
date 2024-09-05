namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	protected new ref partial struct TypeMetadataBuilder<TEnum>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IEnumType"/> type.
		/// </summary>
		internal sealed class EnumTypeMetadata : JEnumTypeMetadata<TEnum>
		{
			/// <inheritdoc cref="JEnumTypeMetadata.Fields"/>
			private readonly IEnumFieldList _fields;
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IInterfaceSet _interfaces;

			/// <inheritdoc/>
			public override Type Type => typeof(TEnum);
			/// <inheritdoc/>
			public override IInterfaceSet Interfaces => this._interfaces;
			/// <inheritdoc/>
			public override JClassTypeMetadata BaseMetadata => JEnumObject.typeMetadata;
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TEnum>();
			/// <inheritdoc/>
			public override IEnumFieldList Fields => this._fields;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.</param>
			/// <param name="fields">Enum field list.</param>
			public EnumTypeMetadata(TypeMetadataBuilder builder, IEnumFieldList fields) : base(
				builder.DataTypeName, builder.Signature)
			{
				this._fields = fields;
				this._interfaces = InterfaceSet.GetClassInterfaces(JEnumObject.typeMetadata, builder.GetInterfaceSet());
			}

			/// <inheritdoc/>
			public override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TEnum>();

			/// <inheritdoc/>
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal override JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass<TEnum>();
			/// <inheritdoc/>
			internal override JEnumObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> IEnumType<TEnum>.Create(jClass.Environment, localRef);
			/// <inheritdoc/>
			internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
				=> jLocal?.CastTo<TEnum>(dispose);
			/// <inheritdoc/>
			internal override JEnumObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				JLocalObject.Validate<TEnum>(jGlobal);
				return IEnumType<TEnum>.Create(env, jGlobal);
			}
			/// <inheritdoc/>
			internal override JFunctionDefinition<TEnum> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
				ReadOnlySpan<JArgumentMetadata> paramsMetadata)
				=> JFunctionDefinition<TEnum>.Create(functionName, paramsMetadata);
			/// <inheritdoc/>
			internal override JFieldDefinition<TEnum> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
				=> new(fieldName);
		}
	}
}