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
	public void StaticSet<TValue>(JClassObject jClass, TValue? value) where TValue : IObject
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
}