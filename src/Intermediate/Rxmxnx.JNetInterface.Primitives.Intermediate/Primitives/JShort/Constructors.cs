namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JShort
{
	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort() => this._value = default;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(SByte value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Char value) => this._value = (Int16)value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Double value) => this._value = IPrimitiveNumericType.GetIntegerValue<Int16, Double>(value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Single value) => this._value = IPrimitiveNumericType.GetIntegerValue<Int16, Single>(value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Int32 value) => this._value = Unsafe.As<Int32, Int16>(ref value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Int64 value) => this._value = Unsafe.As<Int64, Int16>(ref value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JShort(Int16 value) => this._value = value;
}