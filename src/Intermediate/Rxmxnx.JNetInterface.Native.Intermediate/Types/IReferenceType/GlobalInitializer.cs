namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JGlobalBase"/> instance and
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected readonly ref struct GlobalInitializer
	{
		/// <summary>
		/// Current <see cref="IEnvironment"/> instance.
		/// </summary>
		public IEnvironment Environment { get; init; }
		/// <summary>
		/// Created object global instance.
		/// </summary>
		public JGlobalBase Global { get; init; }
	}
}