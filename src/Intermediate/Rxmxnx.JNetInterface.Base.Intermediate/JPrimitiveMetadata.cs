namespace Rxmxnx.JNetInterface;

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
public abstract record JPrimitiveMetadata
{
	/// <summary>
	/// JNI signature for an array of current primitive type.
	/// </summary>
	public abstract CString ArraySignature { get; }
	/// <summary>
	/// Size of current primitive type in bytes.
	/// </summary>
	public abstract Int32 SizeOf { get; }
	/// <summary>
	/// Managed type of internal value of <see cref="IPrimitive"/>.
	/// </summary>
	public abstract Type Type { get; }

	/// <summary>
	/// Compares the <paramref name="primitive"/> with another object and returns an integer that indicates whether
	/// <paramref name="primitive"/> precedes, follows, or occurs in the same position in the sort order as the
	/// other object.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of JNI primitive structure.</typeparam>
	/// <typeparam name="TValue">Type of the .NET equivalent structure.</typeparam>
	/// <param name="primitive">A primitive instance.</param>
	/// <param name="obj">An object to compare with <paramref name="primitive"/>.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// The return value has these meanings:
	/// <list type="table">
	///     <listheader>
	///         <term>Value</term><description>Condition</description>
	///     </listheader>
	///     <item>
	///         <term>Less than zero</term>
	///         <description><paramref name="primitive"/> precedes <paramref name="obj"/> in the sort order.</description>
	///     </item>
	///     <item>
	///         <term>Zero</term>
	///         <description>
	///         <paramref name="primitive"/> occurs in the same position in the sort order as <paramref name="obj"/>.
	///         </description>
	///     </item>
	///     <item>
	///         <term>Greater than zero</term>
	///         <description><paramref name="primitive"/> follows <paramref name="obj"/> in the sort order.</description>
	///     </item>
	/// </list>
	/// </returns>
	/// <exception cref="ArgumentException"><paramref name="obj"/> is not the same type as this instance.</exception>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static Int32 CompareTo<TPrimitive, TValue>(TPrimitive primitive, Object? obj)
		where TPrimitive : unmanaged, IPrimitive<TPrimitive, TValue>, IComparable<TPrimitive>, IEquatable<TPrimitive>,
		IComparable<TValue>
		where TValue : unmanaged, IComparable<TValue>, IEquatable<TValue>, IConvertible, IComparable
		=> obj switch
		{
			IWrapper<Boolean> w => primitive.CompareTo(w.Value),
			IComparable c => -c.CompareTo(primitive.Value),
			_ => primitive.Value.CompareTo(obj),
		};
}

/// <summary>
/// This record stores the metadata for a value <see cref="IPrimitive"/> type.
/// </summary>
/// <typeparam name="TPrimitive">Type of <see cref="IPrimitive"/>.</typeparam>
internal sealed record JPrimitiveMetadata<TPrimitive> : JPrimitiveMetadata where TPrimitive : unmanaged, IPrimitive
{
	public override CString ArraySignature => TPrimitive.ArraySignature;
	public override Int32 SizeOf => NativeUtilities.SizeOf<TPrimitive>();
	public override Type Type => typeof(TPrimitive);
}