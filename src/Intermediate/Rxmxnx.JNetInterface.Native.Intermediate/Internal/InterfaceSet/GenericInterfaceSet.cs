namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet
{
	/// <summary>
	/// Generic interface set.
	/// </summary>
	/// <typeparam name="TInterface"><see cref="IInterfaceType{TInterface}"/> type.</typeparam>
	private sealed class GenericInterfaceSet<TInterface> : IAppendableInterfaceSet, IRecursiveInterfaceSet
		where TInterface : JInterfaceObject<TInterface>, IInterfaceType<TInterface>
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new GenericInterfaceSet<TInterface>();

		/// <summary>
		/// Constructor.
		/// </summary>
		private GenericInterfaceSet() { }

#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<TInterface>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> InterfaceSet.SameInterface(item, IInterfaceType.GetMetadata<TInterface>());

		void IRecursiveInterfaceSet.ForEach<T>(IRecursiveInterfaceSet.RecursiveState<T> state)
			=> state.Execute(IInterfaceType.GetMetadata<TInterface>());
	}

	/// <summary>
	/// Generic interface set.
	/// </summary>
	/// <typeparam name="TInterface0"><see cref="IInterfaceType{TInterface0}"/> type.</typeparam>
	/// <typeparam name="TInterface1"><see cref="IInterfaceType{TInterface1}"/> type.</typeparam>
	private sealed class GenericInterfaceSet<TInterface0, TInterface1> : IAppendableInterfaceSet, IRecursiveInterfaceSet
		where TInterface0 : JInterfaceObject<TInterface0>, IInterfaceType<TInterface0>
		where TInterface1 : JInterfaceObject<TInterface1>, IInterfaceType<TInterface1>
	{
		/// <summary>
		/// Array set instance.
		/// </summary>
		public static readonly IInterfaceSet Instance = new GenericInterfaceSet<TInterface0, TInterface1>();

		/// <summary>
		/// Constructor.
		/// </summary>
		private GenericInterfaceSet() { }

#if !PACKAGE
		[ExcludeFromCodeCoverage]
#endif
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		/// <inheritdoc/>
		public IEnumerator<JInterfaceTypeMetadata> GetEnumerator()
		{
			yield return IInterfaceType.GetMetadata<TInterface0>();
			yield return IInterfaceType.GetMetadata<TInterface1>();
		}
		/// <inheritdoc/>
		public Boolean Contains(JInterfaceTypeMetadata item)
			=> InterfaceSet.SameInterface(item, IInterfaceType.GetMetadata<TInterface0>()) ||
				InterfaceSet.SameInterface(item, IInterfaceType.GetMetadata<TInterface1>());

		void IRecursiveInterfaceSet.ForEach<T>(IRecursiveInterfaceSet.RecursiveState<T> state)
		{
			state.Execute(IInterfaceType.GetMetadata<TInterface0>());
			state.Execute(IInterfaceType.GetMetadata<TInterface1>());
		}
	}
}