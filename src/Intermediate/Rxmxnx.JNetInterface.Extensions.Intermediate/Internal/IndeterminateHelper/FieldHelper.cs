namespace Rxmxnx.JNetInterface.Internal;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal static unsafe partial class IndeterminateHelper
{
	/// <summary>
	/// Retrieves the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jField">Reflected field instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedGet(JFieldObject jField, JLocalObject jLocal)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateHelper.GetFieldType(jField.Definition);

		if (returnType.Length != 1)
		{
			IEnvironment env = jField.Environment;
			JLocalObject? jObject = env.AccessFeature.GetField<JLocalObject>(jField, jLocal, jField.Definition);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[sizeof(JValue.PrimitiveValue)];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JBoolean>(bytes, jField, jLocal);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JByte>(bytes, jField, jLocal);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JChar>(bytes, jField, jLocal);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JDouble>(bytes, jField, jLocal);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JFloat>(bytes, jField, jLocal);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JInt>(bytes, jField, jLocal);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JLong>(bytes, jField, jLocal);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JShort>(bytes, jField, jLocal);
				break;
		}
		return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], returnType);
	}
	/// <summary>
	/// Retrieves the value of a static field on the declaring class of given <see cref="JMethodObject"/> instance.
	/// </summary>
	/// <param name="jField">Reflected field instance.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedStaticGet(JFieldObject jField)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateHelper.GetFieldType(jField.Definition);

		if (returnType.Length != 1)
		{
			IEnvironment env = jField.Environment;
			JLocalObject? jObject = env.AccessFeature.GetStaticField<JLocalObject>(jField, jField.Definition);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[sizeof(JValue.PrimitiveValue)];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JBoolean>(bytes, jField);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JByte>(bytes, jField);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JChar>(bytes, jField);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JDouble>(bytes, jField);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JFloat>(bytes, jField);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JInt>(bytes, jField);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JLong>(bytes, jField);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateHelper.CopyReflectedPrimitiveFieldValue<JShort>(bytes, jField);
				break;
		}
		return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], returnType);
	}
	/// <summary>
	/// Sets <paramref name="value"/> as the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jField">Reflected field instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static void ReflectedSet<TValue>(JFieldObject jField, JLocalObject jLocal, TValue? value)
		where TValue : IObject
	{
		IEnvironment env = jField.Environment;
		ReadOnlySpan<Byte> signature = IndeterminateHelper.GetFieldType(jField.Definition);
		IndeterminateResult fieldValue = IndeterminateField.GetFieldValue(env, value, signature);

		switch (signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.BooleanValue);
				break;
			case CommonNames.ByteSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.ByteValue);
				break;
			case CommonNames.CharSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.CharValue);
				break;
			case CommonNames.DoubleSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.DoubleValue);
				break;
			case CommonNames.FloatSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.FloatValue);
				break;
			case CommonNames.IntSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.IntValue);
				break;
			case CommonNames.LongSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.LongValue);
				break;
			case CommonNames.ShortSignatureChar:
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, fieldValue.ShortValue);
				break;
			default:
				IndeterminateHelper.SetReflectedFieldObject(jField, fieldValue, jLocal);
				break;
		}
	}
	/// <summary>
	/// Sets <paramref name="value"/> as the value of a static field on the declaring class of given
	/// <see cref="JMethodObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jField">Reflected field instance.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static void ReflectedStaticSet<TValue>(JFieldObject jField, TValue? value) where TValue : IObject
	{
		IEnvironment env = jField.Environment;
		ReadOnlySpan<Byte> signature = IndeterminateHelper.GetFieldType(jField.Definition);
		IndeterminateResult fieldValue = IndeterminateField.GetFieldValue(env, value, signature);

		switch (signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.BooleanValue);
				break;
			case CommonNames.ByteSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.ByteValue);
				break;
			case CommonNames.CharSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.CharValue);
				break;
			case CommonNames.DoubleSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.DoubleValue);
				break;
			case CommonNames.FloatSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.FloatValue);
				break;
			case CommonNames.IntSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.IntValue);
				break;
			case CommonNames.LongSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.LongValue);
				break;
			case CommonNames.ShortSignatureChar:
				env.AccessFeature.SetStaticField(jField, jField.Definition, fieldValue.ShortValue);
				break;
			default:
				IndeterminateHelper.SetReflectedFieldObject(jField, fieldValue);
				break;
		}
	}
}