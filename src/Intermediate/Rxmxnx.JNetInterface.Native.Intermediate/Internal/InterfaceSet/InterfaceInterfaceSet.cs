namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for interfaces.
	/// </summary>
	public sealed class InterfaceInterfaceSet : InterfaceSet
	{
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
			Parallel.ForEach(this.GetEnumerable().Select(i => i.Interfaces), (ii, s) =>
			{
				if (!ii.Contains(item)) return;
				result = true;
				s.Stop();
			});
			return result;
		}
		/// <inheritdoc/>
		public override void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			HashSet<String> hashes = [];
			base.ForEach((state, hashes, true, action), InterfaceSet.ForEachImpl);
		}

		/// <inheritdoc/>
		private protected override IEnumerable<JInterfaceTypeMetadata> GetEnumerable()
			=> base.GetEnumerable().Union(base.GetEnumerable().SelectMany(i => i.Interfaces)).Distinct();
	}
}