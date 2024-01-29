namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject<TElement> : IList<TElement>, IReadOnlyList<TElement>
{
	/// <summary>
	/// Empty <see cref="IList{T}"/> instance.
	/// </summary>
	private static readonly IList<TElement> emptyList = Array.Empty<TElement>();

	Boolean ICollection<TElement>.Remove(TElement item) => JArrayObject<TElement>.emptyList.Remove(item);
	Int32 ICollection<TElement>.Count => this.Length;
	Boolean ICollection<TElement>.IsReadOnly => true;
	Int32 IList<TElement>.IndexOf(TElement item) => this.Environment.ArrayFeature.IndexOf(this, item);
	void IList<TElement>.Insert(Int32 index, TElement item) => JArrayObject<TElement>.emptyList.Insert(index, item);
	void IList<TElement>.RemoveAt(Int32 index) => JArrayObject<TElement>.emptyList.RemoveAt(index);
	void ICollection<TElement>.Add(TElement item) => JArrayObject<TElement>.emptyList.Add(item);
	void ICollection<TElement>.Clear() => JArrayObject<TElement>.emptyList.Clear();
	Boolean ICollection<TElement>.Contains(TElement item) => this.Environment.ArrayFeature.IndexOf(this, item) >= 0;
	void ICollection<TElement>.CopyTo(TElement[] array, Int32 arrayIndex) { throw new NotImplementedException(); }
	Int32 IReadOnlyCollection<TElement>.Count => this.Length;
}