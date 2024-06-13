namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract JClassObject AsClassObject(JClassLocalRef classRef);
	/// <inheritdoc/>
	public abstract JClassObject AsClassObject(JReferenceObject jObject);
	/// <inheritdoc/>
	public abstract JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>;
	/// <inheritdoc/>
	public abstract JClassObject GetClass(ITypeInformation typeInformation);
	/// <inheritdoc/>
	public abstract JClassObject GetObjectClass(ObjectMetadata objectMetadata);
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
	/// <inheritdoc/>
	public abstract JReferenceTypeMetadata GetTypeMetadata(JClassObject? jClass);
	/// <inheritdoc/>
	public abstract JModuleObject? GetModule(JClassObject jClass);
	/// <inheritdoc/>
	public abstract void ThrowNew<TThrowable>(CString? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
	/// <inheritdoc/>
	public abstract void ThrowNew<TThrowable>(String? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
}