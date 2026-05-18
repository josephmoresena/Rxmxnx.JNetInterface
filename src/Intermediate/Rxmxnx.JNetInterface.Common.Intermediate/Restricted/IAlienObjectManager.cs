namespace Rxmxnx.JNetInterface.Restricted;

/// <summary>
/// Represents an alien object manager.
/// </summary>
internal interface IAlienObjectManager
{
	/// <summary>
	/// Retrieves type of given reference.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JReferenceType"/> value.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	JReferenceType GetReferenceType(JObjectLocalRef localRef);
	/// <summary>
	/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
	/// </summary>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	JClassObject GetReferenceTypeClass(JClassLocalRef classRef, Boolean keepReference = false);
#if !ANDROID
	/// <summary>
	/// Retrieves object class reference.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
	JClassObject GetObjectClass(JObjectLocalRef localRef);
#else
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	public JClassObject GetObjectClass(ITypeInformation typeInformation, JObjectLocalRef localRef,
		out JReferenceTypeMetadata typeMetadata);
#endif
	/// <summary>
	/// Retrieves the class object and instantiation metadata.
	/// </summary>
	/// <param name="localRef">Object instance to get class.</param>
	/// <param name="typeMetadata">Output. Instantiation metadata.</param>
	/// <returns>Object's class <see cref="JClassObject"/> instance</returns>
	JClassObject GetObjectClass(JObjectLocalRef localRef, out JReferenceTypeMetadata typeMetadata);
}