namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject : IEqualityOperators<JStringObject, JStringObject, Boolean>,
	IEqualityOperators<JStringObject, String, Boolean>
{
	/// <inheritdoc/>
	public static Boolean operator ==(JStringObject? left, JStringObject? right)
		=> left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	public static Boolean operator !=(JStringObject? left, JStringObject? right) => !(left == right);
	/// <inheritdoc/>
	public static Boolean operator ==(JStringObject? left, String? right) => left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	public static Boolean operator !=(JStringObject? left, String? right) => !(left == right);
}