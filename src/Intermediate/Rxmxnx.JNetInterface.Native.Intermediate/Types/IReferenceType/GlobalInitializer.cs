namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JGlobalBase"/> instance and
	/// <see cref="IEnvironment"/> instance.
	/// </summary>
	protected ref struct GlobalInitializer
	{
		/// <summary>
		/// Internal instance.
		/// </summary>
		private readonly InternalGlobalInitializer _instance;

		/// <summary>
		/// Current <see cref="IEnvironment"/> instance.
		/// </summary>
		public IEnvironment Environment
		{
			get => this._instance.Environment;
			internal init => this._instance.Environment = value;
		}
		/// <summary>
		/// Created object global instance.
		/// </summary>
		public JGlobalBase Global
		{
			get => this._instance.Global;
			internal init => this._instance.Global = value;
		}

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="initializer">A <see cref="InternalClassInitializer"/> instance.</param>
		private GlobalInitializer(InternalGlobalInitializer initializer) => this._instance = initializer;

		/// <summary>
		/// Retrieves a <see cref="InternalGlobalInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <returns>A <see cref="InternalGlobalInitializer"/> value.</returns>
		internal InternalGlobalInitializer ToInternal() => this._instance;
		/// <summary>
		/// Retrieves a <see cref="GlobalInitializer"/> instance from <paramref name="initializer"/>
		/// </summary>
		/// <param name="initializer">A <see cref="InternalGlobalInitializer"/> instance.</param>
		/// <returns>A <see cref="GlobalInitializer"/> value.</returns>
		internal static GlobalInitializer FromInternal(InternalGlobalInitializer initializer) => new(initializer);
	}
}