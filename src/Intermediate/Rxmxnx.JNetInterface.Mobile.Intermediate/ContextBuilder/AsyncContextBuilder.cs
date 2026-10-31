namespace Rxmxnx.JNetInterface.ContextBuilder;

/// <summary>
/// Builder type for initialize an async JNI context.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public partial struct AsyncContextBuilder()
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="threadName">Thread creation name.</param>
	/// <param name="threadGroup">Thread creation group.</param>
	internal AsyncContextBuilder(CString? threadName, JGlobalBase? threadGroup) : this()
	{
		this._threadName = threadName;
		this._threadGroup = threadGroup;
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
	public AsyncContextBuilder With(params JBoolean[] values)
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
	public AsyncContextBuilder With(params Boolean[] values)
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
	public AsyncContextBuilder With(params JByte[] values)
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
	public AsyncContextBuilder With(params SByte[] values)
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
	public AsyncContextBuilder With(params JChar[] values)
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
	public AsyncContextBuilder With(params Char[] values)
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
	public AsyncContextBuilder With(params JDouble[] values)
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
	public AsyncContextBuilder With(params Double[] values)
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
	public AsyncContextBuilder With(params JFloat[] values)
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
	public AsyncContextBuilder With(params Single[] values)
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
	public AsyncContextBuilder With(params JInt[] values)
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
	public AsyncContextBuilder With(params Int32[] values)
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
	public AsyncContextBuilder With(params JLong[] values)
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
	public AsyncContextBuilder With(params Int64[] values)
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
	public AsyncContextBuilder With(params JShort[] values)
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
	public AsyncContextBuilder With(params Int16[] values)
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
	public AsyncContextBuilder With(params IJavaPeerable?[] values)
	{
		this._objects = values;
		return this;
	}
}