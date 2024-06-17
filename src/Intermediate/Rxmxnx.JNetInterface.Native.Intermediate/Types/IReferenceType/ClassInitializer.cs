namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JClassObject"/> instance and
	/// <see cref="JObjectLocalRef"/> reference.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected readonly ref struct ClassInitializer
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
			get => this._instance.Class!;
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
		[ExcludeFromCodeCoverage]
		internal Boolean RealClass
		{
			get => this._instance.OverrideClass;
			init => this._instance.OverrideClass = value;
		}

		/// <summary>
		/// Retrieves an instance with <typeparamref name="TClass"/> class instance.
		/// </summary>
		/// <typeparam name="TClass">Datatype class</typeparam>
		/// <returns>A <see cref="ObjectInitializer"/> value.</returns>
		internal ClassInitializer WithClass<TClass>() where TClass : IDataType<TClass>
		{
			IEnvironment env = this.Class.Environment;
			JClassObject jClass = env.ClassFeature.GetClass<TClass>();
			return new() { Class = jClass, LocalReference = this._instance.LocalReference, RealClass = true, };
		}

		/// <summary>
		/// Retrieves a <see cref="InternalClassInitializer"/> instance
		/// from current instance.
		/// </summary>
		/// <returns>A <see cref="InternalClassInitializer"/> value.</returns>
		internal InternalClassInitializer ToInternal() => this._instance;
	}
}