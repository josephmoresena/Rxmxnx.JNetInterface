namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal <see cref="IReferenceType"/> instance initializer using <see cref="JLocalObject"/> instance and
/// <see cref="JClassObject"/> instance.
/// </summary>
internal ref struct InternalObjectInitializer
{
	/// <summary>
	/// Created object previous instance.
	/// </summary>
	public JLocalObject Instance { get; set; }
	/// <summary>
	/// Override class instance.
	/// </summary>
	public JClassObject? Class { get; set; }
}