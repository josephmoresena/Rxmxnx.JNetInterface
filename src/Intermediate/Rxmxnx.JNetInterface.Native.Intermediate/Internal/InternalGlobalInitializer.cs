namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal <see cref="IReferenceType"/> instance initializer using <see cref="IEnvironment"/> instance and
/// <see cref="JGlobalBase"/> instance.
/// </summary>
internal ref struct InternalGlobalInitializer
{
	/// <summary>
	/// Current <see cref="IEnvironment"/> instance.
	/// </summary>
	public IEnvironment Environment { get; set; }
	/// <summary>
	/// Created object global instance.
	/// </summary>
	public JGlobalBase Global { get; set; }
}