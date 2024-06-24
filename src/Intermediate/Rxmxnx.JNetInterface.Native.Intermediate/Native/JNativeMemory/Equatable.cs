namespace Rxmxnx.JNetInterface.Native;

public abstract partial class JNativeMemory : IEquatable<JNativeMemory>
{
	/// <inheritdoc/>
	public Boolean Equals(JNativeMemory? other) => this._adapter.Equals(other?._adapter);
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JNativeMemory other && this.Equals(other);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._adapter.GetHashCode();
}