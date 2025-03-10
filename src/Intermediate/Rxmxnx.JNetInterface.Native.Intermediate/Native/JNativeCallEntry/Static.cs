namespace Rxmxnx.JNetInterface.Native;

public partial class JNativeCallEntry
{
	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using <paramref name="definition"/> and
	/// <paramref name="ptr"/>.
	/// </summary>
	/// <param name="definition">Java call definition.</param>
	/// <param name="ptr">Pointer to function.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry Create(JMethodDefinition definition, IntPtr ptr) => new(ptr, definition);
	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using <paramref name="definition"/> and
	/// <paramref name="ptr"/>.
	/// </summary>
	/// <param name="definition">Java call definition.</param>
	/// <param name="ptr">Pointer to function.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry Create(JFunctionDefinition definition, IntPtr ptr) => new(ptr, definition);
	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using <paramref name="definition"/> and
	/// <paramref name="managedFunction"/>.
	/// </summary>
	/// <typeparam name="T">Type of call.</typeparam>
	/// <param name="definition">Java call definition.</param>
	/// <param name="managedFunction">Delegate.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry Create<T>(JMethodDefinition definition, T managedFunction) where T : Delegate
		=> new GenericEntry<T>(managedFunction, definition);
	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using <paramref name="definition"/> and
	/// <paramref name="del"/>.
	/// </summary>
	/// <typeparam name="T">Type of call.</typeparam>
	/// <param name="definition">Java call definition.</param>
	/// <param name="del">Delegate.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry Create<T>(JFunctionDefinition definition, T del) where T : Delegate
		=> new GenericEntry<T>(del, definition);

	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using parameterless definition and
	/// instance parameterless method delegate.
	/// </summary>
	/// <param name="definition">Java parameterless method call definition.</param>
	/// <param name="del">Instance parameterless method definition.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry CreateParameterless(JMethodDefinition.Parameterless definition,
		ParameterlessInstanceMethodDelegate del)
		=> new GenericEntry<ParameterlessInstanceMethodDelegate>(del, definition);
	/// <summary>
	/// Creates a <see cref="JNativeCallEntry"/> instance using parameterless definition and
	/// static parameterless method delegate.
	/// </summary>
	/// <param name="definition">Java parameterless method call definition.</param>
	/// <param name="del">Static parameterless method definition.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry CreateParameterless(JMethodDefinition.Parameterless definition,
		ParameterlessStaticMethodDelegate del)
		=> new GenericEntry<ParameterlessStaticMethodDelegate>(del, definition);
}