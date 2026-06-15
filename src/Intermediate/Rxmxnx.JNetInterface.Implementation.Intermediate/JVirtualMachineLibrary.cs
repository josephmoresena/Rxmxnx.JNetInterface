namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class stores a loaded native JVM library.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS907)]
#endif
public abstract unsafe partial class JVirtualMachineLibrary
{
	/// <summary>
	/// Library handle.
	/// </summary>
	public IntPtr Handle { get; private init; }

	/// <summary>
	/// Indicates whether the current library supports <c>JNI_GetCreatedJavaVMs</c>.
	/// </summary>
	private protected abstract Boolean HasCreatedVirtualMachines { get; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="handle">Library handle.</param>
	private JVirtualMachineLibrary(IntPtr handle) => this.Handle = handle;

	/// <summary>
	/// Retrieves the latest JNI version supported by the current library.
	/// </summary>
	/// <returns>The latest JNI version supported.</returns>
	public Int32 GetLatestSupportedVersion()
	{
		Int32 version = -1;
		foreach (Int32 jniVersion in JVirtualMachine.JniVersions)
		{
			VirtualMachineInitArgumentValue initValue = new() { Version = jniVersion, };
			if (this.GetDefaultVirtualMachineInitArgs(ref initValue) != JResult.Ok)
				break;
			version = jniVersion;
		}
		return ImplementationValidationUtilities.ThrowIfInvalidVersion(version);
	}
	/// <summary>
	/// Retrieves the default VM initialization argument for the current JVM library.
	/// </summary>
	/// <param name="jreVersion">The requested JRE version.</param>
	/// <returns>A <see cref="JVirtualMachineInitArg"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public JVirtualMachineInitArg GetDefaultArgument(JRuntimeVersion jreVersion)
		=> this.GetDefaultArgument((Int32)jreVersion);
	/// <summary>
	/// Retrieves the default VM initialization argument for the current JVM library.
	/// </summary>
	/// <param name="jniVersion">The requested JNI version.</param>
	/// <returns>A <see cref="JVirtualMachineInitArg"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public JVirtualMachineInitArg GetDefaultArgument(Int32 jniVersion = (Int32)JRuntimeVersion.J6)
	{
		VirtualMachineInitArgumentValue initValue = new()
		{
			Version = jniVersion < JVirtualMachine.JniVersions[0] ? JVirtualMachine.JniVersions[0] : jniVersion,
		};
		ImplementationValidationUtilities.ThrowIfInvalidResult(this.GetDefaultVirtualMachineInitArgs(ref initValue));
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
					this.CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef, in value);
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
		_ = this.GetCreatedVirtualMachines(default, 0, out Int32 vmCount);
		if (vmCount <= 0) return [];
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
		if (!this.HasCreatedVirtualMachines)
			ImplementationValidationUtilities.ThrowIfInvalidResult(JResult.VersionError);
		JVirtualMachineRef[] arr = new JVirtualMachineRef[vmCount];
		fixed (JVirtualMachineRef* ptr = arr)
			result = this.GetCreatedVirtualMachines(ptr, arr.Length, out vmCount);
		return arr;
	}

	/// <summary>
	/// Loads the virtual machine library linked statically to the current binary.
	/// </summary>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance for the current binary.
	/// </returns>
	/// <remarks>When this method is used, the <c>jvm</c> library must be statically linked.</remarks>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static JVirtualMachineLibrary LoadStaticLibrary()
		=> JVirtualMachineLibrary.Create<VirtualMachineStaticLibrary>();
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
		return handle.HasValue ? JVirtualMachineLibrary.Create(handle.Value, true) : default;
	}
	/// <summary>
	/// Creates a virtual machine library using <typeparamref name="TLibrary"/> type.
	/// </summary>
	/// <typeparam name="TLibrary">A <see cref="IVirtualMachineLibraryType"/> type.</typeparam>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance by <typeparamref name="TLibrary"/>.
	/// </returns>
	public static JVirtualMachineLibrary Create<TLibrary>() where TLibrary : IVirtualMachineLibraryType
	{
		if (!AotInfo.IsNativeAot && TLibrary.IsStatic)
			throw new InvalidOperationException(IMessageResource.GetInstance().AotRequired);
		return new Impl<PInvokeInvocation>(default, PInvokeInvocation.GetInvocationSet<TLibrary>());
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
	public static JVirtualMachineLibrary? Create(IntPtr handle) => JVirtualMachineLibrary.Create(handle, false);

	/// <summary>
	/// Creates a virtual machine library using loaded JVM library.
	/// </summary>
	/// <param name="handle">Handle of loaded JVM library.</param>
	/// <param name="ownHandle">Indicates whether <paramref name="handle"/> is own by the current call.</param>
	/// <returns>
	/// A <see cref="JVirtualMachineLibrary"/> instance if <paramref name="handle"/> is
	/// valid for a JVM library; otherwise, <see langword="null"/>.
	/// </returns>
	private static Impl<InvocationFunctionSet>? Create(IntPtr handle, Boolean ownHandle)
	{
		Span<IntPtr> functions = stackalloc IntPtr[4];
		if (!JVirtualMachineLibrary.TryGetJniExport(
			    handle, IVirtualMachineLibraryType.GetDefaultVirtualMachineInitArgsSymbol,
			    out functions[0])) goto Release;
		if (!JVirtualMachineLibrary.TryGetJniExport(handle, IVirtualMachineLibraryType.CreateVirtualMachineSymbol,
		                                            out functions[1])) goto Release;
		Boolean hasCreatedVm =
			JVirtualMachineLibrary.TryGetJniExport(handle, IVirtualMachineLibraryType.GetCreatedVirtualMachinesSymbol,
			                                       out functions[2]);
		ref InvocationFunctionSet functionSet = ref Unsafe.As<IntPtr, InvocationFunctionSet>(ref functions[0]);
		return new(handle, functionSet, hasCreatedVm);
		Release:
		if (ownHandle)
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
	{
		Boolean found = NativeLibrary.TryGetExport(handle, name, out address);
		JTrace.GetJniExport(handle, name, found, address);
		if (found) return true;
		String auxName = name + "_Impl";
		found = NativeLibrary.TryGetExport(handle, auxName, out address);
		JTrace.GetJniExport(handle, auxName, found, address);
		return found;
	}
}