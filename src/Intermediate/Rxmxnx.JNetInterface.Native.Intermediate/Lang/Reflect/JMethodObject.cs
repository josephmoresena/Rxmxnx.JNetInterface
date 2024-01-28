namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Method</c> instance.
/// </summary>
public sealed class JMethodObject : JExecutableObject, IClassType<JMethodObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JExecutableObject>
	                                                      .Create<JMethodObject>(
		                                                      UnicodeClassNames.MethodObject(), JTypeModifier.Final)
	                                                      .Implements<JAnnotatedElementObject>()
	                                                      .Implements<JGenericDeclarationObject>()
	                                                      .Implements<JMemberObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JMethodObject.metadata;

	/// <inheritdoc/>
	private JMethodObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMethodObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JMethodObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JMethodObject IReferenceType<JMethodObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JMethodObject IReferenceType<JMethodObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JMethodObject IReferenceType<JMethodObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}