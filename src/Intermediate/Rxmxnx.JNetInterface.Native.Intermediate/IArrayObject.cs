namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Object</c> instance.
/// </summary>
public interface IArrayObject<out TElement> : IObject where TElement : IObject, IDataType<TElement> { }