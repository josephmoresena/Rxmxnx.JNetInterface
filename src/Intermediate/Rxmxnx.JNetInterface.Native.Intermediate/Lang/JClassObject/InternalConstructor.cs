namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JClassObject(IEnvironment env, Boolean isDummy) : base(env, default, isDummy)
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<JClassObject>();
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
		(this as ILocalObject).Lifetime.SetClass(this);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	internal JClassObject(JClassObject jClassClassObject, ITypeInformation metadata) : base(
		jClassClassObject.Environment, default, jClassClassObject.IsDummy, jClassClassObject)
	{
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JClassLocalRef"/> reference.</param>
	internal JClassObject(JClassObject jClassClassObject, JClassLocalRef localRef) : base(
		jClassClassObject.Environment, localRef.Value, jClassClassObject.IsDummy, jClassClassObject) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jClassRef">Local class reference.</param>
	/// <param name="hash">Internal hash.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JClassObject(IEnvironment env, JClassLocalRef jClassRef, String? hash, Boolean isDummy) : base(
		env, jClassRef.Value, isDummy, env.ClassProvider.ClassObject)
		=> this._hash ??= hash;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JClassObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
	{
		(this as ILocalObject).Lifetime.SetClass(env.ClassProvider.ClassObject);
	}
}