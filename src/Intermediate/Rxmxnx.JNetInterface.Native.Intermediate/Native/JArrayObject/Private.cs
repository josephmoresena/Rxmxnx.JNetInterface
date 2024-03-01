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

public partial class JArrayObject<TElement> : IArrayType, IReferenceType<JArrayObject<TElement>>,
	IArrayObject<TElement>, IEnumerableSequence<TElement?>
{
	static Type IDataType.FamilyType => typeof(JArrayObject);
	static JDataTypeMetadata IDataType<JArrayObject<TElement>>.Metadata => JArrayObject<TElement>.Metadata;

	TElement? IEnumerableSequence<TElement?>.GetItem(Int32 index) => this[index];
	Int32 IEnumerableSequence<TElement?>.GetSize() => this.Length;

	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.
		Create(IReferenceType.ObjectInitializer initializer)
		=> initializer.Instance.CastTo<JArrayObject<TElement>>();
}