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
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal ObjectMetadata(JClassObject jClass)
	{
		this.ObjectClassName = jClass.Name;
		this.ObjectSignature = jClass.ClassSignature;
		this.ObjectClassHash = jClass.Hash;
		this.FromProxy = jClass.IsProxy;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeInformation"><see cref="ITypeInformation"/> instance.</param>
	/// <param name="fromProxy">Indicates whether the current instance is a dummy object (fake java object).</param>
	private protected ObjectMetadata(ITypeInformation typeInformation, Boolean? fromProxy)
	{
		this.ObjectClassName = typeInformation.ClassName;
		this.ObjectSignature = typeInformation.Signature;
		this.ObjectClassHash = typeInformation.Hash;
		this.FromProxy = fromProxy;
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
	}

	/// <inheritdoc cref="Object.ToString()"/>
	/// <remarks>Use this method for trace.</remarks>
	public virtual String ToTraceText() => this.ToString();
}