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
		private readonly Object _lock = new();
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
			lock (this._lock)
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
			lock (this._lock) this._set.Add(superViewHash);
		}
	}
}