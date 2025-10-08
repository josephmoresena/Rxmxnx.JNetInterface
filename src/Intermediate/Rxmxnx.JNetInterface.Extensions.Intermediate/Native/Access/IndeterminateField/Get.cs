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
			Span<JValue.PrimitiveValue> pValue = stackalloc JValue.PrimitiveValue[1];
			env.AccessFeature.GetPrimitiveField(pValue.AsBytes(), jLocal, jClass, this.Definition);
			return new(pValue[0], signature);
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
			Span<JValue.PrimitiveValue> pValue = stackalloc JValue.PrimitiveValue[1];
			env.AccessFeature.GetPrimitiveStaticField(pValue.AsBytes(), jClass, this.Definition);
			return new(pValue[0], signature);
		}

		JLocalObject? jObject = env.AccessFeature.GetStaticField<JLocalObject>(jClass, this.Definition);
		return new(jObject, signature);
	}
}