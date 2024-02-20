namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject : IComparisonOperators<JStringObject, JStringObject, Boolean>,
	IComparisonOperators<JStringObject, String, Boolean>
{
	/// <inheritdoc/>
	public static Boolean operator ==(JStringObject? left, JStringObject? right)
		=> left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	public static Boolean operator !=(JStringObject? left, JStringObject? right) => !(left == right);
	/// <inheritdoc/>
	public static Boolean operator >(JStringObject left, JStringObject right) => left.CompareTo(right) > 0;
	/// <inheritdoc/>
	public static Boolean operator >=(JStringObject left, JStringObject right) => left.CompareTo(right) >= 0;
	/// <inheritdoc/>
	public static Boolean operator <(JStringObject left, JStringObject right) => left.CompareTo(right) < 0;
	/// <inheritdoc/>
	public static Boolean operator <=(JStringObject left, JStringObject right) => left.CompareTo(right) <= 0;
	/// <inheritdoc/>
	public static Boolean operator ==(JStringObject? left, String? right) => left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	public static Boolean operator !=(JStringObject? left, String? right) => !(left == right);
	/// <inheritdoc/>
	public static Boolean operator >(JStringObject left, String right) => left.CompareTo(right) > 0;
	/// <inheritdoc/>
	public static Boolean operator >=(JStringObject left, String right) => left.CompareTo(right) >= 0;
	/// <inheritdoc/>
	public static Boolean operator <(JStringObject left, String right) => left.CompareTo(right) < 0;
	/// <inheritdoc/>
	public static Boolean operator <=(JStringObject left, String right) => left.CompareTo(right) <= 0;
}