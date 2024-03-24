namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for classes.
	/// </summary>
	private sealed class ClassInterfaceSet : InterfaceSet
	{
		/// <summary>
		/// Base type metadata.
		/// </summary>
		private readonly IInterfaceSet _baseInterfaces;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="baseMetadata">Base metadata.</param>
		/// <param name="set">Interface set.</param>
		public ClassInterfaceSet(JReferenceTypeMetadata baseMetadata, ImmutableHashSet<JInterfaceTypeMetadata> set) :
			base(set)
			=> this._baseInterfaces = baseMetadata.Interfaces;

		/// <inheritdoc/>
		public override Boolean Contains(JInterfaceTypeMetadata item)
			=> base.Contains(item) || this._baseInterfaces.Contains(item);
		/// <inheritdoc/>
		public override void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			_ = InterfaceSet.OpenSetOperation(out Boolean isNew);
			try
			{
				base.ForEach(state, action);
				this._baseInterfaces.ForEach(state, action);
			}
			finally
			{
				InterfaceSet.CloseSetOperation(isNew);
			}
		}

		/// <inheritdoc/>
		private protected override IEnumerable<JInterfaceTypeMetadata> GetEnumerable()
			=> base.GetEnumerable().Union(this._baseInterfaces).Distinct();
	}
}