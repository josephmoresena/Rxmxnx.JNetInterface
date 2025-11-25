namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachineLibrary
{
	/// <summary>
	/// Generic implementation of <see cref="JVirtualMachineLibrary"/> class.
	/// </summary>
	/// <typeparam name="TFunctionSet">A <see cref="IInvocationFunctionSet"/> type.</typeparam>
#if !PACKAGE
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
	                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
	private sealed unsafe class Impl<TFunctionSet> : JVirtualMachineLibrary where TFunctionSet : IInvocationFunctionSet
	{
		/// <summary>
		/// Pointer to exported Java Library functions.
		/// </summary>
		private readonly TFunctionSet _functions;

		/// <summary>
		/// Private constructor.
		/// </summary>
		/// <param name="handle">Library handle.</param>
		/// <param name="functions">A <see cref="InvocationFunctionSet"/> value.</param>
		/// <param name="hasCreatedVm">
		/// Indicates whether the function <c>JNI_GetCreatedJavaVMs</c> is available on the current library.
		/// </param>
		public Impl(IntPtr handle, TFunctionSet functions, Boolean hasCreatedVm) : base(handle, hasCreatedVm)
			=> this._functions = functions;

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initArg)
			=> this._functions.GetDefaultVirtualMachineInitArgs(ref initArg);
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override JResult CreateVirtualMachine(out JVirtualMachineRef vmRef,
			out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue initArg)
			=> this._functions.CreateVirtualMachine(out vmRef, out envRef, in initArg);
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected override JResult GetCreatedVirtualMachines(JVirtualMachineRef* arr, Int32 arrSize,
			out Int32 count)
			=> this._functions.GetCreatedVirtualMachines(arr, arrSize, out count);
	}
}