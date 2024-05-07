namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// Java native call entry.
/// </summary>
public record JNativeCallEntry : IFixedPointer
{
	/// <summary>
	/// Internal call definition.
	/// </summary>
	private readonly JCallDefinition _definition;

	/// <summary>
	/// Method name.
	/// </summary>
	public CString Name => this._definition.Information[0];
	/// <summary>
	/// Method signature.
	/// </summary>
	public CString Signature => this._definition.Information[1];
	/// <summary>
	/// Definition hash.
	/// </summary>
	public String Hash => this._definition.Information.ToString();
	/// <inheritdoc/>
	public IntPtr Pointer { get; }

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
	/// <paramref name="del"/>.
	/// </summary>
	/// <typeparam name="T">Type of call.</typeparam>
	/// <param name="definition">Java call definition.</param>
	/// <param name="del">Delegate.</param>
	/// <returns>A <see cref="JNativeCallEntry"/> instance.</returns>
	public static JNativeCallEntry Create<T>(JMethodDefinition definition, T del) where T : Delegate
		=> new GenericEntry<T>(del, definition);
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
		/// <summary>
		/// Delegate.
		/// </summary>
		private readonly T _del;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="del">Delegate.</param>
		/// <param name="definition">Definition.</param>
		public GenericEntry(T del, JCallDefinition definition) : base(del.GetUnsafeIntPtr(), definition)
			=> this._del = del;
	}
}