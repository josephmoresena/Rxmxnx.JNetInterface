using System.Numerics;

namespace Rxmxnx.JNetInterface.ApplicationTest;

public sealed partial class JCompiler : IComparable<JCompiler>, IComparable, IEquatable<JCompiler>,
	IComparisonOperators<JCompiler, JCompiler, Boolean>
{
	/// <inheritdoc/>
	public Int32 CompareTo(Object? obj)
	{
		if (obj is null) return 1;
		if (Object.ReferenceEquals(this, obj)) return 0;
		return obj is JCompiler other ?
			this.CompareTo(other) :
			throw new ArgumentException($"Object must be of type {nameof(JCompiler)}");
	}
	/// <inheritdoc/>
	public Int32 CompareTo(JCompiler? other)
	{
		if (Object.ReferenceEquals(this, other)) return 0;
		if (other is null) return 1;
		Int32 versionComparison = this.JdkVersion.CompareTo(other.JdkVersion);
		return versionComparison != 0 ?
			versionComparison :
			String.Compare(this.JdkPath, other.JdkPath, StringComparison.Ordinal);
	}
	/// <inheritdoc/>
	public Boolean Equals(JCompiler? other)
	{
		if (other is null) return false;
		if (Object.ReferenceEquals(this, other)) return true;
		return this.JdkPath == other.JdkPath && this.JdkVersion.Equals(other.JdkVersion);
	}
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj)
		=> Object.ReferenceEquals(this, obj) || (obj is JCompiler other && this.Equals(other));
	/// <inheritdoc/>
	public override Int32 GetHashCode() => HashCode.Combine(this.JdkPath, this.JdkVersion);

	/// <inheritdoc/>
	public static Boolean operator <(JCompiler? left, JCompiler? right)
		=> Comparer<JCompiler>.Default.Compare(left, right) < 0;
	/// <inheritdoc/>
	public static Boolean operator >(JCompiler? left, JCompiler? right)
		=> Comparer<JCompiler>.Default.Compare(left, right) > 0;
	/// <inheritdoc/>
	public static Boolean operator <=(JCompiler? left, JCompiler? right)
		=> Comparer<JCompiler>.Default.Compare(left, right) <= 0;
	/// <inheritdoc/>
	public static Boolean operator >=(JCompiler? left, JCompiler? right)
		=> Comparer<JCompiler>.Default.Compare(left, right) >= 0;
	/// <inheritdoc/>
	public static Boolean operator ==(JCompiler? left, JCompiler? right) => Object.Equals(left, right);
	/// <inheritdoc/>
	public static Boolean operator !=(JCompiler? left, JCompiler? right) => !Object.Equals(left, right);
}