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
	private JVirtualMachineInitOption(JVirtualMachineInitOptionValue value)
	{
		this.Name = JVirtualMachineInitOption.GetUnsafeCString(value.Name);
		this.ExtraInfo = JVirtualMachineInitOption.GetUnsafeCString(value.ExtraInfo);
	}

	/// <inheritdoc cref="Object.ToString()"/>
	internal String ToSimplifiedString()
		=> $"{{ {nameof(JVirtualMachineInitOption.Name)} = {this.Name}, {nameof(JVirtualMachineInitOption.ExtraInfo)} = {this.ExtraInfo} }}";

	/// <summary>
	/// Creates a <see cref="JVirtualMachineInitOption"/> array from a read-only span of
	/// <see cref="JVirtualMachineInitOptionValue"/> values.
	/// </summary>
	/// <param name="values">A read-only span of <see cref="JVirtualMachineInitOptionValue"/> values.</param>
	/// <returns>A <see cref="JVirtualMachineInitOption"/> array.</returns>
	internal static List<JVirtualMachineInitOption> GetOptions(ReadOnlySpan<JVirtualMachineInitOptionValue> values)
	{
		List<JVirtualMachineInitOption> result = new(values.Length);
		for (Int32 i = 0; i < values.Length; i++)
			result.Add(new(values[i]));
		return result;
	}
	/// <summary>
	/// Creates a <see cref="CStringSequence"/> from <paramref name="options"/> array.
	/// </summary>
	/// <param name="options">A <see cref="JVirtualMachineInitOption"/> array.</param>
	/// <returns>A <see cref="CStringSequence"/> instance.</returns>
	internal static CStringSequence GetOptionsSequence(IList<JVirtualMachineInitOption> options)
	{
		List<CString> list = new(options.Count * 2);
		foreach (JVirtualMachineInitOption option in options)
		{
			list.Add(option.Name);
			list.Add(option.ExtraInfo);
		}
		return new(list);
	}
	/// <summary>
	/// Creates a <see cref="IFixedContext{JVirtualMachineInitOptionValue}.IDisposable"/> from <see cref="CStringSequence"/>
	/// instance.
	/// </summary>
	/// <param name="sequence">A fixed <see cref="CStringSequence"/> instance with UTF-8 text.</param>
	/// <returns>A <see cref="IFixedContext{JVirtualMachineInitOptionValue}.IDisposable"/> array.</returns>
	internal static IFixedContext<JVirtualMachineInitOptionValue>.IDisposable GetContext(CStringSequence sequence)
	{
		JVirtualMachineInitOptionValue[] arr = new JVirtualMachineInitOptionValue[sequence.Count / 2];
		for (Int32 i = 0; i < arr.Length; i += 2)
		{
			arr[i] = new()
			{
				Name = sequence[i].AsSpan().GetUnsafeValPtr(),
				ExtraInfo = sequence[i + 1].AsSpan().GetUnsafeValPtr(),
			};
		}
		Memory<JVirtualMachineInitOptionValue> mem = arr.AsMemory();
		IFixedContext<JVirtualMachineInitOptionValue>.IDisposable ctx = mem.GetFixedContext();
		return ctx;
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