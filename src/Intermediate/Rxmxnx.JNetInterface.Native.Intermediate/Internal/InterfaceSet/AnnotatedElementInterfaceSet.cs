namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// AnnotatedElement interface set.
	/// </summary>
	private sealed class AnnotatedElementInterfaceSet : IAppendableInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new AnnotatedElementInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private AnnotatedElementInterfaceSet() { }

		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<JAnnotatedElementObject>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> item.Equals(IInterfaceType.GetMetadata<JAnnotatedElementObject>());
		/// <inheritdoc/>
		public void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		{
			HashSet<String> hashes = InterfaceSet.OpenSetOperation(out Boolean isNew);
			try
			{
				if (hashes.Add(IInterfaceType.GetMetadata<JAnnotatedElementObject>().Hash))
					action(state, IInterfaceType.GetMetadata<JAnnotatedElementObject>());
			}
			finally
			{
				InterfaceSet.CloseSetOperation(isNew);
			}
		}
	}
}