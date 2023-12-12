namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jArrayRef">Local array reference.</param>
	/// <param name="length">Array length.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JArrayObject(IEnvironment env, JArrayLocalRef jArrayRef, Int32? length, Boolean isDummy,
		JClassObject? jClass = default) : base(env, jArrayRef.Value, isDummy, jClass)
		=> this._length = length;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JArrayObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass)
	{
		if (jLocal is not JArrayObject jArray)
			return;
		this._length = jArray.Length;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	internal JArrayObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal)
		=> this._length ??= env.ArrayProvider.GetArrayLength(jGlobal);
}

public partial class JArrayObject<TElement>
{
	/// <inheritdoc/>
	internal JArrayObject(IEnvironment env, JArrayLocalRef jArrayRef, Int32? length, Boolean isDummy,
		JClassObject? jClass = default) : base(env, jArrayRef, length, isDummy, jClass) { }
	/// <inheritdoc/>
	internal JArrayObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}