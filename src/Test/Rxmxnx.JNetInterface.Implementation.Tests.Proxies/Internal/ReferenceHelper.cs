namespace Rxmxnx.JNetInterface.Tests.Internal;

internal static partial class ReferenceHelper
{
	public static readonly IFixture Fixture = new Fixture().RegisterReferences();
	public static readonly ConcurrentDictionary<JEnvironmentRef, NativeInterfaceProxy> NativeProxies = new();
	public static readonly ConcurrentDictionary<JVirtualMachineRef, InvokeInterfaceProxy> InvokeProxies = new();

	public static unsafe Boolean IsClassName<TDataType>(Byte* className) where TDataType : IDataType<TDataType>
	{
		fixed (Byte* dataTypeName = TDataType.Metadata.ClassName)
			return className == dataTypeName;
	}
	public static unsafe Boolean IsTypeField(Byte* fieldName)
	{
		fixed (Byte* primitiveTypeName = NativeFunctionSetImpl.PrimitiveTypeDefinition.Name)
			return fieldName == primitiveTypeName;
	}
	public static unsafe Boolean IsGetNameMethod(Byte* methodName)
	{
		fixed (Byte* getNameName = NativeFunctionSetImpl.GetNameDefinition.Name)
			return methodName == getNameName;
	}
	public static unsafe Boolean IsIsPrimitiveMethod(Byte* methodName)
	{
		fixed (Byte* isPrimitiveName = NativeFunctionSetImpl.IsPrimitiveDefinition.Name)
			return methodName == isPrimitiveName;
	}
	public static unsafe Boolean IsGetModifiersMethod(Byte* methodName)
	{
		fixed (Byte* getModifiersName = NativeFunctionSetImpl.GetModifiersDefinition.Name)
			return methodName == getModifiersName;
	}
	public static unsafe Boolean IsGetInterfacesMethod(Byte* methodName)
	{
		fixed (Byte* getInterfacesName = NativeFunctionSetImpl.GetInterfacesDefinition.Name)
			return methodName == getInterfacesName;
	}
	public static unsafe Boolean IsGetMessageMethod(Byte* methodName)
	{
		fixed (Byte* getMessageName = NativeFunctionSetImpl.GetMessageDefinition.Name)
			return methodName == getMessageName;
	}
	public static unsafe Boolean IsGetStackTraceMethod(Byte* methodName)
	{
		fixed (Byte* getStackTraceName = NativeFunctionSetImpl.GetStackTraceDefinition.Name)
			return methodName == getStackTraceName;
	}
	public static unsafe Boolean IsGetClassNameMethod(Byte* methodName)
	{
		fixed (Byte* getClassNameName = NativeFunctionSetImpl.GetClassNameDefinition.Name)
			return methodName == getClassNameName;
	}
	public static unsafe Boolean IsGetLineNumberMethod(Byte* methodName)
	{
		fixed (Byte* getLineNumberName = NativeFunctionSetImpl.GetLineNumberDefinition.Name)
			return methodName == getLineNumberName;
	}
	public static unsafe Boolean IsGetFileNameMethod(Byte* methodName)
	{
		fixed (Byte* getFileNameName = NativeFunctionSetImpl.GetFileNameDefinition.Name)
			return methodName == getFileNameName;
	}
	public static unsafe Boolean IsGetMethodNameMethod(Byte* methodName)
	{
		fixed (Byte* getMethodNameName = NativeFunctionSetImpl.GetMethodNameDefinition.Name)
			return methodName == getMethodNameName;
	}
	public static unsafe Boolean IsIsNativeMethodMethod(Byte* methodName)
	{
		fixed (Byte* isNativeMethodName = NativeFunctionSetImpl.IsNativeMethodDefinition.Name)
			return methodName == isNativeMethodName;
	}
}