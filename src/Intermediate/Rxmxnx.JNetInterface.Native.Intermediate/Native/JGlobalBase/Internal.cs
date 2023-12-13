namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Instance metadata.
	/// </summary>
	internal JObjectMetadata ObjectMetadata => this._objectMetadata;
	/// <summary>
	/// Indicates whether current instance has objects.
	/// </summary>
	internal Boolean HasObjects => !this._objects.IsEmpty;
	/// <summary>
	/// Assignation types cache.
	/// </summary>
	internal AssignableTypeCache AssignationCache => this._assignableTypes;

	/// <summary>
	/// Refresh current metadata instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	internal void RefreshMetadata(ILocalObject jLocal) { this._objectMetadata = ILocalObject.CreateMetadata(jLocal); }
	/// <inheritdoc/>
	internal override ReadOnlySpan<Byte> AsSpan() => this._value.Reference.AsBytes();
	/// <inheritdoc/>
	internal override Boolean IsAssignableTo<TDataType>()
	{
		Boolean? result = this._assignableTypes.IsAssignableTo<TDataType>();
		if (result.HasValue) return result.Value;
		return this.JniSecure() ?
			JGlobalBase.IsAssignableTo<TDataType>(this) :
			Task.Factory.StartNew(JGlobalBase.IsAssignableTo<TDataType>, this).Result;
	}
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>(Boolean isAssignable)
		=> this._assignableTypes.SetAssignableTo<TDataType>(isAssignable);
	/// <inheritdoc/>
	internal override void ClearValue() => this._value.Value = default;

	/// <inheritdoc cref="JReferenceObject.IsAssignableTo{TDataType}"/>
	/// <param name="obj">A <see cref="JGlobalBase"/> instance.</param>
	private static Boolean IsAssignableTo<TDataType>(Object? obj)
		where TDataType : JReferenceObject, IDataType<TDataType>
	{
		if (obj is not JGlobalBase jGlobal) return false;
		using IThread thread = jGlobal.VirtualMachine.CreateThread(ThreadPurpose.CheckAssignability);
		return thread.ClassProvider.IsAssignableTo<TDataType>(jGlobal);
	}
}