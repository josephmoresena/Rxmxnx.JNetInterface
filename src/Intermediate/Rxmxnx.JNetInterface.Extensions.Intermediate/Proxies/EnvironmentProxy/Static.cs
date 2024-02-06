namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.
	/// </summary>
	/// <param name="env">A <see cref="EnvironmentProxy"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.</returns>
	public static JClassObject CreateClassObject(EnvironmentProxy env, JClassLocalRef classRef = default) => new(env);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JLocalObject CreateObject(JClassObject jClass, JObjectLocalRef localRef) => new(jClass, localRef);
	/// <summary>
	/// Creates a <typeparamref name="TObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <typeparam name="TObject"></typeparam>
	/// <returns>A <typeparamref name="TObject"/> instance.</returns>
	public static TObject CreateObject<TObject>(JClassObject jClass, JObjectLocalRef localRef)
		where TObject : JLocalObject, IReferenceType<TObject>
	{
		JReferenceTypeMetadata metadata = IReferenceType.GetMetadata<TObject>();
		return (TObject)metadata.CreateInstance(jClass, localRef, true);
	}
	/// <summary>
	/// Creates a <typeparamref name="TObject"/> instance.
	/// </summary>
	/// <param name="env">A <see cref="EnvironmentProxy"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <typeparam name="TObject"></typeparam>
	/// <returns>A <typeparamref name="TObject"/> instance.</returns>
	public static TObject CreateObject<TObject>(EnvironmentProxy env, JObjectLocalRef localRef)
		where TObject : JLocalObject, IReferenceType<TObject>
	{
		JReferenceTypeMetadata metadata = IReferenceType.GetMetadata<TObject>();
		JClassObject jClass = env.GetClass<TObject>();
		return (TObject)metadata.CreateInstance(jClass, localRef);
	}
}