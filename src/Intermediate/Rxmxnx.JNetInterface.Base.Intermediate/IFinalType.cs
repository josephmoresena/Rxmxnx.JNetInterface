namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a java non-inheritable type.
/// </summary>
/// <typeparam name="TFinal">Type of current Java datatype.</typeparam>
public interface IFinalType<out TFinal> : IDataType<TFinal> where TFinal : IDataType<TFinal>
{
}