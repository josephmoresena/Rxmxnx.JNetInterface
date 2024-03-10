namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Retrieves a <see cref="JVirtualMachine"/> from given <paramref name="jEnv"/>.
		/// </summary>
		/// <param name="jEnv">A <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="IVirtualMachine"/> instance.</returns>
		public static JVirtualMachine GetVirtualMachine(JEnvironmentRef jEnv)
		{
			Int32 index = EnvironmentCache.delegateIndex[typeof(GetVirtualMachineDelegate)].Index;
			GetVirtualMachineDelegate getVirtualMachine =
				jEnv.Reference.Reference[index].GetUnsafeDelegate<GetVirtualMachineDelegate>()!;
			JniException? jniException = getVirtualMachine(jEnv, out JVirtualMachineRef vmRef);
			if (jniException is null)
				return (JVirtualMachine)JVirtualMachine.GetVirtualMachine(vmRef);
			throw jniException;
		}
	}
}