namespace Rxmxnx.JNetInterface.Native;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4035,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
public abstract partial class JNativeMemory : IEquatable<JNativeMemory>
{
	/// <inheritdoc/>
	public Boolean Equals(JNativeMemory? other) => this._adapter.Equals(other?._adapter);
	/// <inheritdoc/>
	public sealed override Boolean Equals(Object? obj) => obj is JNativeMemory other && this.Equals(other);
	/// <inheritdoc/>
	public sealed override Int32 GetHashCode() => this._adapter.GetHashCode();
}