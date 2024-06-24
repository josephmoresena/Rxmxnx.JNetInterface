namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java native call entry.
/// </summary>
public record JNativeCallEntry : IFixedPointer
{
	/// <summary>
	/// Represents a parameterless instance method delegate.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public delegate void ParameterlessInstanceMethodDelegate(JEnvironmentRef envRef, JObjectLocalRef localRef);

	/// <summary>
	/// Represents a parameterless static method delegate.
	/// </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public delegate void ParameterlessStaticMethodDelegate(JEnvironmentRef envRef, JClassLocalRef classRef);

	/// <summary>
	/// Internal call definition.
	/// </summary>
	private readonly JCallDefinition _definition;

	/// <summary>
	/// Method name.
	/// </summary>
	public CString Name => this._definition.Name;
	/// <summary>
	/// Method descriptor.
	/// </summary>
	public CString Descriptor => this._definition.Descriptor;
	/// <summary>
	/// Definition hash.
	/// </summary>
	public String Hash => this._definition.Hash;
	/// <summary>
	/// Managed function.
	/// </summary>
	public virtual Delegate? Delegate => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="ptr">A <see cref="IntPtr"/> function pointer.</param>
	/// <param name="definition">A <see cref="JCallDefinition"/> instance.</param>
	private JNativeCallEntry(IntPtr ptr, JCallDefinition definition)
	{
		this.Pointer = ptr;
		this._definition = definition;
	}
	/// <inheritdoc/>
	public IntPtr Pointer { get; }

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
	/// Java native method entry.
	/// </summary>
	private sealed record GenericEntry<T> : JNativeCallEntry where T : Delegate
	{
		/// <inheritdoc/>
		public override Delegate? Delegate { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="del">Delegate.</param>
		/// <param name="definition">Definition.</param>
		public GenericEntry(T del, JCallDefinition definition) : base(del.GetUnsafeIntPtr(), definition)
			=> this.Delegate = del;
	}
}