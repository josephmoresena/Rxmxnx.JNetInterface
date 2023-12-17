namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> JNI information container.
/// </summary>
public interface ITypeInformation
{
	/// <summary>
	/// JNI class name.
	/// </summary>
	public CString ClassName { get; }
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	public CString Signature { get; }
	/// <summary>
	/// Current datatype hash.
	/// </summary>
	public String Hash { get; }
}