namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IReferenceFeature
{
	/// <summary>
	/// Indicates whether current instance is not dummy.
	/// </summary>
	internal Boolean RealEnvironment { get; }

	/// <summary>
	/// Retrieves <see cref="ObjectLifetime"/> instance for <paramref name="localRef"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="jClass">Current <see cref="JClassObject"/> instance.</param>
	/// <param name="overrideClass">Indicates whether <paramref name="jClass"/> should override instance class.</param>
	/// <returns>A <see cref="ObjectLifetime"/> instance for <paramref name="localRef"/>.</returns>
	internal ObjectLifetime? GetLifetime(JLocalObject jLocal, JObjectLocalRef localRef, JClassObject? jClass,
		Boolean overrideClass);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> value.</typeparam>
	/// <param name="primitive">A primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.</returns>
	internal JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}