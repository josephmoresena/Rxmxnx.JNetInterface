namespace Rxmxnx.JNetInterface.Native;

public partial class JInterfaceObject
{
	protected sealed partial class JTypeMetadataBuilder<TInterface>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IInterfaceType"/> type.
		/// </summary>
		internal sealed record JInterfaceGenericTypeMetadata : JInterfaceTypeMetadata
		{
			/// <inheritdoc cref="JDataTypeMetadata.Interfaces"/>
			private readonly IImmutableSet<JInterfaceTypeMetadata> _interfaces;

			/// <inheritdoc/>
			public override Type Type => typeof(TInterface);
			/// <inheritdoc/>
			public override IImmutableSet<JInterfaceTypeMetadata> Interfaces => this._interfaces;

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="interfaceName">Interface name of current type.</param>
			/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
			/// <param name="signature">JNI signature for current type.</param>
			/// <param name="arraySignature">Array JNI signature for current type.</param>
			internal JInterfaceGenericTypeMetadata(CString interfaceName,
				IImmutableSet<JInterfaceTypeMetadata> interfaces, CString? signature, CString? arraySignature) : base(
				interfaceName, signature, arraySignature)
				=> this._interfaces = interfaces;

			/// <inheritdoc/>
			internal override IDataType? CreateInstance(JObject? jObject) => TInterface.Create(jObject);
			/// <inheritdoc/>
			[UnconditionalSuppressMessage("Trim analysis", "IL2091")]
			internal override Type GetImplementingType<TReference>() => typeof(IDerivedType<TReference, TInterface>);
		}
	}
}