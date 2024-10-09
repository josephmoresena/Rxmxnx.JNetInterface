namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public class JCloneNotSupportedExceptionObject : JExceptionObject, IThrowableType<JCloneNotSupportedExceptionObject>
	{
		private static readonly JThrowableTypeMetadata<JCloneNotSupportedExceptionObject> typeMetadata =
			TypeMetadataBuilder<JExceptionObject>
				.Create<JCloneNotSupportedExceptionObject>(
					TypeMetadataBuilderTests.classNames[typeof(JCloneNotSupportedExceptionObject)]).Build();
		static JThrowableTypeMetadata<JCloneNotSupportedExceptionObject>
			IThrowableType<JCloneNotSupportedExceptionObject>.Metadata
			=> JCloneNotSupportedExceptionObject.typeMetadata;
		protected JCloneNotSupportedExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		protected JCloneNotSupportedExceptionObject(IReferenceType.GlobalInitializer initializer) :
			base(initializer) { }
		protected JCloneNotSupportedExceptionObject(IReferenceType.ObjectInitializer initializer) :
			base(initializer) { }
		static JCloneNotSupportedExceptionObject IClassType<JCloneNotSupportedExceptionObject>.Create(
			IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JCloneNotSupportedExceptionObject IClassType<JCloneNotSupportedExceptionObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JCloneNotSupportedExceptionObject IClassType<JCloneNotSupportedExceptionObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}