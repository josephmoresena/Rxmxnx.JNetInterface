namespace Rxmxnx.JNetInterface.Lang;

public partial class JObject
{
    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    protected JObject() : this(JValue.Empty) { }
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Object reference.</param>
    protected JObject(JObjectLocalRef value) : this(JValue.Create(value)) { }
    /// <summary>
    /// Constructor.
    /// </summary>
    protected JObject(JObject jObject) => this._value = jObject._value;
}