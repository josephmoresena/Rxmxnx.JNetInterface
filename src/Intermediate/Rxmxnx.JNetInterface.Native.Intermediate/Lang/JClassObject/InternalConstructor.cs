namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	internal JClassObject(IEnvironment env) : base(env)
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<JClassObject>();
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
		this._isFinal = true;
		this.Lifetime.SetClass(this);
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="metadata">A <see cref="JDataTypeMetadata"/> instance.</param>
	/// <param name="classRef">Local class reference.</param>
	internal JClassObject(JClassObject jClassClassObject, ITypeInformation metadata, JClassLocalRef classRef = default)
		: base(jClassClassObject, classRef.Value)
	{
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
	}
}