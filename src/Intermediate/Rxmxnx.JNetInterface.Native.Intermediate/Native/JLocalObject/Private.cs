namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Internal <see cref="IEnvironment"/> instance.
	/// </summary>
	private readonly IEnvironment _env;
	/// <summary>
	/// Internal <see cref="ObjectLifetime"/> instance.
	/// </summary>
	private readonly ObjectLifetime _lifetime;
	/// <summary>
	/// Instance class object.
	/// </summary>
	private JClassObject? _class;

	/// <summary>
	/// Current <see cref="JGlobal"/> instance.
	/// </summary>
	private JGlobal? _global;
	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
	/// <summary>
	/// Indicates whether the current class is the real object class.
	/// </summary>
	private Boolean _isRealClass;
	/// <summary>
	/// Current <see cref="JWeak"/> instance.
	/// </summary>
	private JWeak? _weak;

	/// <summary>
	/// Loads the class object in the current instance.
	/// </summary>
	private void LoadClassObject()
	{
		if (this._class is not null && this._isRealClass)
			return;
		this._class = this._env.ClassProvider.GetObjectClass(this);
		this._isRealClass = true;
	}
	/// <summary>
	/// Retrieves the loaded global object for current instance.
	/// </summary>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for current instance.</returns>
	private JGlobalBase? GetGlobalObject()
	{
		if (this._global is not null && this._global.IsValid(this._env))
			return this._global;
		if (this._weak is not null && this._weak.IsValid(this._env))
			return this._weak;
		return default;
	}

	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="metadata">The object metadata for <paramref name="jLocal"/>.</param>
	private static void ProcessMetadata(JLocalObject jLocal, JObjectMetadata metadata)
		=> jLocal.ProcessMetadata(metadata);
}