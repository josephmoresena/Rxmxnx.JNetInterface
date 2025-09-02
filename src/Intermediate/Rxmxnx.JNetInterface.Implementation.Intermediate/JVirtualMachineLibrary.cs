namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class stores a loaded native JVM library.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public sealed unsafe partial class JVirtualMachineLibrary
{
	/// <summary>
	/// Library handle.
	/// </summary>
	public IntPtr Handle { get; }

	/// <summary>
	/// Retrieves the latest JNI version supported by the current library.
	/// </summary>
	/// <returns>The latest JNI version supported.</returns>
	public Int32 GetLatestSupportedVersion()
	{
		Int32 version = -1;
		foreach (Int32 jniVersion in JVirtualMachineLibrary.jniVersions)
		{
			VirtualMachineInitArgumentValue initValue = new() { Version = jniVersion, };
			if (this._functions.GetDefaultVirtualMachineInitArgs(ref initValue) != JResult.Ok)
				break;
			version = jniVersion;
		}
		return ImplementationValidationUtilities.ThrowIfInvalidVersion(version);
	}
	/// <summary>
	/// Retrieves the default VM initialization argument for the current JVM library.
	/// </summary>
	/// <param name="jniVersion">The requested JNI version.</param>
	/// <returns>A <see cref="JVirtualMachineInitArg"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public JVirtualMachineInitArg GetDefaultArgument(Int32 jniVersion = 0x00010006)
	{
		VirtualMachineInitArgumentValue initValue = new()
		{
			Version = jniVersion < JVirtualMachineLibrary.jniVersions[0] ?
				JVirtualMachineLibrary.jniVersions[0] :
				jniVersion,
		};
		ImplementationValidationUtilities.ThrowIfInvalidResult(
			this._functions.GetDefaultVirtualMachineInitArgs(ref initValue));
		return new(initValue);
	}
	/// <summary>
	/// Creates a <see cref="IInvokedVirtualMachine"/> instance.
	/// </summary>
	/// <param name="arg">VM initialization argument.</param>
	/// <param name="env">Output. <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="IInvokedVirtualMachine"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public IInvokedVirtualMachine CreateVirtualMachine(JVirtualMachineInitArg arg, out IEnvironment env)
	{
		CStringSequence sequence = arg.Options;
		fixed (void* _ = sequence)
		{
			Span<VirtualMachineInitOptionValue> options =
				stackalloc VirtualMachineInitOptionValue[sequence.NonEmptyCount];
			arg.CopyOptions(options);
			fixed (VirtualMachineInitOptionValue* ptr = &MemoryMarshal.GetReference(options))
			{
				VirtualMachineInitArgumentValue value = new()
				{
					Version = arg.Version,
					OptionsLength = options.Length,
					Options = ptr,
					IgnoreUnrecognized = arg.IgnoreUnrecognized,
				};
				JResult result =
					this._functions.CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
					                                     in value);
				ImplementationValidationUtilities.ThrowIfInvalidResult(result);
				return JVirtualMachine.GetVirtualMachine(vmRef, envRef, out env);
			}
		}
	}
	/// <summary>
	/// Retrieves all the created <see cref="IVirtualMachine"/> instances.
	/// </summary>
	/// <returns>A read-only list of <see cref="IVirtualMachine"/> instances.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public IVirtualMachine[] GetCreatedVirtualMachines()
	{
		_ = this._functions.GetCreatedVirtualMachines(default, 0, out Int32 vmCount);
		if (vmCount <= 0) return [];
		JVirtualMachineRef[] arr = this.GetCreatedVirtualMachines(vmCount, out JResult result);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		return arr.Select(JVirtualMachine.GetVirtualMachine).ToArray();
	}

	/// <summary>
	/// Loads a virtual machine library exposed by <paramref name="libraryPath"/>.
	/// </summary>
	/// <param name="libraryPath">Path to JVM library.</param>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance if <paramref name="libraryPath"/> is a
	/// valid JVM library; otherwise, <see langword="null"/>.
	/// </returns>
	public static JVirtualMachineLibrary? LoadLibrary(String libraryPath)
	{
		IntPtr? handle = NativeUtilities.LoadNativeLib(libraryPath);
		JTrace.LoadLibrary(libraryPath, handle);
		return handle.HasValue ? JVirtualMachineLibrary.Create(handle.Value) : default;
	}
	/// <summary>
	/// Creates a virtual machine library using loaded JVM library.
	/// </summary>
	/// <param name="handle">Handle of loaded JVM library.</param>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance if <paramref name="handle"/> is
	/// valid for a JVM library; otherwise, <see langword="null"/>.
	/// </returns>
	public static JVirtualMachineLibrary? Create(IntPtr handle)
	{
		Span<IntPtr> functions = stackalloc IntPtr[3];
		if (JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.getDefaultVirtualMachineInitArgsName,
		                                           out functions[0]) &&
		    JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.createVirtualMachineName,
		                                           out functions[1]) &&
		    JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.getCreatedVirtualMachinesName,
		                                           out functions[2]))
			return new(handle, Unsafe.As<IntPtr, InvocationFunctionSet>(ref functions[0]));
		NativeLibrary.Free(handle);
		return default;
	}
}