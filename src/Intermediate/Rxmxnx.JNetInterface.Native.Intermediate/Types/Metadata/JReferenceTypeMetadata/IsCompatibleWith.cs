namespace Rxmxnx.JNetInterface.Types.Metadata;

#if !PACKAGE
public abstract partial class JReferenceTypeMetadata
#else
public abstract partial class JDataTypeMetadata
#endif
{
	/// <summary>
	/// Indicates whether the current datatype is compatible with <paramref name="jEnvironment"/> instance.
	/// </summary>
	/// <param name="jEnvironment">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the current datatype is compatible with <paramref name="jEnvironment"/> instance;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public Boolean IsCompatibleWith(IEnvironment jEnvironment)
	{
		IVirtualMachine jVirtualMachine = jEnvironment.VirtualMachine;
		if (jVirtualMachine.AndroidApiLevel > 0)
			return this.AndroidApiLevel >= 0 && this.AndroidApiLevel <= jVirtualMachine.AndroidApiLevel;
		if (this.Since is not JRuntimeVersion.Undefined)
			return (Int32)this.Since <= jEnvironment.Version || this.Since <= jEnvironment.VirtualMachine.Version;
		return false;
	}
}