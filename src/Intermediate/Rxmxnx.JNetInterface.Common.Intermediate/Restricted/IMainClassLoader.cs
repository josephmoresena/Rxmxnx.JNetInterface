namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents a main class loader object.
/// </summary>
internal interface IMainClassLoader
{
	/// <summary>
	/// JNI virtual machine reference.
	/// </summary>
	JVirtualMachineRef VirtualMachineRef { get; }
	/// <summary>
	/// JNI environment reference.
	/// </summary>
	JEnvironmentRef EnvironmentRef { get; }

	/// <summary>
	/// JNI version.
	/// </summary>
	Int32 Version { get; }
	/// <summary>
	/// Retrieves the current JRE version.
	/// </summary>
	/// <param name="systemClassRef"><c>java.lang.System</c> class reference.</param>
	/// <param name="initializing">Indicates whether the current call occurs on VM initializing.</param>
	/// <returns>Current JRE version.</returns>
	JRuntimeVersion GetVersion(JClassLocalRef systemClassRef, Boolean initializing);
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="classMetadata">Class metadata.</param>
	/// <param name="wClassGlobal">Wrapper class global instance.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	JGlobalRef GetPrimitiveMainClassGlobalRef(ClassObjectMetadata classMetadata, JGlobalBase? wClassGlobal = default);
	/// <summary>
	/// Retrieves a global reference for given class name.
	/// </summary>
	/// <param name="typeInformation">Type information.</param>
	/// <returns>A <see cref="JGlobalRef"/> reference.</returns>
	JGlobalRef GetMainClassGlobalRef(ITypeInformation typeInformation);
}