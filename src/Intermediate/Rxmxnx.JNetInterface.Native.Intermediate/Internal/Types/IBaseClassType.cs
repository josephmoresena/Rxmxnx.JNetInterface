namespace Rxmxnx.JNetInterface.Internal.Types;

/// <inheritdoc cref="IClassType{TClass}"/>
/// <remarks>
/// This interface was created only for force the implementation of <see cref="IDataType.Metadata"/> on
/// <see cref="JLocalObject"/> derived types.
/// </remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
internal interface IBaseClassType<out TClass> : IBaseReferenceType, IClassType<TClass>
	where TClass : JReferenceObject, IBaseClassType<TClass>
{
	static JDataTypeMetadata IDataType.Metadata => JLocalObject.JObjectClassMetadata;
}