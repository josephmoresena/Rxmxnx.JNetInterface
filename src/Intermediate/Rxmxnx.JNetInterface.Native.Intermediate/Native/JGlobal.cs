namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which remain valid across
/// different threads until it is explicitly unloaded.
/// </summary>
public sealed class JGlobal : JGlobalBase
{
	/// <summary>
	/// Indicates whether current instance is not disposable.
	/// </summary>
	private readonly Boolean _isDisposable;
	/// <summary>
	/// Weak reference to <see cref="JGlobal"/> instance.
	/// </summary>
	private readonly WeakReference<JGlobal?> _secondary = new(default);

	/// <summary>
	/// Global reference.
	/// </summary>
	internal JGlobalRef Reference => this.As<JGlobalRef>();
	/// <inheritdoc cref="JGlobal"/>
	internal override Boolean IsDisposable => this._isDisposable;

	/// <summary>
	/// Secondary <see cref="ObjectLifetime"/>
	/// </summary>
	private JGlobal? Secondary => this._secondary.TryGetTarget(out JGlobal? result) ? result : default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobal(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) :
		base(vm, metadata, isDummy, globalRef)
		=> this._isDisposable = metadata.ObjectClassName.AsSpan().SequenceEqual(UnicodeClassNames.ClassObject);

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
		base.SetValue(NativeUtilities.Transform<JGlobalRef, IntPtr>(globalRef));
		if (this.Secondary is not null && !this._isDisposable) this.Secondary.SetValue(globalRef);
	}

	/// <summary>
	/// Retrieves cacheable instance.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JGlobal"/> cacheable instance.</returns>
	public JGlobal GetCacheable(IEnvironment env)
	{
		if (!this.IsValid(env) || this.Secondary is null || this.Secondary._isDisposable) return this;
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