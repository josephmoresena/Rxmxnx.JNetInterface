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
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;

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

	Int32? IEnvironment.EnsuredCapacity => throw new NotImplementedException();
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

		if (jObject is JReferenceObject jRefObj && jOther is JReferenceObject jRefOther)
		{
			ValidationUtilities.ThrowIfDummy(jRefObj);
			ValidationUtilities.ThrowIfDummy(jRefOther);
			IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
			return isSameObject(this._cache.Reference, jRefObj.As<JObjectLocalRef>(),
			                    jRefOther.As<JObjectLocalRef>()) == JBoolean.TrueValue;
		}

		return JEnvironment.Equals(jObject as IEquatable<IPrimitiveType>, jOther as IPrimitiveType) ??
			JEnvironment.Equals(jOther as IEquatable<IPrimitiveType>, jObject as IPrimitiveType) ??
			JEnvironment.Equals(jObject as IEquatable<JPrimitiveObject>, jOther as JPrimitiveObject) ??
			JEnvironment.Equals(jOther as IEquatable<JPrimitiveObject>, jObject as JPrimitiveObject) ?? false;
	}
	TObject IEnvironment.CreateParameterObject<TObject>(JObjectLocalRef objRef) => throw new NotImplementedException();
	JClassObject IEnvironment.CreateParameterObject(JClassLocalRef classRef) => throw new NotImplementedException();
	JStringObject IEnvironment.CreateParameterObject(JStringLocalRef stringRef) => throw new NotImplementedException();
	JArrayObject<TElement> IEnvironment.CreateParameterArray<TElement>(JArrayLocalRef arrayRef)
		=> throw new NotImplementedException();

	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
	private static Boolean? Equals<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
}