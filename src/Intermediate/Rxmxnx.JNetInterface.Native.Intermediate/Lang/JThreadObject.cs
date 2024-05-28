namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Thread</c> instance.
/// </summary>
public class JThreadObject : JLocalObject, IClassType<JThreadObject>, IInterfaceObject<JRunnableObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JClassTypeMetadata<JThreadObject> typeMetadata = TypeMetadataBuilder<JThreadObject>
	                                                                         .Create(UnicodeClassNames.ThreadObject())
	                                                                         .Implements<JRunnableObject>().Build();

	static JClassTypeMetadata<JThreadObject> IClassType<JThreadObject>.Metadata => JThreadObject.typeMetadata;

	/// <summary>
	/// Indicates whether current thread is virtual.
	/// </summary>
	public Boolean? IsVirtual => this.Environment.IsVirtual(this);

	/// <inheritdoc/>
	private JThreadObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JThreadObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JThreadObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JThreadObject IClassType<JThreadObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JThreadObject IClassType<JThreadObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JThreadObject IClassType<JThreadObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}