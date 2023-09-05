namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	static JTypeKind IDataType.Kind => JTypeKind.Array;
	static Type IDataType.FamilyType => typeof(JArrayObject);

	/// <summary>
	/// Array length.
	/// </summary>
	private Int32? _length;
}

public partial class JArrayObject<TElement>
{
	static JDataTypeMetadata IDataType.Metadata => JArrayGenericTypeMetadata.Instance;

	/// <inheritdoc/>
	private JArrayObject(JLocalObject jLocal) : base(
		jLocal, jLocal.Environment.ClassProvider.GetClass<JArrayObject<TElement>>()) { }
}