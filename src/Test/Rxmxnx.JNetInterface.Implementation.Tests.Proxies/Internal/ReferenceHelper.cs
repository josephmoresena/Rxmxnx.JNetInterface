namespace Rxmxnx.JNetInterface.Tests.Internal;

internal static partial class ReferenceHelper
{
	private static readonly Object invokeLock = new();
	private static readonly Object nativeLock = new();

	public static readonly IFixture Fixture = new Fixture().RegisterReferences();

	private static readonly Dictionary<JEnvironmentRef, NativeInterfaceProxy> nativeProxies = new(Byte.MaxValue);
	private static readonly Dictionary<JVirtualMachineRef, InvokeInterfaceProxy> invokeProxies = new(Byte.MaxValue);

	public static Boolean Contains(NativeInterfaceProxy proxy)
	{
		lock (ReferenceHelper.nativeLock)
			return Object.ReferenceEquals(proxy, ReferenceHelper.nativeProxies.GetValueOrDefault(proxy.Reference));
	}
	public static InvokeInterfaceProxy Initialize(InvokeInterfaceProxy proxy)
	{
		lock (ReferenceHelper.invokeLock)
			ReferenceHelper.invokeProxies[proxy.Reference] = proxy;
		return proxy;
	}
	public static NativeInterfaceProxy Initialize(NativeInterfaceProxy proxy)
	{
		lock (ReferenceHelper.nativeLock)
			ReferenceHelper.nativeProxies[proxy.Reference] = proxy;
		return proxy;
	}
	public static void FinalizeProxy(InvokeInterfaceProxy proxy)
	{
		lock (ReferenceHelper.invokeLock)
		{
			if (Object.ReferenceEquals(proxy, ReferenceHelper.invokeProxies.GetValueOrDefault(proxy.Reference)))
				ReferenceHelper.invokeProxies.Remove(proxy.Reference, out _);
			proxy.ClearReceivedCalls();
			proxy.GetEnv(Arg.Any<ValPtr<JEnvironmentRef>>(), Arg.Any<Int32>()).Returns(JResult.DetachedThreadError);
			proxy.AttachCurrentThread(Arg.Any<ValPtr<JEnvironmentRef>>(),
			                          Arg.Any<ReadOnlyValPtr<VirtualMachineArgumentValueWrapper>>())
			     .Returns(JResult.DetachedThreadError);
			ProxyFactory.Instance.InvokeMemory.Free(proxy.Reference);
		}
	}
	public static void FinalizeProxy(NativeInterfaceProxy proxy, Boolean finalizeVm)
	{
		lock (ReferenceHelper.nativeLock)
		{
			Boolean remove =
				Object.ReferenceEquals(proxy, ReferenceHelper.nativeProxies.GetValueOrDefault(proxy.Reference));
			if (remove)
				ReferenceHelper.nativeProxies.Remove(proxy.Reference, out _);
			proxy.ClearReceivedCalls();
			proxy.GetVirtualMachine(Arg.Any<ValPtr<JVirtualMachineRef>>()).Returns(JResult.DetachedThreadError);
			if (remove && finalizeVm) ReferenceHelper.FinalizeProxy(proxy.VirtualMachine);
			ProxyFactory.Instance.NativeMemory.Free(proxy.Reference);
		}
	}

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

	private static InvokeInterfaceProxy GetProxy(JVirtualMachineRef vmRef)
	{
		IntPtr ptr = ReferenceHelper.InvokeInterface.AsSpan().GetUnsafeIntPtr();
		lock (ReferenceHelper.invokeLock)
		{
			InvokeInterfaceProxy? result = ReferenceHelper.invokeProxies.GetValueOrDefault(vmRef);
			if (result is not null && result.Reference.Reference.Pointer != ptr) result = default;
			if (result?.AllowedThread != null && result.AllowedThread != Environment.CurrentManagedThreadId)
				result = default;
			return result ?? InvokeInterfaceProxy.Detached;
		}
	}
	private static NativeInterfaceProxy GetProxy(JEnvironmentRef envRef)
	{
		IntPtr ptr = ReferenceHelper.NativeInterface.AsSpan().GetUnsafeIntPtr();
		lock (ReferenceHelper.nativeLock)
		{
			NativeInterfaceProxy? result = ReferenceHelper.nativeProxies.GetValueOrDefault(envRef);
			if (result is not null && result.Reference.Reference.Pointer != ptr) result = default;
			return result ?? NativeInterfaceProxy.Detached;
		}
	}
}