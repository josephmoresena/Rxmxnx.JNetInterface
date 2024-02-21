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
		JClassObject enumClass = env.ClassFeature.EnumClassObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.nameDefinition, jEnum, enumClass)!;
	}
	/// <inheritdoc/>
	public override JStringObject GetName<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getName, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override Int32 GetOrdinal(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumClassObject;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jEnum, enumClass, NativeFunctionSetImpl.ordinalDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(NativeFunctionSetImpl.getClassDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override JStringObject GetClassName(JClassObject jClass)
		=> JFunctionDefinition.Invoke(NativeFunctionSetImpl.getName, jClass)!;
	/// <inheritdoc/>
	public override Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                        NativeFunctionSetImpl.getLineNumberDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}
	/// <inheritdoc/>
	public override JStringObject GetFileName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(NativeFunctionSetImpl.getFileNameDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(NativeFunctionSetImpl.getMethodNameDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                        NativeFunctionSetImpl.isNativeMethodDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}

	/// <inheritdoc/>
	public override TPrimitive GetPrimitiveValue<TPrimitive>(JNumberObject jNumber)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IEnvironment env = jNumber.Environment;
		JClassObject numberClass = env.ClassFeature.NumberClassObject;
		JFunctionDefinition functionDefinition = metadata.NativeType switch
		{
			JNativeType.JByte => NativeFunctionSetImpl.byteValueDefinition,
			JNativeType.JShort => NativeFunctionSetImpl.shortValueDefinition,
			JNativeType.JInt => NativeFunctionSetImpl.intValueDefinition,
			JNativeType.JLong => NativeFunctionSetImpl.longValueDefinition,
			JNativeType.JFloat => NativeFunctionSetImpl.floatValueDefinition,
			_ => NativeFunctionSetImpl.doubleValueDefinition,
		};
		Span<Byte> bytes = stackalloc Byte[metadata.SizeOf];
		env.AccessFeature.CallPrimitiveFunction(bytes, jNumber, numberClass, functionDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes.AsValue<TPrimitive>();
	}

	/// <inheritdoc/>
	public override JStringObject GetMessage(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getMessageDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getStackTraceDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override Boolean IsPrimitiveClass(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jClass, jClass.Class, NativeFunctionSetImpl.isPrimitiveClass,
		                                        false, Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Boolean IsDirectBuffer(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jBuffer, env.ClassFeature.BufferClassObject,
		                                        NativeFunctionSetImpl.isDirectBuffer, false, Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Int64 BufferCapacity(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jBuffer, env.ClassFeature.BufferClassObject,
		                                        NativeFunctionSetImpl.bufferCapacity, false, Array.Empty<IObject>());
		return bytes.AsValue<Int64>();
	}
	/// <inheritdoc/>
	public override JClassObject GetDeclaringClass<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getDeclaringClass, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetParameterTypes(JExecutableObject jExecutable)
	{
		IEnvironment env = jExecutable.Environment;
		JClassObject executableClass = env.ClassFeature.GetClass<JExecutableObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getParameterTypes, jExecutable, executableClass)!;
	}
	/// <inheritdoc/>
	public override JClassObject? GetReturnType(JExecutableObject jMethod)
	{
		IEnvironment env = jMethod.Environment;
		JClassObject methodClass = env.ClassFeature.GetClass<JMethodObject>();
		return jMethod is JMethodObject || jMethod.InstanceOf<JMethodObject>() ?
			JFunctionDefinition.Invoke(NativeFunctionSetImpl.getReturnType, jMethod, methodClass) :
			default;
	}
	/// <inheritdoc/>
	public override JClassObject GetFieldType(JFieldObject jField)
	{
		IEnvironment env = jField.Environment;
		JClassObject methodClass = env.ClassFeature.GetClass<JFieldObject>();
		return JFunctionDefinition.Invoke(NativeFunctionSetImpl.getType, jField, methodClass)!;
	}
}