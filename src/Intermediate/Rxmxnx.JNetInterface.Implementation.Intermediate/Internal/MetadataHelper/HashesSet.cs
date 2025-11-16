namespace Rxmxnx.JNetInterface.Internal;

internal static partial class MetadataHelper
{
	/// <summary>
	/// This class is a hashes set.
	/// </summary>
	private sealed class HashesSet
	{
		/// <summary>
		/// Lock object.
		/// </summary>
#if NET9_0_OR_GREATER
		private readonly Lock _lock = new();
#else
		private readonly Object _lock = new();
#endif
		/// <summary>
		/// Internal hashes set.
		/// </summary>
		private readonly HashSet<String> _set = [];

		/// <summary>
		/// Retrieves <see cref="JReferenceTypeMetadata"/> for current view.
		/// </summary>
		/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
		public JReferenceTypeMetadata? GetViewMetadata()
		{
#if NET9_0_OR_GREATER
			using (this._lock.EnterScope())
#else
			lock (this._lock)
#endif
			{
				foreach (String hashView in this._set)
				{
					if (MetadataHelper.GetMetadata(hashView) is { } viewResult)
						return viewResult;
				}
			}
			return default;
		}
		/// <summary>
		/// Adds to current set <paramref name="superViewHash"/>.
		/// </summary>
		/// <param name="superViewHash">Super view class hash.</param>
		public void Add(String superViewHash)
		{
#if NET9_0_OR_GREATER
			using (this._lock.EnterScope())
#else
			lock (this._lock)
#endif
				this._set.Add(superViewHash);
		}
		/// <summary>
		/// Determines whether <paramref name="superViewHash"/> is contained in the current set.
		/// </summary>
		/// <param name="superViewHash">Super view class hash.</param>
		/// <returns>
		/// <see langword="true"/> if <paramref name="superViewHash"/> is contained in the current set;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		public Boolean Contains(String superViewHash)
		{
#if NET9_0_OR_GREATER
			using (this._lock.EnterScope())
#else
			lock (this._lock)
#endif
				return this._set.Contains(superViewHash);
		}
	}
}