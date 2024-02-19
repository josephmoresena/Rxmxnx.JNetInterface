namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject : IEnumerable<Char>, IComparable, IComparable<String?>, IComparable<JStringObject?>,
	IComparable<IWrapper<String>?>, IEquatable<JStringObject?>, IEquatable<IWrapper<String>?>
{
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	Boolean IEquatable<IWrapper<String>?>.Equals(IWrapper<String>? other) => this.Value.Equals(other?.Value);
	Int32 IComparable.CompareTo(Object? obj)
		=> obj switch
		{
			JStringObject jString => this.CompareTo(jString),
			String str => this.CompareTo(str),
			IWrapper<String> wStr => this.CompareTo(wStr.Value),
			IComparable ic => -ic.CompareTo(this.Value),
			_ => this.Value.CompareTo(obj),
		};
	Int32 IComparable<IWrapper<String>?>.CompareTo(IWrapper<String>? other)
		=> String.Compare(this.Value, other?.Value, StringComparison.Ordinal);

	/// <inheritdoc cref="String.GetEnumerator()"/>
	public IEnumerator<Char> GetEnumerator() => this.Value.GetEnumerator();
	/// <inheritdoc/>
	public Int32 CompareTo(JStringObject? other) => this.CompareTo(other?.Value);
	/// <inheritdoc/>
	public Int32 CompareTo(String? other) => String.Compare(this.Value, other, StringComparison.Ordinal);
	/// <inheritdoc/>
	public Boolean Equals(JStringObject? other) => this.Value.Equals(other?.Value);
}