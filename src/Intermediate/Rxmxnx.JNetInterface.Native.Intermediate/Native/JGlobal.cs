namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents a <see cref="JObject"/> instance which remain valid across
/// different threads until it is explicitly unloaded.
/// </summary>
public abstract class JGlobal : JGlobalBase
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="globalRef">Global object reference.</param>
	internal JGlobal(ILocalObject jLocal, JGlobalRef globalRef) : base(jLocal, globalRef) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm"><see cref="IVirtualMachine"/> instance.</param>
	/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="globalRef">Global reference.</param>
	internal JGlobal(IVirtualMachine vm, JObjectMetadata metadata, Boolean isDummy, JGlobalRef globalRef) : base(
		vm, metadata, isDummy, globalRef) { }

	/// <inheritdoc/>
	public override Boolean IsValid(IEnvironment env)
		=> base.IsValid(env) && env.GetReferenceType(this) == JReferenceType.GlobalRefType;

	/// <inheritdoc cref="JGlobalBase.Load(JObjectLifetime)"/>
	internal new virtual JGlobal? Load(JObjectLifetime lifetime) => base.Load(lifetime) as JGlobal;

	/// <summary>
	/// Synchronizes current instance with <paramref name="other"/>.
	/// </summary>
	/// <param name="other">A <see cref="JGlobal"/> instance.</param>
	internal abstract void Synchronize(JGlobal other);
}