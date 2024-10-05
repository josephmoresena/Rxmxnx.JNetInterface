namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public class JThreadLocalObject : JLocalObject, IClassType<JThreadLocalObject>
	{
		private static readonly JClassTypeMetadata<JThreadLocalObject> typeMetadata =
			TypeMetadataBuilder<JThreadLocalObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JThreadLocalObject)]).Build();
		static JClassTypeMetadata<JThreadLocalObject> IClassType<JThreadLocalObject>.Metadata
			=> JThreadLocalObject.typeMetadata;
		protected JThreadLocalObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		protected JThreadLocalObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		protected JThreadLocalObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JThreadLocalObject IClassType<JThreadLocalObject>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JThreadLocalObject IClassType<JThreadLocalObject>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JThreadLocalObject IClassType<JThreadLocalObject>.Create(IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}