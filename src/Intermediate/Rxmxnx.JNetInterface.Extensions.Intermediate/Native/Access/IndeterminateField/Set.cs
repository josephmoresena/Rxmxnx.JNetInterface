namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public unsafe partial class IndeterminateField
{
	/// <summary>
	/// Sets <paramref name="value"/> as the value of a field on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TValue">A <see cref="IObject"/> type.</typeparam>
	/// <param name="jLocal">Target object.</param>
	/// <param name="value">New field value.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	public void Set<TValue>(JLocalObject jLocal, TValue? value) where TValue : IObject
		=> this.Set(jLocal, jLocal.Class, value);
	/// <summary>
	/// Sets <paramref name="value"/> as the value of a field on given <see cref="JLocalObject"/> instance.
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
		if (signature.Length == 1)
		{
			Byte primitiveSignature = signature[0];
			delegate* <JLocalObject, JClassObject, Byte, IndeterminateResult, JFieldDefinition, void> set =
				primitiveSignature switch
				{
					CommonNames.BooleanSignatureChar => &SetPrimitive<JBoolean>,
					CommonNames.ByteSignatureChar => &SetPrimitive<JByte>,
					CommonNames.CharSignatureChar => &SetPrimitive<JChar>,
					CommonNames.DoubleSignatureChar => &SetPrimitive<JDouble>,
					CommonNames.FloatSignatureChar => &SetPrimitive<JFloat>,
					CommonNames.IntSignatureChar => &SetPrimitive<JInt>,
					CommonNames.LongSignatureChar => &SetPrimitive<JLong>,
					CommonNames.ShortSignatureChar => &SetPrimitive<JShort>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
			set(jLocal, jClass, primitiveSignature, fieldValue, this.Definition);
			return;
		}
		IndeterminateField.SetFieldObject(jClass, this.Definition, fieldValue, jLocal);
		return;
		static void SetPrimitive<TPrimitive>(JLocalObject jLocal, JClassObject jClass, Byte signature,
			IndeterminateResult fieldValue, JFieldDefinition definition)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			IEnvironment env = jClass.Environment;
			Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
			fieldValue.CopyPrimitiveValue(signature, bytes);
			env.AccessFeature.SetField(jLocal, jClass, definition,
			                           Unsafe.As<Byte, TPrimitive>(ref MemoryMarshal.GetReference(bytes)));
		}
	}
	/// <summary>
	/// Sets <paramref name="value"/> as the value of a static field on given <see cref="JClassObject"/> instance.
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
		if (signature.Length == 1)
		{
			Byte primitiveSignature = signature[0];
			delegate* <JClassObject, Byte, IndeterminateResult, JFieldDefinition, void> set = primitiveSignature switch
			{
				CommonNames.BooleanSignatureChar => &SetPrimitive<JBoolean>,
				CommonNames.ByteSignatureChar => &SetPrimitive<JByte>,
				CommonNames.CharSignatureChar => &SetPrimitive<JChar>,
				CommonNames.DoubleSignatureChar => &SetPrimitive<JDouble>,
				CommonNames.FloatSignatureChar => &SetPrimitive<JFloat>,
				CommonNames.IntSignatureChar => &SetPrimitive<JInt>,
				CommonNames.LongSignatureChar => &SetPrimitive<JLong>,
				CommonNames.ShortSignatureChar => &SetPrimitive<JShort>,
				_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
			};
			set(jClass, primitiveSignature, fieldValue, this.Definition);
			return;
		}
		IndeterminateField.SetFieldObject(jClass, this.Definition, fieldValue);
		return;
		static void SetPrimitive<TPrimitive>(JClassObject jClass, Byte signature, IndeterminateResult fieldValue,
			JFieldDefinition definition) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			IEnvironment env = jClass.Environment;
			Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
			fieldValue.CopyPrimitiveValue(signature, bytes);
			env.AccessFeature.SetStaticField(jClass, definition,
			                                 Unsafe.As<Byte, TPrimitive>(ref MemoryMarshal.GetReference(bytes)));
		}
	}
}