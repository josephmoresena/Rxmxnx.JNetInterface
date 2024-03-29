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

	/// <inheritdoc/>
	private JArrayObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }

	TElement? IEnumerableSequence<TElement?>.GetItem(Int32 index) => this[index];
	Int32 IEnumerableSequence<TElement?>.GetSize() => this.Length;

	/// <summary>
	/// Creates a <see cref="JArrayObject{TElement}"/> instance from <paramref name="initializer"/>.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> instance.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance from <paramref name="initializer"/>.</returns>
	private static JArrayObject<TElement> Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	/// <summary>
	/// Creates a <see cref="JArrayObject{TElement}"/> instance from <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>A <see cref="JArrayObject{TElement}"/> instance from <paramref name="jGlobal"/>.</returns>
	private static JArrayObject<TElement> Create(IEnvironment env, JGlobalBase jGlobal)
	{
		JDataTypeMetadata elementMetadata = IDataType.GetMetadata<TElement>();
		if (!jGlobal.ObjectMetadata.ObjectClassName.AsSpan().SequenceEqual(elementMetadata.ArraySignature))
			JLocalObject.Validate<JArrayObject<TElement>>(jGlobal, env);
		return new(env, jGlobal);
	}

	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.Create(
		IReferenceType.ClassInitializer initializer)
		=> JArrayObject<TElement>.Create(initializer);
	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> JArrayObject<TElement>.Create(initializer.Instance);
	static JArrayObject<TElement> IReferenceType<JArrayObject<TElement>>.
		Create(IReferenceType.GlobalInitializer initializer)
		=> JArrayObject<TElement>.Create(initializer.Environment, initializer.Global);
}