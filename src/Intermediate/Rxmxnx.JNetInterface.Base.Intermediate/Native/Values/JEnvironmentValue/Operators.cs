namespace Rxmxnx.JNetInterface.Native.Values;

public partial struct JEnvironmentValue
{
	/// <inheritdoc/>
	public static Boolean operator ==(JEnvironmentValue left, JEnvironmentValue right) => left._functions.Equals(right._functions);
	/// <inheritdoc/>
	public static Boolean operator !=(JEnvironmentValue left, JEnvironmentValue right) => !left._functions.Equals(right._functions);
}