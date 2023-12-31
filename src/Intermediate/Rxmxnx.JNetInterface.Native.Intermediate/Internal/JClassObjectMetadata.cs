namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal sealed record JClassObjectMetadata : JObjectMetadata
{
	/// <summary>
	/// Class name of current object.
	/// </summary>
	public CString Name { get; init; }
	/// <summary>
	/// Class signature of current object.
	/// </summary>
	public CString ClassSignature { get; init; }
	/// <summary>
	/// Class hash of current object.
	/// </summary>
	public String Hash { get; init; }
	/// <summary>
	/// Indicates whether the class of current object is final.
	/// </summary>
	public Boolean? IsFinal { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	public JClassObjectMetadata(JObjectMetadata metadata) : base(metadata)
	{
		JClassObjectMetadata? classMetadata = metadata as JClassObjectMetadata;
		this.Name = classMetadata?.Name!;
		this.ClassSignature = classMetadata?.ClassSignature!;
		this.Hash = classMetadata?.Hash!;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public JClassObjectMetadata(JClassObject jClass) : base(UnicodeClassNames.JClassObjectClassName,
	                                                        UnicodeObjectSignatures.JClassObjectSignature)
	{
		this.Name = jClass.Name;
		this.ClassSignature = jClass.ClassSignature;
		this.Hash = jClass.Hash;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	private JClassObjectMetadata(ITypeInformation metadata) : base(UnicodeClassNames.JClassObjectClassName,
	                                                               UnicodeObjectSignatures.JClassObjectSignature)
	{
		this.Name = metadata.ClassName;
		this.ClassSignature = metadata.Signature;
		this.Hash = metadata.Hash;
	}

	/// <summary>
	/// Creates a <see cref="JClassObjectMetadata"/> for given <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IReferenceType"/> type.</typeparam>
	/// <returns>A <see cref="JClassObjectMetadata"/> instance.</returns>
	public static JClassObjectMetadata Create<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		return new(metadata);
	}
}