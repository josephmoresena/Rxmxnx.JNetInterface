namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Instance metadata.
	/// </summary>
	internal ObjectMetadata ObjectMetadata { get; private set; }
	/// <summary>
	/// Assignation types cache.
	/// </summary>
	internal AssignableTypeCache AssignationCache { get; } = new();
	/// <summary>
	/// Indicates whether current instance has objects.
	/// </summary>
	internal Boolean HasObjects => !this._objects.IsEmpty;
	/// <summary>
	/// Indicates whether current instance is disposable.
	/// </summary>
	private protected virtual Boolean IsDisposable => false;

	/// <summary>
	/// Refresh current metadata instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	internal void RefreshMetadata(ILocalObject jLocal) { this.ObjectMetadata = ILocalObject.CreateMetadata(jLocal); }
	/// <inheritdoc/>
	private protected override ReadOnlySpan<Byte> AsSpan() => this._value.Reference.AsBytes();
	/// <inheritdoc/>
	internal override Boolean IsAssignableTo<TDataType>()
	{
		Boolean? result = this.AssignationCache.IsAssignableTo<TDataType>();
		if (result.HasValue) return result.Value;
		return this.JniSecure() ?
			JGlobalBase.IsAssignableTo<TDataType>(this) :
			Task.Factory.StartNew(JGlobalBase.IsAssignableTo<TDataType>, this).Result;
	}
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
	/// <inheritdoc/>
	internal override void ClearValue() => this._value.Value = default;
	/// <inheritdoc/>
	private protected override IDisposable GetSynchronizer()
	{
		IThread env = this.VirtualMachine.CreateThread(ThreadPurpose.SynchronizeGlobalReference);
		return env.ReferenceFeature.GetSynchronizer(this);
	}

	/// <inheritdoc cref="JReferenceObject.IsAssignableTo{TDataType}"/>
	/// <param name="obj">A <see cref="JGlobalBase"/> instance.</param>
	private static Boolean IsAssignableTo<TDataType>(Object? obj)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		if (obj is not JGlobalBase jGlobal) return false;
		using IThread thread = jGlobal.VirtualMachine.CreateThread(ThreadPurpose.CheckAssignability);
		return thread.ClassFeature.IsAssignableTo<TDataType>(jGlobal);
	}
}