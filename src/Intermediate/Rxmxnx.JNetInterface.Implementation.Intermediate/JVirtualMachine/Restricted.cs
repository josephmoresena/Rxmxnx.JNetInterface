namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	IEnumerable<ITypeInformation> IMainClassSet.ClassesInformation => JVirtualMachine.MainClassesInformation;
	Boolean IMainClassSet.Contains(String classHash) => JVirtualMachine.userMainClasses.ContainsKey(classHash);
}