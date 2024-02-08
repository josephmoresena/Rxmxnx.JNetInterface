namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Void</c> instance.
/// </summary>
public sealed class JVoidObject : JLocalObject.Uninstantiable<JVoidObject>, IPrimitiveEquatable,
	IPrimitiveWrapperType<JVoidObject>, IUninstantiableType<JVoidObject>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JVoidObject> typeMetadata =
		new(JTypeMetadataBuilder<JVoidObject>.Build(JPrimitiveTypeMetadata.VoidMetadata));

	static JPrimitiveWrapperTypeMetadata<JVoidObject> IPrimitiveWrapperType<JVoidObject>.Metadata
		=> JVoidObject.typeMetadata;
	static JPrimitiveTypeMetadata IPrimitiveWrapperType<JVoidObject>.PrimitiveMetadata
		=> JPrimitiveTypeMetadata.VoidMetadata;

	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => ValidationUtilities.ThrowVoidEquality();
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => ValidationUtilities.ThrowVoidEquality();
}