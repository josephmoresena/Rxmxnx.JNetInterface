namespace Rxmxnx.JNetInterface.Primitives;

public readonly partial struct JByte
{
	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte() => this._value = default;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(SByte value) => this._value = value;
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Char value) => this._value = NativeUtilities.AsBytes(value).ToValue<SByte>();
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Double value) => this._value = IPrimitiveNumericType.GetIntegerValue<SByte>(value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Single value) => this._value = IPrimitiveNumericType.GetIntegerValue<SByte>(value);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Int32 value) => this._value = NativeUtilities.AsBytes(value).ToValue<SByte>();
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Int64 value) => this._value = NativeUtilities.AsBytes(value).ToValue<SByte>();
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">Internal value.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromCodeCoverage]
	public JByte(Int16 value) => this._value = NativeUtilities.AsBytes(value).ToValue<SByte>();
}