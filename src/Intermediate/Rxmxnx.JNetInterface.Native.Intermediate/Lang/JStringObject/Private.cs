namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Instance value.
	/// </summary>
	private String? _value;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JStringObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.StringClassObject)
		=> this._value = jLocal.Environment.StringProvider.ToString(jLocal);
}