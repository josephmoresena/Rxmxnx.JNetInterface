namespace Rxmxnx.JNetInterface.Internal;

internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// <c>Enum.name()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless NameDefinition = new("name"u8);
	/// <summary>
	/// <c>Enum.ordinal()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless OrdinalDefinition = new("ordinal"u8);
	/// <summary>
	/// <c>StackTraceElement.getNameClass()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetClassNameDefinition =
		new("getClassName"u8);
	/// <summary>
	/// <c>StackTraceElement.getLineNumber()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless GetLineNumberDefinition = new("getLineNumber"u8);
	/// <summary>
	/// <c>StackTraceElement.getFileName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetFileNameDefinition =
		new("getFileName"u8);
	/// <summary>
	/// <c>StackTraceElement.getMethodName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetMethodNameDefinition =
		new("getMethodName"u8);
	/// <summary>
	/// <c>StackTraceElement.isNativeMethod()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsNativeMethodDefinition =
		new("isNativeMethod"u8);

	/// <summary>
	/// <c>Number.byteValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JByte>.Parameterless ByteValueDefinition = new("byteValue"u8);
	/// <summary>
	/// <c>Number.doubleValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JDouble>.Parameterless DoubleValueDefinition = new("doubleValue"u8);
	/// <summary>
	/// <c>Number.shortValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JShort>.Parameterless ShortValueDefinition = new("shortValue"u8);
	/// <summary>
	/// <c>Number.intValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless IntValueDefinition = new("intValue"u8);
	/// <summary>
	/// <c>Number.longValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JLong>.Parameterless LongValueDefinition = new("longValue"u8);
	/// <summary>
	/// <c>Number.floatValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JFloat>.Parameterless FloatValueDefinition = new("floatValue"u8);
	/// <summary>
	/// <c>Throwable.getStackTrace()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JStackTraceElementObject>>.Parameterless
		GetStackTraceDefinition = new("getStackTrace"u8);

	/// <summary>
	/// <c>Class.getModifiers()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless GetModifiersDefinition = new("getModifiers"u8);
	/// <summary>
	/// <c>Class.getInterfaces()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless GetInterfacesDefinition =
		new("getInterfaces"u8);

	/// <summary>
	/// <c>Buffer.isDirect()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsDirectBufferDefinition = new("isDirect"u8);
	/// <summary>
	/// <c>Buffer.capacity()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JLong>.Parameterless BufferCapacityDefinition = new("capacity"u8);

	/// <summary>
	/// <c>Member.getDeclaringClass()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetDeclaringClassDefinition =
		new("getDeclaringClass"u8);

	/// <summary>
	/// <c>Executable.getParameterTypes()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless GetParameterTypesDefinition =
		new("getParameterTypes"u8);
	/// <summary>
	/// <c>Method.getReturnType()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetReturnTypeDefinition =
		new("getReturnType"u8);
	/// <summary>
	/// <c>Field.getType()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetTypeDefinition = new("getType"u8);

	/// <summary>
	/// <c>Class.getName()</c> or <c>Member.getName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetNameDefinition = new("getName"u8);
	/// <summary>
	/// <c>Class.isPrimitive()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsPrimitiveDefinition = new("isPrimitive"u8);

	/// <summary>
	/// <c>Throwable.getMessage()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetMessageDefinition = new("getMessage"u8);

	/// <summary>
	/// <c>Boolean.booleanValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless BooleanValueDefinition = new("booleanValue"u8);
	/// <summary>
	/// <c>Character.charValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JChar>.Parameterless CharValueDefinition = new("charValue"u8);

	/// <summary>
	/// Constructor <c>java.lang.Boolean(boolean)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JBoolean> BooleanConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Byte(byte)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JByte> ByteConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Character(char)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JChar> CharacterConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Double(double)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JDouble> DoubleConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Float(float)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JFloat> FloatConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Integer(int)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JInt> IntegerConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Long(long)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JLong> LongConstructor = new();
	/// <summary>
	/// Constructor <c>java.lang.Short(short)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JShort> ShortConstructor = new();

	/// <summary>
	/// The class instance representing the primitive type in wrapper classes.
	/// </summary>
	public static readonly JFieldDefinition<JClassObject> PrimitiveTypeDefinition = new("TYPE"u8);

	/// <summary>
	/// Indicates whether an array class is final.
	/// </summary>
	/// <param name="arrayClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="arrayClass"/> is final; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	private static Boolean IsFinalArrayType(JClassObject arrayClass)
	{
		Int32 dimension = arrayClass.ArrayDimension;
		if (dimension + 1 == arrayClass.ClassSignature.Length) return true;
		IEnvironment env = arrayClass.Environment;
		JClassObject elementClass = env.ClassFeature.GetClass(arrayClass.ClassSignature.AsSpan()[(dimension + 1)..^1]);
		return elementClass.IsFinal;
	}
	/// <summary>
	/// Indicates whether a non-array class is final.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jClass"/> is final; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	private static JModifierObject.Modifiers GetClassModifiers(JClassObject jClass)
	{
		if (jClass.ArrayDimension + 1 == jClass.ClassSignature.Length) return JModifierObject.PrimitiveModifiers;
		IEnvironment env = jClass.Environment;
		JClassObject classClass = env.ClassFeature.ClassObject;
		JModifierObject.Modifiers result = default;
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jClass, classClass,
		                                        NativeFunctionSetImpl.GetModifiersDefinition, false, []);
		return result;
	}
}