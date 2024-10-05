namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JThreadStateObject : JEnumObject<JThreadStateObject>, IEnumType<JThreadStateObject>
	{
		private static readonly JEnumTypeMetadata<JThreadStateObject> typeMetadata =
			TypeMetadataBuilder<JThreadStateObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JThreadStateObject)])
				.AppendValue(0, new(() => "NEW"u8)).AppendValue(1, new(() => "RUNNABLE"u8))
				.AppendValue(2, new(() => "BLOCKED"u8)).AppendValues(3, [
					new(() => "WAITING"u8), new(() => "TIMED_WAITING"u8),
					new(() => "TERMINATED"u8),
				]).Build();
		static JEnumTypeMetadata<JThreadStateObject> IEnumType<JThreadStateObject>.Metadata
			=> JThreadStateObject.typeMetadata;
		private JThreadStateObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private JThreadStateObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		private JThreadStateObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		static JThreadStateObject IEnumType<JThreadStateObject>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JThreadStateObject IEnumType<JThreadStateObject>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JThreadStateObject IEnumType<JThreadStateObject>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}