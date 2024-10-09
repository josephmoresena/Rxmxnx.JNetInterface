namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public class JUnsupportedOperationExceptionObject : JRuntimeExceptionObject,
		IThrowableType<JUnsupportedOperationExceptionObject>
	{
		private static readonly JThrowableTypeMetadata<JUnsupportedOperationExceptionObject> typeMetadata =
			TypeMetadataBuilder<JRuntimeExceptionObject>
				.Create<JUnsupportedOperationExceptionObject>(
					TypeMetadataBuilderTests.classNames[typeof(JUnsupportedOperationExceptionObject)]).Build();
		static JThrowableTypeMetadata<JUnsupportedOperationExceptionObject>
			IThrowableType<JUnsupportedOperationExceptionObject>.Metadata
			=> JUnsupportedOperationExceptionObject.typeMetadata;
		protected JUnsupportedOperationExceptionObject(IReferenceType.ClassInitializer initializer) :
			base(initializer) { }
		protected JUnsupportedOperationExceptionObject(IReferenceType.GlobalInitializer initializer) :
			base(initializer) { }
		protected JUnsupportedOperationExceptionObject(IReferenceType.ObjectInitializer initializer) :
			base(initializer) { }
		static JUnsupportedOperationExceptionObject IClassType<JUnsupportedOperationExceptionObject>.Create(
			IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JUnsupportedOperationExceptionObject IClassType<JUnsupportedOperationExceptionObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JUnsupportedOperationExceptionObject IClassType<JUnsupportedOperationExceptionObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}