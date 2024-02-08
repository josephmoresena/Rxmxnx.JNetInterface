namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java throwable class type instance.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IThrowableType<TThrowable> : IClassType<TThrowable>
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JThrowableTypeMetadata<TThrowable> Metadata { get; }

	static JClassTypeMetadata<TThrowable> IClassType<TThrowable>.Metadata => TThrowable.Metadata;
	static JDataTypeMetadata IDataType<TThrowable>.Metadata => TThrowable.Metadata;

	/// <summary>
	/// Creates a <typeparamref name="TThrowable"/> instance from <paramref name="jGlobal"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <typeparamref name="TThrowable"/> instance from <paramref name="jGlobal"/>.</returns>
	internal static virtual TThrowable Create(IEnvironment env, JGlobalBase jGlobal)
		=> TThrowable.Create(new GlobalInitializer { Environment = env, Global = jGlobal, });
}