namespace Rxmxnx.JNetInterface.Internal;

internal partial interface IPrimitiveType<TPrimitive, TValue>
{
	/// <summary>
	/// Compares the <paramref name="primitive"/> with another object and returns an integer that indicates whether
	/// <paramref name="primitive"/> precedes, follows, or occurs in the same position in the sort order as the
	/// other object.
	/// </summary>
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
	public static Int32 Compare(TPrimitive primitive, Object? obj)
		=> obj switch
		{
			TPrimitive p => primitive.CompareTo(p),
			TValue v => primitive.Value.CompareTo(v),
			IWrapper<TPrimitive> wp => primitive.CompareTo(wp.Value),
			IComparable<TPrimitive> cp => -cp.CompareTo(primitive),
			IWrapper<TValue> wv => primitive.CompareTo(wv.Value),
			IComparable<TValue> cv => -cv.CompareTo(primitive.Value),
			IPrimitiveType ip => -ip.CompareTo(primitive.Value),
			IComparable c => -c.CompareTo(primitive.Value),
			_ => primitive.Value.CompareTo(obj),
		};
	/// <summary>
	/// Indicates whether <paramref name="primitive"/> and a specified object are equal.
	/// </summary>
	/// <param name="primitive">A primitive instance.</param>
	/// <param name="obj">An object to compare with <paramref name="primitive"/>.</param>
	/// <returns>
	/// <see langword="true"/> if the current object is equal to the other parameter; otherwise, <see langword="false"/>.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Boolean Equals(TPrimitive primitive, Object? obj)
		=> obj switch
		{
			TPrimitive p => primitive.Equals(p),
			TValue v => primitive.Value.Equals(v),
			IWrapper<TPrimitive> wp => primitive.Equals(wp.Value),
			IEquatable<TPrimitive> ep => ep.Equals(primitive),
			IWrapper<TValue> wv => primitive.Equals(wv.Value),
			IEquatable<TValue> ev => ev.Equals(primitive.Value),
			IPrimitiveType ip => ip.CompareTo(primitive.Value) == 0,
			_ => primitive.Value.Equals(obj),
		};
}