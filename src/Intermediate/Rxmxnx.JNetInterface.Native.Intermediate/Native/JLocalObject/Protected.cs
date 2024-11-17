namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.ClassInitializer initializer) : base(initializer.Class.IsProxy)
		=> this.Lifetime = initializer.Class.Environment.ReferenceFeature.GetLifetime(this, initializer.ToInternal());
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.GlobalInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.GlobalInitializer initializer) : base(initializer.Global)
	{
		this.Lifetime = new(initializer.Environment, this, initializer.Global);
		JLocalObject.ProcessMetadata(this, initializer.Global.ObjectMetadata);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="initializer">A <see cref="IReferenceType.ObjectInitializer"/> initializer.</param>
	protected JLocalObject(IReferenceType.ObjectInitializer initializer) : base(initializer.Instance)
	{
		JLocalObject jLocal = initializer.Instance;
		jLocal.Lifetime.Load(this);
		this.Lifetime = jLocal.Lifetime;
		this.Lifetime.SetClass(initializer.Class);
	}

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	protected virtual void Dispose(Boolean disposing)
	{
		JLocalObject.finalizerExecution = !disposing;
		try
		{
			//Disposed or uninstantiated object.
			if (this.Lifetime is { IsDisposed: true, } or null) return;
			this.Lifetime.Unload(this);
		}
		finally
		{
			JLocalObject.finalizerExecution = false;
		}
	}
	/// <summary>
	/// Creates the object metadata for the current instance.
	/// </summary>
	/// <returns>The object metadata for the current instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected virtual ObjectMetadata CreateMetadata() => new(this.Lifetime.GetLoadClassObject(this));
	/// <summary>
	/// Process the object metadata.
	/// </summary>
	/// <param name="instanceMetadata">The object metadata for the current instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected virtual void ProcessMetadata(ObjectMetadata instanceMetadata) => this.Lifetime.SetClass(instanceMetadata);
}