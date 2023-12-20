namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="isDummy">Indicates whether the current instance is a dummy object.</param>
	internal JClassObject(IEnvironment env, Boolean isDummy) : base(env, default, isDummy)
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<JClassObject>();
		this._className = metadata.ClassName;
		this._signature = metadata.Signature;
		this._hash = metadata.Hash;
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
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	internal JClassObject(JClassObject jClassClassObject, JClassLocalRef classRef) 
		: base(jClassClassObject, classRef.Value) { }
}