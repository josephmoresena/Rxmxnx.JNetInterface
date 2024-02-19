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
		public static IVirtualMachine GetVirtualMachine(JEnvironmentRef jEnv)
		{
			Int32 index = EnvironmentCache.delegateIndex[typeof(GetVirtualMachineDelegate)];
			GetVirtualMachineDelegate getVirtualMachine =
				jEnv.Reference.Reference[index].GetUnsafeDelegate<GetVirtualMachineDelegate>()!;
			JResult result = getVirtualMachine(jEnv, out JVirtualMachineRef vmRef);
			if (result == JResult.Ok)
				return JVirtualMachine.GetVirtualMachine(vmRef);
			throw new JniException(result);
		}
	}
}