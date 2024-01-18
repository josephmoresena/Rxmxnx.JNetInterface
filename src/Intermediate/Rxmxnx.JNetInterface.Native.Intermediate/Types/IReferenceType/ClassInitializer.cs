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
		/// Internal instance.
		/// </summary>
		private readonly InternalClassInitializer _instance;

		/// <summary>
		/// Created object class.
		/// </summary>
		public JClassObject Class
		{
			get => this._instance.Class;
			init => this._instance.Class = value;
		}
		/// <summary>
		/// Created object reference.
		/// </summary>
		public JObjectLocalRef LocalReference
		{
			get => this._instance.LocalReference;
			init => this._instance.LocalReference = value;
		}

		/// <summary>
		/// Indicates whether <see cref="Class"/> is real object class.
		/// </summary>
		internal Boolean RealClass
		{
			get => this._instance.RealClass;
			init => this._instance.RealClass = value;
		}

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="initializer">A <see cref="InternalClassInitializer"/> instance.</param>
		private ClassInitializer(InternalClassInitializer initializer) => this._instance = initializer;

		/// <summary>
		/// Retrieves a <see cref="InternalClassInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <returns>A <see cref="InternalClassInitializer"/> value.</returns>
		internal InternalClassInitializer ToInternal() => this._instance;

		/// <summary>
		/// Retrieves a <see cref="ClassInitializer"/> instance from <paramref name="initializer"/>
		/// </summary>
		/// <param name="initializer">A <see cref="InternalClassInitializer"/> instance.</param>
		/// <returns>A <see cref="ClassInitializer"/> value.</returns>
		internal static ClassInitializer FromInternal(InternalClassInitializer initializer) => new(initializer);
	}
}