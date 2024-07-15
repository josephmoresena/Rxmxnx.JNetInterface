namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JTestObject : JLocalObject, IClassType<JTestObject>
{
	public const String ClassNamePrefix = "rxmxnx/jnetinterface/test/Test";
	private static readonly ConcurrentDictionary<Int32, JClassTypeMetadata> metadatas = new();

	[ThreadStatic]
	private static JClassTypeMetadata<JTestObject>? metadata;

	static JClassTypeMetadata<JTestObject> IClassType<JTestObject>.Metadata
	{
		get
		{
			if (JTestObject.metadata is not null)
				return JTestObject.metadata;
			JTestObject.metadata = TypeMetadataBuilder<JTestObject>.Create(
				(CString)$"{JTestObject.ClassNamePrefix}{Random.Shared.Next()}${Guid.NewGuid():N}",
				Random.Shared.Next(0, 10) > 5 ? JTypeModifier.Final : JTypeModifier.Extensible).Build();
			JTestObject.metadatas[System.Environment.CurrentManagedThreadId] = JTestObject.metadata;
			return JTestObject.metadata;
		}
	}

	private JTestObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private JTestObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private JTestObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static JClassTypeMetadata? GetThreadMetadata()
		=> JTestObject.metadatas.GetValueOrDefault(System.Environment.CurrentManagedThreadId);
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.ObjectInitializer initializer) => new(initializer);
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
}