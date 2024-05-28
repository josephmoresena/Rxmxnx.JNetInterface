namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for interfaces.
	/// </summary>
	/// <remarks>
	/// Constructor.
	/// </remarks>
	/// <param name="set">Interface set.</param>
	public sealed class InterfaceInterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set) : InterfaceSet(set)
	{
		/// <inheritdoc/>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3267,
		                 Justification = CommonConstants.NonStandardLinqJustification)]
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