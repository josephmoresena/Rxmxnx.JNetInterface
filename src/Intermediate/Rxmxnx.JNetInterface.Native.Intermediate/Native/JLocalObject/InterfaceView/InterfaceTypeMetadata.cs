namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	public abstract partial class InterfaceView
	{
		protected ref partial struct TypeMetadataBuilder<TInterface>
		{
			/// <summary>
			/// This record stores the metadata for a class <see cref="IInterfaceType"/> type.
			/// </summary>
			internal sealed record InterfaceTypeMetadata : JInterfaceTypeMetadata<TInterface>
			{
				/// <inheritdoc cref="JReferenceTypeMetadata.Interfaces"/>
				private readonly IReadOnlySet<JInterfaceTypeMetadata> _interfaces;
				/// <inheritdoc cref="InterfaceTypeMetadata.ProxyMetadata"/>
				private readonly JClassTypeMetadata _proxyMetadata = Proxy<TInterface>.ProxyMetadata;

				/// <inheritdoc/>
				public override Type InterfaceType => typeof(IInterfaceObject<TInterface>);
				/// <inheritdoc/>
				public override Type Type => typeof(TInterface);
				/// <inheritdoc/>
				public override JClassTypeMetadata ProxyMetadata => this._proxyMetadata;
				/// <inheritdoc/>
				public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TInterface>();
				/// <inheritdoc/>
				public override IReadOnlySet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

				/// <summary>
				/// Constructor.
				/// </summary>
				/// <param name="builder">A <see cref="JLocalObject.TypeMetadataBuilder"/> instance.</param>
				internal InterfaceTypeMetadata(TypeMetadataBuilder builder) :
					base(builder.DataTypeName, builder.Signature, builder.IsAnnotation)
					=> this._interfaces = builder.IsAnnotation ?
						InterfaceSet.AnnotationSet :
						InterfaceSet.GetInterfaceInterfaces(builder.GetInterfaceSet());

				/// <inheritdoc/>
				public override String ToString()
					=> $"{nameof(JDataTypeMetadata)} {{ {base.ToString()}{nameof(JDataTypeMetadata.Hash)} = {this.Hash} }}";

				/// <inheritdoc/>
				internal override JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
					Boolean realClass = false)
					=> new Proxy<TInterface>(new IReferenceType.ClassInitializer
					{
						Class = jClass, RealClass = realClass, LocalReference = localRef,
					});
				/// <inheritdoc/>
				internal override JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false)
				{
					if (jLocal == null) return default;
					if (jLocal is not IInterfaceObject<TInterface>)
						JLocalObject.Validate<TInterface>(jLocal);
					return IInterfaceType<TInterface>.Create(jLocal);
				}
				/// <inheritdoc/>
				internal override JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal)
				{
					if (jGlobal is null) return default;
					JLocalObject.Validate<TInterface>(jGlobal);
					return new Proxy<TInterface>(
						new IReferenceType.GlobalInitializer { Global = jGlobal, Environment = env, });
				}
				/// <inheritdoc/>
				internal override JFunctionDefinition<TInterface> CreateFunctionDefinition(
					ReadOnlySpan<Byte> functionName, JArgumentMetadata[] metadata)
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
}