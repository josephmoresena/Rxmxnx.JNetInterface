namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes an <c>[]</c> instance.
/// </summary>
public interface IArrayObject<out TElement> : IObject where TElement : IObject, IDataType<TElement>;