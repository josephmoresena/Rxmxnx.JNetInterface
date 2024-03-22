namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Primitive wrapper class interface set.
	/// </summary>
	private sealed class PrimitiveWrapperInterfaceSet : IInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new PrimitiveWrapperInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private PrimitiveWrapperInterfaceSet() { }

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
			action(state, IInterfaceType.GetMetadata<JSerializableObject>());
			action(state, IInterfaceType.GetMetadata<JComparableObject>());
		}
	}
}