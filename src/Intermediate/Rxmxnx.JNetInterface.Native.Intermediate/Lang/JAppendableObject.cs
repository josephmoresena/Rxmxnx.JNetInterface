namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JAppendableObject>;

/// <summary>
/// This class represents a local <c>java.lang.Appendable</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JAppendableObject : JInterfaceObject<JAppendableObject>, IInterfaceType<JAppendableObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.AppendableHash, 20);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JAppendableObject>(
			JAppendableObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JAppendableObject>.Metadata => JAppendableObject.typeMetadata;

	/// <inheritdoc/>
	private JAppendableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JAppendableObject IInterfaceType<JAppendableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}