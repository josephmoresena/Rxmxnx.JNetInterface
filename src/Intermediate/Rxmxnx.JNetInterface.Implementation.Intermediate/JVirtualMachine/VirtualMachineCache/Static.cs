namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private sealed partial record VirtualMachineCache
	{
		/// <summary>
		/// Delegates dictionary.
		/// </summary>
		private static readonly Dictionary<Type, Func<JVirtualMachineRef, IntPtr>> getPointer = new()
		{
			{ typeof(DestroyVirtualMachineDelegate), r => r.Reference.Reference.DestroyJavaVmPointer },
			{ typeof(AttachCurrentThreadDelegate), r => r.Reference.Reference.AttachCurrentThreadPointer },
			{ typeof(DetachCurrentThreadDelegate), r => r.Reference.Reference.DetachCurrentThreadPointer },
			{ typeof(GetEnvDelegate), r => r.Reference.Reference.GetEnvPointer },
			{
				typeof(AttachCurrentThreadAsDaemonDelegate),
				r => r.Reference.Reference.AttachCurrentThreadAsDaemonPointer
			},
		};
	}
}