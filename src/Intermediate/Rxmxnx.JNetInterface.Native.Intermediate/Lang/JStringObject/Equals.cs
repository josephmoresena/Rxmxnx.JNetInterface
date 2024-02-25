namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject : IEquatable<JStringObject?>, IEquatable<IWrapper<String>?>
{
	Boolean IEquatable<IWrapper<String>?>.Equals(IWrapper<String>? other) => this.Value.Equals(other?.Value);

	/// <inheritdoc/>
	public Boolean Equals(JStringObject? other) => this.Value.Equals(other?.Value);
	/// <inheritdoc/>
	public Boolean Equals(String? other) => this.Value.Equals(other);
	/// <inheritdoc cref="Object.Equals(Object?)"/>
	public override Boolean Equals(Object? obj)
		=> obj switch
		{
			JStringObject jString => this._value is not null && jString._value is not null ?
				this._value.Equals(jString._value) :
				this.Environment.IsSameObject(this, jString),
			String str => this.Value.Equals(str),
			IWrapper<String> wStr => this.Value.Equals(wStr.Value),
			_ => base.Equals(obj),
		};
}