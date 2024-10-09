namespace Rxmxnx.JNetInterface.Internal;

internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// <c>Enum.name()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless NameDefinition =
		new(NativeFunctionSet.NameFunctionInfo);
	/// <summary>
	/// <c>Enum.ordinal()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless OrdinalDefinition =
		new(NativeFunctionSet.OrdinalFunctionInfo);
	/// <summary>
	/// <c>StackTraceElement.getNameClass()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetClassNameDefinition =
		new(NativeFunctionSet.GetClassNameFunctionInfo);
	/// <summary>
	/// <c>StackTraceElement.getLineNumber()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless GetLineNumberDefinition =
		new(NativeFunctionSet.GetLineNumberFunctionInfo);
	/// <summary>
	/// <c>StackTraceElement.getFileName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetFileNameDefinition =
		new(NativeFunctionSet.GetFileNameFunctionInfo);
	/// <summary>
	/// <c>StackTraceElement.getMethodName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetMethodNameDefinition =
		new(NativeFunctionSet.GetMethodNameFunctionInfo);
	/// <summary>
	/// <c>StackTraceElement.isNativeMethod()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsNativeMethodDefinition =
		new(NativeFunctionSet.IsNativeMethodFunctionInfo);

	/// <summary>
	/// <c>Number.byteValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JByte>.Parameterless ByteValueDefinition =
		new(NativeFunctionSet.ByteValueFunctionInfo);
	/// <summary>
	/// <c>Number.doubleValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JDouble>.Parameterless DoubleValueDefinition =
		new(NativeFunctionSet.DoubleValueFunctionInfo);
	/// <summary>
	/// <c>Number.shortValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JShort>.Parameterless ShortValueDefinition =
		new(NativeFunctionSet.ShortValueFunctionInfo);
	/// <summary>
	/// <c>Number.intValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless IntValueDefinition =
		new(NativeFunctionSet.IntValueFunctionInfo);
	/// <summary>
	/// <c>Number.longValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JLong>.Parameterless LongValueDefinition =
		new(NativeFunctionSet.LongValueFunctionInfo);
	/// <summary>
	/// <c>Number.floatValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JFloat>.Parameterless FloatValueDefinition =
		new(NativeFunctionSet.FloatValueFunctionInfo);
	/// <summary>
	/// <c>Throwable.getStackTrace()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JStackTraceElementObject>>.Parameterless
		GetStackTraceDefinition = new(NativeFunctionSet.GetStackTraceFunctionInfo);

	/// <summary>
	/// <c>Class.getModifiers()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JInt>.Parameterless GetModifiersDefinition =
		new(NativeFunctionSet.GetModifiersFunctionInfo);
	/// <summary>
	/// <c>Class.getInterfaces()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless GetInterfacesDefinition =
		new(NativeFunctionSet.GetInterfacesFunctionInfo);

	/// <summary>
	/// <c>Buffer.isDirect()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsDirectBufferDefinition =
		new(NativeFunctionSet.IsDirectBufferFunctionInfo);
	/// <summary>
	/// <c>Buffer.capacity()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JLong>.Parameterless BufferCapacityDefinition =
		new(NativeFunctionSet.BufferCapacityFunctionInfo);

	/// <summary>
	/// <c>Member.getDeclaringClass()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetDeclaringClassDefinition =
		new(NativeFunctionSet.GetDeclaringClassFunctionInfo);

	/// <summary>
	/// <c>Executable.getParameterTypes()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JArrayObject<JClassObject>>.Parameterless GetParameterTypesDefinition =
		new(NativeFunctionSet.GetParameterTypesFunctionInfo);
	/// <summary>
	/// <c>Method.getReturnType()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetReturnTypeDefinition =
		new(NativeFunctionSet.GetReturnTypeFunctionInfo);
	/// <summary>
	/// <c>Field.getType()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JClassObject>.Parameterless GetTypeDefinition =
		new(NativeFunctionSet.GetTypeFunctionInfo);

	/// <summary>
	/// <c>Class.getName()</c> or <c>Member.getName()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetNameDefinition =
		new(NativeFunctionSet.GetNameFunctionInfo);
	/// <summary>
	/// <c>Class.isPrimitive()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless IsPrimitiveDefinition =
		new(NativeFunctionSet.IsPrimitiveFunctionInfo);

	/// <summary>
	/// <c>Throwable.getMessage()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JStringObject>.Parameterless GetMessageDefinition =
		new(NativeFunctionSet.GetMessageFunctionInfo);

	/// <summary>
	/// <c>Boolean.booleanValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JBoolean>.Parameterless BooleanValueDefinition =
		new(NativeFunctionSet.BooleanValueFunctionInfo);
	/// <summary>
	/// <c>Character.charValue()</c> definition.
	/// </summary>
	public static readonly JFunctionDefinition<JChar>.Parameterless CharValueDefinition =
		new(NativeFunctionSet.CharValueFunctionInfo);

	/// <summary>
	/// Constructor <c>java.lang.Boolean(boolean)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JBoolean> BooleanConstructor =
		new(NativeFunctionSet.BooleanConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Byte(byte)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JByte> ByteConstructor =
		new(NativeFunctionSet.ByteConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Character(char)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JChar> CharacterConstructor =
		new(NativeFunctionSet.CharacterConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Double(double)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JDouble> DoubleConstructor =
		new(NativeFunctionSet.DoubleConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Float(float)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JFloat> FloatConstructor =
		new(NativeFunctionSet.FloatConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Integer(int)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JInt> IntegerConstructor =
		new(NativeFunctionSet.IntegerConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Long(long)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JLong> LongConstructor =
		new(NativeFunctionSet.LongConstructorInfo);
	/// <summary>
	/// Constructor <c>java.lang.Short(short)</c>
	/// </summary>
	public static readonly PrimitiveWrapperConstructor<JShort> ShortConstructor =
		new(NativeFunctionSet.ShortConstructorInfo);

	/// <summary>
	/// The class instance representing the primitive type in wrapper classes.
	/// </summary>
	public static readonly JFieldDefinition<JClassObject> PrimitiveTypeDefinition =
		new(NativeFunctionSet.PrimitiveTypeFieldInfo);

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
		_ = env.ClassFeature.GetTypeMetadata(elementClass);
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