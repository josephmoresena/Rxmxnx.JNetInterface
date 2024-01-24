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

public partial class JArrayObject<TElement> : IArrayType<JArrayObject<TElement>, TElement>,
	IEnumerableSequence<TElement?>
{
	static JDataTypeMetadata IDataType.Metadata => JArrayGenericTypeMetadata.Instance;
	static Type IDataType.FamilyType => typeof(JArrayObject);

	TElement? IEnumerableSequence<TElement?>.GetItem(Int32 index) => this[index];
	Int32 IEnumerableSequence<TElement?>.GetSize() => this.Length;

	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer.ToInternal());
	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> JArrayObject<TElement>.Create(initializer.Instance);
	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.
		Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer.Environment, initializer.Global);
}