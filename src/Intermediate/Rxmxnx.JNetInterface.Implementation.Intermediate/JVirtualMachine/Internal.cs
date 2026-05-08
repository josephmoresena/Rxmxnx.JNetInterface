namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Support JNI versions.
	/// </summary>
	internal static ReadOnlySpan<Int32> JniVersions
		=>
		[
			(Int32)JRuntimeVersion.SEd2, //JNI_VERSION_1_2
			(Int32)JRuntimeVersion.SEd4, //JNI_VERSION_1_4
			(Int32)JRuntimeVersion.J6, //JNI_VERSION_1_6
			(Int32)JRuntimeVersion.J8, //JNI_VERSION_1_8
			(Int32)JRuntimeVersion.J9, //JNI_VERSION_9
			(Int32)JRuntimeVersion.J10, //JNI_VERSION_10
			(Int32)JRuntimeVersion.J19, //JNI_VERSION_19
			(Int32)JRuntimeVersion.J20, //JNI_VERSION_20
			(Int32)JRuntimeVersion.J21, //JNI_VERSION_21
			(Int32)JRuntimeVersion.J24, //JNI_VERSION_24
		];

	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance that <paramref name="envRef"/>
	/// references to.
	/// </summary>
	/// <param name="envRef"><see cref="JEnvironmentRef"/> reference to JNI interface.</param>
	/// <returns>
	/// The <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>.
	/// </returns>
	internal JEnvironment GetEnvironment(JEnvironmentRef envRef) => this._core.ThreadCache.Get(envRef, out _);

	/// <summary>
	/// Retrieves the <see cref="IInvokedVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="env">Output. <see cref="IEnvironment"/> instance.</param>
	/// <returns>The <see cref="IInvokedVirtualMachine"/> instance referenced by <paramref name="reference"/>.</returns>
	internal static IInvokedVirtualMachine GetVirtualMachine(JVirtualMachineRef reference, JEnvironmentRef envRef,
		out IEnvironment env)
	{
		JVirtualMachine vm = ReferenceCache.Instance.Get(reference, out _, true);
		env = vm._core.ThreadCache.Get(envRef, out _);
		if (vm is IInvokedVirtualMachine invoked) return invoked;
		return new Invoked(vm);
	}
	/// <summary>
	/// Detaches current thread from <see cref="IVirtualMachine"/> referenced by <paramref name="vmRef"/>.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	/// <param name="thread">A <see cref="Thread"/> instance.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void DetachCurrentThread(JVirtualMachineRef vmRef, JEnvironmentRef envRef, Thread thread)
		=> VirtualMachineCore.DetachCurrentThread(ReferenceCache.Instance.Get(vmRef)?._core, envRef, thread);
	/// <summary>
	/// Removes the <see cref="IEnvironment"/> instance referenced by <paramref name="envRef"/>
	/// into the <see cref="IVirtualMachine"/> referenced by <paramref name="vmRef"/>.
	/// </summary>
	/// <param name="vmRef">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <param name="envRef">A <see cref="JEnvironmentRef"/> reference.</param>
	internal static void RemoveEnvironment(JVirtualMachineRef vmRef, JEnvironmentRef envRef)
	{
		JVirtualMachine? vm = ReferenceCache.Instance.Get(vmRef);
		vm?._core.ThreadCache.Remove(envRef);
	}
	/// <summary>
	/// Indicates whether the class for <paramref name="hash"/> is a main class.
	/// </summary>
	/// <param name="hash">A class hash instance.</param>
	/// <returns>
	/// <see langword="true"/> if the class for <paramref name="hash"/> is a main class; otherwise;
	/// <see langword="false"/>.
	/// </returns>
	internal static Boolean IsMainClass(String hash) => JVirtualMachine.userMainClasses.ContainsKey(hash);
}