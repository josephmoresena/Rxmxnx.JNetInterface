namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class InternalFunctionCache : FunctionCache
{
	/// <summary>
	/// Internal function cache.
	/// </summary>
	public static readonly InternalFunctionCache Instance = new();

	/// <summary>
	/// Private constructor.
	/// </summary>
	private InternalFunctionCache() { }

	/// <inheritdoc/>
	public override JStringObject GetName(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumClassObject;
		return JFunctionDefinition.Invoke(InternalFunctionCache.nameDefinition, jEnum, enumClass)!;
	}
	/// <inheritdoc/>
	public override Int32 GetOrdinal(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumClassObject;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jEnum, enumClass, InternalFunctionCache.ordinalDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(InternalFunctionCache.getClassDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                        InternalFunctionCache.getLineNumberDefinition, false,
		                                        Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}
	/// <inheritdoc/>
	public override JStringObject GetFileName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(InternalFunctionCache.getFileNameDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition.Invoke(InternalFunctionCache.getMethodNameDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                        InternalFunctionCache.isNativeMethodDefinition, false,
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
			JNativeType.JByte => InternalFunctionCache.byteValueDefinition,
			JNativeType.JShort => InternalFunctionCache.shortValueDefinition,
			JNativeType.JInt => InternalFunctionCache.intValueDefinition,
			JNativeType.JLong => InternalFunctionCache.longValueDefinition,
			JNativeType.JFloat => InternalFunctionCache.floatValueDefinition,
			_ => InternalFunctionCache.doubleValueDefinition,
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
		return JFunctionDefinition.Invoke(InternalFunctionCache.getMessageDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		return JFunctionDefinition.Invoke(InternalFunctionCache.getStackTraceDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override JStringObject GetClassName(JClassObject jClass)
		=> JFunctionDefinition.Invoke(InternalFunctionCache.getName, jClass)!;
	/// <inheritdoc/>
	public override Boolean IsPrimitiveClass(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jClass, jClass.Class, InternalFunctionCache.isPrimitiveClass,
		                                        false, Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Boolean IsDirectBuffer(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessFeature.CallPrimitiveFunction(bytes, jBuffer, env.ClassFeature.BufferClassObject,
		                                        InternalFunctionCache.isDirectBuffer, false, Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}
	/// <inheritdoc/>
	public override Int64 BufferCapacity(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int64)];
		env.AccessFeature.CallPrimitiveFunction(bytes, jBuffer, env.ClassFeature.BufferClassObject,
		                                        InternalFunctionCache.bufferCapacity, false, Array.Empty<IObject>());
		return bytes.AsValue<Int64>();
	}
	/// <inheritdoc/>
	public override JStringObject GetName<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(InternalFunctionCache.getName, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override JClassObject GetDeclaringClass<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		return JFunctionDefinition.Invoke(InternalFunctionCache.getDeclaringClass, jMember, memberInterface)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetParameterTypes(JExecutableObject jExecutable)
	{
		IEnvironment env = jExecutable.Environment;
		JClassObject executableClass = env.ClassFeature.GetClass<JExecutableObject>();
		return JFunctionDefinition.Invoke(InternalFunctionCache.getParameterTypes, jExecutable, executableClass)!;
	}
	/// <inheritdoc/>
	public override JClassObject? GetReturnType(JExecutableObject jMethod)
	{
		IEnvironment env = jMethod.Environment;
		JClassObject methodClass = env.ClassFeature.GetClass<JMemberObject>();
		return jMethod is JMethodObject || jMethod.InstanceOf<JMethodObject>() ?
			JFunctionDefinition.Invoke(InternalFunctionCache.getReturnType, jMethod, methodClass) :
			default;
	}
}