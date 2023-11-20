namespace Rxmxnx.JNetInterface.Native;

public partial class JGlobalBase
{
	/// <summary>
	/// Instance metadata.
	/// </summary>
	internal JObjectMetadata ObjectMetadata => this._objectMetadata;

	/// <summary>
	/// Refresh current metadata instance.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	internal void RefreshMetadata(ILocalObject jLocal) { this._objectMetadata = ILocalObject.CreateMetadata(jLocal); }

	/// <inheritdoc/>
	internal override Boolean IsAssignableTo<TDataType>()
	{
		if (this._assignableTypes.IsAssignableTo<TDataType>())
			return true;
		return this.IsAssignableToTask<TDataType>().Result;
	}
	/// <inheritdoc/>
	internal override void SetAssignableTo<TDataType>() => this._assignableTypes.SetAssignableTo<TDataType>();

	/// <summary>
	/// Task to perform <see cref="JReferenceObject.IsAssignableTo{TDataType}()"/> method.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// A task that represents the operation of <see cref="JReferenceObject.IsAssignableTo{TDataType}()"/>. The result of task
	/// contains
	/// <see langword="true"/> if current instance is assignable to <typeparamref name="TDataType"/> type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	internal abstract Task<Boolean> IsAssignableToTask<TDataType>();
}