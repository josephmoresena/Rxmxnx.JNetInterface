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
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal ObjectMetadata(JClassObject jClass)
	{
		this.ObjectClassName = jClass.Name;
		this.ObjectSignature = jClass.ClassSignature;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	protected ObjectMetadata(ObjectMetadata metadata)
	{
		this.ObjectClassName = metadata.ObjectClassName;
		this.ObjectSignature = metadata.ObjectSignature;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="objectClassName">Class name of current instance.</param>
	/// <param name="objectSignature">Class signature of current instance.</param>
	internal ObjectMetadata(CString objectClassName, CString objectSignature)
	{
		this.ObjectClassName = objectClassName;
		this.ObjectSignature = objectSignature;
	}

	/// <summary>
	/// Retrieves the java class for current object.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for current object.</returns>
	internal JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass(this.ObjectClassName);
}