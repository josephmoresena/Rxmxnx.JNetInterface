namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// This interface exposes a java class provider instance.
/// </summary>
public interface IClassProvider
{
	/// <summary>
	/// <c>java.lang.Class&lt;?&gt;</c> class instance.
	/// </summary>
	JClassObject ClassObject => this.GetClass<JClassObject>();
	/// <summary>
	/// <c>java.lang.String</c> class instance.
	/// </summary>
	JClassObject StringClassObject => this.GetClass<JStringObject>();

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
	Boolean IsAssignableTo<TDataType>(JReferenceObject jObject) where TDataType : JLocalObject, IDataType<TDataType>;
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	JClassObject GetClass(CString className);
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
	JClassObject? GetSuperClass(IClass jClass);
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
	Boolean IsAssignableFrom(IClass jClass, IClass otherClass);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <param name="className">Name of class to load.</param>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	JClassObject LoadClass(CString className, ReadOnlySpan<Byte> rawClassBytes, JLocalObject? jClassLoader = default);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <param name="className">Name of class to load.</param>
	/// <param name="rawClassBytes">Stream with binary class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	JClassObject LoadClass(CString className, Stream rawClassBytes, JLocalObject? jClassLoader = default);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <typeparam name="TDataType">The type with class definition.</typeparam>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	JClassObject LoadClass<TDataType>(ReadOnlySpan<Byte> rawClassBytes, JLocalObject? jClassLoader = default);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <typeparam name="TDataType">The type with class definition.</typeparam>
	/// <param name="rawClassBytes">Stream with binary class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	JClassObject LoadClass<TDataType>(Stream rawClassBytes, JLocalObject? jClassLoader = default);

	/// <summary>
	/// Retrieves the class info.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="name">Output. Class name.</param>
	/// <param name="signature">Output. Class signature.</param>
	/// <param name="hash">Output. Class hash.</param>
	void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash);
}