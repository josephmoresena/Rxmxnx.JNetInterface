namespace Rxmxnx.JNetInterface.Native;

public partial class JInterfaceObject
{
	protected ref partial struct JTypeMetadataBuilder<TInterface>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IInterfaceType"/> type.
		/// </summary>
		internal sealed record JInterfaceGenericTypeMetadata : JInterfaceTypeMetadata
		{
			/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
			private readonly IImmutableSet<JInterfaceTypeMetadata> _interfaces;

			/// <inheritdoc/>
			public override Type InterfaceType => typeof(IInterfaceObject<TInterface>);
			/// <inheritdoc/>
			public override Type Type => typeof(TInterface);
			/// <inheritdoc/>
			public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TInterface>();
			/// <inheritdoc/>
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.</param>
			internal JInterfaceGenericTypeMetadata(JTypeMetadataBuilder builder) : base(
				builder.DataTypeName, builder.Signature)
				=> this._interfaces = builder.CreateInterfaceSet();

			/// <inheritdoc/>
			internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
				Boolean realClass = false)
				=> TInterface.Create(new IReferenceType.ClassInitializer
				{
					Class = jClass, RealClass = realClass, LocalReference = localRef,
				});
			/// <inheritdoc/>
			internal override TInterface? ParseInstance(JLocalObject? jLocal)
			{
				switch (jLocal)
				{
					case null:
						return default;
					case TInterface result:
						return result;
					default:
						JLocalObject.Validate<TInterface>(jLocal);
						return TInterface.Create(jLocal);
				}
			}
			/// <inheritdoc/>
			internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
			{
				if (jGlobal is null) return default;
				if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(this.ClassName))
					JLocalObject.Validate<TInterface>(jGlobal, env);
				return TInterface.Create(new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
			}
			/// <inheritdoc/>
			internal override JFunctionDefinition<TInterface> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
				JArgumentMetadata[] metadata)
				=> JFunctionDefinition<TInterface>.Create(functionName, metadata);
			/// <inheritdoc/>
			internal override JFieldDefinition<TInterface> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName)
				=> new(fieldName);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TInterface>();
		}
	}
}