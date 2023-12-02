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
	/// Library handler.
	/// </summary>
	public IntPtr Handler { get; private init; }

	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="handler">Library handler.</param>
	/// <param name="getDefaultVirtualMachineInitArgs">A <see cref="GetDefaultVirtualMachineInitArgsDelegate"/> delegate.</param>
	/// <param name="createVirtualMachine">A <see cref="CreateVirtualMachineDelegate"/> delegate.</param>
	/// <param name="getCreatedVirtualMachines">A <see cref="GetCreatedVirtualMachinesDelegate"/> delegate.</param>
	internal JVirtualMachineLibrary(IntPtr handler,
		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs,
		CreateVirtualMachineDelegate createVirtualMachine, GetCreatedVirtualMachinesDelegate getCreatedVirtualMachines)
	{
		this.Handler = handler;
		this._getDefaultVirtualMachineInitArgs = getDefaultVirtualMachineInitArgs;
		this._createVirtualMachine = createVirtualMachine;
		this._getCreatedVirtualMachines = getCreatedVirtualMachines;
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
			Version = jniVersion < 0x00010006 ? 0x00010006 : jniVersion,
		};
		JResult result = this._getDefaultVirtualMachineInitArgs(ref initValue);
		if (result == JResult.Ok)
			return new(initValue);
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
		(IInvokedVirtualMachine vm, env) =
			sequence.WithSafeFixed((arg, this), JVirtualMachineLibrary.CreateVirtualMachine);
		return vm;
	}
	/// <summary>
	/// Retrieves all of the created <see cref="IVirtualMachine"/> instances.
	/// </summary>
	/// <returns>A read-only list of <see cref="IVirtualMachine"/> instances.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	public IReadOnlyList<IVirtualMachine> GetCreatedVirtualMachines()
	{
		_ = this._getCreatedVirtualMachines(ValPtr<JVirtualMachineRef>.Zero, 0,
		                                    out Int32 vmCount);
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
		result = this._getCreatedVirtualMachines((ValPtr<JVirtualMachineRef>)fixedContext.Pointer, arr.Length, out vmCount);
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
		IntPtr? handler = NativeUtilities.LoadNativeLib(libraryPath);
		if (!handler.HasValue)
			return default;
		GetDefaultVirtualMachineInitArgsDelegate? getDefaultVirtualMachineInitArgs =
			NativeUtilities.GetNativeMethod<GetDefaultVirtualMachineInitArgsDelegate>(
				handler.Value, JVirtualMachineLibrary.getDefaultVirtualMachineInitArgsName);
		CreateVirtualMachineDelegate? createVirtualMachine =
			NativeUtilities.GetNativeMethod<CreateVirtualMachineDelegate>(
				handler.Value, JVirtualMachineLibrary.createVirtualMachineName);
		GetCreatedVirtualMachinesDelegate? getCreatedVirtualMachines =
			NativeUtilities.GetNativeMethod<GetCreatedVirtualMachinesDelegate>(
				handler.Value, JVirtualMachineLibrary.getCreatedVirtualMachinesName);
		if (getDefaultVirtualMachineInitArgs is not null && createVirtualMachine is not null &&
		    getCreatedVirtualMachines is not null)
			return new(handler.Value, getDefaultVirtualMachineInitArgs, createVirtualMachine,
			           getCreatedVirtualMachines);
		NativeLibrary.Free(handler.Value);
		return default;
	}

	/// <summary>
	/// Creates <see cref="IInvokedVirtualMachine"/> instance.
	/// </summary>
	/// <param name="memoryList">A <see cref="ReadOnlyFixedMemoryList"/> with options UTF-8 text.</param>
	/// <param name="args">Tuple (<see cref="JVirtualMachineInitArg"/>, <see cref="JVirtualMachineLibrary"/>).</param>
	/// <returns>Created <see cref="IInvokedVirtualMachine"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	private static (IInvokedVirtualMachine vm, IEnvironment env) CreateVirtualMachine(
		ReadOnlyFixedMemoryList memoryList, (JVirtualMachineInitArg arg, JVirtualMachineLibrary library) args)
	{
		ReadOnlySpan<JVirtualMachineInitOptionValue> options = JVirtualMachineInitOption.GetSpan(memoryList);
		return options.WithSafeFixed(args, JVirtualMachineLibrary.CreateVirtualMachine);
	}
	/// <summary>
	/// Creates <see cref="IInvokedVirtualMachine"/> instance.
	/// </summary>
	/// <param name="ctx">A <see cref="IReadOnlyFixedContext{JVirtualMachineInitOptionValue}"/> with options.</param>
	/// <param name="args">Tuple (<see cref="JVirtualMachineInitArg"/>, <see cref="JVirtualMachineLibrary"/>).</param>
	/// <returns>Created <see cref="IInvokedVirtualMachine"/> instance.</returns>
	/// <exception cref="JniException">If JNI call ends with an error.</exception>
	private static (IInvokedVirtualMachine vm, IEnvironment env) CreateVirtualMachine(
		in IReadOnlyFixedContext<JVirtualMachineInitOptionValue> ctx,
		(JVirtualMachineInitArg arg, JVirtualMachineLibrary library) args)
	{
		JVirtualMachineInitArgumentValue value = new()
		{
			Version = args.arg.Version,
			OptionsLenght = ctx.Values.Length,
			Options = ctx.Pointer,
			IgnoreUnrecognized = ((JBoolean)args.arg.IgnoreUnrecognized).ByteValue,
		};
		JResult result =
			args.library._createVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef, in value);
		if (result == JResult.Ok)
			return (JVirtualMachine.GetVirtualMachine(vmRef, envRef, out IEnvironment env), env);
		throw new JniException(result);
	}
}