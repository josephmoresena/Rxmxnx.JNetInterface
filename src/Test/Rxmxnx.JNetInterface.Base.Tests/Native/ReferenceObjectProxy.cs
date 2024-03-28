namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class ReferenceObjectProxy : JReferenceObject
{
	public override CString ObjectClassName { get; }
	public override CString ObjectSignature { get; }

	public ReferenceObjectProxy(CString className, CString signature, Boolean isProxy) : base(isProxy)
	{
		this.ObjectClassName = className;
		this.ObjectSignature = signature;
	}
	public ReferenceObjectProxy(JReferenceObject jObject) : base(jObject)
	{
		this.ObjectClassName = jObject.ObjectClassName;
		this.ObjectSignature = jObject.ObjectClassName;
	}

	public event Func<Type, JReferenceObject, Boolean> InstanceOfEvent = default!;
	public event ReadOnlySpanFunc<Byte> AsSpanEvent = default!;
	public event Func<IDisposable> GetSynchronizerEvent = default!;
	public event Action<Type, JReferenceObject> SetAssignableToEvent = default!;

	private protected override Boolean IsInstanceOf<TDataType>()
		=> this.InstanceOfEvent?.Invoke(typeof(TDataType), this) ?? false;
	private protected override ReadOnlySpan<Byte> AsSpan()
	{
		ReadOnlySpanFunc<Byte> asSpanEvent = this.AsSpanEvent ?? (() => ReadOnlySpan<Byte>.Empty);
		return asSpanEvent();
	}
	private protected override IDisposable GetSynchronizer() => this.GetSynchronizerEvent?.Invoke()!;

	internal override void ClearValue()
	{
		foreach (ref readonly Byte value in this.AsSpan())
			Unsafe.AsRef(in value) = default;
	}
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this.SetAssignableToEvent?.Invoke(typeof(TDataType), this);
}