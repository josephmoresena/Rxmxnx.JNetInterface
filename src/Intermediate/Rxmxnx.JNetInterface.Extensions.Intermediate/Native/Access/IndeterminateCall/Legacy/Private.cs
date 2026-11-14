namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public abstract unsafe partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring a call class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult FunctionCall(JFunctionDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args
#endif
	)
	{
		ReadOnlySpan<Byte> signature = this.ReturnType;
		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JLocalObject, JClassObject, Boolean, JFunctionDefinition,
				ReadOnlySpan<IObject?>, void> invoke = signature[0] switch
				{
					CommonNames.BooleanSignatureChar => &Invoke<JBoolean>,
					CommonNames.ByteSignatureChar => &Invoke<JByte>,
					CommonNames.CharSignatureChar => &Invoke<JChar>,
					CommonNames.DoubleSignatureChar => &Invoke<JDouble>,
					CommonNames.FloatSignatureChar => &Invoke<JFloat>,
					CommonNames.IntSignatureChar => &Invoke<JInt>,
					CommonNames.LongSignatureChar => &Invoke<JLong>,
					CommonNames.ShortSignatureChar => &Invoke<JShort>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
			invoke(out JValue.PrimitiveValue pValue, jLocal, jClass, nonVirtual, definition, args);
			return new(pValue, signature);
		}
		IEnvironment env = jLocal.Environment;
		JLocalObject? jObject =
			env.AccessFeature.CallFunction<JLocalObject>(jLocal, jClass, definition, nonVirtual, args);
		return new(jObject, signature);
		static void Invoke<TPrimitive>(out JValue.PrimitiveValue result, JLocalObject jLocal, JClassObject jClass,
			Boolean nonVirtual, JFunctionDefinition definition, ReadOnlySpan<IObject?> args)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jLocal.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.CallFunction<TPrimitive>(jLocal, jClass, definition, nonVirtual, args);
		}
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult StaticFunctionCall(JFunctionDefinition definition, JClassObject jClass,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args
#endif
	)
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JClassObject, JFunctionDefinition, ReadOnlySpan<IObject?>, void>
				invoke = signature[0] switch
				{
					CommonNames.BooleanSignatureChar => &Invoke<JBoolean>,
					CommonNames.ByteSignatureChar => &Invoke<JByte>,
					CommonNames.CharSignatureChar => &Invoke<JChar>,
					CommonNames.DoubleSignatureChar => &Invoke<JDouble>,
					CommonNames.FloatSignatureChar => &Invoke<JFloat>,
					CommonNames.IntSignatureChar => &Invoke<JInt>,
					CommonNames.LongSignatureChar => &Invoke<JLong>,
					CommonNames.ShortSignatureChar => &Invoke<JShort>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
			invoke(out JValue.PrimitiveValue pValue, jClass, definition, args);
			return new(pValue, signature);
		}

		JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject>(jClass, definition, args);
		return new(jObject, signature);
		static void Invoke<TPrimitive>(out JValue.PrimitiveValue result, JClassObject jClass,
			JFunctionDefinition definition, ReadOnlySpan<IObject?> args)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jClass.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.CallStaticFunction<TPrimitive>(jClass, definition, args);
		}
	}

	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring a call class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	private static void MethodCall(JMethodDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, definition, nonVirtual, args);
	}
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	private static void StaticMethodCall(JMethodDefinition definition, JClassObject jClass, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, definition, args);
	}

	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private static TObject NewCall<TObject>(JConstructorDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jClass, definition, args);
	}
}