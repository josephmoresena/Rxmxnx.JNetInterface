namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	protected ref partial struct JTypeMetadataBuilder<TThrowable>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IClassType"/> type.
		/// </summary>
		internal sealed record JThrowableGenericTypeMetadata : JThrowableTypeMetadata
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
			private readonly JClassTypeMetadata? _baseMetadata;
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
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.</param>
			/// <param name="modifier">Modifier of current type.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			public JThrowableGenericTypeMetadata(JTypeMetadataBuilder builder, JTypeModifier modifier,
				JClassTypeMetadata? baseMetadata) : base(builder.DataTypeName, builder.Signature)
			{
				this._modifier = modifier;
				this._interfaces = builder.CreateInterfaceSet();
				this._baseMetadata = baseMetadata;
			}

			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> TThrowable.Create(new IReferenceType.ClassInitializer
				{
					Class = jClass, RealClass = realClass, LocalReference = localRef,
				});
			/// <inheritdoc/>
			internal override TThrowable? ParseInstance(JLocalObject? jLocal)
			{
				switch (jLocal)
				{
					case null:
						return default;
					case TThrowable result:
						return result;
					default:
						JLocalObject.Validate<TThrowable>(jLocal);
						return TThrowable.Create(jLocal);
				}
			}
			/// <inheritdoc/>
			internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(this.ClassName))
					JLocalObject.Validate<TThrowable>(jGlobal, env);
				return TThrowable.Create(new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
			}
			/// <inheritdoc/>
			internal override JThrowableException CreateException(JGlobalBase jGlobalThrowable,
				String? exceptionMessage = default)
				=> new JThrowableException<TThrowable>(jGlobalThrowable, exceptionMessage);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TThrowable>();
		}
	}
}