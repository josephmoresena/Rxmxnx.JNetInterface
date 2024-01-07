namespace Rxmxnx.JNetInterface.Restricted;

public partial interface IReferenceFeature
{
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <see cref="IPrimitiveType"/> value.</typeparam>
	/// <param name="primitive">A primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> wrapper instance for <paramref name="primitive"/> value.</returns>
	internal JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
}