namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class represents the base of any java reference type instance.
/// </summary>
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
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JReferenceObject(Boolean? isDummy = default)
	{
		this._isDummy = isDummy.GetValueOrDefault();
		this._id = isDummy.HasValue ? JReferenceObject.CreateInstanceId() : -1;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jObject"><see cref="JReferenceObject"/> instance.</param>
	internal JReferenceObject(JReferenceObject jObject)
	{
		this._isDummy = jObject._isDummy;
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