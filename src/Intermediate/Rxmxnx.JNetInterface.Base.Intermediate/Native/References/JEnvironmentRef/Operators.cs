namespace Rxmxnx.JNetInterface.Native.References;

public partial struct JEnvironmentRef
{
    /// <inheritdoc/>
    public static Boolean operator ==(JEnvironmentRef left, JEnvironmentRef right) => left._value.Equals(right._value);
    /// <inheritdoc/>
    public static Boolean operator !=(JEnvironmentRef left, JEnvironmentRef right) => !left._value.Equals(right._value);
}