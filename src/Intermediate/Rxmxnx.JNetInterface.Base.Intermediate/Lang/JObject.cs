namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a <c>java.lang.Object</c> instance.
/// </summary>
public abstract partial class JObject : IEquatable<JObject>
{
	/// <summary>
	/// Object signature.
	/// </summary>
	public abstract CString ObjectClassName { get; }
	/// <summary>
	/// Object signature.
	/// </summary>
	public abstract CString ObjectSignature { get; }

    /// <inheritdoc/>
    public virtual Boolean Equals(JObject? other) => this._value.Equals(other?._value);

	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._value.Value.GetHashCode();
	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => obj is JObject jObj && this.Equals(jObj);
}