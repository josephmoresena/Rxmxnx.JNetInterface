namespace Rxmxnx.JNetInterface.Types;

/// <summary>
/// This interface exposes an object that represents a java throwable class type instance.
/// </summary>
public interface IThrowableType : IClassType
{
	// .NET 7.0 has issues inheriting static abstract members in non-generic interfaces from base classes.
	static Type IDataType.FamilyType => typeof(JThrowableObject);
}

/// <summary>
/// This interface exposes an object that represents a java throwable class type instance.
/// </summary>
/// <typeparam name="TThrowable">Type of java enum type.</typeparam>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface
	IThrowableType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TThrowable> : IThrowableType,
	IClassType<TThrowable> where TThrowable : JThrowableObject, IThrowableType<TThrowable>
{
	/// <summary>
	/// Current type metadata.
	/// </summary>
	[ReadOnly(true)]
	protected new static abstract JThrowableTypeMetadata<TThrowable> Metadata { get; }

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	static JClassTypeMetadata<TThrowable> IClassType<TThrowable>.Metadata => TThrowable.Metadata;
	static JDataTypeMetadata IDataType<TThrowable>.Metadata => TThrowable.Metadata;
}