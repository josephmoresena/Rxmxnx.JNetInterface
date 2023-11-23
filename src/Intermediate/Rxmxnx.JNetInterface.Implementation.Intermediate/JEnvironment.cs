namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class implements <see cref="IVirtualMachine"/> interface.
/// </summary>
public partial class JEnvironment : IEnvironment
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
	IEnumProvider IEnvironment.EnumProvider => this._cache;
	IArrayProvider IEnvironment.ArrayProvider => this._cache;

	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		if (jObject is not JReferenceObject jRefObj || jRefObj.IsDefault || jRefObj.IsDummy)
			return JReferenceType.InvalidRefType;
		GetObjectRefTypeDelegate getObjectRefType = this._cache.GetDelegate<GetObjectRefTypeDelegate>();
		return getObjectRefType(this._cache.Reference, jRefObj.As<JObjectLocalRef>());
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		if ((jObject.IsDefault && (jOther is null || jOther.IsDefault)) || jObject.Equals(jOther))
			return true;

		if (jObject is not JReferenceObject jRefObj || jOther is not JReferenceObject jRefOther)
			return JEnvironment.Equals(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
				JEnvironment.Equals(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
				JEnvironment.Equals(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
				JEnvironment.Equals(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ?? false;

		ValidationUtilities.ThrowIfDummy(jRefObj);
		ValidationUtilities.ThrowIfDummy(jRefOther);
		IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
		return isSameObject(this._cache.Reference, jRefObj.As<JObjectLocalRef>(), jRefOther.As<JObjectLocalRef>()) ==
			JBoolean.TrueValue;
	}
	TObject IEnvironment.CreateParameterObject<TObject>(JObjectLocalRef localRef)
	{
		using JLocalObject jLocal = new(this, localRef, false, true, this._cache.GetClass<TObject>());
		jLocal.SetAssignableTo<TObject>();
		return (TObject)IReferenceType.GetMetadata<TObject>().ParseInstance(jLocal);
	}
	JClassObject IEnvironment.CreateParameterObject(JClassLocalRef classRef) => throw new NotImplementedException();
	JStringObject IEnvironment.CreateParameterObject(JStringLocalRef stringRef)
		=> new(this, stringRef, null, false, true);
	JArrayObject<TElement> IEnvironment.CreateParameterArray<TElement>(JArrayLocalRef arrayRef)
		=> new(this, arrayRef, null, false, true);

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
	private static Boolean? Equals<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
}