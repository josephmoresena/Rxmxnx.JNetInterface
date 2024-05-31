namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Runnable</c> instance.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS110,
                 Justification = CommonConstants.JavaInheritanceJustification)]
public sealed class JRunnableObject : JInterfaceObject<JRunnableObject>, IInterfaceType<JRunnableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata<JRunnableObject> typeMetadata =
		TypeMetadataBuilder<JRunnableObject>.Create(UnicodeClassNames.RunnableInterface()).Build();

	static JInterfaceTypeMetadata<JRunnableObject> IInterfaceType<JRunnableObject>.Metadata
		=> JRunnableObject.typeMetadata;

	/// <inheritdoc/>
	private JRunnableObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JRunnableObject IInterfaceType<JRunnableObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
}