namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Void</c> instance.
/// </summary>
public sealed class JVoidObject : JLocalObject.Uninstantiable<JVoidObject>, IPrimitiveEquatable,
	IPrimitiveWrapperType<JVoidObject>, IUninstantiableType<JVoidObject>
{
	private static readonly JPrimitiveWrapperTypeMetadata<JVoidObject> typeMetadata =
		new(JLocalObject.CreateBuiltInMetadata<JVoidObject>(JPrimitiveTypeMetadata.VoidMetadata));

	static JPrimitiveWrapperTypeMetadata<JVoidObject> IPrimitiveWrapperType<JVoidObject>.Metadata
		=> JVoidObject.typeMetadata;
	static JPrimitiveTypeMetadata IPrimitiveWrapperType<JVoidObject>.PrimitiveMetadata
		=> JPrimitiveTypeMetadata.VoidMetadata;

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other)
		=> CommonValidationUtilities.ThrowVoidEquality();
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => CommonValidationUtilities.ThrowVoidEquality();

#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IPrimitiveWrapperType<JVoidObject>.SetPrimitiveValue(IPrimitiveType value) { }
}