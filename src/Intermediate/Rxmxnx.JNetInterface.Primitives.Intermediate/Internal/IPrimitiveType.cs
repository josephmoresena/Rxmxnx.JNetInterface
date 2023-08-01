namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an object that represents a java primitive value.
/// </summary>
/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
internal partial interface IPrimitiveType<TPrimitive, TValue> : IPrimitiveType<TPrimitive>, IPrimitiveWrapper<TValue>
	where TPrimitive : unmanaged, IPrimitiveType<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>
	where TValue : unmanaged, IComparable, IConvertible, IComparable<TValue>, IEquatable<TValue>
{
	/// <inheritdoc cref="IDataType.ExcludingTypes"/>
	internal static readonly ImmutableHashSet<Type> ExcludingPrimitiveTypes =
		IDataType.ExcludingBasicTypes.Union(new[]
		{
			typeof(IPrimitiveType<TPrimitive, TValue>),
			typeof(IPrimitiveWrapper<TValue>),
			typeof(JPrimitiveObject<TPrimitive>),
		});

	static IImmutableSet<Type> IDataType.ExcludingTypes => IPrimitiveType<TPrimitive, TValue>.ExcludingPrimitiveTypes;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<Byte> span, ref Int32 offset)
	{
		ref TValue refValue = ref Unsafe.AsRef(this.Value);
		refValue.AsBytes().CopyTo(span[offset..]);
		offset += IDataType.GetMetadata<TPrimitive>().SizeOf;
	}
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IObject.CopyTo(Span<JValue> span, Int32 index) => span[index] = JValue.Create(this.Value);

	/// <summary>
	/// Defines an implicit conversion of a given <typeparamref name="TValue"/> to <typeparamref name="TPrimitive"/>.
	/// </summary>
	/// <param name="value">A <typeparamref name="TValue"/> to implicitly convert.</param>
	static abstract implicit operator TPrimitive(TValue value);
}