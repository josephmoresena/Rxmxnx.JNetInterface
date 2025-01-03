namespace Rxmxnx.JNetInterface.Internal;

internal abstract partial class InfoSequenceBase : IEquatable<InfoSequenceBase>,
	IEqualityOperators<InfoSequenceBase, InfoSequenceBase, Boolean>
{
	/// <inheritdoc/>
	public Boolean Equals(InfoSequenceBase? other) => this.Hash.Equals(other?.Hash);
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override Boolean Equals(Object? obj) => this.Equals(obj as InfoSequenceBase);

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public static Boolean operator ==(InfoSequenceBase? left, InfoSequenceBase? right)
		=> left?.Equals(right) ?? right is null;
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public static Boolean operator !=(InfoSequenceBase? left, InfoSequenceBase? right) => !(left == right);
}