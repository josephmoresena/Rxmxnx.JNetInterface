namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JInt
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
	public JInt() => this._value = default;
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
	public JInt(SByte value) => this._value = value;
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
	public JInt(Char value) => this._value = value;
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
	public JInt(Double value) => this._value = IPrimitiveNumericType.GetIntegerValue<Int32, Double>(value);
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
	public JInt(Single value) => this._value = IPrimitiveNumericType.GetIntegerValue<Int32, Single>(value);
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
	public JInt(Int32 value) => this._value = value;
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
	public JInt(Int64 value) => this._value = NativeUtilities.AsBytes(value).ToValue<Int32>();
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
	public JInt(Int16 value) => this._value = value;
}