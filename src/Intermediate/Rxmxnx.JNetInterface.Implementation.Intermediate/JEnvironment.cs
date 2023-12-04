namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JEnvironment : IEnvironment, IEquatable<JEnvironment>, IEquatable<IEnvironment>
{
	/// <summary>
	/// <see cref="JEnvironment"/> cache.
	/// </summary>
	private readonly JEnvironmentCache _cache;

	/// <summary>
	/// Thread name.
	/// </summary>
	public virtual CString Name => CString.Zero;
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;
	/// <summary>
	/// Indicates whether current thread is daemon.
	/// </summary>
	public virtual Boolean IsDaemon => false;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal JEnvironment(IVirtualMachine vm, JEnvironmentRef envRef) => this._cache = new(vm, envRef);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(JEnvironmentCache cache) => this._cache = cache;

	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._cache.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._cache.VirtualMachine;
	/// <inheritdoc/>
	public Int32 Version => this._cache.Version;

	Int32? IEnvironment.LocalCapacity
	{
		get => this._cache.Capacity;
		set => this._cache.EnsureLocalCapacity(value.GetValueOrDefault());
	}
	IAccessProvider IEnvironment.AccessProvider => this._cache;
	IClassProvider IEnvironment.ClassProvider => this._cache;
	IReferenceProvider IEnvironment.ReferenceProvider => this._cache;
	IStringProvider IEnvironment.StringProvider => this._cache;
	IArrayProvider IEnvironment.ArrayProvider => this._cache;

	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsDummy)
			return JReferenceType.InvalidRefType;
		GetObjectRefTypeDelegate getObjectRefType = this._cache.GetDelegate<GetObjectRefTypeDelegate>();
		JReferenceType result = getObjectRefType(this._cache.Reference, jRefObj.As<JObjectLocalRef>());
		this._cache.CheckJniError();
		return result;
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		if ((jObject.IsDefault && (jOther is null || jOther.IsDefault)) || jObject.Equals(jOther))
			return true;

		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return JEnvironment.EqualEquatable(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				JEnvironment.EqualEquatable(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				JEnvironment.EqualEquatable(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ?? false;

		ValidationUtilities.ThrowIfDummy(jRefObj);
		ValidationUtilities.ThrowIfDummy(jRefOther);
		IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
		Byte result = isSameObject(this._cache.Reference, jRefObj.As<JObjectLocalRef>(), jRefOther.As<JObjectLocalRef>());
		this._cache.CheckJniError();
		return result == JBoolean.TrueValue;
	}
	TObject? IEnvironment.CreateParameterObject<TObject>(JObjectLocalRef localRef) where TObject : class
	{
		if (localRef == default) return default;
		JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)JMetadataHelper.GetMetadata<TObject>();
		using JLocalObject jLocal = new(this, localRef, false, true, this._cache.GetClass<TObject>());
		return this._cache.Register(metadata.ParseInstance(jLocal) as TObject);
	}
	JClassObject? IEnvironment.CreateParameterObject(JClassLocalRef classRef) => throw new NotImplementedException();
	JStringObject? IEnvironment.CreateParameterObject(JStringLocalRef stringRef)
		=> this._cache.Register<JStringObject>(stringRef.Value != default ? new(this, stringRef, null, false, true) : default);
	JArrayObject<TElement>? IEnvironment.CreateParameterArray<TElement>(JArrayLocalRef arrayRef)
		=> this._cache.Register<JArrayObject<TElement>>(arrayRef.Value != default ? new(this, arrayRef, null, false, true) : default);
	JObjectLocalRef IEnvironment.GetReturn(JLocalObject? jLocal) => throw new NotImplementedException();
	JClassLocalRef IEnvironment.GetReturn(JClassObject? jClass) => throw new NotImplementedException();
	JArrayLocalRef IEnvironment.GetReturn(JArrayObject? jArray) => throw new NotImplementedException();
	JThrowableLocalRef IEnvironment.GetReturn(JThrowableObject? jThrowable) => throw new NotImplementedException();
	
	Boolean IEquatable<JEnvironment>.Equals(JEnvironment? other) => other is not null && this._cache.Equals(other._cache);
	Boolean IEquatable<IEnvironment>.Equals(IEnvironment? other) => this.Reference == other?.Reference;

	/// <inheritdoc/>
	public override Boolean Equals(Object? obj) => 
		obj is JEnvironment other && this._cache.Equals(other._cache) ||
		obj is IEnvironment env && this.Reference == env.Reference;
	/// <inheritdoc/>
	public override Int32 GetHashCode() => this._cache.GetHashCode();

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <returns>The <see cref="IEnvironment"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IEnvironment GetEnvironment(JEnvironmentRef reference)
	{
		IVirtualMachine vm = JEnvironmentCache.GetVirtualMachine(reference);
		return vm.GetEnvironment(reference);
	}

	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
}