namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java throwable class type instance.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface
	IThrowableType<
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable> : IClassType<TThrowable>
	where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JThrowableTypeMetadata<TThrowable> Metadata { get; }

	[ExcludeFromCodeCoverage]
	static JClassTypeMetadata<TThrowable> IClassType<TThrowable>.Metadata => TThrowable.Metadata;
	static JDataTypeMetadata IDataType<TThrowable>.Metadata => TThrowable.Metadata;
}