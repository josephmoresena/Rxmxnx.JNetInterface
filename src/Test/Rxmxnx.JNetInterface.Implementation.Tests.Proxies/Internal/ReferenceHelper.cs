namespace Rxmxnx.JNetInterface.Tests.Internal;

internal static partial class ReferenceHelper
{
	public static readonly IFixture Fixture = new Fixture().RegisterReferences();

	public static JVirtualMachineRef InitializeProxy(InvokeInterfaceProxy proxy)
	{
		IntPtr ptr = ReferenceHelper.invokeHelper.Get();
		JVirtualMachineRef result = NativeUtilities.Transform<IntPtr, JVirtualMachineRef>(ptr);
		ReferenceHelper.invokeProxies[result] = proxy;
		return result;
	}
	public static void FinalizeProxy(JVirtualMachineRef vmRef) => ReferenceHelper.invokeProxies.TryRemove(vmRef, out _);
	public static JEnvironmentRef InitializeProxy(NativeInterfaceProxy proxy)
	{
		IntPtr ptr = ReferenceHelper.nativeHelper.Get();
		JEnvironmentRef result = NativeUtilities.Transform<IntPtr, JEnvironmentRef>(ptr);
		ReferenceHelper.nativeProxies[result] = proxy;
		return result;
	}
	public static void FinalizeProxy(JEnvironmentRef envRef) => ReferenceHelper.nativeProxies.TryRemove(envRef, out _);

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
}