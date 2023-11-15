namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject : IDataType
{
	static JTypeKind IDataType.Kind => JTypeKind.Array;
	static Type IDataType.FamilyType => typeof(JArrayObject);

	/// <summary>
	/// Array length.
	/// </summary>
	private Int32? _length;
}

public partial class JArrayObject<TElement> : IEnumerableSequence<TElement?>
{
	static JDataTypeMetadata IDataType.Metadata => JArrayGenericTypeMetadata.Instance;
	static Type IDataType.FamilyType => typeof(JArrayObject);

	/// <inheritdoc/>
	private JArrayObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassProvider.GetClass<JArrayObject<TElement>>()) { }

	TElement? IEnumerableSequence<TElement?>.GetItem(Int32 index) => this[index];
	Int32 IEnumerableSequence<TElement?>.GetSize() => this.Length;
}