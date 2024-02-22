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
}