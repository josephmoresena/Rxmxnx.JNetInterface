namespace Rxmxnx.JNetInterface.Lang;

public partial class JStringObject
{
	/// <summary>
	/// Retrieves the native memory of UTF-16 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Char> GetChars(JMemoryReferenceKind referenceKind = JMemoryReferenceKind.Local)
	{
		IVirtualMachine vm = this.Environment.VirtualMachine;
		JNativeMemoryHandler handler = referenceKind switch
		{
			JMemoryReferenceKind.ThreadIndependent => JNativeMemoryHandler.CreateWeakHandler(this, false),
			JMemoryReferenceKind.ThreadUnrestricted => JNativeMemoryHandler.CreateGlobalHandler(this, false),
			_ => JNativeMemoryHandler.CreateHandler(this, false),
		};
		return new(vm, handler);
	}
	/// <summary>
	/// Retrieves the critical native memory of UTF-16 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Char> GetCriticalChars(
		JMemoryReferenceKind referenceKind = JMemoryReferenceKind.ThreadUnrestricted)
	{
		IVirtualMachine vm = this.Environment.VirtualMachine;
		JNativeMemoryHandler handler = referenceKind switch
		{
			JMemoryReferenceKind.ThreadIndependent => JNativeMemoryHandler.CreateWeakHandler(this, true),
			JMemoryReferenceKind.ThreadUnrestricted => JNativeMemoryHandler.CreateGlobalHandler(this, true),
			_ => JNativeMemoryHandler.CreateHandler(this, true),
		};
		return new(vm, handler);
	}
	/// <summary>
	/// Retrieves the native memory of UTF-8 characters.
	/// </summary>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <returns>A <see cref="JNativeMemory{Char}"/> instance.</returns>
	public JNativeMemory<Byte> GetUtf8Chars(JMemoryReferenceKind referenceKind = JMemoryReferenceKind.Local)
	{
		IVirtualMachine vm = this.Environment.VirtualMachine;
		JNativeMemoryHandler handler = referenceKind switch
		{
			JMemoryReferenceKind.ThreadIndependent => JNativeMemoryHandler.CreateUtf8WeakHandler(this),
			JMemoryReferenceKind.ThreadUnrestricted => JNativeMemoryHandler.CreateUtf8GlobalHandler(this),
			_ => JNativeMemoryHandler.CreateUtf8Handler(this),
		};
		return new(vm, handler);
	}
}