namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents the base of any java reference type instance.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JReferenceObject : JObject
{
	/// <summary>
	/// Indicates whether current instance is default value.
	/// </summary>
	public Boolean IsDefault => this.IsBlankSpan();

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	/// <param name="isProxy">Indicates whether the current instance is a proxy object.</param>
	private protected JReferenceObject(Boolean isProxy)
	{
		this._isProxy = isProxy;
		this._id = JReferenceObject.GetInstanceId(this);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	private protected JReferenceObject(JReferenceObject jObject)
	{
		this._isProxy = jObject._isProxy;
		this._id = JReferenceObject.GetInstanceId(this, jObject);
	}

	/// <summary>
	/// Indicates whether current instance is an instance of <typeparamref name="TDataType"/> type class.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current instance is an instance of <typeparamref name="TDataType"/>
	/// type class; otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean InstanceOf<TDataType>() where TDataType : JReferenceObject, IDataType<TDataType>
		=> this.IsInstanceOf<TDataType>();

	/// <summary>
	/// Tries to obtain a synchronized instance for current instance.
	/// </summary>
	/// <returns>A <see cref="IDisposable"/> synchronized</returns>
	public IDisposable? Synchronize() => this.IsDefault ? default : this.GetSynchronizer();

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
		=> other switch
		{
			null => this.IsDefault,
			JReferenceObject jObject => this.IsProxy == jObject.IsProxy && this.IsDefault == jObject.IsDefault &&
				this.AsSpan().SequenceEqual(jObject.AsSpan()),
			_ => false,
		};
}