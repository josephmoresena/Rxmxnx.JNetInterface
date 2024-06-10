namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// This struct stores assignation key.
	/// </summary>
	private readonly struct AssignationKey : IEquatable<AssignationKey>
	{
		/// <summary>
		/// Hash of source type.
		/// </summary>
		public String FromHash { get; init; }
		/// <summary>
		/// Hash of destination type.
		/// </summary>
		public String ToHash { get; init; }

		/// <summary>
		/// Indicates whether From and To hash are same.
		/// </summary>
		public Boolean IsSame => this.FromHash.AsSpan().SequenceEqual(this.ToHash);

		/// <summary>
		/// Creates a reversed instance.
		/// </summary>
		/// <returns>A <see cref="AssignationKey"/> instance.</returns>
		public AssignationKey Reverse() => new() { FromHash = this.ToHash, ToHash = this.FromHash, };

		/// <inheritdoc/>
		public Boolean Equals(AssignationKey obj)
			=> this.FromHash.AsSpan().SequenceEqual(obj.FromHash) && this.ToHash.AsSpan().SequenceEqual(obj.ToHash);

		/// <inheritdoc/>
		public override Int32 GetHashCode() => HashCode.Combine(this.FromHash, this.ToHash);
		/// <inheritdoc/>
		public override Boolean Equals(Object? obj) => obj is AssignationKey key && this.Equals(key);
	}
}