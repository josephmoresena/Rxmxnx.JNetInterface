﻿namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a <c>java.lang.Object</c> instance.
/// </summary>
public abstract class JObject : IObject, IEquatable<JObject>
{
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
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override Boolean Equals(Object? obj) => obj is JObject other && this.Equals(other);
	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override Int32 GetHashCode() => HashCode.Combine(this.ObjectClassName, this.ObjectSignature);

	/// <summary>
	/// Determines whether a specified <see cref="JObject"/> and a <see cref="JObject"/> instance
	/// have the same value.
	/// </summary>
	/// <param name="left">The <see cref="JObject"/> to compare.</param>
	/// <param name="right">The <see cref="JObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is the same as the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator ==(JObject? left, JObject? right) => left?.Equals(right) ?? right is null;
	/// <summary>
	/// Determines whether a specified <see cref="JObject"/> and a <see cref="JObject"/> instance
	/// have different values.
	/// </summary>
	/// <param name="left">The <see cref="JObject"/> to compare.</param>
	/// <param name="right">The <see cref="JObject"/> to compare.</param>
	/// <returns>
	/// <see langword="true"/> if the value of <paramref name="left"/> is different from the value
	/// of <paramref name="right"/>; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean operator !=(JObject? left, JObject? right) => !(left == right);

	/// <inheritdoc cref="IObject.CopyTo(Span{Byte}, ref Int32)"/>
	private protected abstract void CopyTo(Span<Byte> span, ref Int32 offset);
	/// <inheritdoc cref="IObject.CopyTo(Span{JValue}, Int32)"/>
	private protected abstract void CopyTo(Span<JValue> span, Int32 index);

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
		=> jObject is null || jObject.IsDefaultInstance();
}