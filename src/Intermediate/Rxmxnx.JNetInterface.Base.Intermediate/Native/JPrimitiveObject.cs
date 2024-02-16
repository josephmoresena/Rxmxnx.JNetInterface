namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
internal abstract partial class JPrimitiveObject : JObject
{
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	protected abstract Int32 SizeOf { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected JPrimitiveObject() { }

	/// <summary>
	/// Interprets current instance as byte.
	/// </summary>
	/// <returns>Current instance as <see cref="Byte"/> value.</returns>
	public abstract Byte ToByte();

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	public override Int32 GetHashCode() => HashCode.Combine(Convert.ToHexString(this.AsSpan()), this.SizeOf);

	/// <summary>
	/// Retrieves a <typeparamref name="TPrimitive"/> value from current instance.
	/// </summary>
	/// <typeparam name="TPrimitive"><see cref="IPrimitiveType"/> type.</typeparam>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TPrimitive"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TPrimitive AsPrimitive<TPrimitive, TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IWrapper<TValue>, IComparable<TPrimitive>,
		IEquatable<TPrimitive>
		=> (this as IWrapper<TPrimitive>)?.Value ?? this.AsValue<TPrimitive>();
}

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of java primitive value.</typeparam>
internal sealed partial class JPrimitiveObject<TPrimitive> : JPrimitiveObject.Generic<TPrimitive>, IPrimitiveType,
	IEquatable<JPrimitiveObject<TPrimitive>>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IEquatable<TPrimitive>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	public JPrimitiveObject(TPrimitive value) : base(value) { }

	public override CString ObjectClassName => IPrimitiveType.GetMetadata<TPrimitive>().ClassName;
	public override CString ObjectSignature => IPrimitiveType.GetMetadata<TPrimitive>().Signature;

	/// <inheritdoc cref="IComparable.CompareTo"/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this.Value.GetHashCode();
}