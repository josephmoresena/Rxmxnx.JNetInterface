namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for classes.
	/// </summary>
	/// <param name="baseMetadata">Base metadata.</param>
	/// <param name="set">Interface set.</param>
	private sealed class ClassInterfaceSet(
		JReferenceTypeMetadata baseMetadata,
		ImmutableHashSet<JInterfaceTypeMetadata> set) : InterfaceSet(set)
	{
		/// <summary>
		/// Base type metadata.
		/// </summary>
		private readonly IInterfaceSet _baseInterfaces = baseMetadata.Interfaces;

		/// <inheritdoc/>
		public override Boolean Contains(JInterfaceTypeMetadata item)
			=> base.Contains(item) || this._baseInterfaces.Contains(item);
		/// <inheritdoc/>
		public override void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			base.ForEach(state, action);
			this._baseInterfaces.ForEach(state, action);
		}

		/// <inheritdoc/>
		private protected override IEnumerable<JInterfaceTypeMetadata> GetEnumerable()
			=> base.GetEnumerable().Union(this._baseInterfaces).Distinct();
	}
}