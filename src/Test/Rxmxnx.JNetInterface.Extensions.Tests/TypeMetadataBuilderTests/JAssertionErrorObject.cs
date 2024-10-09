namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public class JAssertionErrorObject : JErrorObject, IThrowableType<JAssertionErrorObject>
	{
		private static readonly JThrowableTypeMetadata<JAssertionErrorObject> typeMetadata =
			TypeMetadataBuilder<JErrorObject>
				.Create<JAssertionErrorObject>(TypeMetadataBuilderTests.classNames[typeof(JAssertionErrorObject)])
				.Build();
		static JThrowableTypeMetadata<JAssertionErrorObject> IThrowableType<JAssertionErrorObject>.Metadata
			=> JAssertionErrorObject.typeMetadata;
		protected JAssertionErrorObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		protected JAssertionErrorObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		protected JAssertionErrorObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JAssertionErrorObject IClassType<JAssertionErrorObject>.Create(
			IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JAssertionErrorObject IClassType<JAssertionErrorObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JAssertionErrorObject IClassType<JAssertionErrorObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}