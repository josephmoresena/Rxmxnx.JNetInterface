namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> JNI information container.
/// </summary>
public interface ITypeInformation
{
	/// <summary>
	/// JNI class name.
	/// </summary>
	CString ClassName { get; }
	/// <summary>
	/// JNI signature for object instances of this type.
	/// </summary>
	CString Signature { get; }
	/// <summary>
	/// Current datatype hash.
	/// </summary>
	String Hash { get; }
	/// <summary>
	/// Kind of the current type.
	/// </summary>
	JTypeKind Kind { get; }
	/// <summary>
	/// Indicates whether current type is final;
	/// </summary>
	Boolean? IsFinal { get; }
}