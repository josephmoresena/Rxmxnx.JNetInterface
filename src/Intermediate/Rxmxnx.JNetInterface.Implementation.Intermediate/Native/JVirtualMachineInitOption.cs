namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores an option for a VM initialization.
/// </summary>
public sealed class JVirtualMachineInitOption
{
	/// <summary>
	/// Option name.
	/// </summary>
	public CString Name { get; init; }
	/// <summary>
	/// Option extra information.
	/// </summary>
	public CString ExtraInfo { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public JVirtualMachineInitOption()
	{
		this.Name = CString.Zero;
		this.ExtraInfo = CString.Zero;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="value">A <see cref="VirtualMachineInitOptionValue"/> value.</param>
	private JVirtualMachineInitOption(VirtualMachineInitOptionValue value)
	{
		this.Name = JVirtualMachineInitOption.GetUnsafeCString(value.Name);
		this.ExtraInfo = JVirtualMachineInitOption.GetUnsafeCString(value.ExtraInfo);
	}

	/// <inheritdoc cref="Object.ToString()"/>
	internal String ToSimplifiedString()
		=> $"{{ {nameof(JVirtualMachineInitOption.Name)} = {this.Name}, {nameof(JVirtualMachineInitOption.ExtraInfo)} = {this.ExtraInfo} }}";

	/// <summary>
	/// Creates a <see cref="JVirtualMachineInitOption"/> array from a read-only span of
	/// <see cref="VirtualMachineInitOptionValue"/> values.
	/// </summary>
	/// <param name="values">A read-only span of <see cref="VirtualMachineInitOptionValue"/> values.</param>
	/// <returns>A <see cref="JVirtualMachineInitOption"/> array.</returns>
	internal static JVirtualMachineInitOption[] GetOptions(ReadOnlySpan<VirtualMachineInitOptionValue> values)
	{
		JVirtualMachineInitOption[] result = new JVirtualMachineInitOption[values.Length];
		for (Int32 i = 0; i < values.Length; i++)
			result[i] = new(values[i]);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="CStringSequence"/> from <paramref name="options"/> array.
	/// </summary>
	/// <param name="options">A <see cref="JVirtualMachineInitOption"/> array.</param>
	/// <returns>A <see cref="CStringSequence"/> instance.</returns>
	internal static CStringSequence GetOptionsSequence(IList<JVirtualMachineInitOption> options)
	{
		CString[] list = new CString[options.Count * 2];
		for (Int32 index = 0; index < options.Count; index++)
		{
			JVirtualMachineInitOption option = options[index];
			list[index * 2] = option.Name;
			list[index * 2 + 1] = option.ExtraInfo;
		}
		return new(list);
	}
	/// <summary>
	/// Creates a <see cref="Memory{JVirtualMachineInitOptionValue}"/> from <see cref="CStringSequence"/>
	/// instance.
	/// </summary>
	/// <param name="sequence">A fixed <see cref="CStringSequence"/> instance with UTF-8 text.</param>
	/// <returns>A <see cref="Memory{JVirtualMachineInitOptionValue}"/> array.</returns>
	internal static Span<VirtualMachineInitOptionValue> GetSpan(CStringSequence sequence)
	{
		VirtualMachineInitOptionValue[] arr = new VirtualMachineInitOptionValue[sequence.Count / 2];
		for (Int32 i = 0; i < arr.Length; i += 2)
			arr[i] = new(sequence[i].AsSpan().GetUnsafeValPtr(), sequence[i + 1].AsSpan().GetUnsafeValPtr());
		return arr.AsSpan();
	}

	/// <summary>
	/// Retrieves an unsafe <see cref="CString"/> from given pointer.
	/// </summary>
	/// <param name="ptr">A UTF-8 string pointer.</param>
	/// <returns>A <see cref="CString"/> instance.</returns>
	private static CString GetUnsafeCString(IntPtr ptr)
	{
		Int32 length = 0;
		while ((ptr + length).GetUnsafeReference<Byte>() != default)
			length++;
		return CString.CreateUnsafe(ptr, length, true);
	}
}