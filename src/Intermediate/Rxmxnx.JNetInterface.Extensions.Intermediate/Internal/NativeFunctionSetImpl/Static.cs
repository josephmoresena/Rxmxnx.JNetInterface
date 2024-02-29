namespace Rxmxnx.JNetInterface.Internal;

internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// <c>Enum.name()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> nameDefinition = new(UnicodeMethodNames.Name());
	/// <summary>
	/// <c>Enum.ordinal()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> ordinalDefinition = new(UnicodeMethodNames.Ordinal());
	/// <summary>
	/// <c>StackTraceElement.getClass()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getClassDefinition =
		new(UnicodeMethodNames.GetClassName());
	/// <summary>
	/// <c>StackTraceElement.getLineNumber()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> getLineNumberDefinition = new(UnicodeMethodNames.GetLineNumber());
	/// <summary>
	/// <c>StackTraceElement.getFileName()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getFileNameDefinition =
		new(UnicodeMethodNames.GetFileName());
	/// <summary>
	/// <c>StackTraceElement.getMethodName()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getMethodNameDefinition =
		new(UnicodeMethodNames.GetMethodName());
	/// <summary>
	/// <c>StackTraceElement.isNativeMethod()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JBoolean> isNativeMethodDefinition =
		new(UnicodeMethodNames.IsNativeMethod());

	/// <summary>
	/// <c>Number.byteValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JByte> byteValueDefinition = new(UnicodeMethodNames.ByteValue());
	/// <summary>
	/// <c>Number.doubleValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JByte> doubleValueDefinition = new(UnicodeMethodNames.DoubleValue());
	/// <summary>
	/// <c>Number.shortValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JShort> shortValueDefinition = new(UnicodeMethodNames.ShortValue());
	/// <summary>
	/// <c>Number.intValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> intValueDefinition = new(UnicodeMethodNames.IntValue());
	/// <summary>
	/// <c>Number.longValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JLong> longValueDefinition = new(UnicodeMethodNames.LongValue());
	/// <summary>
	/// <c>Number.floatValue()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JFloat> floatValueDefinition = new(UnicodeMethodNames.FloatValue());
	/// <summary>
	/// <c>Throwable.getMessage()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JStringObject> getMessageDefinition =
		new(UnicodeMethodNames.GetMessage());
	/// <summary>
	/// <c>Throwable.getStackTrace()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JStackTraceElementObject>> getStackTraceDefinition =
		new(UnicodeMethodNames.GetStackTrace());

	/// <summary>
	/// <c>Class.getModifiers()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JInt> getModifiersDefinition = new(UnicodeMethodNames.GetModifiers());
	/// <summary>
	/// <c>Class.getComponentType()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getComponentTypeDefinition =
		new(UnicodeMethodNames.GetComponentType());
	/// <summary>
	/// <c>Class.getInterfaces()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JClassObject>> getInterfacesDefinition =
		new(UnicodeMethodNames.GetInterfaces());

	/// <summary>
	/// <c>Buffer.isDirect()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JBoolean> isDirectBuffer = new(UnicodeMethodNames.IsDirect());
	/// <summary>
	/// <c>Buffer.capacity()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JLong> bufferCapacity = new(UnicodeMethodNames.Capacity());

	/// <summary>
	/// <c>Member.getDeclaringClass()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getDeclaringClass =
		new(UnicodeMethodNames.GetDeclaringClass());

	/// <summary>
	/// <c>Executable.getParameterTypes()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JArrayObject<JClassObject>> getParameterTypes =
		new(UnicodeMethodNames.GetParameterTypes());
	/// <summary>
	/// <c>Method.getReturnType()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getReturnType = new(UnicodeMethodNames.GetReturnType());
	/// <summary>
	/// <c>Field.getParameterTypes()</c> definition.
	/// </summary>
	private static readonly JFunctionDefinition<JClassObject> getType = new(UnicodeMethodNames.GetFieldType());

	/// <summary>
	/// <c>Class.getName()</c> or <c>Member.getName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject> GetNameDefinition = new(UnicodeMethodNames.GetName());
	/// <summary>
	/// <c>Class.isPrimitive()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean> IsPrimitiveDefinition = new(UnicodeMethodNames.IsPrimitive());

	/// <summary>
	/// <c>Boolean.booleanValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>
		BooleanValueDefinition = new(UnicodeMethodNames.BooleanValue());
	/// <summary>
	/// <c>Character.charValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JChar> CharValueDefinition = new(UnicodeMethodNames.CharValue());

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
	public static readonly JFieldDefinition<JClassObject> PrimitiveTypeDefinition = new(UnicodeMethodNames.TypeField());

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
		JClassObject? elementClass;
		do
			elementClass = JFunctionDefinition.Invoke(NativeFunctionSetImpl.getComponentTypeDefinition, arrayClass)!;
		while (elementClass.IsArray);
		return NativeFunctionSetImpl.GetClassModifiers(elementClass).HasFlag(JModifierObject.Modifier.Final);
	}
	/// <summary>
	/// Indicates whether a non-array class is final.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jClass"/> is final; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	private static JModifierObject.Modifier GetClassModifiers(JClassObject jClass)
	{
		if (jClass.IsPrimitive) return JModifierObject.PrimitiveModifiers;
		IEnvironment env = jClass.Environment;
		JModifierObject.Modifier result = default;
		env.AccessFeature.CallPrimitiveFunction(result.AsBytes(), jClass, jClass.Class,
		                                        NativeFunctionSetImpl.getModifiersDefinition, false,
		                                        Array.Empty<IObject>());
		return result;
	}
}