namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes an appendable property.
/// </summary>
internal interface IAppendableProperty
{
	/// <summary>
	/// Property name.
	/// </summary>
	String PropertyName { get; }

	/// <summary>
	/// Appends to <paramref name="stringBuilder"/> the value of current instance.
	/// </summary>
	/// <param name="stringBuilder">A <see cref="StringBuilder"/> instance.</param>
	void AppendValue(StringBuilder stringBuilder);
}