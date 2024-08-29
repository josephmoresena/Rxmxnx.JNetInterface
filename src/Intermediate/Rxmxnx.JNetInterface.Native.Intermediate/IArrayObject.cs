namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an <c>[]</c> instance.
/// </summary>
/// <typeparam name="TElement">Element type of array.</typeparam>
public interface IArrayObject<out TElement> : IObject where TElement : IDataType<TElement>;