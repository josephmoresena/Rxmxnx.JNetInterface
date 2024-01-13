namespace Rxmxnx.JNetInterface.Native;

public partial class JLocalObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	internal static readonly JClassTypeMetadata JObjectClassMetadata = JTypeMetadataBuilder<JLocalObject>
	                                                                   .Create(JObject.JObjectClassName)
	                                                                   .WithSignature(JObject.JObjectSignature).Build();

	static JClassTypeMetadata IBaseClassType<JLocalObject>.SuperClassMetadata => JLocalObject.JObjectClassMetadata;

	/// <summary>
	/// Internal reference value.
	/// </summary>
	internal JObjectLocalRef InternalReference => base.To<JObjectLocalRef>();
	/// <inheritdoc cref="ILocalObject.Lifetime"/>
	internal ObjectLifetime Lifetime => this._lifetime;

	/// <summary>
	/// Interprets internal current value as <typeparamref name="TReference"/> value.
	/// </summary>
	/// <typeparam name="TReference">Type of value.</typeparam>
	/// <returns>A read-only reference of <typeparamref name="TReference"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal ref readonly TReference InternalAs<TReference>() where TReference : unmanaged, INativeType<TReference>
		=> ref base.As<TReference>();

	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue(JObjectLocalRef localRef) => this._lifetime.SetValue(this, localRef);
	/// <summary>
	/// Sets the current instance value.
	/// </summary>
	/// <typeparam name="TValue">Type of <see langword="IObjectReference"/> instance.</typeparam>
	/// <param name="localRef">A local object reference the value of current instance.</param>
	internal void SetValue<TValue>(TValue localRef) where TValue : unmanaged, IObjectReferenceType
		=> this._lifetime.SetValue(this, localRef);
	/// <inheritdoc/>
	internal override ReadOnlySpan<Byte> AsSpan() => this._lifetime.Span;
	/// <inheritdoc/>
	internal override ref readonly TValue As<TValue>()
	{
		JGlobalBase? jGlobal = this._lifetime.GetGlobalObject();
		if (jGlobal is not null)
			return ref jGlobal.As<TValue>();
		return ref base.As<TValue>();
	}
	/// <inheritdoc/>
	internal override TValue To<TValue>() => this._lifetime.GetGlobalObject()?.To<TValue>() ?? base.To<TValue>();
	/// <inheritdoc/>
	internal override void ClearValue() => this._lifetime.Dispose();
	/// <inheritdoc/>
	internal override IDisposable GetSynchronizer()
	{
		IEnvironment env = this._lifetime.Environment;
		return env.ReferenceFeature.GetSynchronizer(this);
	}
	/// <inheritdoc/>
	internal override Boolean IsAssignableTo<TDataType>() => this._lifetime.IsAssignableTo<TDataType>(this);
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this._lifetime.SetAssignableTo<TDataType>(isAssignable);
	/// <inheritdoc/>
	internal override Boolean IsDefaultInstance()
	{
		if (this._lifetime.GetGlobalObject() is { } jGlobal && !jGlobal.IsDefaultInstance())
			return false;
		return base.IsDefaultInstance();
	}

	/// <summary>
	/// Retrieves the loaded global object for given object.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The loaded <see cref="JGlobalBase"/> object for <paramref name="jLocal"/>.</returns>
	internal static JGlobalBase? GetGlobalObject(JLocalObject jLocal) => jLocal._lifetime.GetGlobalObject();
	/// <summary>
	/// Retrieves initial final <typaramref name="TObject"/> instance for <paramref name="localRef"/>.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IReferenceType"/> type.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="metadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>Initial <typaramref name="TObject"/> instance for <paramref name="localRef"/>.</returns>
	internal static TObject Create<TObject>(JClassObject jClass, JReferenceTypeMetadata metadata, JObjectLocalRef localRef)
		where TObject : JLocalObject, IReferenceType<TObject>
	{
		using JLocalObject jLocalTemp = new(jClass, localRef);
		return (TObject)metadata.ParseInstance(jLocalTemp);
	}
}