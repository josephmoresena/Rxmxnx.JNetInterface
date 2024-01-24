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
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="builder">A <see cref="JLocalObject.JTypeMetadataBuilder"/> instance.</param>
			internal JInterfaceGenericTypeMetadata(JTypeMetadataBuilder builder) : base(
				builder.DataTypeName, builder.Signature)
				=> this._interfaces = builder.CreateInterfaceSet();

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
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TInterface>();
		}
	}
}