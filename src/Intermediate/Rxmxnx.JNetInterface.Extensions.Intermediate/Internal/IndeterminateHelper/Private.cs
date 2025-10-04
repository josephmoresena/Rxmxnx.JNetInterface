namespace Rxmxnx.JNetInterface.Internal;

internal static partial class IndeterminateHelper
{
	/// <summary>
	/// Copies the primitive value of a reflected field instance to <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="jField">Reflected field object.</param>
	/// <param name="jLocal">Target object.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static void CopyReflectedPrimitiveFieldValue<TPrimitive>(Span<Byte> bytes, JFieldObject jField,
		JLocalObject? jLocal = default) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jField.Environment;
		TPrimitive result = jLocal is not null ?
			env.AccessFeature.GetField<TPrimitive>(jField, jLocal, jField.Definition) :
			env.AccessFeature.GetStaticField<TPrimitive>(jField, jField.Definition);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Sets the value of a reflected field instance from <paramref name="fieldValue"/> instance.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="fieldValue">Value to set to.</param>
	/// <param name="jLocal">Target object.</param>
	private static void SetReflectedFieldObject(JFieldObject jField, IndeterminateResult fieldValue,
		JLocalObject? jLocal = default)
	{
		IEnvironment env = jField.Environment;
		JLocalObject? jObject = IndeterminateHelper.GetObjectValue(env, fieldValue, out Boolean newObject);
		try
		{
			if (jLocal is not null)
				env.AccessFeature.SetField(jField, jLocal, jField.Definition, jObject);
			else
				env.AccessFeature.SetStaticField(jField, jField.Definition, jObject);
		}
		finally
		{
			if (newObject)
				jObject?.Dispose();
		}
	}
	/// <summary>
	/// Retrieves the field type signature from <paramref name="definition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JFieldDefinition"/> instance.</param>
	/// <returns>Field type definition.</returns>
	private static ReadOnlySpan<Byte> GetFieldType(JFieldDefinition definition)
	{
		ReadOnlySpan<Byte> descriptorSpan = definition.Descriptor.AsSpan();
		Int32 offset = descriptorSpan.IndexOf(CommonNames.MethodParameterSuffixChar) + 1;
		return definition.Descriptor.AsSpan()[offset..];
	}
	/// <summary>
	/// Retrieves the return type signature from <paramref name="definition"/>.
	/// </summary>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	/// <returns>Return type definition.</returns>
	private static ReadOnlySpan<Byte> GetReturnType(JCallDefinition definition)
	{
		ReadOnlySpan<Byte> descriptorSpan = definition.Descriptor.AsSpan();
		Int32 offset = descriptorSpan.IndexOf(CommonNames.MethodParameterSuffixChar) + 1;
		return definition.Descriptor.AsSpan()[offset..];
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
		JLocalObject jLocal, Boolean nonVirtual,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args
#endif
	)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateHelper.GetReturnType(definition);
		if (returnType.Length != 1)
		{
			IEnvironment env = jFunction.Environment;
			JLocalObject? jObject =
				env.AccessFeature.CallFunction<JLocalObject>(jFunction, jLocal, definition, nonVirtual, args);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JBoolean>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JByte>(bytes, definition, jFunction, jLocal,
				                                                          nonVirtual, args);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JChar>(bytes, definition, jFunction, jLocal,
				                                                          nonVirtual, args);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JDouble>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JFloat>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JInt>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JLong>(bytes, definition, jFunction, jLocal,
				                                                          nonVirtual, args);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateHelper.ReflectedPrimitiveFunctionCall<JShort>(
					bytes, definition, jFunction, jLocal, nonVirtual, args);
				break;
		}
		return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], returnType);
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
		JMethodObject jFunction,
#if NET9_0_OR_GREATER
		params ReadOnlySpan<IObject?> args
#else
		ReadOnlySpan<IObject?> args
#endif
	)
	{
		ReadOnlySpan<Byte> returnType = IndeterminateHelper.GetReturnType(definition);
		if (returnType.Length != 1)
		{
			IEnvironment env = jFunction.Environment;
			JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject>(jFunction, definition, args);
			return new(jObject, returnType);
		}

		Span<Byte> bytes = stackalloc Byte[JValue.PrimitiveSize];
		switch (returnType[0])
		{
			case CommonNames.BooleanSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JBoolean>(bytes, definition, jFunction, args);
				break;
			case CommonNames.ByteSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JByte>(bytes, definition, jFunction, args);
				break;
			case CommonNames.CharSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JChar>(bytes, definition, jFunction, args);
				break;
			case CommonNames.DoubleSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JDouble>(bytes, definition, jFunction, args);
				break;
			case CommonNames.FloatSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JFloat>(bytes, definition, jFunction, args);
				break;
			case CommonNames.IntSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JInt>(bytes, definition, jFunction, args);
				break;
			case CommonNames.LongSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JLong>(bytes, definition, jFunction, args);
				break;
			case CommonNames.ShortSignatureChar:
				IndeterminateHelper.ReflectedStaticPrimitiveFunctionCall<JShort>(bytes, definition, jFunction, args);
				break;
		}
		return new(MemoryMarshal.Cast<Byte, JValue.PrimitiveValue>(bytes)[0], returnType);
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
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	private static TObject ReflectedNewCall<TObject>(JConstructorDefinition definition, JConstructorObject jConstructor,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jConstructor, definition, args);
	}
	/// <summary>
	/// Invokes a primitive function on given <see cref="JLocalObject"/> instance and copies its result to
	/// <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected function object.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	private static void ReflectedPrimitiveFunctionCall<TPrimitive>(Span<Byte> bytes, JFunctionDefinition definition,
		JMethodObject jMethod, JLocalObject jLocal, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jMethod.Environment;
		TPrimitive result = env.AccessFeature.CallFunction<TPrimitive>(jMethod, jLocal, definition, nonVirtual, args);
		result.CopyTo(bytes);
	}
	/// <summary>
	/// Invokes a primitive static function on the declaring class of given <see cref="JMethodObject"/> instance and copies its
	/// result to <paramref name="bytes"/>.
	/// </summary>
	/// <param name="bytes">Buffer to hold primitive result.</param>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jMethod">Reflected function object.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private static void ReflectedStaticPrimitiveFunctionCall<TPrimitive>(Span<Byte> bytes,
		JFunctionDefinition definition, JMethodObject jMethod, ReadOnlySpan<IObject?> args)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		IEnvironment env = jMethod.Environment;
		TPrimitive result = env.AccessFeature.CallStaticFunction<TPrimitive>(jMethod, definition, args);
		result.CopyTo(bytes);
	}
}