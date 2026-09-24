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
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring a call class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private IndeterminateResult FunctionCall<TArgs>(JFunctionDefinition definition, JLocalObject jLocal,
		JClassObject jClass, Boolean nonVirtual, scoped in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		ReadOnlySpan<Byte> signature = this.ReturnType;
		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JLocalObject, JClassObject, Boolean, JFunctionDefinition, in TArgs?,
				void> invoke = signature[0] switch
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
			invoke(out JValue.PrimitiveValue pValue, jLocal, jClass, nonVirtual, definition, in args);
			return new(pValue, signature);
		}
		IEnvironment env = jLocal.Environment;
		JLocalObject? jObject =
			env.AccessFeature.CallFunction<JLocalObject, TArgs>(jLocal, jClass, definition, nonVirtual, in args);
		return new(jObject, signature);
		static void Invoke<TPrimitive>(out JValue.PrimitiveValue result, JLocalObject jLocal, JClassObject jClass,
			Boolean nonVirtual, JFunctionDefinition definition, in TArgs? args)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jLocal.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.CallFunction<TPrimitive, TArgs>(jLocal, jClass, definition, nonVirtual, in args);
		}
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the function.</typeparam>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private IndeterminateResult StaticFunctionCall<TArgs>(JFunctionDefinition definition, JClassObject jClass,
		scoped in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			delegate* <out JValue.PrimitiveValue, JClassObject, JFunctionDefinition, in TArgs?, void> invoke =
				signature[0] switch
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
			invoke(out JValue.PrimitiveValue pValue, jClass, definition, in args);
			return new(pValue, signature);
		}

		JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject, TArgs>(jClass, definition, in args);
		return new(jObject, signature);
		static void Invoke<TPrimitive>(out JValue.PrimitiveValue result, JClassObject jClass,
			JFunctionDefinition definition, in TArgs? args) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			result = default;
			IEnvironment env = jClass.Environment;
			Unsafe.As<JValue.PrimitiveValue, TPrimitive>(ref result) =
				env.AccessFeature.CallStaticFunction<TPrimitive, TArgs>(jClass, definition, in args);
		}
	}
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring a call class.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static void MethodCall<TArgs>(JMethodDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, definition, nonVirtual, in args);
	}
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the method.</typeparam>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static void StaticMethodCall<TArgs>(JMethodDefinition definition, JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, definition, in args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	private static TObject NewCall<TObject, TArgs>(JConstructorDefinition definition, JClassObject jClass,
		in TArgs? args) where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallConstructor<TObject, TArgs>(jClass, definition, in args);
	}
}