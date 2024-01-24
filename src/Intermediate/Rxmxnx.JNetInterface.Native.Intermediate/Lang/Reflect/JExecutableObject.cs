namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Executable</c> instance.
/// </summary>
public partial class JExecutableObject : JAccessibleObject, IClassType<JExecutableObject>,
	IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JMemberObject>
{
	/// <summary>
	/// class metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JAccessibleObject>
	                                                      .Create<JExecutableObject>(
		                                                      UnicodeClassNames.ExecutableObject(),
		                                                      JTypeModifier.Abstract)
	                                                      .Implements<JAnnotatedElementObject>()
	                                                      .Implements<JGenericDeclarationObject>()
	                                                      .Implements<JMemberObject>().Build();

	static JDataTypeMetadata IDataType.Metadata => JExecutableObject.metadata;

	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	protected JExecutableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JExecutableObject IReferenceType<JExecutableObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JExecutableObject IReferenceType<JExecutableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JExecutableObject IReferenceType<JExecutableObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}