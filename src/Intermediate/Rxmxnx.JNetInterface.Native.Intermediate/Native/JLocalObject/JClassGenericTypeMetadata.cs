namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	protected ref partial struct JTypeMetadataBuilder<TClass>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IClassType"/> type.
		/// </summary>
		internal sealed record JClassGenericTypeMetadata : JClassTypeMetadata
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
			private readonly JClassTypeMetadata? _baseMetadata;
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IReadOnlySet<JInterfaceTypeMetadata> _interfaces;
			/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
			private readonly JTypeModifier _modifier;

			/// <inheritdoc/>
			public override Type Type => typeof(TClass);
			/// <inheritdoc/>
			public override JClassTypeMetadata? BaseMetadata => this._baseMetadata;
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TClass>();
			/// <inheritdoc/>
			public override JTypeModifier Modifier => this._modifier;
			/// <inheritdoc/>
			public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JTypeMetadataBuilder"/> instance.</param>
			/// <param name="modifier">Modifier of current type.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			public JClassGenericTypeMetadata(JTypeMetadataBuilder builder, JTypeModifier modifier,
				JClassTypeMetadata? baseMetadata) : base(builder.DataTypeName, builder.Signature)
			{
				this._modifier = modifier;
				this._interfaces = InterfaceSet.GetClassInterfaces(baseMetadata, builder.GetInterfaceSet());
				this._baseMetadata = baseMetadata;
			}

			/// <inheritdoc/>
			public override String ToString()
				=> $"{{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> TClass.Create(new IReferenceType.ClassInitializer
				{
					Class = jClass, RealClass = realClass, LocalReference = localRef,
				});
			/// <inheritdoc/>
			internal override TClass? ParseInstance(JLocalObject? jLocal)
			{
				switch (jLocal)
				{
					case null:
						return default;
					case TClass result:
						return result;
					default:
						JLocalObject.Validate<TClass>(jLocal);
						return TClass.Create(jLocal);
				}
			}
			/// <inheritdoc/>
			internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(this.ClassName))
					JLocalObject.Validate<TClass>(jGlobal, env);
				return TClass.Create(new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
			}
			/// <inheritdoc/>
			internal override JFunctionDefinition<TClass> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
				JArgumentMetadata[] metadata)
				=> JFunctionDefinition<TClass>.Create(functionName, metadata);
			/// <inheritdoc/>
			internal override JFieldDefinition<TClass> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
				=> new(fieldName);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TClass>();
		}
	}
}