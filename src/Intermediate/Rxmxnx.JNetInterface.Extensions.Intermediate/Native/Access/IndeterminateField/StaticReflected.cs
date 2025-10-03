namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// Copies the primitive value of a reflected field instance to <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="jField">Reflected field object.</param>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void CopyReflectedPrimitiveFieldValue<TPrimitive>(Span<Byte> bytes, JFieldObject jField,
		JLocalObject? jLocal = default) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jField.Environment;
		TPrimitive result = jLocal is not null ?
			env.AccessFeature.GetField<TPrimitive>(jField, jLocal, jField.Definition) :
			env.AccessFeature.GetStaticField<TPrimitive>(jField, jField.Definition);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Sets the value of a reflected field instance from <paramref name="fieldValue"/> instance.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="fieldValue">Value to set to.</param>
	/// <param name="jLocal">Target object.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void SetReflectedFieldObject(JFieldObject jField, IndeterminateResult fieldValue,
		JLocalObject? jLocal = default)
	{
		IEnvironment env = jField.Environment;
		JLocalObject? jObject = IndeterminateField.CreateObject(env, fieldValue, out Boolean newObject);
		try
		{
			if (jLocal is not null)
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, jObject);
			else
				env.AccessFeature.SetStaticField(jField, jField.Definition, jObject);
		}
		finally
		{
			if (newObject)
				jObject?.Dispose();
		}
	}
}