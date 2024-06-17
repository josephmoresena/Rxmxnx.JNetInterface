namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IClassFeature
{
	/// <summary>
	/// Retrieves the java class named <paramref name="className"/>.
	/// </summary>
	/// <param name="className">Class name.</param>
	/// <returns>The class instance with given class name.</returns>
	internal JClassObject GetClass(ReadOnlySpan<Byte> className);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <param name="className">Name of class to load.</param>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	internal JClassObject LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader = default);
	/// <summary>
	/// Loads a java class from its binary information into the current VM.
	/// </summary>
	/// <typeparam name="TDataType">The type with class definition.</typeparam>
	/// <param name="rawClassBytes">Binary span with class information.</param>
	/// <param name="jClassLoader">Optional. The object used as class loader.</param>
	/// <returns>A new <see cref="JClassObject"/> instance.</returns>
	internal JClassObject LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
		ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader = default)
		where TDataType : JLocalObject, IReferenceType<TDataType>;
	/// <summary>
	/// Retrieves the class info.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="name">Output. Class name.</param>
	/// <param name="signature">Output. Class signature.</param>
	/// <param name="hash">Output. Class hash.</param>
	internal void GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash);
}