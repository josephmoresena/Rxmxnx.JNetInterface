namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI Native I/O feature.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public partial interface INioFeature
{
	/// <summary>
	/// Retrieves the starting address of the memory region referenced by <paramref name="buffer"/>.
	/// </summary>
	/// <param name="buffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>The starting address of the memory region referenced by <paramref name="buffer"/></returns>
	IntPtr GetDirectAddress(JBufferObject buffer);
	/// <summary>
	/// Retrieves the capacity of the memory region referenced by <paramref name="buffer"/>.
	/// </summary>
	/// <param name="buffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>The capacity of the memory region referenced by <paramref name="buffer"/>.</returns>
	Int64 GetDirectCapacity(JBufferObject buffer);
}