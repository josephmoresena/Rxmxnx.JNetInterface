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
		internal override JArrayTypeMetadata? GetArrayMetadata()
			=> JArrayTypeMetadata.GetArrayArrayMetadata(this.ArraySignature, typeof(TElement));
		/// <inheritdoc/>
		internal override void SetObjectElement(JArrayObject jArray, Int32 index, JLocalObject? value)
		{
			if (jArray is not JArrayObject<TElement> jGenericArray) return;
			switch (value)
			{
				case TElement element:
					jGenericArray[index] = element;
					break;
				case null:
					jGenericArray[index] = default;
					break;
				default:
					JArrayGenericTypeMetadata.SetObjectElement(jGenericArray, index, value);
					break;
			}
		}
		/// <summary>
		/// Sets the object element with <paramref name="index"/> on <paramref name="jArray"/>.
		/// </summary>
		/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
		/// <param name="index">Element index.</param>
		/// <param name="value">Object instance.</param>
		private static void SetObjectElement(JArrayObject<TElement> jArray, Int32 index, JLocalObject value)
		{
			JReferenceTypeMetadata elementMetadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TElement>();
			TElement element = (TElement)(Object)elementMetadata.ParseInstance(value);
			try
			{
				jArray[index] = element;
			}
			finally
			{
				((IDisposable)element).Dispose();
			}
		}
	}
}