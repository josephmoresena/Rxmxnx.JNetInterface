namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java throwable class type instance.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrowableType : IClassType;

/// <summary>
/// This interface exposes an object that represents a java enum type instance.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrowableType<out TThrowable> : IThrowableType, IClassType<TThrowable>
	where TThrowable : JReferenceObject, IThrowableType<TThrowable>;