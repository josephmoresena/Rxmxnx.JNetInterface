namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.CharSequence</c> instance.
/// </summary>
public sealed class JCharSequenceObject : JInterfaceObject<JCharSequenceObject>, IInterfaceType<JCharSequenceObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JCharSequenceObject>
	                                                              .Create(UnicodeClassNames.CharSequenceInterface())
	                                                              .Build();

	static JDataTypeMetadata IDataType.Metadata => JCharSequenceObject.typeMetadata;

	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	/// <inheritdoc/>
	private JCharSequenceObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	static JCharSequenceObject IReferenceType<JCharSequenceObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCharSequenceObject IReferenceType<JCharSequenceObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JCharSequenceObject IReferenceType<JCharSequenceObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}