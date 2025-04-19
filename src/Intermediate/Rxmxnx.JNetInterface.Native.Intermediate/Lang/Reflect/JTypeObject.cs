namespace Rxmxnx.JNetInterface.Lang.Reflect;

using TypeMetadata = JInterfaceTypeMetadata<JTypeObject>;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Type</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JTypeObject : JInterfaceObject<JTypeObject>, IInterfaceType<JTypeObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.TypeHash, 22);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JTypeObject>(JTypeObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JTypeObject>.Metadata => JTypeObject.typeMetadata;

	/// <inheritdoc/>
	private JTypeObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JTypeObject IInterfaceType<JTypeObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}