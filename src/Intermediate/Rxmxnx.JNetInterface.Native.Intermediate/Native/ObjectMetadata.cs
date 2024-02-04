namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JLocalObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record ObjectMetadata
{
	/// <inheritdoc cref="IObject.ObjectClassName"/>
	private readonly CString _objectClassName;
	/// <inheritdoc cref="IObject.ObjectSignature"/>
	private readonly CString _objectSignature;

	/// <inheritdoc cref="IObject.ObjectClassName"/>
	public CString ObjectClassName => this._objectClassName;
	/// <inheritdoc cref="IObject.ObjectSignature"/>
	public CString ObjectSignature => this._objectSignature;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal ObjectMetadata(JClassObject jClass)
	{
		this._objectClassName = jClass.Name;
		this._objectSignature = jClass.ClassSignature;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	protected ObjectMetadata(ObjectMetadata metadata)
	{
		this._objectClassName = metadata._objectClassName;
		this._objectSignature = metadata._objectSignature;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="objectClassName">Class name of current instance.</param>
	/// <param name="objectSignature">Class signature of current instance.</param>
	internal ObjectMetadata(CString objectClassName, CString objectSignature)
	{
		this._objectClassName = objectClassName;
		this._objectSignature = objectSignature;
	}

	/// <summary>
	/// Retrieves the java class for current object.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for current object.</returns>
	internal JClassObject GetClass(IEnvironment env) => env.ClassFeature.GetClass(this._objectClassName);
}