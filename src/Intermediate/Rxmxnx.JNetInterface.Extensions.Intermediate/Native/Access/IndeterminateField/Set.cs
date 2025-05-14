namespace Rxmxnx.JNetInterface.Native.Access;

public partial class IndeterminateField
{
	/// <summary>
	/// Sets <paramref name="value"/> as the value of field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public void Set<TValue>(JLocalObject jLocal, TValue? value) where TValue : IObject
		=> this.Set(jLocal, jLocal.Class, value);
	/// <summary>
	/// Sets <paramref name="value"/> as the value of field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring field class.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public void Set<TValue>(JLocalObject jLocal, JClassObject jClass, TValue? value) where TValue : IObject
	{
		IEnvironment env = jLocal.Environment;
		ReadOnlySpan<Byte> signature = this.FieldType;
		IndeterminateResult fieldValue = IndeterminateField.GetFieldValue(env, value, signature);

		switch (signature[0])
		{
			case CommonNames.BooleanSignatureChar:
			case CommonNames.ByteSignatureChar:
			case CommonNames.CharSignatureChar:
			case CommonNames.DoubleSignatureChar:
			case CommonNames.FloatSignatureChar:
			case CommonNames.IntSignatureChar:
			case CommonNames.LongSignatureChar:
			case CommonNames.ShortSignatureChar:
				Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
				fieldValue.CopyPrimitiveValue(signature[0], bytes);
				env.AccessFeature.SetPrimitiveField(jLocal, jClass, this.Definition, bytes);
				break;
			default:
				IndeterminateField.SetFieldObject(jClass, this.Definition, fieldValue, jLocal);
				break;
		}
	}
	/// <summary>
	/// Sets <paramref name="value"/> as the value of static field on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jClass">Target class.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public unsafe void StaticSet<TValue>(JClassObject jClass, TValue? value) where TValue : IObject
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.FieldType;
		IndeterminateResult fieldValue = IndeterminateField.GetFieldValue(env, value, signature);

		switch (signature[0])
		{
			case CommonNames.BooleanSignatureChar:
			case CommonNames.ByteSignatureChar:
			case CommonNames.CharSignatureChar:
			case CommonNames.DoubleSignatureChar:
			case CommonNames.FloatSignatureChar:
			case CommonNames.IntSignatureChar:
			case CommonNames.LongSignatureChar:
			case CommonNames.ShortSignatureChar:
				Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
				fieldValue.CopyPrimitiveValue(signature[0], bytes);
				env.AccessFeature.SetPrimitiveStaticField(jClass, this.Definition, bytes);
				break;
			default:
				IndeterminateField.SetFieldObject(jClass, this.Definition, fieldValue);
				break;
		}
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
		ReadOnlySpan<Byte> signature = IndeterminateField.GetFieldType(jField.Definition);
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
				IndeterminateField.SetReflectedFieldObject(jField, fieldValue, jLocal);
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
		ReadOnlySpan<Byte> signature = IndeterminateField.GetFieldType(jField.Definition);
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
				IndeterminateField.SetReflectedFieldObject(jField, fieldValue);
				break;
		}
	}
}