namespace Rxmxnx.JNetInterface.Internal;

internal sealed record DataTypeInfo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType> where TDataType : IDataType<TDataType>
{
	/// <summary>
	/// Indicates whether <typeparamref name="TDataType"/> is a java interface type.
	/// </summary>
	private readonly Boolean _interface;
	/// <summary>
	/// Indicates whether <typeparamref name="TDataType"/> is a java non-inheritable type.
	/// </summary>
	private readonly Boolean _final;

	public Boolean IsFinal => this._final && !this._interface;
	
	public DataTypeInfo()
	{
		Type type = typeof(TDataType);
		foreach (Type typeInterface in type.GetInterfaces())
		{
			if (typeInterface == typeof(IInterfaceType<TDataType>))
				this._interface = true;
			else if (typeInterface == typeof(IFinalType<TDataType>))
				this._final = true;
		}
	}
}