namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Array interface set.
	/// </summary>
	private sealed class ArrayInterfaceSet : IInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new ArrayInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private ArrayInterfaceSet() { }

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<JSerializableObject>();
			yield return IInterfaceType.GetMetadata<JCloneableObject>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> item.Equals(IInterfaceType.GetMetadata<JSerializableObject>()) ||
				item.Equals(IInterfaceType.GetMetadata<JCloneableObject>());
		/// <inheritdoc/>
		public void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			action(state, IInterfaceType.GetMetadata<JSerializableObject>());
			action(state, IInterfaceType.GetMetadata<JCloneableObject>());
		}
	}
}