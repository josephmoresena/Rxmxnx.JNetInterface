namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Fully qualified class name.
	/// </summary>
	private CString? _className;
	/// <summary>
	/// JNI signature for an object of current instance.
	/// </summary>
	private String? _hash;
	/// <summary>
	/// Indicates whether current class is final.
	/// </summary>
	private Boolean? _isFinal;
	/// <summary>
	/// JNI signature for an object of current instance.
	/// </summary>
	private CString? _signature;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JClassObject(JLocalObject jLocal) : base(jLocal, jLocal.Environment.ClassProvider.ClassObject)
		=> this.Initialize(jLocal as IClass);

	/// <summary>
	/// Loads class info.
	/// </summary>
	private void LoadClassInfo()
	{
		if (this._className is null || this._signature is null)
			this.Environment.ClassProvider.GetClassInfo(this, out this._className, out this._signature, out this._hash);
	}

	/// <summary>
	/// Initialize class properties from a <see cref="IClass"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="IClass"/> instance.</param>
	private void Initialize(IClass? jClass)
	{
		if (jClass is null)
			return;

		this._className = jClass.Name;
		this._signature = jClass.ClassSignature;
		this._hash = jClass.Hash;
		this._isFinal = jClass.IsFinal;
	}
}