namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Void</c> instance.
/// </summary>
public sealed class JVoidObject : JLocalObject.Uninstantiable<JVoidObject>, IPrimitiveEquatable,
	IPrimitiveWrapperType<JVoidObject>, IUninstantiableType<JVoidObject>
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => JPrimitiveTypeMetadata.VoidMetadata;
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JVoidObject>();

	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other)
		=> throw new InvalidOperationException("A Void instance can't be equatable.");
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other)
		=> throw new InvalidOperationException("A Void instance can't be equatable.");
}