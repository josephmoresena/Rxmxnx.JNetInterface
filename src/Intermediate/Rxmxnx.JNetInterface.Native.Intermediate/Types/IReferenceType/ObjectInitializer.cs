namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JLocalObject"/> instance.
	/// </summary>
	protected ref struct ObjectInitializer
	{
		/// <summary>
		/// Internal instance.
		/// </summary>
		private readonly InternalObjectInitializer _instance;

		/// <summary>
		/// Created object previous instance.
		/// </summary>
		public JLocalObject Instance
		{
			get => this._instance.Instance;
			private init => this._instance.Instance = value;
		}
		/// <summary>
		/// Override class instance.
		/// </summary>
		public JClassObject? Class
		{
			get => this._instance.Class;
			init => this._instance.Class = value;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public ObjectInitializer(JLocalObject jLocal) => this.Instance = jLocal;

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="initializer">A <see cref="InternalClassInitializer"/> instance.</param>
		private ObjectInitializer(InternalObjectInitializer initializer) => this._instance = initializer;

		/// <summary>
		/// Defines an explicit conversion of a given <see cref="JLocalObject"/> to
		/// <see cref="ObjectInitializer"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="StackTraceElementObjectMetadata"/> to implicitly convert.</param>
		public static implicit operator ObjectInitializer(JLocalObject jLocal) => new() { Instance = jLocal, };

		/// <summary>
		/// Retrieves an instance with <typeparamref name="TClass"/> class instance.
		/// </summary>
		/// <typeparam name="TClass">Datatype class</typeparam>
		/// <returns>A <see cref="ObjectInitializer"/> value.</returns>
		internal ObjectInitializer WithClass<TClass>() where TClass : IDataType<TClass>
		{
			IEnvironment env = this._instance.Instance.Environment;
			JClassObject jClass = env.ClassFeature.GetClass<TClass>();
			return new(this.Instance) { Class = jClass, };
		}
		/// <summary>
		/// Retrieves a <see cref="InternalObjectInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <returns>A <see cref="InternalObjectInitializer"/> value.</returns>
		internal InternalObjectInitializer ToInternal() => this._instance;
		/// <summary>
		/// Retrieves a <see cref="InternalObjectInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <param name="jClass">Override class.</param>
		/// <returns>A <see cref="InternalObjectInitializer"/> value.</returns>
		internal InternalObjectInitializer ToInternal(JClassObject jClass)
			=> new() { Instance = this._instance.Instance, Class = jClass, };
		/// <summary>
		/// Retrieves a <see cref="InternalObjectInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <typeparam name="TClass">Datatype class</typeparam>
		/// <returns>A <see cref="InternalObjectInitializer"/> value.</returns>
		internal InternalObjectInitializer ToInternal<TClass>() where TClass : IDataType<TClass>
		{
			IEnvironment env = this._instance.Instance.Environment;
			JClassObject jClass = env.ClassFeature.GetClass<TClass>();
			return this.ToInternal(jClass);
		}
		/// <summary>
		/// Retrieves a <see cref="ObjectInitializer"/> instance from <paramref name="initializer"/>
		/// </summary>
		/// <param name="initializer">A <see cref="InternalObjectInitializer"/> instance.</param>
		/// <returns>A <see cref="ObjectInitializer"/> value.</returns>
		internal static ObjectInitializer FromInternal(InternalObjectInitializer initializer) => new(initializer);
	}
}