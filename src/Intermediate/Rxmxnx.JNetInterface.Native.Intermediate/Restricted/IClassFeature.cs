namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI classing feature.
/// </summary>
internal partial interface IClassFeature
{
	/// <summary>
	/// Retrieves the current <paramref name="classRef"/> reference as <see cref="JClassObject"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	JClassObject AsClassObject(JClassLocalRef classRef);
	/// <summary>
	/// Retrieves the current <paramref name="jObject"/> instance as <see cref="JClassObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	JClassObject AsClassObject(JReferenceObject jObject);
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>The class instance for given type.</returns>
	JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>;
	/// <summary>
	/// Retrieves the java class for <paramref name="typeInformation"/>
	/// </summary>
	/// <param name="typeInformation">A <see cref="ClassObjectMetadata"/> instance.</param>
	/// <returns>The class instance of <paramref name="typeInformation"/>.</returns>
	JClassObject GetClass(ITypeInformation typeInformation);
	/// <summary>
	/// Retrieves the java class of <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The class instance of <paramref name="jLocal"/>.</returns>
	JClassObject GetObjectClass(JLocalObject jLocal);
	/// <summary>
	/// Retrieves the java class of <paramref name="objectMetadata"/>
	/// </summary>
	/// <param name="objectMetadata">A <see cref="ObjectMetadata"/> instance.</param>
	/// <returns>The class instance of <paramref name="objectMetadata"/>.</returns>
	JClassObject GetObjectClass(ObjectMetadata objectMetadata);
	/// <summary>
	/// Retrieves the java super class of given class instance.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <returns>The class instance of the super class of given class.</returns>
	JClassObject? GetSuperClass(JClassObject jClass);
	/// <summary>
	/// Determines whether an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <param name="otherClass">Other java class instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass);
	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is an instance of <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="jObject"/> is an instance of
	/// <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass);
	/// <summary>
	/// Indicates whether <paramref name="jObject"/> is an instance of
	/// <typeparamref name="TDataType"/> type class.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="jObject"/> is an instance of
	/// <typeparamref name="TDataType"/> type class; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsInstanceOf<TDataType>(JReferenceObject jObject) where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Retrieves <see cref="JReferenceTypeMetadata"/> from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	[return: NotNullIfNotNull(nameof(jClass))]
	JReferenceTypeMetadata? GetTypeMetadata(JClassObject? jClass);
	/// <summary>
	/// Retrieves the <see cref="JModuleObject"/> from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JModuleObject"/> instance.</returns>
	/// <exception cref="InvalidOperationException">
	/// Throws if JNI version doesn't support modules.
	/// </exception>
	JModuleObject? GetModule(JClassObject jClass);
	/// <summary>
	/// Throws an exception from <paramref name="jClass"/> class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	void ThrowNew(JClassObject jClass, String? message, Boolean throwException);
	/// <summary>
	/// Throws an exception from <paramref name="jClass"/> class.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="message">The message used to construct the <c>java.lang.Throwable</c> instance.</param>
	/// <param name="throwException">Indicates whether exception should be thrown in managed code.</param>
	void ThrowNew(JClassObject jClass, CString? message, Boolean throwException);
	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="JThrowableObject"/> type.</typeparam>
	/// <param name="message">
	/// The message used to construct the <c>java.lang.Throwable</c> instance.
	/// The string is encoded in modified UTF-8.
	/// </param>
	/// <param name="throwException">
	/// Indicates whether exception should be thrown in managed code.
	/// </param>
	void ThrowNew<TThrowable>(CString? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
	/// <summary>
	/// Throws an exception from <typeparamref name="TThrowable"/> type.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="JThrowableObject"/> type.</typeparam>
	/// <param name="message">
	/// The message used to construct the <c>java.lang.Throwable</c> instance.
	/// </param>
	/// <param name="throwException">
	/// Indicates whether exception should be thrown in managed code.
	/// </param>
	void ThrowNew<TThrowable>(String? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
}