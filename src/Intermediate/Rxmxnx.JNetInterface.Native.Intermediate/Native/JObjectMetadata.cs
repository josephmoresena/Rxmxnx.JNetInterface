namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JLocalObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public record JObjectMetadata
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
	/// <param name="jClass"><see cref="IClass"/> instance.</param>
	internal JObjectMetadata(IClass jClass)
	{
		this._objectClassName = jClass.Name;
		this._objectSignature = jClass.ClassSignature;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	protected JObjectMetadata(JObjectMetadata metadata)
	{
		this._objectClassName = metadata._objectClassName;
		this._objectSignature = metadata._objectSignature;
	}

	/// <summary>
	/// Retrieves the java class for current object.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>The class instance for current object.</returns>
	internal JClassObject GetClass(IEnvironment env) => env.ClassProvider.GetClass(this._objectClassName);
}