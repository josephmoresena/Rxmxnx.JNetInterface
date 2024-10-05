namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public class JInheritableThreadLocalObject : JThreadLocalObject, IClassType<JInheritableThreadLocalObject>
	{
		private static readonly JClassTypeMetadata<JInheritableThreadLocalObject> typeMetadata =
			TypeMetadataBuilder<JThreadLocalObject>
				.Create<JInheritableThreadLocalObject>(
					TypeMetadataBuilderTests.classNames[typeof(JInheritableThreadLocalObject)]).Build();
		static JClassTypeMetadata<JInheritableThreadLocalObject> IClassType<JInheritableThreadLocalObject>.Metadata
			=> JInheritableThreadLocalObject.typeMetadata;
		private JInheritableThreadLocalObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private JInheritableThreadLocalObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private JInheritableThreadLocalObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JInheritableThreadLocalObject IClassType<JInheritableThreadLocalObject>.Create(
			IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JInheritableThreadLocalObject IClassType<JInheritableThreadLocalObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JInheritableThreadLocalObject IClassType<JInheritableThreadLocalObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}