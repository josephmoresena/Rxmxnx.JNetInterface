// ReSharper disable HeapView.BoxingAllocation

namespace Rxmxnx.JNetInterface.Types;

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
	private protected static Int32 Compare(TPrimitive primitive, Object? obj)
		=> obj switch
		{
			TPrimitive p => primitive.Value.CompareTo(p.Value),
			TValue v => primitive.Value.CompareTo(v),
			JByte b => b.Value.CompareTo(primitive.ToSByte(CultureInfo.InvariantCulture)),
			JChar c => c.Value.CompareTo(primitive.ToChar(CultureInfo.InvariantCulture)),
			JDouble d => d.Value.CompareTo(primitive.ToDouble(CultureInfo.InvariantCulture)),
			JFloat f => f.Value.CompareTo(primitive.ToSingle(CultureInfo.InvariantCulture)),
			JInt i => i.Value.CompareTo(primitive.ToInt32(CultureInfo.InvariantCulture)),
			JLong x => x.Value.CompareTo(primitive.ToInt64(CultureInfo.InvariantCulture)),
			JShort s => s.Value.CompareTo(primitive.ToInt16(CultureInfo.InvariantCulture)),
			IBase<TPrimitive> wp => primitive.Value.CompareTo(wp.Value.Value),
			IBase<TValue> wv => primitive.Value.CompareTo(wv.Value),
			IComparable<TPrimitive> cp => -cp.CompareTo(primitive),
			IComparable<TValue> cv => -cv.CompareTo(primitive.Value),
			IComparable<JByte> bc => bc.CompareTo(primitive.ToSByte(CultureInfo.InvariantCulture)),
			IComparable<JChar> cc => cc.CompareTo(primitive.ToChar(CultureInfo.InvariantCulture)),
			IComparable<JDouble> dc => dc.CompareTo(primitive.ToDouble(CultureInfo.InvariantCulture)),
			IComparable<JFloat> fc => fc.CompareTo(primitive.ToSingle(CultureInfo.InvariantCulture)),
			IComparable<JInt> ic => ic.CompareTo(primitive.ToInt32(CultureInfo.InvariantCulture)),
			IComparable<JLong> xc => xc.CompareTo(primitive.ToInt64(CultureInfo.InvariantCulture)),
			IComparable<JShort> sc => sc.CompareTo(primitive.ToInt16(CultureInfo.InvariantCulture)),
			IComparable c => -c.CompareTo(primitive.Value),
			IBase<IComparable> wc => wc.Value.CompareTo(primitive.Value),
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
	private protected static Boolean Equals(TPrimitive primitive, Object? obj)
	{
		try
		{
			return obj switch
			{
				TPrimitive p => primitive.Value.Equals(p.Value),
				TValue v => primitive.Value.Equals(v),
				JBoolean z => primitive is JBoolean zz && zz == z,
				JByte b => b.Value.Equals(primitive.ToSByte(CultureInfo.InvariantCulture)),
				JChar c => c.Value.Equals(primitive.ToChar(CultureInfo.InvariantCulture)),
				JDouble d => d.Value.Equals(primitive.ToDouble(CultureInfo.InvariantCulture)),
				JFloat f => f.Value.Equals(primitive.ToSingle(CultureInfo.InvariantCulture)),
				JInt i => i.Value.Equals(primitive.ToInt32(CultureInfo.InvariantCulture)),
				JLong x => x.Value.Equals(primitive.ToInt64(CultureInfo.InvariantCulture)),
				JShort s => s.Value.Equals(primitive.ToInt16(CultureInfo.InvariantCulture)),
				Boolean bl => primitive is JBoolean z && z.Value == bl,
				IBase<TValue> wv => primitive.Value.Equals(wv.Value),
				IBase<TPrimitive> wp => primitive.Value.Equals(wp.Value.Value),
				IEquatable<TPrimitive> ep => ep.Equals(primitive),
				IEquatable<TValue> ev => ev.Equals(primitive.Value),
				IComparable<TPrimitive> cp => -cp.CompareTo(primitive) == 0,
				IComparable<TValue> cv => -cv.CompareTo(primitive.Value) == 0,
				IBase<JBoolean> z => primitive is JBoolean zz && zz == z.Value,
				IBase<JByte> b => b.Value.Value.Equals(primitive.ToSByte(CultureInfo.InvariantCulture)),
				IBase<JChar> c => c.Value.Value.Equals(primitive.ToChar(CultureInfo.InvariantCulture)),
				IBase<JDouble> d => d.Value.Value.Equals(primitive.ToDouble(CultureInfo.InvariantCulture)),
				IBase<JFloat> f => f.Value.Value.Equals(primitive.ToSingle(CultureInfo.InvariantCulture)),
				IBase<JInt> i => i.Value.Value.Equals(primitive.ToInt32(CultureInfo.InvariantCulture)),
				IBase<JLong> x => x.Value.Value.Equals(primitive.ToInt64(CultureInfo.InvariantCulture)),
				IBase<JShort> s => s.Value.Value.Equals(primitive.ToInt16(CultureInfo.InvariantCulture)),
				IEquatable<JByte> be => be.Equals(primitive.ToSByte(CultureInfo.InvariantCulture)),
				IEquatable<JChar> ce => ce.Equals(primitive.ToChar(CultureInfo.InvariantCulture)),
				IEquatable<JDouble> de => de.Equals(primitive.ToDouble(CultureInfo.InvariantCulture)),
				IEquatable<JFloat> fe => fe.Equals(primitive.ToSingle(CultureInfo.InvariantCulture)),
				IEquatable<JInt> ie => ie.Equals(primitive.ToInt32(CultureInfo.InvariantCulture)),
				IEquatable<JLong> xe => xe.Equals(primitive.ToInt64(CultureInfo.InvariantCulture)),
				IEquatable<JShort> se => se.Equals(primitive.ToInt16(CultureInfo.InvariantCulture)),
				IBase<Boolean> wbl => primitive is JBoolean z && z.Value == wbl.Value,
				IComparable c => c.CompareTo(primitive.Value) == 0,
				IBase<IComparable> wc => wc.Value.CompareTo(primitive.Value) == 0,
				_ => primitive.Value.Equals(obj),
			};
		}
		catch (Exception)
		{
			// Unconvertible
			return false;
		}
	}
}