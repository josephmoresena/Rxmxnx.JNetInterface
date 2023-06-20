namespace Rxmxnx.JNetInterface.Native.Values;

public partial struct JVirtualMachineValue
{
    /// <inheritdoc/>
    public static Boolean operator ==(JVirtualMachineValue left, JVirtualMachineValue right) => left._functions.Equals(right._functions);
    /// <inheritdoc/>
    public static Boolean operator !=(JVirtualMachineValue left, JVirtualMachineValue right) => !left._functions.Equals(right._functions);
}