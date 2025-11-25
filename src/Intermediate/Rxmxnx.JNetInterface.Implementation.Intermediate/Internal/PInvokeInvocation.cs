namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// P/Invoke definition of <see cref="IInvocationFunctionSet"/> interface.
/// </summary>
internal abstract class PInvokeInvocation : IInvocationFunctionSet
{
	/// <inheritdoc/>
	public abstract JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc/>
	public abstract JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue initArg);
	/// <inheritdoc/>
	public abstract unsafe JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize, out Int32 count);

	/// <summary>
	/// Retrieves <see cref="PInvokeInvocation"/> for <typeparamref name="TLibrary"/> type.
	/// </summary>
	/// <typeparam name="TLibrary">A <see cref="IVirtualMachineLibraryType"/> type.</typeparam>
	/// <returns>The <see cref="PInvokeInvocation"/> for <typeparamref name="TLibrary"/> type.</returns>
	public static PInvokeInvocation GetInvocationSet<TLibrary>() where TLibrary : IVirtualMachineLibraryType
		=> Impl<TLibrary>.Instance;

	/// <summary>
	/// A <see cref="PInvokeInvocation"/> Implementation.
	/// </summary>
	/// <typeparam name="TLibrary"></typeparam>
	private sealed class Impl<TLibrary> : PInvokeInvocation where TLibrary : IVirtualMachineLibraryType
	{
		/// <summary>
		/// Singleton instance.
		/// </summary>
		public static readonly Impl<TLibrary> Instance = new();

		/// <inheritdoc/>
		public override JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg)
			=> TLibrary.GetDefaultVirtualMachineInitArgs(ref initArg);
		/// <inheritdoc/>
		public override JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
			in VirtualMachineInitArgumentValue initArg)
			=> TLibrary.CreateVirtualMachine(out vmRef, out envRef, in initArg);
		/// <inheritdoc/>
#if !PACKAGE
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
		                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
		public override unsafe JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize,
			out Int32 count)
			=> TLibrary.GetCreatedVirtualMachines(arr, arrSize, out count);
	}
}