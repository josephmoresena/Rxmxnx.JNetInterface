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
			foreach (JInterfaceTypeMetadata interfaceTypeMetadata in this._internalSet)
			{
				if (interfaceTypeMetadata.Interfaces.Contains(item))
					return true;
			}
			return false;
		}

		/// <inheritdoc/>
		private protected override IEnumerable<JInterfaceTypeMetadata> GetEnumerable()
			=> base.GetEnumerable().Union(base.GetEnumerable().SelectMany(i => i.Interfaces)).Distinct();
	}
}