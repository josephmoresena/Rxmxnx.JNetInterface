namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Annotation interface set.
	/// </summary>
	public sealed class AnnotationInterfaceSet : IInterfaceSet
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new AnnotationInterfaceSet();

		/// <summary>
		/// Constructor.
		/// </summary>
		private AnnotationInterfaceSet() { }

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<JAnnotationObject>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> item.Equals(IInterfaceType.GetMetadata<JAnnotationObject>());
		/// <inheritdoc/>
		public void ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
			=> action(state, IInterfaceType.GetMetadata<JAnnotationObject>());
	}
}