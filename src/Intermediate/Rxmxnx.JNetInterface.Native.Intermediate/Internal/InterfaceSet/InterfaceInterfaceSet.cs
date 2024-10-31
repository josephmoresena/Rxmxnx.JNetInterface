namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Interface set for interfaces.
	/// </summary>
	/// <param name="set">Interface set.</param>
	public sealed class InterfaceInterfaceSet(ImmutableHashSet<JInterfaceTypeMetadata> set)
		: InterfaceSet(set), IRecursiveInterfaceSet
	{
		/// <inheritdoc/>
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3267,
		                 Justification = CommonConstants.NonStandardLinqJustification)]
		public override Boolean Contains(JInterfaceTypeMetadata item)
		{
			if (base.Contains(item)) return true;
			using ImmutableHashSet<JInterfaceTypeMetadata>.Enumerator enumerator = this._internalSet.GetEnumerator();
			while (enumerator.MoveNext())
				if (enumerator.Current.Interfaces.Contains(item))
					return true;
			return false;
		}

		/// <inheritdoc cref="IInterfaceSet.ForEach{T}(T,Action{T,JInterfaceTypeMetadata})"/>
		public override void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			IRecursiveInterfaceSet.RecursiveState<T> recursiveState = new(state, action);
			base.ForEach(recursiveState, IRecursiveInterfaceSet.RecursiveState<T>.Execute);
		}

		void IRecursiveInterfaceSet.ForEach<T>(IRecursiveInterfaceSet.RecursiveState<T> state)
			=> base.ForEach(state, IRecursiveInterfaceSet.RecursiveState<T>.Execute);

		/// <inheritdoc/>
		private protected override IEnumerable<JInterfaceTypeMetadata> GetEnumerable()
			=> base.GetEnumerable().Union(base.GetEnumerable().SelectMany(i => i.Interfaces)).Distinct();
	}
}