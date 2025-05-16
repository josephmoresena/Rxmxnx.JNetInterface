namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// Retrieves the value of field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult Get(JLocalObject jLocal) => this.Get(jLocal, jLocal.Class);
	/// <summary>
	/// Retrieves the value of field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring field class.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult Get(JLocalObject jLocal, JClassObject jClass)
	{
		IEnvironment env = jLocal.Environment;
		ReadOnlySpan<Byte> signature = this.FieldType;

		if (signature.Length == 1)
		{
			Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
			env.AccessFeature.GetPrimitiveField(bytes, jLocal, jClass, this.Definition);
			return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], signature);
		}

		JLocalObject? jObject = env.AccessFeature.GetField<JLocalObject>(jLocal, jClass, this.Definition);
		return new(jObject, signature);
	}
	/// <summary>
	/// Retrieves the value of static field on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.FieldType;

		if (signature.Length == 1)
		{
			Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
			env.AccessFeature.GetPrimitiveStaticField(bytes, jClass, this.Definition);
			return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], signature);
		}

		JLocalObject? jObject = env.AccessFeature.GetStaticField<JLocalObject>(jClass, this.Definition);
		return new(jObject, signature);
	}

	/// <summary>
	/// Retrieves the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jField">Reflected field instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public static IndeterminateResult ReflectedGet(JFieldObject jField, JLocalObject jLocal)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateField.GetFieldType(jField.Definition);

		if (returnType.Length != 1)
		{
			IEnvironment env = jField.Environment;
			JLocalObject? jObject = env.AccessFeature.GetField<JLocalObject>(jField, jLocal, jField.Definition);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JBoolean>(bytes, jField, jLocal);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JByte>(bytes, jField, jLocal);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JChar>(bytes, jField, jLocal);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JDouble>(bytes, jField, jLocal);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JFloat>(bytes, jField, jLocal);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JInt>(bytes, jField, jLocal);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JLong>(bytes, jField, jLocal);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JShort>(bytes, jField, jLocal);
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
		ReadOnlySpan<Byte> returnType = IndeterminateField.GetFieldType(jField.Definition);

		if (returnType.Length != 1)
		{
			IEnvironment env = jField.Environment;
			JLocalObject? jObject = env.AccessFeature.GetStaticField<JLocalObject>(jField, jField.Definition);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JBoolean>(bytes, jField);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JByte>(bytes, jField);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JChar>(bytes, jField);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JDouble>(bytes, jField);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JFloat>(bytes, jField);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JInt>(bytes, jField);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JLong>(bytes, jField);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateField.CopyReflectedPrimitiveFieldValue<JShort>(bytes, jField);
				break;
		}
		return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], returnType);
	}
}