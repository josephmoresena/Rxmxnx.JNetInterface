namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed partial class EnvironmentCache
	{
		/// <summary>
		/// Retrieves a <see cref="JVirtualMachine"/> from given <paramref name="envRef"/>.
		/// </summary>
		/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
		/// <returns>A <see cref="IVirtualMachine"/> instance.</returns>
		public static unsafe JVirtualMachine GetVirtualMachine(JEnvironmentRef envRef)
		{
			ref readonly JEnvironmentValue refValue = ref envRef.Reference;
			ref readonly NativeInterface nativeInterface =
				ref Unsafe.AsRef<NativeInterface>(refValue.Pointer.ToPointer());
			JniException? jniException = nativeInterface.GetVirtualMachine(envRef, out JVirtualMachineRef vmRef);
			if (jniException is null)
				return (JVirtualMachine)JVirtualMachine.GetVirtualMachine(vmRef);
			throw jniException;
		}
	}
}