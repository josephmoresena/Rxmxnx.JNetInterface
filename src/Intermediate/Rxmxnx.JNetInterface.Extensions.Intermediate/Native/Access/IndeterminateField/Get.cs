namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public unsafe partial class IndeterminateField
{
	/// <summary>
	/// Retrieves the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult Get(JLocalObject jLocal) => this.Get(jLocal, jLocal.Class);
	/// <summary>
	/// Retrieves the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring field class.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult Get(JLocalObject jLocal, JClassObject jClass)
	{
		ReadOnlySpan<Byte> signature = this.FieldType;
		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JLocalObject, JClassObject, JFieldDefinition, void> getPrimitive =
				signature[0] switch
				{
					CommonNames.BooleanSignatureChar => &GetPrimitive<JBoolean>,
					CommonNames.ByteSignatureChar => &GetPrimitive<JByte>,
					CommonNames.CharSignatureChar => &GetPrimitive<JChar>,
					CommonNames.DoubleSignatureChar => &GetPrimitive<JDouble>,
					CommonNames.FloatSignatureChar => &GetPrimitive<JFloat>,
					CommonNames.IntSignatureChar => &GetPrimitive<JInt>,
					CommonNames.LongSignatureChar => &GetPrimitive<JLong>,
					CommonNames.ShortSignatureChar => &GetPrimitive<JShort>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
			getPrimitive(out JValue.PrimitiveValue pValue, jLocal, jClass, this.Definition);
			return new(pValue, signature);
		}
		IEnvironment env = jLocal.Environment;
		JLocalObject? jObject = env.AccessFeature.GetField<JLocalObject>(jLocal, jClass, this.Definition);
		return new(jObject, signature);
		static void GetPrimitive<TPrimitive>(out JValue.PrimitiveValue result, JLocalObject jLocal, JClassObject jClass,
			JFieldDefinition definition) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jLocal.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.GetField<TPrimitive>(jLocal, jClass, definition);
		}
	}
	/// <summary>
	/// Retrieves the value of a static field on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public IndeterminateResult StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.FieldType;

		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JClassObject, JFieldDefinition, void> getPrimitive =
				signature[0] switch
				{
					CommonNames.BooleanSignatureChar => &GetPrimitive<JBoolean>,
					CommonNames.ByteSignatureChar => &GetPrimitive<JByte>,
					CommonNames.CharSignatureChar => &GetPrimitive<JChar>,
					CommonNames.DoubleSignatureChar => &GetPrimitive<JDouble>,
					CommonNames.FloatSignatureChar => &GetPrimitive<JFloat>,
					CommonNames.IntSignatureChar => &GetPrimitive<JInt>,
					CommonNames.LongSignatureChar => &GetPrimitive<JLong>,
					CommonNames.ShortSignatureChar => &GetPrimitive<JShort>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
			getPrimitive(out JValue.PrimitiveValue pValue, jClass, this.Definition);
			return new(pValue, signature);
		}
		JLocalObject? jObject = env.AccessFeature.GetStaticField<JLocalObject>(jClass, this.Definition);
		return new(jObject, signature);
		static void GetPrimitive<TPrimitive>(out JValue.PrimitiveValue result, JClassObject jClass,
			JFieldDefinition definition) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jClass.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.GetStaticField<TPrimitive>(jClass, definition);
		}
	}
}