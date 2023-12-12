namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a <c>java.lang.Object</c> instance.
/// </summary>
public abstract class JObject : IObject, IEquatable<JObject>
{
	/// <summary>
	/// JNI name of <c>java.lang.Object</c> class.
	/// </summary>
	public static readonly CString JObjectClassName = UnicodeClassNames.JObjectClassName;
	/// <summary>
	/// JNI signature for <c>java.lang.Object</c> object.
	/// </summary>
	public static readonly CString JObjectSignature = UnicodeObjectSignatures.JObjectSignature;

	/// <summary>
	/// Object class name.
	/// </summary>
	public abstract CString ObjectClassName { get; }
	/// <summary>
	/// Object signature.
	/// </summary>
	public abstract CString ObjectSignature { get; }

	/// <inheritdoc/>
	public abstract Boolean Equals(JObject? other);

	CString IObject.ObjectClassName => this.ObjectClassName;
	CString IObject.ObjectSignature => this.ObjectSignature;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset) => this.CopyTo(span, ref offset);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) => this.CopyTo(span, index);

	/// <inheritdoc cref="IObject.CopyTo(Span{Byte}, ref Int32)"/>
	internal abstract void CopyTo(Span<Byte> span, ref Int32 offset);
	/// <inheritdoc cref="IObject.CopyTo(Span{JValue}, Int32)"/>
	internal abstract void CopyTo(Span<JValue> span, Int32 index);

	/// <summary>
	/// Indicates whether <paramref name="jObject"/> instance is <see langword="null"/> or
	/// default value.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> instance is <see langword="null"/> or
	/// default value; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsNullOrDefault([NotNullWhen(false)] JReferenceObject? jObject)
		=> jObject is null || jObject.IsDefault;
}