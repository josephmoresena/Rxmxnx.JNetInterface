namespace Rxmxnx.JNetInterface.Types;

public partial interface IReferenceType
{
	/// <summary>
	/// A <see cref="IReferenceType"/> instance initializer using <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <remarks>
	/// Constructor.
	/// </remarks>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected readonly ref struct ObjectInitializer(JLocalObject jLocal)
	{
		/// <summary>
		/// Created object previous instance.
		/// </summary>
		public JLocalObject Instance { get; init; } = jLocal;
		/// <summary>
		/// Override class instance.
		/// </summary>
		public JClassObject? Class { get; init; }

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
			IEnvironment env = this.Instance.Environment;
			JClassObject jClass = env.ClassFeature.GetClass<TClass>();
			return new(this.Instance) { Class = jClass, };
		}
	}
}