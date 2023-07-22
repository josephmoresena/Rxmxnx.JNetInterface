namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This interface exposes a java interface type.
/// </summary>
/// <typeparam name="TInterface">Type of current Java datatype.</typeparam>
internal interface IInterfaceType<out TInterface> : IDataType<TInterface> where TInterface : IDataType<TInterface>
{
}