namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JLocalObject"/> instance.
	/// </summary>
	protected ref struct ObjectInitializer
	{
		/// <summary>
		/// Created object previous instance.
		/// </summary>
		public JLocalObject Instance { get; internal init; }
	}
}