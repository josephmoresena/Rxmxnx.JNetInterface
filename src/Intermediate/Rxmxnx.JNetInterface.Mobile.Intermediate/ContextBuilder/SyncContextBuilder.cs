namespace Rxmxnx.JNetInterface.ContextBuilder;

/// <summary>
/// Builder type for initialize a sync JNI context.
/// </summary>
public ref partial struct SyncContextBuilder()
{
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	[UnscopedRef]
	public SyncContextBuilder With(ReadOnlySpan<JBoolean> values)
	{
		this._booleans = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JBoolean[] values)
	{
		this._booleans = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Boolean> values)
	{
		this._booleans = MemoryMarshal.Cast<Boolean, JBoolean>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Boolean[] values)
	{
		this._booleans = MemoryMarshal.Cast<Boolean, JBoolean>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JByte> values)
	{
		this._bytes = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JByte[] values)
	{
		this._bytes = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<SByte> values)
	{
		this._bytes = MemoryMarshal.Cast<SByte, JByte>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params SByte[] values)
	{
		this._bytes = MemoryMarshal.Cast<SByte, JByte>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JChar> values)
	{
		this._chars = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JChar[] values)
	{
		this._chars = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Char> values)
	{
		this._chars = MemoryMarshal.Cast<Char, JChar>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Char[] values)
	{
		this._chars = MemoryMarshal.Cast<Char, JChar>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JDouble> values)
	{
		this._doubles = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JDouble[] values)
	{
		this._doubles = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Double> values)
	{
		this._doubles = MemoryMarshal.Cast<Double, JDouble>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Double[] values)
	{
		this._doubles = MemoryMarshal.Cast<Double, JDouble>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JFloat> values)
	{
		this._floats = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JFloat[] values)
	{
		this._floats = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Single> values)
	{
		this._floats = MemoryMarshal.Cast<Single, JFloat>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Single[] values)
	{
		this._floats = MemoryMarshal.Cast<Single, JFloat>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JInt> values)
	{
		this._ints = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JInt[] values)
	{
		this._ints = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Int32> values)
	{
		this._ints = MemoryMarshal.Cast<Int32, JInt>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Int32[] values)
	{
		this._ints = MemoryMarshal.Cast<Int32, JInt>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JLong> values)
	{
		this._longs = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JLong[] values)
	{
		this._longs = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Int64> values)
	{
		this._longs = MemoryMarshal.Cast<Int64, JLong>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Int64[] values)
	{
		this._longs = MemoryMarshal.Cast<Int64, JLong>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<JShort> values)
	{
		this._shorts = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params JShort[] values)
	{
		this._shorts = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<Int16> values)
	{
		this._shorts = MemoryMarshal.Cast<Int16, JShort>(values);
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params Int16[] values)
	{
		this._shorts = MemoryMarshal.Cast<Int16, JShort>(values.AsSpan());
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Read-only span of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(ReadOnlySpan<IJavaPeerable?> values)
	{
		this._objects = values;
		return this;
	}
	/// <summary>
	/// Sets the parameters for de building contexts.
	/// </summary>
	/// <param name="values">Array of primitive values.</param>
	/// <returns>Builder instance.</returns>
	/// <remarks>
	/// When this method is called, any previously passed values of the same type will be replaced by the values
	/// provided in the current call.
	/// </remarks>
	public SyncContextBuilder With(params IJavaPeerable?[] values)
	{
		this._objects = values;
		return this;
	}
}