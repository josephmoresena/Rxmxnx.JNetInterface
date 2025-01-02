namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="definition">Internal definition.</param>
	/// <param name="returnType">Return type signature.</param>
	private IndeterminateCall(JCallDefinition definition, CString returnType)
	{
		this.Definition = definition;
		this.ReturnType = returnType;
	}

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult FunctionCall(JFunctionDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			IndeterminateResult result = new(out Span<Byte> bytes, signature);
			env.AccessFeature.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, args);
			return result;
		}

		JLocalObject? jObject =
			env.AccessFeature.CallFunction<JLocalObject>(jLocal, jClass, definition, nonVirtual, args);
		return new(jObject, signature);
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult StaticFunctionCall(JFunctionDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			IndeterminateResult result = new(out Span<Byte> bytes, signature);
			env.AccessFeature.CallPrimitiveStaticFunction(bytes, jClass, definition, args);
			return result;
		}

		JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject>(jClass, definition, args);
		return new(jObject, signature);
	}

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jFunction">Reflected function instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static IndeterminateResult ReflectedFunctionCall(JFunctionDefinition definition, JMethodObject jFunction,
		JLocalObject jLocal, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateCall.GetReturnType(definition);
		if (returnType.Length != 1)
		{
			IEnvironment env = jFunction.Environment;
			JLocalObject? jObject =
				env.AccessFeature.CallFunction<JLocalObject>(jFunction, jLocal, definition, nonVirtual, args);
			return new(jObject, returnType);
		}

		IndeterminateResult result = new(out Span<Byte> bytes, returnType);
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JBoolean>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JByte>(bytes, definition, jFunction, jLocal,
				                                                        nonVirtual, args);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JChar>(bytes, definition, jFunction, jLocal,
				                                                        nonVirtual, args);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JDouble>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JFloat>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JInt>(bytes, definition, jFunction, jLocal, nonVirtual,
				                                                       args);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JLong>(bytes, definition, jFunction, jLocal,
				                                                        nonVirtual, args);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateCall.ReflectedPrimitiveFunctionCall<JShort>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
		}
		return result;
	}
	/// <summary>
	/// Invokes a static function on the declaring class of given <see cref="JMethodObject"/> instance and
	/// returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jFunction">Reflected function instance.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static IndeterminateResult ReflectedStaticFunctionCall(JFunctionDefinition definition,
		JMethodObject jFunction, ReadOnlySpan<IObject?> args)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateCall.GetReturnType(definition);
		if (returnType.Length != 1)
		{
			IEnvironment env = jFunction.Environment;
			JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject>(jFunction, definition, args);
			return new(jObject, returnType);
		}

		IndeterminateResult result = new(out Span<Byte> bytes, returnType);
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JBoolean>(bytes, definition, jFunction, args);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JByte>(bytes, definition, jFunction, args);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JChar>(bytes, definition, jFunction, args);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JDouble>(bytes, definition, jFunction, args);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JFloat>(bytes, definition, jFunction, args);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JInt>(bytes, definition, jFunction, args);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JLong>(bytes, definition, jFunction, args);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateCall.ReflectedPrimitiveStaticFunctionCall<JShort>(bytes, definition, jFunction, args);
				break;
		}
		return result;
	}
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	private static void MethodCall(JMethodDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, definition, nonVirtual, args);
	}
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	private static void ReflectedMethodCall(JMethodDefinition definition, JMethodObject jMethod, JLocalObject jLocal,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jMethod, jLocal, definition, nonVirtual, args);
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
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected method instance.</param>
	/// <param name="args">Method arguments.</param>
	private static void ReflectedStaticMethodCall(JMethodDefinition definition, JMethodObject jMethod,
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jMethod.Environment;
		env.AccessFeature.CallStaticMethod(jMethod, definition, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	private static TObject NewCall<TObject>(JConstructorDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jClass, definition, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
	private static TObject ReflectedNewCall<TObject>(JConstructorDefinition definition, JConstructorObject jConstructor,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jConstructor, definition, args);
	}
	/// <summary>
	/// Creates a primitive <see cref="JFunctionDefinition"/> instance.
	/// </summary>
	/// <param name="functionName">UTF-8 function name.</param>
	/// <param name="returnTypeSignature">Return type signature.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JFunctionDefinition"/> instance.</returns>
	private static JFunctionDefinition CreatePrimitiveFunction(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<Byte> returnTypeSignature, ReadOnlySpan<JArgumentMetadata> args)
		=> returnTypeSignature[0] switch
		{
			CommonNames.BooleanSignatureChar => JFunctionDefinition<JBoolean>.Create(functionName, args),
			CommonNames.ByteSignatureChar => JFunctionDefinition<JByte>.Create(functionName, args),
			CommonNames.CharSignatureChar => JFunctionDefinition<JChar>.Create(functionName, args),
			CommonNames.DoubleSignatureChar => JFunctionDefinition<JDouble>.Create(functionName, args),
			CommonNames.FloatSignatureChar => JFunctionDefinition<JFloat>.Create(functionName, args),
			CommonNames.IntSignatureChar => JFunctionDefinition<JInt>.Create(functionName, args),
			CommonNames.LongSignatureChar => JFunctionDefinition<JLong>.Create(functionName, args),
			_ => JFunctionDefinition<JShort>.Create(functionName, args),
		};
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected function object.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static void ReflectedPrimitiveFunctionCall<TPrimitive>(Span<Byte> bytes, JFunctionDefinition definition,
		JMethodObject jMethod, JLocalObject jLocal, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jMethod.Environment;
		TPrimitive result = env.AccessFeature.CallFunction<TPrimitive>(jMethod, jLocal, definition, nonVirtual, args);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Invokes a primitive static function on the declaring class of given <see cref="JMethodObject"/> instance and
	/// returns its result.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected function object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static void ReflectedPrimitiveStaticFunctionCall<TPrimitive>(Span<Byte> bytes,
		JFunctionDefinition definition, JMethodObject jMethod, ReadOnlySpan<IObject?> args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jMethod.Environment;
		TPrimitive result = env.AccessFeature.CallStaticFunction<TPrimitive>(jMethod, definition, args);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Retrieves the return type signature from <paramref name="definition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <returns>Return type definition.</returns>
	private static CString GetReturnType(JCallDefinition definition)
	{
		Int32 offset = definition.Descriptor.AsSpan().IndexOf(CommonNames.MethodParameterSuffixChar) + 1;
		CString returnType = definition.Descriptor[offset..];
		return returnType;
	}
}