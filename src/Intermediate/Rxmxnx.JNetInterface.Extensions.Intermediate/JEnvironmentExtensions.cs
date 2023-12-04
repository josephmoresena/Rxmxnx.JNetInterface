/// <summary>
/// Set of <see cref="IEnvironment"/> extensions.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1050
public static class JEnvironmentExtensions
#pragma warning restore CA1050
{
	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JBoolean"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JBooleanObject"/> instance.</returns>
	public static JBooleanObject ToJObject(this JBoolean value, IEnvironment env) => JBooleanObject.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JByteObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JByteObject"/> instance.</returns>
	public static JByteObject ToJObject(this JByte value, IEnvironment env)
		=> JNumberObject<JByte, JByteObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JCharacterObject"/> instance.</returns>
	public static JCharacterObject ToJObject(this JChar value, IEnvironment env) => JCharacterObject.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JDoubleObject"/> instance.</returns>
	public static JDoubleObject ToJObject(this JDouble value, IEnvironment env)
		=> JNumberObject<JDouble, JDoubleObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JFloatObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JFloatObject"/> instance.</returns>
	public static JFloatObject ToJObject(this JFloat value, IEnvironment env)
		=> JNumberObject<JFloat, JFloatObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JIntegerObject"/> instance.</returns>
	public static JIntegerObject ToJObject(this JInt value, IEnvironment env)
		=> JNumberObject<JInt, JIntegerObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JLongObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JLongObject"/> instance.</returns>
	public static JLongObject ToJObject(this JLong value, IEnvironment env)
		=> JNumberObject<JLong, JLongObject>.Create(env, value);
	/// <summary>
	/// Creates a <see cref="JShortObject"/> from current value.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> value.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JShortObject"/> instance.</returns>
	public static JShortObject ToJObject(this JShort value, IEnvironment env)
		=> JNumberObject<JShort, JShortObject>.Create(env, value);

	/// <summary>
	/// Creates a <see cref="JArrayObject{JBoolean}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>boolean</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JBoolean}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JBoolean>? CreateParameterArray(this IEnvironment env, JBooleanArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JBoolean>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JByte}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>byte</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JByte}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JByte>? CreateParameterArray(this IEnvironment env, JByteArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JByte>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JChar}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>char</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JChar}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JChar>? CreateParameterArray(this IEnvironment env, JCharArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JChar>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JDouble}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>double</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JDouble}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JDouble>? CreateParameterArray(this IEnvironment env, JDoubleArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JDouble>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JFloat}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>float</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JFloat}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JFloat>? CreateParameterArray(this IEnvironment env, JFloatArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JFloat>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JInt}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>int</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JInt}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JInt>? CreateParameterArray(this IEnvironment env, JIntArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JInt>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JLong}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>long</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JLong}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JLong>? CreateParameterArray(this IEnvironment env, JLongArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JLong>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JShort}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>short</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JShort}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<JShort>? CreateParameterArray(this IEnvironment env, JShortArrayLocalRef arrayRef)
		=> env.CreateParameterArray<JShort>(arrayRef.ArrayValue);
	/// <summary>
	/// Creates a <see cref="JArrayObject{JShort}"/> instance for <paramref name="arrayRef"/> reference
	/// whose origin is a JNI argument.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="arrayRef">A local <c>short</c> array reference.</param>
	/// <returns>A <see cref="JArrayObject{JShort}"/> instance passed as JNI argument.</returns>
	public static JArrayObject<TObject>? CreateParameterArray<TObject>(this IEnvironment env, JObjectArrayLocalRef arrayRef)
		where TObject : JLocalObject, IReferenceType<TObject>
		=> env.CreateParameterArray<TObject>(arrayRef.ArrayValue);
	
	/// <summary>
	/// Retrieves a <see cref="JBooleanArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JBoolean}"/> instance.</param>
	/// <returns>A <see cref="JBooleanArrayLocalRef"/> reference from given object.</returns>
	public static JBooleanArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JBoolean>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JBooleanArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JByteArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JByte}"/> instance.</param>
	/// <returns>A <see cref="JByteArrayLocalRef"/> reference from given object.</returns>
	public static JByteArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JByte>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JByteArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JCharArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JChar}"/> instance.</param>
	/// <returns>A <see cref="JCharArrayLocalRef"/> reference from given object.</returns>
	public static JCharArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JChar>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JCharArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JDoubleArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JDouble}"/> instance.</param>
	/// <returns>A <see cref="JDoubleArrayLocalRef"/> reference from given object.</returns>
	public static JDoubleArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JDouble>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JDoubleArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JFloatArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JFloat}"/> instance.</param>
	/// <returns>A <see cref="JFloatArrayLocalRef"/> reference from given object.</returns>
	public static JFloatArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JFloat>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JFloatArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JIntArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JInt}"/> instance.</param>
	/// <returns>A <see cref="JIntArrayLocalRef"/> reference from given object.</returns>
	public static JIntArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JInt>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JIntArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JLongArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JLong}"/> instance.</param>
	/// <returns>A <see cref="JLongArrayLocalRef"/> reference from given object.</returns>
	public static JLongArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JLong>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JLongArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JShortArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JShort}"/> instance.</param>
	/// <returns>A <see cref="JShortArrayLocalRef"/> reference from given object.</returns>
	public static JShortArrayLocalRef GetArrayReturn(this IEnvironment env, JArrayObject<JShort>? jArray)
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JShortArrayLocalRef>(ref arrayRef);
	}
	/// <summary>
	/// Retrieves a <see cref="JObjectArrayLocalRef"/> reference from given array.
	/// </summary>
	/// <param name="env">Current <see cref="IEnvironment"/> instance.</param>
	/// <param name="jArray">A <see cref="JArrayObject{JShort}"/> instance.</param>
	/// <returns>A <see cref="JObjectArrayLocalRef"/> reference from given object.</returns>
	public static JObjectArrayLocalRef GetArrayReturn<TObject>(this IEnvironment env, JArrayObject<TObject>? jArray)
		where TObject : JLocalObject, IReferenceType<TObject>
	{
		JArrayLocalRef arrayRef = env.GetReturn(jArray);
		return Unsafe.As<JArrayLocalRef, JObjectArrayLocalRef>(ref arrayRef);
	}
}