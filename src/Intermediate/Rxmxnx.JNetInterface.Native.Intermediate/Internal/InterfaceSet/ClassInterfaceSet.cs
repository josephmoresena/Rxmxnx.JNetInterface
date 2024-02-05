namespace Rxmxnx.JNetInterface.Internal;

internal partial record InterfaceSet
{
	/// <summary>
	/// Interface set for classes.
	/// </summary>
	private sealed record ClassInterfaceSet : InterfaceSet
	{
		/// <summary>
		/// Base type metadata.
		/// </summary>
		private readonly IReadOnlySet<JInterfaceTypeMetadata> _baseInterfaces;

		/// <inheritdoc/>
		protected override IEnumerable<JInterfaceTypeMetadata> Enumerable
			=> base.Enumerable.Union(this._baseInterfaces).Distinct();

		/// <inheritdoc/>
		public override Int32 Count => this.Enumerable.Count();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="baseMetadata">Base metadata.</param>
		/// <param name="set">Interface set.</param>
		public ClassInterfaceSet(JReferenceTypeMetadata baseMetadata, IReadOnlySet<JInterfaceTypeMetadata> set) :
			base(set)
			=> this._baseInterfaces = baseMetadata.Interfaces;

		/// <inheritdoc/>
		public override Boolean Contains(JInterfaceTypeMetadata item)
			=> base.Contains(item) || this._baseInterfaces.Contains(item);
		/// <inheritdoc/>
		public override String ToString() => base.ToString();
	}
}