namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// CLR type of object metadata.
	/// </summary>
	internal static readonly Type MetadataType = typeof(ClassObjectMetadata);

	/// <summary>
	/// JNI class reference.
	/// </summary>
	internal JClassLocalRef Reference => this.As<JClassLocalRef>();

	/// <summary>
	/// Retrieves array dimension for given class signature.
	/// </summary>
	/// <param name="classSignature">JNI class signature.</param>
	/// <returns>Array dimension for <paramref name="classSignature"/>.</returns>
	internal static Int32 GetArrayDimension(ReadOnlySpan<Byte> classSignature)
	{
		Int32 dimension = 0;
		while (classSignature[dimension] == UnicodeObjectSignatures.ArraySignaturePrefixChar)
			dimension++;
		return dimension;
	}
}