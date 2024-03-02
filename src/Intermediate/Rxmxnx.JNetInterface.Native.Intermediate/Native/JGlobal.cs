namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which remain valid across
/// different threads until it is explicitly unloaded.
/// </summary>
public sealed class JGlobal : JGlobalBase
{
	/// <summary>
	/// Weak reference to <see cref="JGlobal"/> instance.
	/// </summary>
	private readonly WeakReference<JGlobal?> _secondary = new(default);

	/// <inheritdoc cref="JGlobal"/>
	private protected override Boolean IsDisposable { get; }
	/// <summary>
	/// Global reference.
	/// </summary>
	internal JGlobalRef Reference => this.As<JGlobalRef>();

	/// <summary>
	/// Secondary <see cref="ObjectLifetime"/>
	/// </summary>
	private JGlobal? Secondary => this._secondary.TryGetTarget(out JGlobal? result) ? result : default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobal(ILocalObject jLocal, JGlobalRef globalRef) : base(jLocal, globalRef) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	/// <param name="isProxy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobal(IVirtualMachine vm, ObjectMetadata metadata, Boolean isProxy, JGlobalRef globalRef) :
		base(vm, metadata, isProxy, globalRef)
		=> this.IsDisposable = metadata.ObjectClassName.AsSpan().SequenceEqual(UnicodeClassNames.ClassObject);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobal(JGlobalBase jGlobal, JGlobalRef globalRef) : base(jGlobal.VirtualMachine, jGlobal.ObjectMetadata,
	                                                                   jGlobal.IsProxy, globalRef) { }

	/// <inheritdoc/>
	public override Boolean IsValid(IEnvironment env)
		=> base.IsValid(env) && env.GetReferenceType(this) == JReferenceType.GlobalRefType;

	/// <inheritdoc cref="JGlobalBase.Load(ObjectLifetime)"/>
	internal new JGlobal? Load(ObjectLifetime lifetime) => base.Load(lifetime) as JGlobal;

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="globalRef">A global object reference the value of current instance.</param>
	internal void SetValue(JGlobalRef globalRef)
	{
		if (this.Reference == globalRef) return;
		base.SetValue(globalRef.Pointer);
		if (this.Secondary is not null && !this.IsDisposable) this.Secondary.SetValue(globalRef);
	}

	/// <summary>
	/// Retrieves cacheable instance.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JGlobal"/> cacheable instance.</returns>
	public JGlobal GetCacheable(IEnvironment env)
	{
		if (!this.IsValid(env) || this.Secondary is null || this.Secondary.IsDisposable) return this;
		return this.Secondary!;
	}
	/// <summary>
	/// Synchronizes current instance with <paramref name="other"/>.
	/// </summary>
	/// <param name="other">A <see cref="JGlobal"/> instance.</param>
	internal void Synchronize(JGlobal other)
	{
		this._secondary.SetTarget(other);
		other._secondary.SetTarget(other);
		this.AssignationCache.Merge(other.AssignationCache);
	}
}