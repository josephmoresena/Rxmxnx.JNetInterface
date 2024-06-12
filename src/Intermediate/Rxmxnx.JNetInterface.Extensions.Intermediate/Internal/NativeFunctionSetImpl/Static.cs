namespace Rxmxnx.JNetInterface.Internal;

internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// <c>Enum.name()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> nameDefinition = new("name"u8);
	/// <summary>
	/// <c>Enum.ordinal()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> ordinalDefinition = new("ordinal"u8);
	/// <summary>
	/// <c>StackTraceElement.getClass()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getClassDefinition = new("getClassName"u8);
	/// <summary>
	/// <c>StackTraceElement.getLineNumber()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> getLineNumberDefinition = new("getLineNumber"u8);
	/// <summary>
	/// <c>StackTraceElement.getFileName()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getFileNameDefinition = new("getFileName"u8);
	/// <summary>
	/// <c>StackTraceElement.getMethodName()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getMethodNameDefinition = new("getMethodName"u8);
	/// <summary>
	/// <c>StackTraceElement.isNativeMethod()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JBoolean> isNativeMethodDefinition = new("isNativeMethod"u8);

	/// <summary>
	/// <c>Number.byteValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JByte> byteValueDefinition = new("byteValue"u8);
	/// <summary>
	/// <c>Number.doubleValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JDouble> doubleValueDefinition = new("doubleValue"u8);
	/// <summary>
	/// <c>Number.shortValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JShort> shortValueDefinition = new("shortValue"u8);
	/// <summary>
	/// <c>Number.intValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> intValueDefinition = new("intValue"u8);
	/// <summary>
	/// <c>Number.longValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JLong> longValueDefinition = new("longValue"u8);
	/// <summary>
	/// <c>Number.floatValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JFloat> floatValueDefinition = new("floatValue"u8);
	/// <summary>
	/// <c>Throwable.getStackTrace()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JStackTraceElementObject>> getStackTraceDefinition =
		new("getStackTrace"u8);

	/// <summary>
	/// <c>Class.getModifiers()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> getModifiersDefinition = new("getModifiers"u8);
	/// <summary>
	/// <c>Class.getInterfaces()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JClassObject>> getInterfacesDefinition =
		new("getInterfaces"u8);

	/// <summary>
	/// <c>Buffer.isDirect()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JBoolean> isDirectBuffer = new("isDirect"u8);
	/// <summary>
	/// <c>Buffer.capacity()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JLong> bufferCapacity = new("capacity"u8);

	/// <summary>
	/// <c>Member.getDeclaringClass()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getDeclaringClass = new("getDeclaringClass"u8);

	/// <summary>
	/// <c>Executable.getParameterTypes()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JClassObject>> getParameterTypes =
		new("getParameterTypes"u8);
	/// <summary>
	/// <c>Method.getReturnType()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getReturnType = new("getReturnType"u8);
	/// <summary>
	/// <c>Field.getType()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getType = new("getType"u8);

	/// <summary>
	/// <c>Class.getName()</c> or <c>Member.getName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject> GetNameDefinition = new("getName"u8);
	/// <summary>
	/// <c>Class.isPrimitive()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean> IsPrimitiveDefinition = new("isPrimitive"u8);

	/// <summary>
	/// <c>Throwable.getMessage()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject> GetMessageDefinition = new("getMessage"u8);

	/// <summary>
	/// <c>Boolean.booleanValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean> BooleanValueDefinition = new("booleanValue"u8);
	/// <summary>
	/// <c>Character.charValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JChar> CharValueDefinition = new("charValue"u8);

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
		return NativeFunctionSetImpl.GetClassModifiers(elementClass).HasFlag(JModifierObject.Modifiers.Final);
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
		                                        NativeFunctionSetImpl.getModifiersDefinition, false, []);
		return result;
	}
}