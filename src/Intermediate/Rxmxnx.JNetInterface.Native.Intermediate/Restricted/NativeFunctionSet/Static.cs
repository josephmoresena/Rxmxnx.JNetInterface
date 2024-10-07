namespace Rxmxnx.JNetInterface.Restricted;

public abstract partial class NativeFunctionSet
{
	/// <summary>
	/// Information for <c>java.lang.String name()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence NameFunctionInfo =
		new(JAccessibleObjectDefinition.NameFunctionHash, 4, 20);
	/// <summary>
	/// Information for <c>int ordinal()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence OrdinalFunctionInfo =
		new(JAccessibleObjectDefinition.OrdinalFunctionHash, 7, 3);
	/// <summary>
	/// Information for <c>java.lang.String getClassName()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetClassNameFunctionInfo =
		new(JAccessibleObjectDefinition.GetClassNameFunctionHash, 12, 20);
	/// <summary>
	/// Information for <c>int getLineNumber()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetLineNumberFunctionInfo =
		new(JAccessibleObjectDefinition.GetLineNumberFunctionHash, 13, 3);
	/// <summary>
	/// Information for <c>java.lang.String getFileName()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetFileNameFunctionInfo =
		new(JAccessibleObjectDefinition.GetFileNameFunctionHash, 11, 20);
	/// <summary>
	/// Information for <c>java.lang.String getMethodName()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetMethodNameFunctionInfo =
		new(JAccessibleObjectDefinition.GetMethodNameFunctionHash, 13, 20);
	/// <summary>
	/// Information for <c>boolean isNativeMethod()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence IsNativeMethodFunctionInfo =
		new(JAccessibleObjectDefinition.IsNativeMethodFunctionHash, 14, 3);
	/// <summary>
	/// Information for <c>byte byteValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence ByteValueFunctionInfo =
		new(JAccessibleObjectDefinition.ByteValueFunctionHash, 9, 3);
	/// <summary>
	/// Information for <c>double doubleValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence DoubleValueFunctionInfo =
		new(JAccessibleObjectDefinition.DoubleValueFunctionHash, 11, 3);
	/// <summary>
	/// Information for <c>short shortValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence ShortValueFunctionInfo =
		new(JAccessibleObjectDefinition.ShortValueFunctionHash, 10, 3);
	/// <summary>
	/// Information for <c>int intValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence IntValueFunctionInfo =
		new(JAccessibleObjectDefinition.IntValueFunctionHash, 8, 3);
	/// <summary>
	/// Information for <c>long longValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence LongValueFunctionInfo =
		new(JAccessibleObjectDefinition.LongValueFunctionHash, 9, 3);
	/// <summary>
	/// Information for <c>float floatValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence FloatValueFunctionInfo =
		new(JAccessibleObjectDefinition.FloatValueFunctionHash, 10, 3);
	/// <summary>
	/// Information for <c>java.lang.StackTraceElement[] getStackTrace()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetStackTraceFunctionInfo =
		new(JAccessibleObjectDefinition.GetStackTraceFunctionHash, 13, 32);
	/// <summary>
	/// Information for <c>int getModifiers()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetModifiersFunctionInfo =
		new(JAccessibleObjectDefinition.GetModifiersFunctionHash, 12, 3);
	/// <summary>
	/// Information for <c>java.lang.Class[] getInterfaces()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetInterfacesFunctionInfo =
		new(JAccessibleObjectDefinition.GetInterfacesFunctionHash, 13, 20);
	/// <summary>
	/// Information for <c>boolean isDirect()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence IsDirectBufferFunctionInfo =
		new(JAccessibleObjectDefinition.IsDirectBufferFunctionHash, 8, 3);
	/// <summary>
	/// Information for <c>long capacity()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence BufferCapacityFunctionInfo =
		new(JAccessibleObjectDefinition.BufferCapacityFunctionHash, 8, 3);
	/// <summary>
	/// Information for <c>java.lang.Class getDeclaringClass()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetDeclaringClassFunctionInfo =
		new(JAccessibleObjectDefinition.GetDeclaringClassFunctionHash, 17, 19);
	/// <summary>
	/// Information for <c>java.lang.Class[] getParameterTypes()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetParameterTypesFunctionInfo =
		new(JAccessibleObjectDefinition.GetParameterTypesFunctionHash, 17, 20);
	/// <summary>
	/// Information for <c>java.lang.Class getReturnType()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetReturnTypeFunctionInfo =
		new(JAccessibleObjectDefinition.GetReturnTypeFunctionHash, 13, 19);
	/// <summary>
	/// Information for <c>java.lang.Class getType()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetTypeFunctionInfo =
		new(JAccessibleObjectDefinition.GetTypeFunctionHash, 7, 19);
	/// <summary>
	/// Information for <c>java.lang.String getName()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetNameFunctionInfo =
		new(JAccessibleObjectDefinition.GetNameFunctionHash, 7, 20);
	/// <summary>
	/// Information for <c>boolean isPrimitive()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence IsPrimitiveFunctionInfo =
		new(JAccessibleObjectDefinition.IsPrimitiveFunctionHash, 11, 3);
	/// <summary>
	/// Information for <c>java.lang.String getMessage()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence GetMessageFunctionInfo =
		new(JAccessibleObjectDefinition.GetMessageFunctionHash, 10, 20);
	/// <summary>
	/// Information for <c>boolean booleanValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence BooleanValueFunctionInfo =
		new(JAccessibleObjectDefinition.BooleanValueFunctionHash, 12, 3);
	/// <summary>
	/// Information for <c>char charValue()</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence CharValueFunctionInfo =
		new(JAccessibleObjectDefinition.CharValueFunctionHash, 9, 3);
	/// <summary>
	/// Information for <c>ctor(boolean)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence BooleanConstructorInfo =
		new(JAccessibleObjectDefinition.BooleanConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(byte)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence ByteConstructorInfo =
		new(JAccessibleObjectDefinition.ByteConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(char)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence CharacterConstructorInfo =
		new(JAccessibleObjectDefinition.CharacterConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(double)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence DoubleConstructorInfo =
		new(JAccessibleObjectDefinition.DoubleConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(float)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence FloatConstructorInfo =
		new(JAccessibleObjectDefinition.FloatConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(int)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence IntegerConstructorInfo =
		new(JAccessibleObjectDefinition.IntegerConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(long)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence LongConstructorInfo =
		new(JAccessibleObjectDefinition.LongConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>ctor(short)</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence ShortConstructorInfo =
		new(JAccessibleObjectDefinition.ShortConstructorHash, 6, 4);
	/// <summary>
	/// Information for <c>java.lang.Class TYPE</c>.
	/// </summary>
	private protected static readonly AccessibleInfoSequence PrimitiveTypeFieldInfo =
		new(JAccessibleObjectDefinition.PrimitiveTypeFieldHash, 4, 17);
}