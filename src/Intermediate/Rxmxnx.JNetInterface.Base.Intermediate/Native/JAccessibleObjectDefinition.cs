namespace Rxmxnx.JNetInterface.Native;

/// <summary>
///     This class stores a <c>java.lang.Class&lt;?&gt;</c> accesible object definition.
/// </summary>
public abstract record JAccessibleObjectDefinition
{
	/// <summary>
	///     Internal <see cref="CStringSequence"/> containing the name and descriptor of accessible object.
	/// </summary>
	private readonly CStringSequence _sequence;

	/// <summary>
	///     Constructor.
	/// </summary>
	/// <param name="sequence">
	///     <see cref="CStringSequence"/> containing the name and descriptor of the method.
	/// </param>
	internal JAccessibleObjectDefinition(CStringSequence sequence)
	{
		this._sequence = sequence;
	}

	/// <summary>
	///     Accessible object information.
	/// </summary>
	internal CStringSequence Information => this._sequence;
	/// <summary>
	///     The format used for <see cref="JAccessibleObjectDefinition.ToString()"/> method.
	/// </summary>
	internal abstract String ToStringFormat { get; }

	/// <inheritdoc/>
	public override String ToString()
	{
		return String.Format(this.ToStringFormat, this._sequence[0], this._sequence[1]);
	}
	/// <inheritdoc/>
	public override Int32 GetHashCode()
	{
		return this._sequence.GetHashCode();
	}

	/// <summary>
	///     Retrieves the return type from <typeparamref name="TReturn"/>.
	/// </summary>
	/// <typeparam name="TReturn"><see cref="IDataType"/> type.</typeparam>
	/// <returns>Type of return <typeparamref name="TReturn"/> type.</returns>
	protected static Type GetReturnType<TReturn>() where TReturn : IDataType
	{
		return TReturn.PrimitiveMetadata?.Type ?? typeof(TReturn);
	}
}