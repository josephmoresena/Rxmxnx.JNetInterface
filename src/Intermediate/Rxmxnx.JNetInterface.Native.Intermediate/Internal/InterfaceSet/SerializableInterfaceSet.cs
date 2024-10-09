namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Serializable interface set.
	/// </summary>
	private sealed class SerializableInterfaceSet : IAppendableInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new SerializableInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private SerializableInterfaceSet() { }

		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<JSerializableObject>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> item.Equals(IInterfaceType.GetMetadata<JSerializableObject>());
		/// <inheritdoc/>
		public void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			HashSet<String> hashes = InterfaceSet.OpenSetOperation(out Boolean isNew);
			try
			{
				if (hashes.Add(IInterfaceType.GetMetadata<JSerializableObject>().Hash))
					action(state, IInterfaceType.GetMetadata<JSerializableObject>());
			}
			finally
			{
				InterfaceSet.CloseSetOperation(isNew);
			}
		}
	}
}