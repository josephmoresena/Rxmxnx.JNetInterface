namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JVirtualMachineRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JVirtualMachineRef left, JVirtualMachineRef right) => left._value.Equals(right._value);
    /// <inheritdoc/>
    public static Boolean operator !=(JVirtualMachineRef left, JVirtualMachineRef right) => !left._value.Equals(right._value);
}