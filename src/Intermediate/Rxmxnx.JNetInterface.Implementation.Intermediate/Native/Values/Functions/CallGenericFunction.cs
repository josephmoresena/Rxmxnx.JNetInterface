namespace Rxmxnx.JNetInterface.Native.Values.Functions;

/// <summary>
/// Set of function pointers to call Java functions through JNI.
/// </summary>
/// <typeparam name="TReceiver">Type of receiver function.</typeparam>
/// <typeparam name="TResult">Type of return function.</typeparam>
[StructLayout(LayoutKind.Explicit)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal readonly unsafe struct CallGenericFunction<TReceiver, TResult>
	where TReceiver : unmanaged, IWrapper<JObjectLocalRef> where TResult : unmanaged, INativeType
{
	/// <summary>
	/// Managed caller <c>A</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly Managed _managed;
	/// <summary>
	/// Unmanaged caller <c>A</c> function.
	/// </summary>
	[FieldOffset(0)]
	private readonly Unmanaged _unmanaged;

	/// <summary>
	/// Calls <c>A</c> function.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public TResult Call(JEnvironmentRef envRef, TReceiver receiver, JMethodId methodId, JValue* args)
		=> MethodOffset.UseManagedGenericPointers ?
			this._managed.Call(envRef, receiver, methodId, args) :
			this._unmanaged.Call(envRef, receiver, methodId, args);

#pragma warning disable CS0169
#pragma warning disable CS0649
	/// <summary>
	/// Managed caller pointer
	/// </summary>
	private readonly struct Managed
	{
		/// <summary>
		/// Internal reserved entries.
		/// </summary>
		private readonly MethodOffset _offset;
		/// <summary>
		/// Caller <c>A</c> function.
		/// </summary>
		public readonly delegate* managed<JEnvironmentRef, TReceiver, JMethodId, JValue*, TResult> Call;
	}

	/// <summary>
	/// Unmanaged caller pointer
	/// </summary>
	private readonly struct Unmanaged
	{
		/// <summary>
		/// Internal reserved entries.
		/// </summary>
		private readonly MethodOffset _offset;
		/// <summary>
		/// Caller <c>A</c> function.
		/// </summary>
		public readonly delegate* unmanaged<JEnvironmentRef, TReceiver, JMethodId, JValue*, TResult> Call;
	}
#pragma warning restore CS0169
#pragma warning restore CS0649
}