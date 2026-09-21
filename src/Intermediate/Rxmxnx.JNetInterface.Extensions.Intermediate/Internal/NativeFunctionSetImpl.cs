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
		JStringObject? result = NativeFunctionSetImpl.NameDefinition.Invoke(jEnum, enumClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override JStringObject GetName<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		JStringObject? result = NativeFunctionSetImpl.GetNameDefinition.Invoke(jMember, memberInterface);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override Int32 GetOrdinal(JEnumObject jEnum)
	{
		IEnvironment env = jEnum.Environment;
		JClassObject enumClass = env.ClassFeature.EnumObject;
		return NativeFunctionSetImpl.OrdinalDefinition.Invoke(jEnum, enumClass).Value;
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		JStringObject? result = NativeFunctionSetImpl.GetNameDefinition.Invoke(jClass, classClass);
		Debug.Assert(result is not null);
		return result;
	}

	/// <inheritdoc/>
	public override JStringObject GetClassName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		JStringObject? result =
			NativeFunctionSetImpl.GetClassNameDefinition.Invoke(jStackTraceElement, stackTraceElementClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return NativeFunctionSetImpl.GetLineNumberDefinition.Invoke(jStackTraceElement, stackTraceElementClass).Value;
	}
	/// <inheritdoc/>
	public override JStringObject? GetFileName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return NativeFunctionSetImpl.GetFileNameDefinition.Invoke(jStackTraceElement, stackTraceElementClass);
	}
	/// <inheritdoc/>
	public override JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		JStringObject? result =
			NativeFunctionSetImpl.GetMethodNameDefinition.Invoke(jStackTraceElement, stackTraceElementClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement)
	{
		IEnvironment env = jStackTraceElement.Environment;
		JClassObject stackTraceElementClass = env.ClassFeature.StackTraceElementObject;
		return NativeFunctionSetImpl.IsNativeMethodDefinition.Invoke(jStackTraceElement, stackTraceElementClass).Value;
	}

	/// <inheritdoc/>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	public override unsafe TPrimitive GetPrimitiveValue<TPrimitive>(JNumberObject jNumber)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		IEnvironment env = jNumber.Environment;
		JClassObject numberClass = env.ClassFeature.NumberObject;
		JFunctionDefinition<TPrimitive>.Parameterless? functionDefinition =
			(JFunctionDefinition)(metadata.NativeType switch
			{
				JNativeType.JByte => NativeFunctionSetImpl.ByteValueDefinition,
				JNativeType.JShort => NativeFunctionSetImpl.ShortValueDefinition,
				JNativeType.JInt => NativeFunctionSetImpl.IntValueDefinition,
				JNativeType.JLong => NativeFunctionSetImpl.LongValueDefinition,
				JNativeType.JFloat => NativeFunctionSetImpl.FloatValueDefinition,
				_ => NativeFunctionSetImpl.DoubleValueDefinition,
			}) as JFunctionDefinition<TPrimitive>.Parameterless;
		Debug.Assert(functionDefinition is not null);
		return functionDefinition.Invoke(jNumber, numberClass);
	}

	/// <inheritdoc/>
	public override JStringObject GetMessage(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		JStringObject? result = NativeFunctionSetImpl.GetMessageDefinition.Invoke(jThrowable, throwableClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable)
	{
		IEnvironment env = jThrowable.Environment;
		JClassObject throwableClass = env.ClassFeature.ThrowableObject;
		JArrayObject<JStackTraceElementObject>? result =
			NativeFunctionSetImpl.GetStackTraceDefinition.Invoke(jThrowable, throwableClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override Boolean IsPrimitiveClass(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		return NativeFunctionSetImpl.IsPrimitiveDefinition.Invoke(jClass, classClass).Value;
	}
	/// <inheritdoc/>
	public override Boolean IsFinal(JClassObject jClass, out JModifierObject.Modifiers modifiers)
	{
		IEnvironment env = jClass.Environment;
		modifiers = NativeFunctionSetImpl.GetClassModifiers(jClass);
		if (!jClass.IsArray) return modifiers.HasFlag(JModifierObject.Modifiers.Final);
		IsFinalArrayTypeFunc func = new(jClass);
		func.WithFrame(env, out Boolean result);
		return result;
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetInterfaces(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		JArrayObject<JClassObject>? result = NativeFunctionSetImpl.GetInterfacesDefinition.Invoke(jClass, classClass);
		Debug.Assert(result is not null);
		return result;
	}

	/// <inheritdoc/>
	public override Boolean IsDirectBuffer(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		JClassObject bufferClass = env.ClassFeature.BufferObject;
		return NativeFunctionSetImpl.IsDirectBufferDefinition.Invoke(jBuffer, bufferClass).Value;
	}
	/// <inheritdoc/>
	public override Int64 BufferCapacity(JBufferObject jBuffer)
	{
		IEnvironment env = jBuffer.Environment;
		JClassObject jClass = env.ClassFeature.BufferObject;
		return NativeFunctionSetImpl.BufferCapacityDefinition.Invoke(jBuffer, jClass).Value;
	}
	/// <inheritdoc/>
	public override JClassObject GetDeclaringClass<TMember>(TMember jMember)
	{
		IEnvironment env = jMember.Environment;
		JClassObject memberInterface = env.ClassFeature.GetClass<JMemberObject>();
		JClassObject? result = NativeFunctionSetImpl.GetDeclaringClassDefinition.Invoke(jMember, memberInterface);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override JArrayObject<JClassObject> GetParameterTypes(JExecutableObject jExecutable)
	{
		IEnvironment env = jExecutable.Environment;
		Boolean useExecutableClass = IReferenceType.GetMetadata<JExecutableObject>().IsCompatibleWith(env);
		JClassObject jClass = useExecutableClass ? env.ClassFeature.GetClass<JExecutableObject>() : jExecutable.Class;
		JArrayObject<JClassObject>? result =
			NativeFunctionSetImpl.GetParameterTypesDefinition.Invoke(jExecutable, jClass);
		Debug.Assert(result is not null);
		return result;
	}
	/// <inheritdoc/>
	public override JClassObject? GetReturnType(JExecutableObject jMethod)
	{
		if (jMethod is not JMethodObject && (jMethod is JConstructorObject || !jMethod.InstanceOf<JMethodObject>()))
			return default;
		IEnvironment env = jMethod.Environment;
		JClassObject methodClass = env.ClassFeature.MethodObject;
		return NativeFunctionSetImpl.GetReturnTypeDefinition.Invoke(jMethod, methodClass);
	}
	/// <inheritdoc/>
	public override JClassObject GetFieldType(JFieldObject jField)
	{
		IEnvironment env = jField.Environment;
		JClassObject fieldClass = env.ClassFeature.FieldObject;
		JClassObject? result = NativeFunctionSetImpl.GetTypeDefinition.Invoke(jField, fieldClass);
		Debug.Assert(result is not null);
		return result;
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
		JClassObject booleanClass = env.ClassFeature.BooleanObject;
		return NativeFunctionSetImpl.BooleanValueDefinition.Invoke(jBooleanObject, booleanClass);
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
		JClassObject characterClass = env.ClassFeature.CharacterObject;
		return NativeFunctionSetImpl.CharValueDefinition.Invoke(jCharacterObject, characterClass);
	}
	/// <inheritdoc/>
	public override JStringObject? GetProperty(JStringObject jString)
	{
		IEnvironment env = jString.Environment;
		using JClassObject jClass = JClassObject.GetClass<JSystemObject>(env);
		ref NativeCallArgs<JStringObject> args =
			ref Unsafe.As<JStringObject, NativeCallArgs<JStringObject>>(ref jString);
		return env.AccessFeature.CallStaticFunction<JStringObject, NativeCallArgs<JStringObject>>(
			jClass, NativeFunctionSetImpl.GetPropertyDefinition, in args);
	}
	/// <inheritdoc/>
	public override void SetProperty(JStringObject jStringKey, JStringObject? jStringValue)
	{
		IEnvironment env = jStringKey.Environment;
		using JClassObject jClass = JClassObject.GetClass<JSystemObject>(env);
		NativeCallArgs<JStringObject, JStringObject> args = new(jStringKey, jStringValue);
		env.AccessFeature.CallStaticMethod(jClass, NativeFunctionSetImpl.SetPropertyDefinition, in args);
	}
}