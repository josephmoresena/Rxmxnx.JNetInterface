namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Assignation types cache.
	/// </summary>
	internal AssignableTypeCache AssignationCache { get; } = new();
	/// <summary>
	/// Indicates whether the current instance has objects.
	/// </summary>
	internal Boolean HasObjects => !this._objects.IsEmpty;
	/// <summary>
	/// Indicates whether the current instance is disposable.
	/// </summary>
	private protected virtual Boolean IsDisposable => false;

	/// <summary>
	/// Indicates whether the current instance is minimally valid.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if current instance is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	internal Boolean IsMinimalValid(IEnvironment env)
	{
		if (!this.IsValidInstance) return false;
		return env.IsValidationAvoidable(this) || this.IsValid(env);
	}
	/// <summary>
	/// Refresh current metadata instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	internal void RefreshMetadata(ILocalObject jLocal) => this.ObjectMetadata = ILocalObject.CreateMetadata(jLocal);
	/// <inheritdoc/>
	private protected override Boolean IsInstanceOf<TDataType>()
	{
		if (this.ObjectClassName.AsSpan().SequenceEqual(IDataType.GetMetadata<TDataType>().ClassName)) return true;
		Boolean? result = this.AssignationCache.IsAssignableTo<TDataType>();
		if (result.HasValue) return result.Value;
		return this.JniSecure() ?
			JGlobalBase.IsInstanceOf<TDataType>(this) :
			Task.Factory.StartNew(JGlobalBase.IsInstanceOf<TDataType>, this, TaskCreationOptions.LongRunning).Result;
	}
	/// <inheritdoc/>
	private protected override ReadOnlySpan<Byte> AsSpan() => this._value.Reference.AsBytes();
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this.AssignationCache.SetAssignableTo<TDataType>(isAssignable);
	/// <inheritdoc/>
	internal override void ClearValue() => this._value.Value = default;
	/// <inheritdoc/>
	private protected override IDisposable GetSynchronizer()
	{
		using IThread env = this.VirtualMachine.CreateThread(ThreadPurpose.SynchronizeGlobalReference);
		return env.ReferenceFeature.GetSynchronizer(this);
	}
	/// <inheritdoc/>
	private protected override Boolean Same(JReferenceObject jObject)
	{
		if (base.Same(jObject)) return true;
		using IThread thread = this.VirtualMachine.CreateThread(ThreadPurpose.CheckGlobalReference);
		return thread.IsSameObject(this, jObject);
	}
}