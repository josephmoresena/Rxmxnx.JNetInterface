namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class stores a loaded native JVM library.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
public sealed unsafe class JVirtualMachineLibrary
{
	/// <summary>
	/// Name of <c>JNI_GetDefaultJavaVMInitArgs</c> function.
	/// </summary>
	private const String GetDefaultVirtualMachineInitArgsName = "JNI_GetDefaultJavaVMInitArgs";
	/// <summary>
	/// Name of <c>JNI_CreateJavaVM</c> function.
	/// </summary>
	private const String CreateVirtualMachineName = "JNI_CreateJavaVM";
	/// <summary>
	/// Name of <c>JNI_GetCreatedJavaVMs</c> function.
	/// </summary>
	private const String GetCreatedVirtualMachinesName = "JNI_GetCreatedJavaVMs";

	/// <summary>
	/// Support JNI versions.
	/// </summary>
	private static readonly Int32[] jniVersions =
	[
		0x00010006, //JNI_VERSION_1_6
		0x00010008, //JNI_VERSION_1_8
		0x00090000, //JNI_VERSION_9
		0x000a0000, //JNI_VERSION_10
		0x00130000, //JNI_VERSION_19
		0x00140000, //JNI_VERSION_20
		0x00150000, //JNI_VERSION_21
	];

	/// <summary>
	/// Pointer to exported Java Library functions.
	/// </summary>
	private readonly InvocationFunctionSet _functions;

	/// <summary>
	/// Library handle.
	/// </summary>
	public IntPtr Handle { get; private init; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="handle">Library handle.</param>
	/// <param name="functions">A <see cref="InvocationFunctionSet"/> value.</param>
	private JVirtualMachineLibrary(IntPtr handle, InvocationFunctionSet functions)
	{
		this.Handle = handle;
		this._functions = functions;
	}

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
		using IFixedPointer.IDisposable fPtr = sequence.GetFixedPointer();
		// Avoid heap allocation.
		Span<VirtualMachineInitOptionValue> options = stackalloc VirtualMachineInitOptionValue[sequence.NonEmptyCount];
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
	/// <summary>
	/// Retrieves all the created <see cref="IVirtualMachine"/> instances.
	/// </summary>
	/// <returns>A read-only list of <see cref="IVirtualMachine"/> instances.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public IVirtualMachine[] GetCreatedVirtualMachines()
	{
		_ = this._functions.GetCreatedVirtualMachines(default, 0, out Int32 vmCount);
		if (vmCount == 0) return [];
		JVirtualMachineRef[] arr = this.GetCreatedVirtualMachines(vmCount, out JResult result);
		ImplementationValidationUtilities.ThrowIfInvalidResult(result);
		return arr.Select(JVirtualMachine.GetVirtualMachine).ToArray();
	}

	/// <summary>
	/// Retrieves all the created <see cref="JVirtualMachineRef"/> instances.
	/// </summary>
	/// <param name="vmCount">Number of elements to retrieve.</param>
	/// <param name="result">Output. JNI call result.</param>
	/// <returns>An array of <see cref="JVirtualMachineRef"/> references.</returns>
	private JVirtualMachineRef[] GetCreatedVirtualMachines(Int32 vmCount, out JResult result)
	{
		JVirtualMachineRef[] arr = new JVirtualMachineRef[vmCount];
		using MemoryHandle handle = arr.AsMemory().Pin();
		result = this._functions.GetCreatedVirtualMachines((JVirtualMachineRef*)handle.Pointer, arr.Length,
		                                                   out vmCount);
		return arr;
	}

	/// <summary>
	/// Loads a virtual machine library exposed by <paramref name="libraryPath"/>.
	/// </summary>
	/// <param name="libraryPath">Path to JVM library.</param>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance if <paramref name="libraryPath"/> is a
	/// valid JVM library; otherwise, <see langword="null"/>.
	/// </returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static JVirtualMachineLibrary? LoadLibrary(String libraryPath)
	{
		IntPtr? handle = NativeUtilities.LoadNativeLib(libraryPath);
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
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static JVirtualMachineLibrary? Create(IntPtr handle)
	{
		Span<IntPtr> functions = stackalloc IntPtr[3];
		if (JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.GetDefaultVirtualMachineInitArgsName,
		                                           out functions[0]) &&
		    JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.CreateVirtualMachineName,
		                                           out functions[1]) &&
		    JVirtualMachineLibrary.TryGetJniExport(handle, JVirtualMachineLibrary.GetCreatedVirtualMachinesName,
		                                           out functions[2]))
			return new(handle, Unsafe.As<IntPtr, InvocationFunctionSet>(ref functions[0]));
		NativeLibrary.Free(handle);
		return default;
	}

	/// <summary>
	/// Gets the address of JNI exported symbol and returns a value that indicates whether
	/// the method call succeeded.
	/// </summary>
	/// <param name="handle">The native JVM library OS handle.</param>
	/// <param name="name">The name of the exported JNI symbol.</param>
	/// <param name="address">When the method returns, contains the symbol address, if it exists.</param>
	/// <returns>
	/// <see langword="true"/> if the address of the exported symbol was found successfully; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private static Boolean TryGetJniExport(IntPtr handle, String name, out IntPtr address)
		=> NativeLibrary.TryGetExport(handle, name, out address) ||
			NativeLibrary.TryGetExport(handle, name + "_Impl", out address);
}