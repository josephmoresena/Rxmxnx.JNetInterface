namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// Function name of <c>java.lang.Throwable.getMessage().</c>
	/// </summary>
	private static readonly CString getMessageName = new(() => "getMessage"u8);

	/// <summary>
	/// Internal throwable message.
	/// </summary>
	private String? _message;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JThrowableObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out JObjectMetadata metadata), jClass)
	{
		if (metadata is JThrowableObjectMetadata throwableMetadata)
			this._message ??= throwableMetadata.Message;
	}

	/// <summary>
	/// Retrieves the throwable message.
	/// </summary>
	/// <returns>Throwable message.</returns>
	private String GetMessage()
	{
		JFunctionDefinition<JStringObject> definition = new(JThrowableObject.getMessageName);
		using JStringObject jString = definition.Invoke(this)!;
		return jString.Value;
	}
}