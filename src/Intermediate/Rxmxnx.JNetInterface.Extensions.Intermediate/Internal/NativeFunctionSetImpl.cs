namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class NativeFunctionSetImpl : NativeFunctionSet
{
	/// <summary>
	/// Internal function cache.
	/// </summary>
	public static readonly NativeFunctionSetImpl Instance = new();

	/// <summary>
	/// Private constructor.
	/// </summary>
	private NativeFunctionSetImpl() { }

	/// <inheritdoc/>
	public override JStringObject GetName(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.NameDefinition, jEnum, enumClass)!;
	}
	/// <inheritdoc/>
	public override JStringObject GetName<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetNameDefinition, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override Int32 GetOrdinal(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumObject;
		Span<Int32> result = stackalloc Int32[1];
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jEnum, enumClass,
		                                        NativeFunctionSetImpl.OrdinalDefinition, false);
		return result[0];
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetNameDefinition, jClass, classClass)!;
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetClassNameDefinition, jStackTraceElement,
		                                  stackTraceElementClass)!;
	}
	/// <inheritdoc/>
	public override Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Int32> result = stackalloc Int32[1];
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jStackTraceElement, stackTraceElementClass,
		                                        NativeFunctionSetImpl.GetLineNumberDefinition, false);
		return result[0];
	}
	/// <inheritdoc/>
	public override JStringObject? GetFileName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetFileNameDefinition, jStackTraceElement,
		                                  stackTraceElementClass);
	}
	/// <inheritdoc/>
	public override JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetMethodNameDefinition, jStackTraceElement,
		                                  stackTraceElementClass)!;
	}
	/// <inheritdoc/>
	public override Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		env.AccessFeature.CallPrimitiveFunction(bytes, jStackTraceElement, stackTraceElementClass,
		                                        NativeFunctionSetImpl.IsNativeMethodDefinition, false);
		return bytes[0] == JBoolean.TrueValue;
	}

	/// <inheritdoc/>
	public override TPrimitive GetPrimitiveValue<TPrimitive>(JNumberObject jNumber)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IEnvironment env = jNumber.Environment;
		JClassObject numberClass = env.ClassFeature.NumberObject;
		JFunctionDefinition functionDefinition = metadata.NativeType switch
		{
			JNativeType.JByte => NativeFunctionSetImpl.ByteValueDefinition,
			JNativeType.JShort => NativeFunctionSetImpl.ShortValueDefinition,
			JNativeType.JInt => NativeFunctionSetImpl.IntValueDefinition,
			JNativeType.JLong => NativeFunctionSetImpl.LongValueDefinition,
			JNativeType.JFloat => NativeFunctionSetImpl.FloatValueDefinition,
			_ => NativeFunctionSetImpl.DoubleValueDefinition,
		};
		Span<TPrimitive> result = stackalloc TPrimitive[1];
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jNumber, numberClass, functionDefinition, false);
		return result[0];
	}

	/// <inheritdoc/>
	public override JStringObject GetMessage(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetMessageDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetStackTraceDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override Boolean IsPrimitiveClass(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		JClassObject classClass = env.ClassFeature.ClassObject;
		env.AccessFeature.CallPrimitiveFunction(bytes, jClass, classClass, NativeFunctionSetImpl.IsPrimitiveDefinition,
		                                        false);
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Boolean IsFinal(JClassObject jClass, out JModifierObject.Modifiers modifiers)
	{
		IEnvironment env = jClass.Environment;
		modifiers = NativeFunctionSetImpl.GetClassModifiers(jClass);
		return jClass.IsArray ?
			env.WithFrame(IVirtualMachine.IsFinalArrayCapacity, jClass, NativeFunctionSetImpl.IsFinalArrayType) :
			modifiers.HasFlag(JModifierObject.Modifiers.Final);
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetInterfaces(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetInterfacesDefinition, jClass, classClass)!;
	}

	/// <inheritdoc/>
	public override Boolean IsDirectBuffer(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jBuffer, env.ClassFeature.BufferObject,
		                                        NativeFunctionSetImpl.IsDirectBufferDefinition, false);
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Int64 BufferCapacity(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Int64> result = stackalloc Int64[1];
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jBuffer, env.ClassFeature.BufferObject,
		                                        NativeFunctionSetImpl.BufferCapacityDefinition, false);
		return result[0];
	}
	/// <inheritdoc/>
	public override JClassObject GetDeclaringClass<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetDeclaringClassDefinition, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetParameterTypes(JExecutableObject jExecutable)
	{
		IEnvironment env = jExecutable.Environment;
		Boolean useExecutableClass = IReferenceType.GetMetadata<JExecutableObject>().IsCompatibleWith(env);
		JClassObject jClass = useExecutableClass ? env.ClassFeature.GetClass<JExecutableObject>() : jExecutable.Class;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetParameterTypesDefinition, jExecutable, jClass)!;
	}
	/// <inheritdoc/>
	public override JClassObject? GetReturnType(JExecutableObject jMethod)
	{
		if (jMethod is not JMethodObject && (jMethod is JConstructorObject || !jMethod.InstanceOf<JMethodObject>()))
			return default;

		IEnvironment env = jMethod.Environment;
		JClassObject methodClass = env.ClassFeature.MethodObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetReturnTypeDefinition, jMethod, methodClass);
	}
	/// <inheritdoc/>
	public override JClassObject GetFieldType(JFieldObject jField)
	{
		IEnvironment env = jField.Environment;
		JClassObject fieldClass = env.ClassFeature.FieldObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.GetTypeDefinition, jField, fieldClass)!;
	}
#if !PACKAGE
	/// <summary>
	/// Retrieves the <see cref="JBoolean"/> value of <paramref name="jBooleanObject"/>.
	/// </summary>
	/// <param name="jBooleanObject">A <see cref="JBooleanObject"/> instance.</param>
	/// <returns>A <see cref="JBoolean"/> value.</returns>
	public static JBoolean GetValue(JBooleanObject jBooleanObject)
#else
	/// <inheritdoc/>
	public override JBoolean GetValue(JBooleanObject jBooleanObject)
#endif
	{
		IEnvironment env = jBooleanObject.Environment;
		JClassObject jClass = env.ClassFeature.BooleanObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.BooleanValueDefinition, jBooleanObject, jClass);
	}
#if !PACKAGE
	/// <summary>
	/// Retrieves the <see cref="JChar"/> value of <paramref name="jCharacterObject"/>.
	/// </summary>
	/// <param name="jCharacterObject">A <see cref="JCharacterObject"/> instance.</param>
	/// <returns>A <see cref="JChar"/> value.</returns>
	public static JChar GetValue(JCharacterObject jCharacterObject)
#else
	/// <inheritdoc/>
	public override JChar GetValue(JCharacterObject jCharacterObject)
#endif
	{
		IEnvironment env = jCharacterObject.Environment;
		JClassObject jClass = env.ClassFeature.CharacterObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.CharValueDefinition, jCharacterObject, jClass);
	}
	/// <inheritdoc/>
	public override JStringObject? GetProperty(JStringObject jString)
	{
		IEnvironment env = jString.Environment;
		using JClassObject jClass = JClassObject.GetClass<JSystemObject>(env);
		return JFunctionDefinition.StaticInvoke(NativeFunctionSetImpl.GetPropertyDefinition, jClass, [jString,]);
	}
	/// <inheritdoc/>
	public override void SetProperty(JStringObject jStringKey, JStringObject? jStringValue)
	{
		IEnvironment env = jStringKey.Environment;
		using JClassObject jClass = JClassObject.GetClass<JSystemObject>(env);
		JMethodDefinition.StaticInvoke(NativeFunctionSetImpl.SetPropertyDefinition, jClass,
		                               [jStringKey, jStringValue,]);
	}
}