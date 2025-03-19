namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Recursive interface metadata set.
/// </summary>
internal interface IRecursiveInterfaceSet : IInterfaceSet
{
	void IInterfaceSet.ForEach<T>(T state, Action<T, JInterfaceTypeMetadata> action)
		=> this.ForEach(RecursiveState<T>.CreateNonRecursive(state, action));
	/// <summary>
	/// Performs <paramref name="state"/> action for each unique item in current set.
	/// </summary>
	/// <typeparam name="T">Type of state object</typeparam>
	/// <param name="state">Object state.</param>
	protected void ForEach<T>(RecursiveState<T> state)
#if NET9_0_OR_GREATER
		where T : allows ref struct
#endif
		;

	/// <summary>
	/// State struct for recursive action.
	/// </summary>
	/// <typeparam name="T">Type of state object</typeparam>
	protected readonly
#if NET9_0_OR_GREATER
		ref
#endif
		struct RecursiveState<T>
#if NET9_0_OR_GREATER
		where T : allows ref struct
#endif
	{
		/// <summary>
		/// Internal dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, Boolean>? _hashes;
		/// <summary>
		/// Internal state.
		/// </summary>
		private readonly T _state;
		/// <summary>
		/// Internal action.
		/// </summary>
		private readonly Action<T, JInterfaceTypeMetadata> _action;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="state">Object state.</param>
		/// <param name="action">A <see cref="Action{T, JInterfaceTypeMetadata}"/> delegate.</param>
		public RecursiveState(T state, Action<T, JInterfaceTypeMetadata> action) : this(new(), state, action) { }

		/// <summary>
		/// Executes internal function using <paramref name="interfaceTypeMetadata"/>.
		/// </summary>
		/// <param name="interfaceTypeMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		public void Execute(JInterfaceTypeMetadata interfaceTypeMetadata)
		{
			if (this._hashes is not null && !this._hashes.TryAdd(interfaceTypeMetadata.Hash, true)) return;
			this._action(this._state, interfaceTypeMetadata);
			(interfaceTypeMetadata.Interfaces as IRecursiveInterfaceSet)?.ForEach(this);
		}

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="hashes">Hashes dictionary.</param>
		/// <param name="state">Object state.</param>
		/// <param name="action">A <see cref="Action{T, JInterfaceTypeMetadata}"/> delegate.</param>
		private RecursiveState(ConcurrentDictionary<String, Boolean>? hashes, T state,
			Action<T, JInterfaceTypeMetadata> action)
		{
			this._hashes = hashes;
			this._state = state;
			this._action = action;
		}

		/// <summary>
		/// Creates a non-recursive <see cref="RecursiveState{T}"/> instance.
		/// </summary>
		/// <param name="state">Object state.</param>
		/// <param name="action">A <see cref="Action{T, JInterfaceTypeMetadata}"/> delegate.</param>
		/// <returns>A <see cref="RecursiveState{T}"/> instance.</returns>
		public static RecursiveState<T> CreateNonRecursive(T state, Action<T, JInterfaceTypeMetadata> action)
			=> new(default, state, action);

		/// <summary>
		/// Executes internal function using <paramref name="interfaceTypeMetadata"/>.
		/// </summary>
		/// <param name="state"><see cref="RecursiveState{T}"/> instance.</param>
		/// <param name="interfaceTypeMetadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		public static void Execute(RecursiveState<T> state, JInterfaceTypeMetadata interfaceTypeMetadata)
			=> state.Execute(interfaceTypeMetadata);
	}
}