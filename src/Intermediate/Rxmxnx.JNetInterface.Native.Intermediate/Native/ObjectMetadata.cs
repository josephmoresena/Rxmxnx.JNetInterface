namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JLocalObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record ObjectMetadata
{
	/// <inheritdoc cref="IObject.ObjectClassName"/>
	public CString ObjectClassName { get; }
	/// <inheritdoc cref="IObject.ObjectSignature"/>
	public CString ObjectSignature { get; }

	/// <summary>
	/// Indicates whether current instance is proxy.
	/// </summary>
	internal Boolean? FromProxy { get; }
	/// <summary>
	/// Object class hash.
	/// </summary>
	internal String ObjectClassHash { get; }
	/// <summary>
	/// Class type metadata.
	/// </summary>
	internal JReferenceTypeMetadata? TypeMetadata { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="typeMetadata"><see cref="JReferenceTypeMetadata"/> instance.</param>
	internal ObjectMetadata(JClassObject jClass, JReferenceTypeMetadata? typeMetadata = default)
	{
		this.ObjectClassName = jClass.Name;
		this.ObjectSignature = jClass.ClassSignature;
		this.ObjectClassHash = jClass.Hash;
		this.FromProxy = jClass.IsProxy;
		if (CommonNames.ClassObject.SequenceEqual(jClass.Name))
			this.TypeMetadata = IClassType.GetMetadata<JClassObject>();
		else
			this.TypeMetadata = typeMetadata ?? jClass.Environment.ClassFeature.GetTypeMetadata(jClass);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeMetadata"><see cref="JReferenceTypeMetadata"/> instance.</param>
	internal ObjectMetadata(JDataTypeMetadata typeMetadata)
	{
		this.ObjectClassName = typeMetadata.ClassName;
		this.ObjectSignature = typeMetadata.Signature;
		this.ObjectClassHash = typeMetadata.Hash;
		this.TypeMetadata = typeMetadata as JReferenceTypeMetadata;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	protected ObjectMetadata(ObjectMetadata metadata)
	{
		this.ObjectClassName = metadata.ObjectClassName;
		this.ObjectSignature = metadata.ObjectSignature;
		this.ObjectClassHash = metadata.ObjectClassHash;
		this.FromProxy = metadata.FromProxy;
		this.TypeMetadata = metadata.TypeMetadata;
	}

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	public virtual String ToTraceText() => this.ToString();
}