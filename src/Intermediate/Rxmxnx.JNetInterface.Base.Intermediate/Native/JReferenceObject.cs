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
	public Boolean IsDefault => this.AsSpan().AsValue<IntPtr>() == IntPtr.Zero;

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	/// <param name="isProxy">Indicates whether the current instance is a proxy object.</param>
	private protected JReferenceObject(Boolean? isProxy)
	{
		this._isProxy = isProxy.GetValueOrDefault();
		this._id = isProxy.HasValue ? JReferenceObject.CreateInstanceId() : -1;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	private protected JReferenceObject(JReferenceObject jObject)
	{
		this._isProxy = jObject._isProxy;
		this._id = jObject._id != -1 ? JReferenceObject.CreateInstanceId() : -1;
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
	public IDisposable? TryGetSynchronizer() => this.IsDefault ? default : this.GetSynchronizer();

	/// <inheritdoc/>
	public override Boolean Equals(JObject? other)
	{
		if (other is null or JReferenceObject { IsDefault: true, } && this.IsDefault)
			return true;
		return other is JReferenceObject jReference && this.AsSpan().SequenceEqual(jReference.AsSpan());
	}
}