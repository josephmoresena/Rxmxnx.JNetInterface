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
		public static readonly JArrayGenericTypeMetadata Instance = new();

		/// <inheritdoc/>
		public override Type Type => JArrayTypeMetadata.GetArrayType<TElement>() ?? typeof(JArrayObject);
		/// <inheritdoc/>
		public override JDataTypeMetadata ElementMetadata => IDataType.GetMetadata<TElement>();
		/// <inheritdoc/>
		public override JClassTypeMetadata BaseMetadata => JLocalObject.JObjectClassMetadata;

		/// <summary>
		/// Constructor.
		/// </summary>
		private JArrayGenericTypeMetadata() : base(IDataType.GetMetadata<TElement>().ArraySignature,
		                                           JArrayTypeMetadata.GetArrayDeep<TElement>()) { }

		/// <inheritdoc/>
		internal override JArrayObject? ParseInstance(JLocalObject? jLocal)
			=> JArrayTypeMetadata.ParseInstance<TElement>(jLocal);
		/// <inheritdoc/>
		internal override JArrayTypeMetadata? GetArrayMetadata() => JArrayTypeMetadata.GetArrayMetadata<TElement>();
	}
}