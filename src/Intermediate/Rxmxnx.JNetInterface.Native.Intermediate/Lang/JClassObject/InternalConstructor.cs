namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JClassObject(IEnvironment env, JDataTypeMetadata metadata, Boolean isDummy) : base(
		env, default, isDummy, false)
	{
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
		JLocalObject.LoadClassObject(this, metadata.Hash);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jClassRef">Local class reference.</param>
	/// <param name="hash">Internal hash.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JClassObject(IEnvironment env, JClassLocalRef jClassRef, String? hash, Boolean isDummy,
		Boolean isNativeParameter) : base(env, jClassRef.Value, isDummy, isNativeParameter)
	{
		this._hash ??= hash;
		JLocalObject.LoadClassObject(this, hash);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JClassObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		JLocalObject.LoadClassObject(this);
	}
}