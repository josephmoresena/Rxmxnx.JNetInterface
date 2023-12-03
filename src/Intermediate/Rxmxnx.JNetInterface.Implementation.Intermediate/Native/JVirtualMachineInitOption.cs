namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores an option for a VM initialization.
/// </summary>
public sealed record JVirtualMachineInitOption
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
	/// <param name="value">A <see cref="JVirtualMachineInitOptionValue"/> value.</param>
	internal JVirtualMachineInitOption(JVirtualMachineInitOptionValue value)
	{
		this.Name = JVirtualMachineInitOption.GetUnsafeCString(value.Name);
		this.ExtraInfo = JVirtualMachineInitOption.GetUnsafeCString(value.ExtraInfo);
	}

	/// <summary>
	/// Creates a <see cref="JVirtualMachineInitOption"/> array from a read-only span of
	/// <see cref="JVirtualMachineInitOptionValue"/> values.
	/// </summary>
	/// <param name="values">A read-only span of <see cref="JVirtualMachineInitOptionValue"/> values.</param>
	/// <returns>A <see cref="JVirtualMachineInitOption"/> array.</returns>
	internal static JVirtualMachineInitOption[] GetOptions(ReadOnlySpan<JVirtualMachineInitOptionValue> values)
	{
		JVirtualMachineInitOption[] result = new JVirtualMachineInitOption[values.Length];
		for (Int32 i = 0; i < result.Length; i++)
			result[i] = new(values[i]);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="CStringSequence"/> from <paramref name="options"/> array.
	/// </summary>
	/// <param name="options">A <see cref="JVirtualMachineInitOption"/> array.</param>
	/// <returns>A <see cref="CStringSequence"/> instance.</returns>
	internal static CStringSequence GetOptionsSequence(JVirtualMachineInitOption[] options)
	{
		List<CString> list = new(options.Length * 2);
		foreach (JVirtualMachineInitOption option in options)
		{
			list.Add(option.Name);
			list.Add(option.ExtraInfo);
		}
		return new(list);
	}
	/// <summary>
	/// Creates a <see cref="JVirtualMachineInitOptionValue"/> read-only span from
	/// <see cref="ReadOnlyFixedMemoryList"/> instance.
	/// </summary>
	/// <param name="memoryList">A <see cref="ReadOnlyFixedMemoryList"/> instance with UTF-8 text.</param>
	/// <returns>A <see cref="JVirtualMachineInitOptionValue"/> read-only span.</returns>
	internal static ReadOnlySpan<JVirtualMachineInitOptionValue> GetSpan(ReadOnlyFixedMemoryList memoryList)
	{
		Span<JVirtualMachineInitOptionValue> result = new JVirtualMachineInitOptionValue[memoryList.Count / 2];
		for (Int32 i = 0; i < result.Length; i += 2)
			result[i] = new() { Name = (ReadOnlyValPtr<Byte>)memoryList[i].Pointer, ExtraInfo = memoryList[i + 1].Pointer, };
		return result;
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