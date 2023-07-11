namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Instance class object.
	/// </summary>
	private readonly IClass? _class;
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
	private JGlobal? _global;
	/// <summary>
	/// Current <see cref="JWeak"/> instance.
	/// </summary>
	private  JWeak? _weak;
	/// <summary>
	/// Indicates whether the this instance is disposed.
	/// </summary>
	private Boolean _isDisposed;
	
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