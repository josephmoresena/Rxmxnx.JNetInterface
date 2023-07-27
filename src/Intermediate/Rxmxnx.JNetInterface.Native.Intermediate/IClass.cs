namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a <c>java.lang.Class&lt;?&gt;</c> instance.
/// </summary>
public interface IClass
{
	/// <summary>
	/// JNI internal <see cref="JClassLocalRef"/> reference.
	/// </summary>
	internal JClassLocalRef Reference { get; }
	/// <summary>
	/// Fully-qualified class name.
	/// </summary>
	CString Name { get; }
	/// <summary>
	/// JNI signature for the instances of this class.
	/// </summary>
	CString ClassSignature { get; }
	/// <summary>
	/// Indicates whether current class is final.
	/// </summary>
	Boolean? IsFinal { get; }

	/// <summary>
	/// Internal class hash.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal String Hash { get; }
}