namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JRunnableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Runnable</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JRunnableObject : JInterfaceObject<JRunnableObject>, IInterfaceType<JRunnableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.RunnableHash, 18);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JRunnableObject>(JRunnableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JRunnableObject>.Metadata => JRunnableObject.typeMetadata;

	/// <inheritdoc/>
	private JRunnableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JRunnableObject IInterfaceType<JRunnableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}