namespace Rxmxnx.JNetInterface.Native;

public partial class JArrayObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="jArrayRef">Local array reference.</param>
	/// <param name="length">Array length.</param>
	internal JArrayObject(JClassObject jClass, JArrayLocalRef jArrayRef, Int32? length) : base(jClass, jArrayRef.Value)
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
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jArrayRef">Local array reference.</param>
	/// <param name="length">Array length.</param>
	internal JArrayObject(IEnvironment env, JArrayLocalRef jArrayRef, Int32? length) : 
		base(env.ClassProvider.GetClass<JArrayObject<TElement>>(), jArrayRef, length) { }
	/// <inheritdoc/>
	internal JArrayObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
}