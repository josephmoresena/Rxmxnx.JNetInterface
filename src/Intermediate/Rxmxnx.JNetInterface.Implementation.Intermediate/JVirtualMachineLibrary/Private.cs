namespace Rxmxnx.JNetInterface;

public sealed unsafe partial class JVirtualMachineLibrary
{
	/// <summary>
	/// Pointer to exported Java Library functions.
	/// </summary>
	private readonly InvocationFunctionSet _functions;

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
	/// Retrieves all the created <see cref="JVirtualMachineRef"/> instances.
	/// </summary>
	/// <param name="vmCount">Number of elements to retrieve.</param>
	/// <param name="result">Output. JNI call result.</param>
	/// <returns>An array of <see cref="JVirtualMachineRef"/> references.</returns>
	private JVirtualMachineRef[] GetCreatedVirtualMachines(Int32 vmCount, out JResult result)
	{
		JVirtualMachineRef[] arr = new JVirtualMachineRef[vmCount];
		fixed (JVirtualMachineRef* ptr = arr)
			result = this._functions.GetCreatedVirtualMachines(ptr, arr.Length, out vmCount);
		return arr;
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