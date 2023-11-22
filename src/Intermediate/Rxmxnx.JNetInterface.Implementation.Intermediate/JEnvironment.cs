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
	private Int32? _ensuredCapacity;

	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	public virtual Boolean IsDisposable => false;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="vm">A <see cref="IVirtualMachine"/> instance.</param>
	/// <param name="reference">A <see cref="JEnvironmentRef"/> reference.</param>
	internal JEnvironment(IVirtualMachine vm, JEnvironmentRef reference) => this._cache = new(vm, reference);
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="cache">A <see cref="JEnvironment"/> reference.</param>
	private JEnvironment(JEnvironmentCache cache) => this._cache = cache;

	/// <inheritdoc/>
	public JEnvironmentRef Reference => this._cache.Reference;
	/// <inheritdoc/>
	public IVirtualMachine VirtualMachine => this._cache.VirtualMachine;

	Int32? IEnvironment.EnsuredCapacity => this._ensuredCapacity;
	IAccessProvider IEnvironment.AccessProvider => this._cache;
	IClassProvider IEnvironment.ClassProvider => this._cache;
	IReferenceProvider IEnvironment.ReferenceProvider => this._cache;
	IStringProvider IEnvironment.StringProvider => this._cache;
	IEnumProvider IEnvironment.EnumProvider => this._cache;
	IArrayProvider IEnvironment.ArrayProvider => this._cache;

	JReferenceType IEnvironment.GetReferenceType(JObject jObject) => throw new NotImplementedException();
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther) => throw new NotImplementedException();
	TObject IEnvironment.CreateParameterObject<TObject>(JObjectLocalRef objRef) => throw new NotImplementedException();
	JClassObject IEnvironment.CreateParameterObject(JClassLocalRef classRef) => throw new NotImplementedException();
	JStringObject IEnvironment.CreateParameterObject(JStringLocalRef stringRef) => throw new NotImplementedException();
	JArrayObject<TElement> IEnvironment.CreateParameterObject<TElement>(JArrayLocalRef arrayRef)
		=> throw new NotImplementedException();
}