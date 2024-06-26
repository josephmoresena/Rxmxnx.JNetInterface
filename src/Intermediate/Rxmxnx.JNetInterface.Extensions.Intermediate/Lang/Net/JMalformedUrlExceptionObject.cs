namespace Rxmxnx.JNetInterface.Net;

using TypeMetadata = JThrowableTypeMetadata<JMalformedUrlExceptionObject>;

/// <summary>
/// This class represents a local <c>java.net.MalformedURLException</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public class JMalformedUrlExceptionObject : JIoExceptionObject, IThrowableType<JMalformedUrlExceptionObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JIoExceptionObject>
	                                                    .Create<JMalformedUrlExceptionObject>(
		                                                    "java/net/MalformedURLException"u8).Build();

	static TypeMetadata IThrowableType<JMalformedUrlExceptionObject>.Metadata
		=> JMalformedUrlExceptionObject.typeMetadata;

	/// <inheritdoc/>
	protected JMalformedUrlExceptionObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMalformedUrlExceptionObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JMalformedUrlExceptionObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMalformedUrlExceptionObject IClassType<JMalformedUrlExceptionObject>.Create(
		IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMalformedUrlExceptionObject IClassType<JMalformedUrlExceptionObject>.Create(
		IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMalformedUrlExceptionObject IClassType<JMalformedUrlExceptionObject>.Create(
		IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}