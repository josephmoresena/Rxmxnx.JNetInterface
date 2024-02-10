namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject : IEnumerable<Char>
//, IComparable, IComparable<String?>, IComparable<JStringObject?>, IConvertible, IEquatable<String?>, IEquatable<JStringObject?>
{
	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

	/// <inheritdoc cref="String.GetEnumerator()"/>
	public IEnumerator<Char> GetEnumerator() => this.Value.GetEnumerator();
}