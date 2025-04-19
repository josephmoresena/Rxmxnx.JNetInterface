namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class is a JNI instance created from an invalid <see cref="IVirtualMachine"/> instance.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
#endif
internal sealed partial class DeadThread(IVirtualMachine vm) : IThread
{
	/// <summary>
	/// A <see cref="IVirtualMachine"/> instance.
	/// </summary>
	private IVirtualMachine VirtualMachine { get; } = vm;

	Boolean IThread.Attached => false;
	Boolean IThread.Daemon => false;
	CString IThread.Name => CString.Empty;
	JEnvironmentRef IEnvironment.Reference => this.ThrowInvalidResult<JEnvironmentRef>();
	IVirtualMachine IEnvironment.VirtualMachine => this.VirtualMachine;
	Int32 IEnvironment.Version => this.ThrowInvalidResult<Int32>();
	Int32? IEnvironment.LocalCapacity
	{
		get => this.ThrowInvalidResult<Int32>();
		set => _ = this.ThrowInvalidResult<Int32>() + value;
	}
	ThrowableException? IEnvironment.PendingException
	{
		get => this.ThrowInvalidResult<ThrowableException?>();
		set => _ = this.ThrowInvalidResult<ThrowableException?>() ?? value;
	}
	Int32 IEnvironment.UsedStackBytes => this.ThrowInvalidResult<Int32>();
	Int32 IEnvironment.UsableStackBytes
	{
		get => this.ThrowInvalidResult<Int32>();
		set => _ = value + this.ThrowInvalidResult<Int32>();
	}
	IAccessFeature IEnvironment.AccessFeature => this.ThrowInvalidResult<IAccessFeature>();
	IClassFeature IEnvironment.ClassFeature => this;
	IReferenceFeature IEnvironment.ReferenceFeature => this;
	IStringFeature IEnvironment.StringFeature => this;
	IArrayFeature IEnvironment.ArrayFeature => this;
	INioFeature IEnvironment.NioFeature => this.ThrowInvalidResult<INioFeature>();
	NativeFunctionSet IEnvironment.FunctionSet => this.ThrowInvalidResult<NativeFunctionSet>();
	Boolean IEnvironment.NoProxy => false;

	Boolean IEnvironment.IsValidationAvoidable(JGlobalBase jGlobal) => true;
	JReferenceType IEnvironment.GetReferenceType(JObject jObject)
	{
		this.GetReferenceTypeTrace(jObject);
		return JReferenceType.InvalidRefType;
	}
	Boolean IEnvironment.IsSameObject(JObject jObject, JObject? jOther)
	{
		this.IsSameObjectTrace(jObject, jOther);
		return Object.ReferenceEquals(jObject, jOther);
	}
	Boolean IEnvironment.JniSecure() => true;
	void IEnvironment.WithFrame(Int32 capacity, Action action) => this.ThrowInvalidResult<Byte>();
	void IEnvironment.WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
		=> this.ThrowInvalidResult<Byte>();
	TResult IEnvironment.WithFrame<TResult>(Int32 capacity, Func<TResult> func) => this.ThrowInvalidResult<TResult>();
	TResult IEnvironment.WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
		=> this.ThrowInvalidResult<TResult>();
	void IEnvironment.DescribeException() => this.ThrowInvalidResult<Byte>();
	Boolean? IEnvironment.IsVirtual(JThreadObject jThread) => this.ThrowInvalidResult<Boolean?>();
	void IDisposable.Dispose() { this.TraceDispose(); }

	/// <summary>
	/// Throws an <see cref="InvalidOperationException"/>.
	/// </summary>
	/// <typeparam name="TResult">Type of expected result.</typeparam>
	/// <returns>A <typeparamref name="TResult"/> instance.</returns>
	/// <exception cref="InvalidOperationException">Always throws.</exception>
	private TResult ThrowInvalidResult<TResult>()
		=> throw new InvalidOperationException(
			$"JVM {this.VirtualMachine.Reference} was destroyed. Please create a new one in order to use JNI.");
}