namespace Rxmxnx.JNetInterface.Native.Dummies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract JClassObject AsClassObject(JClassLocalRef classRef);
	/// <inheritdoc/>
	public abstract JClassObject AsClassObject(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract Boolean IsAssignableTo<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <inheritdoc/>
	public abstract JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>;
	/// <inheritdoc/>
	public abstract JClassObject GetObjectClass(JLocalObject jLocal);
	/// <inheritdoc/>
	public abstract JClassObject? GetSuperClass(JClassObject jClass);
	/// <inheritdoc/>
	public abstract Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass);
	/// <inheritdoc/>
	public abstract Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass);
	/// <inheritdoc/>
	public abstract Boolean IsInstanceOf<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>;
}