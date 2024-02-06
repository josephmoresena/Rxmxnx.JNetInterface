namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IReferenceFeature
{
	/// <summary>
	/// Retrieves <see cref="ObjectLifetime"/> instance for <paramref name="initializer"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="initializer">A <see cref="IReferenceType.ClassInitializer"/> instance.</param>
	/// <returns>A <see cref="ObjectLifetime"/> instance for <paramref name="initializer"/>.</returns>
	internal ObjectLifetime GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> value.</typeparam>
	/// <param name="primitive">A primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.</returns>
	internal JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <summary>
	/// Enters to monitor.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <exception cref="JniException"/>
	void MonitorEnter(JObjectLocalRef localRef);
	/// <summary>
	/// Exits from monitor.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <exception cref="JniException"/>
	void MonitorExit(JObjectLocalRef localRef);
}