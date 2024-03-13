namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for interfaces.
	/// </summary>
	public sealed class InterfaceInterfaceSet : InterfaceSet
	{
		/// <inheritdoc/>
		public override IEnumerable<JInterfaceTypeMetadata> Enumerable
			=> base.Enumerable.Union(base.Enumerable.SelectMany(i => i.Interfaces.Enumerable)).Distinct();

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="set">Interface set.</param>
		public InterfaceInterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set) : base(set) { }

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
	}
}