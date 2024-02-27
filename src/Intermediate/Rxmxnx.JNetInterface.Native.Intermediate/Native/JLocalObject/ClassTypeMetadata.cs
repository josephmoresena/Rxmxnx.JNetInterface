namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	protected ref partial struct TypeMetadataBuilder<TClass>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IClassType"/> type.
		/// </summary>
		internal sealed record ClassTypeMetadata : JClassTypeMetadata<TClass>
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.BaseMetadata"/>
			private readonly JClassTypeMetadata? _baseMetadata;
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IReadOnlySet<JInterfaceTypeMetadata> _interfaces;
			/// <inheritdoc cref="JDataTypeMetadata.Modifier"/>
			private readonly JTypeModifier _modifier;

			/// <inheritdoc/>
			public override JClassTypeMetadata? BaseMetadata => this._baseMetadata;
			/// <inheritdoc/>
			public override JTypeModifier Modifier => this._modifier;
			/// <inheritdoc/>
			public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="TypeMetadataBuilder"/> instance.</param>
			/// <param name="modifier">Modifier of current type.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			public ClassTypeMetadata(TypeMetadataBuilder builder, JTypeModifier modifier,
				JClassTypeMetadata? baseMetadata) : base(builder.DataTypeName, builder.Signature)
			{
				this._modifier = modifier;
				this._interfaces = InterfaceSet.GetClassInterfaces(baseMetadata, builder.GetInterfaceSet());
				this._baseMetadata = baseMetadata;
			}
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="information">Internal sequence information.</param>
			/// <param name="isVoid">Indicates if current class is <c>void</c> wrapper.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			public ClassTypeMetadata(CStringSequence information, Boolean isVoid, JClassTypeMetadata? baseMetadata) :
				base(information)
			{
				this._modifier = JTypeModifier.Final;
				this._interfaces = isVoid ? InterfaceSet.PrimitiveWrapperSet : InterfaceSet.Empty;
				this._baseMetadata = baseMetadata;
			}

			/// <inheritdoc/>
			public override String ToString()
				=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> TClass.Create(new IReferenceType.ClassInitializer
				{
					Class = jClass, RealClass = realClass, LocalReference = localRef,
				});
			/// <inheritdoc/>
			internal override JReferenceObject? ParseInstance(JLocalObject? jLocal)
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
		}
	}
}