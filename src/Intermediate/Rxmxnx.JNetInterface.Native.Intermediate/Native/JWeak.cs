namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which may remain valid across
/// different threads until it is explicitly unloaded.
/// </summary>
public abstract class JWeak : JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="weakRef">Weak global object reference.</param>
	internal JWeak(ILocalObject jLocal, JWeakRef weakRef) : base(jLocal, weakRef) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="weakRef">Weak global reference.</param>
	internal JWeak(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JWeakRef weakRef) : base(
		vm, metadata, isDummy, weakRef) { }

	/// <inheritdoc/>
	public override Boolean IsValid(IEnvironment env)
		=> base.IsValid(env) && env.GetReferenceType(this) == JReferenceType.WeakGlobalRefType &&
			!env.IsSameObject(this, default);

	/// <inheritdoc cref="JGlobalBase.Load(JLocalObject)"/>
	internal new virtual JWeak? Load(JLocalObject jLocal) => base.Load(jLocal) as JWeak;
}