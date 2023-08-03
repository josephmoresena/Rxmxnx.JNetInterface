namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly JClassTypeMetadata JObjectClassMetadata = JTypeMetadataBuilder<JLocalObject>
	                                                               .Create(JObject.JObjectClassName)
	                                                               .WithSignature(JObject.JObjectSignature).Build();

	/// <summary>
	/// Internal reference value.
	/// </summary>
	internal JObjectLocalRef InternalReference => base.To<JObjectLocalRef>();
	/// <summary>
	/// Internal value.
	/// </summary>
	internal JValue InternalValue => base.Value;
	/// <inheritdoc/>
	internal override JValue Value => this.GetGlobalObject()?.Value ?? base.Value;

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue(JObjectLocalRef localRef)
	{
		if (localRef == default)
			return;
		base.SetValue(localRef);
		this._lifetime.Load(this);
	}
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue<TValue>(TValue localRef) where TValue : unmanaged, IObjectReferenceType
	{
		if (localRef.Equals(default))
			return;
		base.SetValue(localRef);
		this._lifetime.Load(this);
	}

	/// <inheritdoc/>
	internal override ref readonly TValue As<TValue>()
	{
		JGlobalBase? jGlobal = this.GetGlobalObject();
		if (jGlobal is not null)
			return ref jGlobal.As<TValue>();
		return ref base.As<TValue>();
	}
	/// <inheritdoc/>
	internal override TValue To<TValue>() => this.GetGlobalObject()?.To<TValue>() ?? base.To<TValue>();

	/// <summary>
	/// Retrieves the loaded global object for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for <paramref name="jLocal"/>.</returns>
	internal static JGlobalBase? GetGlobalObject(JLocalObject jLocal) => jLocal.GetGlobalObject();
	/// <summary>
	/// Retrieves the metadata for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The object metadata for <paramref name="jLocal"/>.</returns>
	internal static JObjectMetadata CreateMetadata(JLocalObject jLocal) => jLocal.CreateMetadata();
}