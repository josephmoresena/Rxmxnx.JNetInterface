namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <inheritdoc cref="IClass.Reference"/>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();

	/// <summary>
	/// Initialize the current instance with given <see cref="IDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">The <see cref="IDataType"/> with class definition.</typeparam>
	internal void Initialize<TDataType>() where TDataType : JLocalObject, IDataType
	{
		this._className = TDataType.ClassName;
		this._signature = TDataType.Signature;
		this._hash = new CStringSequence(this.Name, this.ClassSignature).ToString();
		this._isFinal = TDataType.Modifier == JTypeModifier.Final;
	}
}