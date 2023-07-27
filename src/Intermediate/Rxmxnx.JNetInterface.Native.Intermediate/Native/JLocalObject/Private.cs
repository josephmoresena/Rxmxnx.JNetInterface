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
}