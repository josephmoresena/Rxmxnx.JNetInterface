namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class InternalFunctionCache : FunctionCache
{
	/// <inheritdoc/>
	public override JStringObject GetName(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassProvider.EnumClassObject;
		return JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.nameDefinition, jEnum, enumClass)!;
	}
	/// <inheritdoc/>
	public override Int32 GetOrdinal(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassProvider.EnumClassObject;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessProvider.CallPrimitiveFunction(bytes, jEnum, enumClass, InternalFunctionCache.ordinalDefinition,
		                                         false, Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.getClassDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[sizeof(Int32)];
		env.AccessProvider.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                         InternalFunctionCache.getLineNumberDefinition, false,
		                                         Array.Empty<IObject>());
		return bytes.AsValue<Int32>();
	}
	/// <inheritdoc/>
	public override JStringObject GetFileName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.getFileNameDefinition, jStackTraceElement)!;
	/// <inheritdoc/>
	public override JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement)
		=> JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.getMethodNameDefinition,
		                                             jStackTraceElement)!;
	/// <inheritdoc/>
	public override Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		Span<Byte> bytes = stackalloc Byte[1];
		env.AccessProvider.CallPrimitiveFunction(bytes, jStackTraceElement, jStackTraceElement.Class,
		                                         InternalFunctionCache.isNativeMethodDefinition, false,
		                                         Array.Empty<IObject>());
		return bytes[0] == JBoolean.TrueValue;
	}

	/// <inheritdoc/>
	public override TPrimitive GetPrimitiveValue<TPrimitive>(JNumberObject jNumber)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IEnvironment env = jNumber.Environment;
		JClassObject numberClass = env.ClassProvider.NumberClassObject;
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
		env.AccessProvider.CallPrimitiveFunction(bytes, jNumber, numberClass, functionDefinition, false,
		                                         Array.Empty<IObject>());
		return bytes.AsValue<TPrimitive>();
	}

	/// <inheritdoc/>
	public override JStringObject GetMessage(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassProvider.ThrowableObject;
		return JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.getMessageDefinition, jThrowable,
		                                                 throwableClass)!;
	}
	/// <inheritdoc/>
	public override JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassProvider.ThrowableObject;
		return JFunctionDefinition<JArrayObject<JStackTraceElementObject>>.Invoke(
			InternalFunctionCache.getStackTraceDefinition, jThrowable, throwableClass)!;
	}
	/// <inheritdoc/>
	public override JStringObject GetClassName(JClassObject jClass)
		=> JFunctionDefinition<JStringObject>.Invoke(InternalFunctionCache.getName, jClass)!;
}