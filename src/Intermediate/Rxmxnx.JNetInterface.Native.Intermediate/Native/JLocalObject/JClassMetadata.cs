namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	protected sealed partial class JMetadataBuilder<TClass>
	{
		/// <summary>
		/// This record stores the metadata for a class <see cref="IDataType"/> type.
		/// </summary>
		public sealed record JClassGenericMetadata : JClassMetadata
		{
			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="className">Class name of current type.</param>
			/// <param name="modifier">Modifier of current type.</param>
			/// <param name="interfaces">Set of interfaces metadata of current type implements.</param>
			/// <param name="baseMetadata">Base type of current type metadata.</param>
			/// <param name="signature">JNI signature for current type.</param>
			/// <param name="arraySignature">Array JNI signature for current type.</param>
			internal JClassGenericMetadata(CString className, JTypeModifier modifier,
				IImmutableSet<JInterfaceMetadata> interfaces, JClassMetadata? baseMetadata,
				CString? signature = default, CString? arraySignature = default) : base(
				typeof(TClass), className, modifier, interfaces, baseMetadata, signature, arraySignature) { }

			/// <inheritdoc/>
			internal override IDataType? Create(JObject? jObject) => TClass.Create(jObject);
		}
	}
}