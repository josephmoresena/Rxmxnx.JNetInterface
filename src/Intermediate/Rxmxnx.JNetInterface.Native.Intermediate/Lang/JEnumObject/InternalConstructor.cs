namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jLocalRef">Local object reference.</param>
	/// <param name="name">Enum instance name.</param>
	/// <param name="ordinal">Enum instance ordinal.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	/// <param name="isNativeParameter">Indicates whether the current instance comes from JNI parameter.</param>
	internal JEnumObject(IEnvironment env, JObjectLocalRef jLocalRef, String? name, Int32? ordinal, Boolean isDummy,
		Boolean isNativeParameter) : base(env, jLocalRef, isDummy, isNativeParameter,
		                                  env.ClassProvider.StringClassObject) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	internal JEnumObject(JLocalObject jLocal, JClassObject jClass) : base(jLocal, jClass) { }
}