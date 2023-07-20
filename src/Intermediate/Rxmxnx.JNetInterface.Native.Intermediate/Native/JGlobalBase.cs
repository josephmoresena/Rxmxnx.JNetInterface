namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which may remain valid across different
/// threads.
/// </summary>
public abstract class JGlobalBase : JReferenceObject, IDisposable
{
	/// <summary>
	/// This collection stores the weak references to the <see cref="JLocalObject"/> associated with 
	/// this instance.
	/// </summary>
	private readonly ConcurrentBag<WeakReference<JLocalObject>> _objects = new();
	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private readonly IVirtualMachine _vm;

	/// <summary>
	/// Indicate whether the current instance is disposed.
	/// </summary>
	private Boolean _isDisposed;

	/// <summary>
	/// <see cref="IVirtualMachine"/> instance.
	/// </summary>
	public IVirtualMachine VirtualMachine => this._vm;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JGlobalBase(IVirtualMachine vm, Boolean isDummy) : base(isDummy) => this._vm = vm;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JGlobalRef globalRef) : base(globalRef, jLocal.IsDummy)
		=> this._vm = jLocal.Environment.VirtualMachine;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JGlobalBase(JLocalObject jLocal, JWeakRef weakRef) : base(weakRef, jLocal.IsDummy)
		=> this._vm = jLocal.Environment.VirtualMachine;

	/// <summary>
	/// Destructor.
	/// </summary>
	~JGlobalBase()
	{
		using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Dispose(false, thread);
	}

	/// <summary>
	/// Indicates whether the current instance is valid.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public virtual Boolean IsValid(IEnvironment env) => !this._isDisposed && !this.IsDefault;

	/// <inheritdoc/>
	public void Dispose()
	{
		using IThread thread = this._vm.CreateThread(ThreadPurpose.RemoveGlobalReference);
		this.Unload(thread);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Performs application-defined tasks associated with freeing, releasing, or resetting
	/// unmanaged resources.
	/// </summary>
	public void Unload(IEnvironment env) => this.Dispose(true, env);

	/// <inheritdoc cref="IDisposable.Dispose()"/>
	/// <param name="disposing">
	/// Indicates whether this method was called from the <see cref="IDisposable.Dispose"/> method.
	/// </param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	protected virtual void Dispose(Boolean disposing, IEnvironment env)
	{
		if (this._isDisposed)
			return;

		if (disposing)
			foreach (WeakReference<JLocalObject> wReference in this._objects)
				if (wReference.TryGetTarget(out JLocalObject? jLocal))
					this.Unload(jLocal);
		this.ClearValue();
		this._isDisposed = this is not IClassType;
	}

	/// <summary>
	/// Retrieves the <see cref="IClass"/> from current global object.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>The <see cref="IClass"/> from current global object.</returns>
	internal IClass? GetObjectClass(IEnvironment env)
		=> !this.ObjectClassName.Equals(JObject.JObjectClassName) ? 
			env.ClassProvider.GetClass(this.ObjectClassName) : default;

	/// <summary>
	/// Associates the current current instance to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <returns>A valid <see cref="JGlobalBase"/> associated to <paramref name="jLocal"/>.</returns>
	protected JGlobalBase? Load(JLocalObject jLocal)
	{
		this._objects.Add(new(jLocal));
		JGlobalBase? result = default;
		if (!this.IsValid(jLocal.Environment)) 
			return result;
		result = this;
		this._objects.Add(new(jLocal));
		return result;
	}
	/// <summary>
	/// Disassociates the current current instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	protected abstract void Unload(in JLocalObject jLocal);
}