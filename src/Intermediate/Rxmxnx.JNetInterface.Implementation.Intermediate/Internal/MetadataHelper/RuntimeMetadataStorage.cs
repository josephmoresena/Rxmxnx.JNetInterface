namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// Runtime metadata storage.
	/// </summary>
	private sealed partial class RuntimeMetadataStorage
	{
		/// <summary>
		/// Runtime metadata assignation dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<AssignationKey, Boolean> _assignationCache;
		/// <summary>
		/// Builtin metadata dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, Boolean> _builtInTypes;
		/// <summary>
		/// Runtime class metadata dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, String> _classTree;
		/// <summary>
		/// Runtime metadata dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, JReferenceTypeMetadata> _runtimeMetadata;
		/// <summary>
		/// Runtime view metadata dictionary.
		/// </summary>
		private readonly ConcurrentDictionary<String, HashesSet> _viewTree;

		/// <summary>
		/// Gets or Sets assignation.
		/// </summary>
		/// <param name="key"></param>
		public Boolean this[AssignationKey key]
		{
			set => this._assignationCache[key] = value;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public RuntimeMetadataStorage()
		{
			this._runtimeMetadata = new(MetadataHelper.initialMetadata);
			this._builtInTypes = new(MetadataHelper.initialBuiltIn);
			this._assignationCache = new();
			this._classTree = new();
			this._viewTree = new();
			if (JVirtualMachine.BuiltInThrowableAutoRegistered)
				RuntimeMetadataStorage.BuiltInThrowableRegistration(this._runtimeMetadata);
			if (JVirtualMachine.ReflectionAutoRegistered)
				RuntimeMetadataStorage.ReflectionRegistration(this._runtimeMetadata);
			if (JVirtualMachine.NioAutoRegistered) RuntimeMetadataStorage.NioRegistration(this._runtimeMetadata);
		}

		/// <summary>
		/// Initialize built-in type.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		public void InitializeBuiltIn(String hash)
		{
			if (!this._builtInTypes.TryGetValue(hash, out Boolean initialized)) return;
			if (initialized) return;

			JReferenceTypeMetadata typeMetadata = this._runtimeMetadata[hash];
			if (typeMetadata.BaseMetadata is not null)
			{
				AssignationKey assignationKey =
					new() { FromHash = typeMetadata.Hash, ToHash = typeMetadata.BaseMetadata.Hash, };
				this._assignationCache[assignationKey] = true;
			}
			InterfaceAssignationState state = new()
			{
				FromHash = typeMetadata.Hash, AssignationCache = this._assignationCache,
			};
			typeMetadata.Interfaces.ForEach(state, (s, i) =>
			{
				AssignationKey assignationKey = new() { FromHash = s.FromHash, ToHash = i.Hash, };
				s.AssignationCache[assignationKey] = true;
			});
			this._builtInTypes[hash] = true;
		}
		/// <summary>
		/// Indicates current hash is for a final built-in type.
		/// </summary>
		/// <param name="hash">A type hash.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="hash"/> is for a final built-in type;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean IsBuiltInAndFinalType(String hash)
		{
			if (!this._builtInTypes.ContainsKey(hash)) return false;
			return this._runtimeMetadata[hash].Modifier is JTypeModifier.Final;
		}
		/// <summary>
		/// Indicates whether current type is registered.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="hash"/> is registered;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean IsRegistered(String hash) => this._runtimeMetadata.ContainsKey(hash);
		/// <summary>
		/// Indicates whether current assignation is registered.
		/// </summary>
		/// <param name="key">Assignation key.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="key"/> is registered;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean IsRegistered(AssignationKey key) => this._assignationCache.ContainsKey(key);
		/// <summary>
		/// Attempts to add the <paramref name="typeMetadata"/> to the current instance.
		/// </summary>
		/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance,</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="typeMetadata"/> was added to current instance successfully;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean TryAdd(JReferenceTypeMetadata typeMetadata)
			=> this._runtimeMetadata.TryAdd(typeMetadata.Hash, typeMetadata);
		/// <summary>
		/// Attempts to get the <see cref="JReferenceTypeMetadata"/> associated with the specified hash from current instance.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		/// <param name="typeMetadata">Output. A <see cref="JReferenceTypeMetadata"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="hash"/> was found into the current instance;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean TryGetValue(String hash, [NotNullWhen(true)] out JReferenceTypeMetadata? typeMetadata)
			=> this._runtimeMetadata.TryGetValue(hash, out typeMetadata);
		/// <summary>
		/// Attempts to get the assignation associated with the specified key from current instance.
		/// </summary>
		/// <param name="key">Assignation key.</param>
		/// <param name="assignable">Output. Indicates whether key is assignable.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="key"/> was found into the current instance;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean TryGetValue(AssignationKey key, out Boolean assignable)
			=> this._assignationCache.TryGetValue(key, out assignable);
		/// <summary>
		/// Tries to get the value associated with the specified hash in the current instance.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
		public JReferenceTypeMetadata? GetValueOrDefault(String hash) => this._runtimeMetadata.GetValueOrDefault(hash);
		/// <summary>
		/// Registres the <see cref="AssignationKey"/> as super-class relationship.
		/// </summary>
		/// <param name="assignationKey">A <see cref="AssignationKey"/> instance.</param>
		public void RegisterSuperClassRelationship(AssignationKey assignationKey)
		{
			this._classTree[assignationKey.FromHash] = assignationKey.ToHash;
			this._assignationCache[assignationKey] = true;
		}
		/// <summary>
		/// Registres the <see cref="AssignationKey"/> as super-view relationship.
		/// </summary>
		/// <param name="assignationKey">A <see cref="AssignationKey"/> instance.</param>
		public void RegisterSuperViewRelationship(AssignationKey assignationKey)
		{
			if (!this._viewTree.TryGetValue(assignationKey.FromHash, out HashesSet? set))
			{
				set = new();
				this._viewTree[assignationKey.FromHash] = set;
			}
			set.Add(assignationKey.ToHash);
			this._assignationCache[assignationKey] = true;
		}
		/// <summary>
		/// Retrieves the super class hash associated to <paramref name="hash"/> type.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		/// <returns>Found super class hash.</returns>
		public String? GetSuperClassHash(String hash) => this._classTree.GetValueOrDefault(hash);
		/// <summary>
		/// Retrieves the super view set associated to <paramref name="hash"/> type.
		/// </summary>
		/// <param name="hash">Type hash.</param>
		/// <returns>Found super view set.</returns>
		public HashesSet? GetHashesSet(String hash) => this._viewTree.GetValueOrDefault(hash);
	}
}