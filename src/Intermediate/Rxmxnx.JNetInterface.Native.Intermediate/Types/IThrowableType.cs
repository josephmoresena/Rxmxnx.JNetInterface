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
	where TThrowable : JReferenceObject, IThrowableType<TThrowable>
{
	/// <summary>
	/// Creates a <typeparamref name="TThrowable"/> instance from <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TThrowable"/> instance from <paramref name="jGlobal"/>.</returns>
	internal static virtual TThrowable Create(IEnvironment env, JGlobalBase jGlobal)
		=> TThrowable.Create(new GlobalInitializer { Environment = env, Global = jGlobal, });
}