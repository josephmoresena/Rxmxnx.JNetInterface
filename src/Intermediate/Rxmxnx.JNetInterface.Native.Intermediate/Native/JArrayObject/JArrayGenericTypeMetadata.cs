namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement>
{
	/// <summary>
	/// This record stores the metadata for a class <see cref="IArrayType"/> type.
	/// </summary>
	private sealed record JArrayGenericTypeMetadata : JArrayTypeMetadata
	{
		/// <summary>
		/// Metadata array instance.
		/// </summary>
		public static readonly JArrayTypeMetadata Instance = new JArrayGenericTypeMetadata();

		/// <inheritdoc/>
		public override Type Type => typeof(JArrayObject<TElement>);
		/// <inheritdoc/>
		public override JDataTypeMetadata ElementMetadata => IDataType.GetMetadata<TElement>();
		/// <inheritdoc/>
		public override JClassTypeMetadata BaseMetadata => JLocalObject.JObjectClassMetadata;

		/// <summary>
		/// Constructor.
		/// </summary>
		private JArrayGenericTypeMetadata() : base(IDataType.GetMetadata<TElement>().ArraySignature) { }

		/// <inheritdoc/>
		internal override IDataType? ParseInstance(JObject? jObject) => jObject as JArrayObject<TElement> ?? JArrayObject<TElement>.Create(jObject);
	}
}