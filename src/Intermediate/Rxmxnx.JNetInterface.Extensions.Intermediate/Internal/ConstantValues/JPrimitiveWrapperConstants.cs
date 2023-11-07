namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Java wrapper classes constants.
/// </summary>
internal static class JPrimitiveWrapperConstants
{
	/// <summary>
	/// JNI signature for <c>java.lang.Boolean[]</c> object.
	/// </summary>
	public const String JBooleanObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JBooleanObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Byte[]</c> object.
	/// </summary>
	public const String JByteObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JByteObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Short[]</c> object.
	/// </summary>
	public const String JShortObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JShortObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Integer[]</c> object.
	/// </summary>
	public const String JIntegerObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JIntegerObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Long[]</c> object.
	/// </summary>
	public const String JLongObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JLongObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Character[]</c> object.
	/// </summary>
	public const String JCharacterObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JCharacterObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Float[]</c> object.
	/// </summary>
	public const String JFloatObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JFloatObjectSignature;
	/// <summary>
	/// JNI signature for <c>java.lang.Double[]</c> object.
	/// </summary>
	public const String JDoubleObjectArraySignature =
		ObjectSignatures.ArraySignaturePrefix + ObjectSignatures.JDoubleObjectSignature;

	/// <summary>
	/// Interfaces for <see cref="IPrimitiveWrapperType"/>.
	/// </summary>
	public static readonly IImmutableSet<JInterfaceTypeMetadata> Interfaces =
		ImmutableHashSet.Create(IInterfaceType.GetMetadata<JSerializableObject>(),
		                        IInterfaceType.GetMetadata<JComparableObject>());
}