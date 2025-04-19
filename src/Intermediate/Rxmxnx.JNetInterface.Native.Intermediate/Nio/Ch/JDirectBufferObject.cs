namespace Rxmxnx.JNetInterface.Nio.Ch;

using TypeMetadata = JInterfaceTypeMetadata<JDirectBufferObject>;

/// <summary>
/// This class represents a local <c>sun.nio.ch.DirectBuffer</c> instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JDirectBufferObject : JInterfaceObject<JDirectBufferObject>, IInterfaceType<JDirectBufferObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.DirectBufferHash, 23);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JDirectBufferObject>(
			JDirectBufferObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JDirectBufferObject>.Metadata => JDirectBufferObject.typeMetadata;

	/// <inheritdoc/>
	private JDirectBufferObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JDirectBufferObject IInterfaceType<JDirectBufferObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}