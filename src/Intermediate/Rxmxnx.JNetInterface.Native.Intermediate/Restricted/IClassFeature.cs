namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes JNI classing feature.
/// </summary>
public partial interface IClassFeature
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
	/// Determines whether <paramref name="jObject"/> can be safely cast to
	/// <typeparamref name="TDataType"/> instance.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jObject"/> can be safely cast to
	/// <typeparamref name="TDataType"/> instance; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsAssignableTo<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>;
	/// <summary>
	/// Retrieves the java class for given type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>The class instance for given type.</returns>
	JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>;
	/// <summary>
	/// Retrieves the java class of <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The class instance of <paramref name="jLocal"/>.</returns>
	JClassObject GetObjectClass(JLocalObject jLocal);
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
}