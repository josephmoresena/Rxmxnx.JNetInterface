namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class JTestObject : JLocalObject, IClassType<JTestObject>
{
	public const String ClassNamePrefix = "rxmxnx/jnetinterface/test/Test";

	[ThreadStatic]
	private static JClassTypeMetadata<JTestObject>? metadata;

	static JClassTypeMetadata<JTestObject> IClassType<JTestObject>.Metadata
		=> JTestObject.metadata ??= TypeMetadataBuilder<JTestObject>
		                            .Create(
			                            (CString)
			                            $"{JTestObject.ClassNamePrefix}{Random.Shared.Next()}${Guid.NewGuid():N}")
		                            .Build();

	private JTestObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private JTestObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private JTestObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.ClassInitializer initializer) => new(initializer);
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.ObjectInitializer initializer) => new(initializer);
	static JTestObject IClassType<JTestObject>.Create(IReferenceType.GlobalInitializer initializer) => new(initializer);
}