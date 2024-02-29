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
		this._isInterface = false;
		this._isEnum = false;
		this._isAnnotation = false;
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
		if (metadata.Kind is not JTypeKind.Undefined)
		{
			this._isInterface = metadata.Kind is JTypeKind.Interface or JTypeKind.Annotation;
			this._isAnnotation = metadata.Kind is JTypeKind.Annotation;
			this._isEnum = metadata.Kind is JTypeKind.Enum;
		}
		if (metadata.Modifier.HasValue)
			this._isFinal = metadata.Modifier is JTypeModifier.Final;
	}
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jClassClassObject"><see cref="JClassObject"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	internal JClassObject(JClassObject jClassClassObject, JClassLocalRef classRef) : base(
		jClassClassObject, classRef.Value) { }
}