namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JStandardOpenOptionObject : JEnumObject<JStandardOpenOptionObject>,
		IEnumType<JStandardOpenOptionObject>, IInterfaceObject<JOpenOptionObject>
	{
		private static readonly JEnumTypeMetadata<JStandardOpenOptionObject> typeMetadata =
			TypeMetadataBuilder<JStandardOpenOptionObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JStandardOpenOptionObject)])
				.Implements<JOpenOptionObject>()
				.AppendValues(0, [new(() => "READ"u8), new(() => "WRITE"u8), new(() => "APPEND"u8),])
				.AppendValue(9, new(() => "DSYNC"u8))
				.AppendValues(6, [new(() => "DELETE_ON_CLOSE"u8), new(() => "SPARSE"u8), new(() => "SYNC"u8),])
				.AppendValue(3, new(() => "TRUNCATE_EXISTING"u8)).AppendValue(4, new(() => "CREATE"u8))
				.AppendValue(5, new(() => "CREATE_NEW"u8)).Build();
		static JEnumTypeMetadata<JStandardOpenOptionObject> IEnumType<JStandardOpenOptionObject>.Metadata
			=> JStandardOpenOptionObject.typeMetadata;
		private JStandardOpenOptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private JStandardOpenOptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		private JStandardOpenOptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		static JStandardOpenOptionObject IEnumType<JStandardOpenOptionObject>.Create(
			IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JStandardOpenOptionObject IEnumType<JStandardOpenOptionObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JStandardOpenOptionObject IEnumType<JStandardOpenOptionObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}