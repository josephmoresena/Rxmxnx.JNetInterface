namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JDouble
{
	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble() => this._value = default;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(SByte value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Char value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Double value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Single value) => this._value = IPrimitiveNumericType.GetSingleValue(value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Int32 value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Int64 value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JDouble(Int16 value) => this._value = value;
}