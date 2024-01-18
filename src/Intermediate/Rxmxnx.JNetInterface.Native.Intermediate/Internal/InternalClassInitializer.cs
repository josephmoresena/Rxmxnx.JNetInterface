namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal <see cref="IReferenceType"/> instance initializer using <see cref="JClassObject"/> instance and
/// <see cref="JObjectLocalRef"/> reference.
/// </summary>
internal ref struct InternalClassInitializer
{
	/// <summary>
	/// Created object class.
	/// </summary>
	public JClassObject Class { get; set; }
	/// <summary>
	/// Created object reference.
	/// </summary>
	public JObjectLocalRef LocalReference { get; set; }
	/// <summary>
	/// Indicates whether <see cref="Class"/> is real object class.
	/// </summary>
	public Boolean RealClass { get; set; }
}