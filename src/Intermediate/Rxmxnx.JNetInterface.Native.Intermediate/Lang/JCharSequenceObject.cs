namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JInterfaceTypeMetadata<JCharSequenceObject>;

/// <summary>
/// This class represents a local <c>java.lang.CharSequence</c> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
#endif
public sealed class JCharSequenceObject : JInterfaceObject<JCharSequenceObject>, IInterfaceType<JCharSequenceObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.CharSequenceHash, 22);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.InterfaceView.CreateBuiltInMetadata<JCharSequenceObject>(
			JCharSequenceObject.typeInfo, InterfaceSet.Empty);

	static TypeMetadata IInterfaceType<JCharSequenceObject>.Metadata => JCharSequenceObject.typeMetadata;

	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharSequenceObject IInterfaceType<JCharSequenceObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}