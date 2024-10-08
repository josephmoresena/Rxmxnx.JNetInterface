namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Serializable and Comparable class interface set.
	/// </summary>
	private sealed class SerializableComparableInterfaceSet : IAppendableInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new SerializableComparableInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private SerializableComparableInterfaceSet() { }

		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<JSerializableObject>();
			yield return IInterfaceType.GetMetadata<JComparableObject>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> item.Equals(IInterfaceType.GetMetadata<JSerializableObject>()) ||
				item.Equals(IInterfaceType.GetMetadata<JComparableObject>());
		/// <inheritdoc/>
		public void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			HashSet<String> hashes = InterfaceSet.OpenSetOperation(out Boolean isNew);
			try
			{
				if (hashes.Add(IInterfaceType.GetMetadata<JSerializableObject>().Hash))
					action(state, IInterfaceType.GetMetadata<JSerializableObject>());
				if (hashes.Add(IInterfaceType.GetMetadata<JComparableObject>().Hash))
					action(state, IInterfaceType.GetMetadata<JComparableObject>());
			}
			finally
			{
				InterfaceSet.CloseSetOperation(isNew);
			}
		}
	}
}