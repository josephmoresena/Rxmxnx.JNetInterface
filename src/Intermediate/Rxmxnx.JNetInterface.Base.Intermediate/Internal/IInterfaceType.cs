namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java interface type.
/// </summary>
internal interface IInterfaceType : IDataType
{
	/// <inheritdoc cref="IDataType.Final"/>
	new static virtual Boolean Final => ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IInterfaceType));
	static Boolean IDataType.Final => ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IInterfaceType));
}

/// <summary>
/// This interface exposes a java interface type.
/// </summary>
/// <typeparam name="TInterface">Type of current Java datatype.</typeparam>
internal interface IInterfaceType<out TInterface> : IInterfaceType, IDataType<TInterface>
	where TInterface : IInterfaceType<TInterface>
{
	static Boolean IInterfaceType.Final => false;
	static Boolean IDataType.Final => TInterface.Final;
}