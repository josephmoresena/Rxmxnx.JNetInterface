namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a java a non-inheritable type.
/// </summary>
public interface IFinalType : IDataType
{
	/// <inheritdoc cref="IDataType.Final"/>
	new static virtual Boolean Final => ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IFinalType));
	static Boolean IDataType.Final => ValidationUtilities.ThrowInvalidInterface<Boolean>(nameof(IFinalType));
}

/// <summary>
/// This interface exposes a java a non-inheritable type.
/// </summary>
/// <typeparam name="TFinal">Type of current Java datatype.</typeparam>
public interface IFinalType<out TFinal> : IFinalType, IDataType<TFinal>
	where TFinal : IFinalType<TFinal>
{
	static Boolean IFinalType.Final => true;
	static Boolean IDataType.Final => TFinal.Final;
}