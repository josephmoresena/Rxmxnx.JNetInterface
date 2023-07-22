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
	/// Current <see cref="JGlobal"/> instance.
	/// </summary>
	private readonly JGlobal? _global;
	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
	/// <summary>
	/// Current <see cref="JWeak"/> instance.
	/// </summary>
	private readonly JWeak? _weak;
	/// <summary>
	/// Instance class object.
	/// </summary>
	private JClassObject? _class;
	/// <summary>
	/// Indicates whether the current class is the real object class.
	/// </summary>
	private Boolean _isRealClass;

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
}