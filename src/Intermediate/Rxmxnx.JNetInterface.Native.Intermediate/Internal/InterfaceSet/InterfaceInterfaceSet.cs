namespace Rxmxnx.JNetInterface.Internal;

internal partial record InterfaceSet
{
	/// <summary>
	/// Interface set for interfaces.
	/// </summary>
	public sealed record InterfaceInterfaceSet : InterfaceSet
	{
		/// <inheritdoc/>
		protected override IEnumerable<JInterfaceTypeMetadata> Enumerable
			=> base.Enumerable.Union(base.Enumerable.SelectMany(i => i.Interfaces)).Distinct();

		/// <inheritdoc/>
		public override Int32 Count => this.Enumerable.Count();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="set">Interface set.</param>
		public InterfaceInterfaceSet(IReadOnlySet<JInterfaceTypeMetadata> set) : base(set) { }

		/// <inheritdoc/>
		public override Boolean Contains(JInterfaceTypeMetadata item)
		{
			if (base.Contains(item)) return true;
			Boolean result = false;
			Parallel.ForEach(this.Enumerable.Select(i => i.Interfaces), (ii, s) =>
			{
				if (!ii.Contains(item)) return;
				result = true;
				s.Stop();
			});
			return result;
		}
		/// <inheritdoc/>
		public override String ToString() => base.ToString();
	}
}