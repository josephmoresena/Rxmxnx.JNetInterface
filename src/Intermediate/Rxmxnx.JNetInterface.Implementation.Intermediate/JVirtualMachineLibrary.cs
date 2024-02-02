namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class stores a loaded native JVM library.
/// </summary>
public sealed record JVirtualMachineLibrary
{
	/// <summary>
	/// Name of <c>JNI_GetDefaultJavaVMInitArgs</c> function.
	/// </summary>
	private const String getDefaultVirtualMachineInitArgsName = "JNI_GetDefaultJavaVMInitArgs";
	/// <summary>
	/// Name of <c>JNI_CreateJavaVM</c> function.
	/// </summary>
	private const String createVirtualMachineName = "JNI_CreateJavaVM";
	/// <summary>
	/// Name of <c>JNI_GetCreatedJavaVMs</c> function.
	/// </summary>
	private const String getCreatedVirtualMachinesName = "JNI_GetCreatedJavaVMs";
	/// <summary>
	/// Support JNI versions.
	/// </summary>
	private static readonly Int32[] jniVersions =
	{
		0x00010006, //JNI_VERSION_1_6
		0x00010008, //JNI_VERSION_1_8
		0x00090000, //JNI_VERSION_9
		0x000a0000, //JNI_VERSION_10
		0x00130000, //JNI_VERSION_19
		0x00140000, //JNI_VERSION_20
		0x00150000, //JNI_VERSION_21
	};
	/// <summary>
	/// Delegate for <c>JNI_CreateJavaVM</c> function. Loads and initializes a Java VM.
	/// </summary>
	private readonly CreateVirtualMachineDelegate _createVirtualMachine;
	/// <summary>
	/// Delegate for <c>JNI_GetCreatedJavaVMs</c> function. Returns all Java VMs that have been created.
	/// </summary>
	private readonly GetCreatedVirtualMachinesDelegate _getCreatedVirtualMachines;

	/// <summary>
	/// Delegate for <c>JNI_GetDefaultJavaVMInitArgs</c> function. Returns a default configuration for the Java VM.
	/// </summary>
	private readonly GetDefaultVirtualMachineInitArgsDelegate _getDefaultVirtualMachineInitArgs;

	/// <summary>
	/// Library handle.
	/// </summary>
	public IntPtr Handle { get; private init; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="handle">Library handle.</param>
	/// <param name="getDefaultVirtualMachineInitArgs">A <see cref="GetDefaultVirtualMachineInitArgsDelegate"/> delegate.</param>
	/// <param name="createVirtualMachine">A <see cref="CreateVirtualMachineDelegate"/> delegate.</param>
	/// <param name="getCreatedVirtualMachines">A <see cref="GetCreatedVirtualMachinesDelegate"/> delegate.</param>
	internal JVirtualMachineLibrary(IntPtr handle,
		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs,
		CreateVirtualMachineDelegate createVirtualMachine, GetCreatedVirtualMachinesDelegate getCreatedVirtualMachines)
	{
		this.Handle = handle;
		this._getDefaultVirtualMachineInitArgs = getDefaultVirtualMachineInitArgs;
		this._createVirtualMachine = createVirtualMachine;
		this._getCreatedVirtualMachines = getCreatedVirtualMachines;
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
			JVirtualMachineInitArgumentValue initValue = new() { Version = jniVersion, };
			if (this._getDefaultVirtualMachineInitArgs(ref initValue) != JResult.Ok)
				break;
			version = jniVersion;
		}
		return version > 0 ? version : throw new InvalidOperationException();
	}
	/// <summary>
	/// Retrieves the default VM initialization argument for current JVM library.
	/// </summary>
	/// <param name="jniVersion">The requested JNI version.</param>
	/// <returns>A <see cref="JVirtualMachineInitArg"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public JVirtualMachineInitArg GetDefaultArgument(Int32 jniVersion = 0x00010006)
	{
		JVirtualMachineInitArgumentValue initValue = new()
		{
			Version = jniVersion < JVirtualMachineLibrary.jniVersions[0] ?
				JVirtualMachineLibrary.jniVersions[0] :
				jniVersion,
		};
		JResult result = this._getDefaultVirtualMachineInitArgs(ref initValue);
		if (result == JResult.Ok) return new(initValue);
		throw new JniException(result);
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
		CStringSequence sequence = JVirtualMachineInitOption.GetOptionsSequence(arg.Options);
		using IFixedPointer.IDisposable fPtr = sequence.GetFixedPointer();
		using IFixedContext<JVirtualMachineInitOptionValue>.IDisposable options =
			JVirtualMachineInitOption.GetContext(sequence);
		JVirtualMachineInitArgumentValue value = new()
		{
			Version = arg.Version,
			OptionsLenght = options.Values.Length,
			Options = options.ValuePointer,
			IgnoreUnrecognized = ((JBoolean)arg.IgnoreUnrecognized).ByteValue,
		};
		JResult result = this._createVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef, in value);
		if (result == JResult.Ok)
			return JVirtualMachine.GetVirtualMachine(vmRef, envRef, out env);
		throw new JniException(result);
	}
	/// <summary>
	/// Retrieves all of the created <see cref="IVirtualMachine"/> instances.
	/// </summary>
	/// <returns>A read-only list of <see cref="IVirtualMachine"/> instances.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public IVirtualMachine[] GetCreatedVirtualMachines()
	{
		_ = this._getCreatedVirtualMachines(ValPtr<JVirtualMachineRef>.Zero, 0, out Int32 vmCount);
		if (vmCount == 0) return Array.Empty<IVirtualMachine>();
		JVirtualMachineRef[] arr = this.GetCreatedVirtualMachines(vmCount, out JResult result);
		if (result == JResult.Ok)
			return arr.Select(JVirtualMachine.GetVirtualMachine).ToArray();
		throw new JniException(result);
	}

	/// <summary>
	/// Retrieves all of the created <see cref="JVirtualMachineRef"/> instances.
	/// </summary>
	/// <param name="vmCount">Number of elements to retrieve.</param>
	/// <param name="result">Output. JNI call result.</param>
	/// <returns>An array of <see cref="JVirtualMachineRef"/> references.</returns>
	private JVirtualMachineRef[] GetCreatedVirtualMachines(Int32 vmCount, out JResult result)
	{
		JVirtualMachineRef[] arr = new JVirtualMachineRef[vmCount];
		using IFixedContext<JVirtualMachineRef>.IDisposable fixedContext = arr.AsMemory().GetFixedContext();
		result = this._getCreatedVirtualMachines(fixedContext.ValuePointer, arr.Length, out vmCount);
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
	public static JVirtualMachineLibrary? LoadLibrary(String libraryPath)
	{
		IntPtr? handle = NativeUtilities.LoadNativeLib(libraryPath);
		if (!handle.HasValue)
			return default;
		GetDefaultVirtualMachineInitArgsDelegate? getDefaultVirtualMachineInitArgs =
			NativeUtilities.GetNativeMethod<GetDefaultVirtualMachineInitArgsDelegate>(
				handle.Value, JVirtualMachineLibrary.getDefaultVirtualMachineInitArgsName);
		CreateVirtualMachineDelegate? createVirtualMachine =
			NativeUtilities.GetNativeMethod<CreateVirtualMachineDelegate>(
				handle.Value, JVirtualMachineLibrary.createVirtualMachineName);
		GetCreatedVirtualMachinesDelegate? getCreatedVirtualMachines =
			NativeUtilities.GetNativeMethod<GetCreatedVirtualMachinesDelegate>(
				handle.Value, JVirtualMachineLibrary.getCreatedVirtualMachinesName);
		if (getDefaultVirtualMachineInitArgs is not null && createVirtualMachine is not null &&
		    getCreatedVirtualMachines is not null)
			return new(handle.Value, getDefaultVirtualMachineInitArgs, createVirtualMachine, getCreatedVirtualMachines);
		NativeLibrary.Free(handle.Value);
		return default;
	}
}