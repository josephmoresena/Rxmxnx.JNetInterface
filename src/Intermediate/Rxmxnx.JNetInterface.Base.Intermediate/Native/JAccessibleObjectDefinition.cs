namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a <c>java.lang.Class&lt;?&gt;</c> accesible object definition.
/// </summary>
public abstract partial record JAccessibleObjectDefinition
{
	/// <inheritdoc/>
	public override String ToString() => String.Format(this.ToStringFormat, this._sequence[0], this._sequence[1]);
    /// <inheritdoc/>
    public override Int32 GetHashCode() => this._sequence.GetHashCode();
}
