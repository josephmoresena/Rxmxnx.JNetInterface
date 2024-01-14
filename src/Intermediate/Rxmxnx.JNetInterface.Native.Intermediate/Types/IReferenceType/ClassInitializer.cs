namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JClassObject"/> instance and
	/// <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	protected ref struct ClassInitializer
	{
		/// <summary>
		/// Created object class.
		/// </summary>
		public JClassObject Class { get; internal init; }
		/// <summary>
		/// Created object reference.
		/// </summary>
		public JObjectLocalRef LocalReference { get; internal init; }
	}
}