namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
internal abstract partial class JPrimitiveObject : JObject
{
	/// <summary>
	/// Size of current type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected JPrimitiveObject() { }

	/// <summary>
	/// Interprets current instance as byte.
	/// </summary>
	/// <returns>Current instance as <see cref="Byte"/> value.</returns>
	public abstract Byte ToByte();

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
		=> this is IWrapper<TPrimitive> pw ? pw.Value : this.AsValue<TPrimitive>();

	/// <summary>
	/// Retrieves a <typeparamref name="TValue"/> value from current instance.
	/// </summary>
	/// <typeparam name="TValue"><see cref="ValueType"/> type.</typeparam>
	/// <returns>
	/// The equivalent <typeparamref name="TValue"/> value to current instance.
	/// </returns>
	/// <exception cref="InvalidCastException"/>
	private TValue AsValue<TValue>()
		where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
		=> this is IWrapper<TValue> vw ?
			vw.Value :
			ValidationUtilities.ThrowIfInvalidCast<TValue>(this as IConvertible);
}

/// <summary>
/// Java object representing a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of java primitive value.</typeparam>
internal sealed class JPrimitiveObject<TPrimitive> : JPrimitiveObject.Generic<TPrimitive>, IPrimitiveType,
	IEquatable<JPrimitiveObject<TPrimitive>>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IEquatable<TPrimitive>
{
	static Type IDataType.FamilyType => typeof(TPrimitive);
	static JDataTypeMetadata IDataType.Metadata => IPrimitiveType.GetMetadata<TPrimitive>();
	static JNativeType IPrimitiveType.JniType => IPrimitiveType.GetMetadata<TPrimitive>().NativeType;
	public override Int32 SizeOf => IPrimitiveType.GetMetadata<TPrimitive>().SizeOf;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Primitive value.</param>
	public JPrimitiveObject(TPrimitive value) : base(value) { }
	/// <inheritdoc cref="IEquatable{TPrimitive}"/>
	public Boolean Equals(JPrimitiveObject<TPrimitive>? other) => other is not null && this.Value.Equals(other.Value);

	public override CString ObjectClassName => IPrimitiveType.GetMetadata<TPrimitive>().ClassName;
	public override CString ObjectSignature => IPrimitiveType.GetMetadata<TPrimitive>().Signature;

	/// <inheritdoc cref="IComparable.CompareTo"/>
	public Int32 CompareTo(Object? obj) => this.Value.CompareTo(obj);

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
		=> other is JPrimitiveObject<TPrimitive> jPrimitive && this.Equals(jPrimitive);
}