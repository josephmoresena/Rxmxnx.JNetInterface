namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	/// <summary>
	/// JNI delegate information.
	/// </summary>
	private sealed record DelegateInfo
	{
		/// <summary>
		/// Index of delegate.
		/// </summary>
		public Int32 Index { get; init; }
		/// <summary>
		/// Name of delegate.
		/// </summary>
		public String Name { get; init; }
		/// <summary>
		/// Level of delegate.
		/// </summary>
		public JniSafetyLevels Level { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public DelegateInfo() => this.Name = default!;
	}
}