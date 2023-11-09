namespace Rxmxnx.JNetInterface.Internal.Types;

/// <inheritdoc cref="IClassType{TClass}"/>
/// <remarks>
/// This interface was created only for force the implementation of <see cref="IDataType.Metadata"/> on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface ISuperClassType<out TClass> : IBaseReferenceType, IClassType<TClass>
	where TClass : JReferenceObject, ISuperClassType<TClass>
{
	/// <inheritdoc cref="IDataType.Metadata"/>
	internal static abstract JClassTypeMetadata SuperClassMetadata { get; }
	static JDataTypeMetadata IDataType.Metadata => TClass.SuperClassMetadata;
}