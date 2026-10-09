namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Represents a slot for managing parameters within a specific context.
/// </summary>
public interface IParameterSlot
{
	/// <summary>
	/// Sets the values of the parameters starting from the specified index.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IObject"/> type of the values.</typeparam>
	/// <param name="index">The zero-based index at which to start setting the parameter values.</param>
	/// <param name="values">
	/// A span containing the values to be set. Each value must implement the <see cref="IObject"/>
	/// interface.
	/// </param>
	void SetParameterValues<TObject>(Byte index, ReadOnlySpan<TObject?> values) where TObject : IObject
	{
		foreach (TObject? value in values)
			this.SetParameterValue(index++, value);
	}
	/// <summary>
	/// Sets the value of the parameter at the specified index.
	/// </summary>
	/// <typeparam name="TObject">The <see cref="IObject"/> type of the value.</typeparam>
	/// <param name="index">The zero-based index of the parameter to be set.</param>
	/// <param name="value">The value to assign to the parameter. The value must implement the <see cref="IObject"/> interface.</param>
	void SetParameterValue<TObject>(Byte index, TObject? value) where TObject : IObject;
	/// <summary>
	/// Sets the value of the parameter at the specified index to null.
	/// </summary>
	/// <param name="index">The zero-based index of the parameter to be set to null.</param>
	void SetParameterNull(Byte index);
}