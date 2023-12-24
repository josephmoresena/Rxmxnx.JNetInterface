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
				=> jLocal as TInterface ?? TInterface.Create(jLocal);
			/// <inheritdoc/>
			[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
			internal override Type GetImplementingType<TReference>() => typeof(IDerivedType<TReference, TInterface>);
			/// <inheritdoc/>
			internal override JArrayTypeMetadata GetArrayMetadata()
				=> JReferenceTypeMetadata.GetArrayMetadata<TInterface>();
		}
	}
}