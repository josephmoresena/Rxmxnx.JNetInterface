namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Represents a slot for managing parameters within a specific context.
/// </summary>
public interface IParameterSlot
{
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
	void SetNullValue(Byte index);
}