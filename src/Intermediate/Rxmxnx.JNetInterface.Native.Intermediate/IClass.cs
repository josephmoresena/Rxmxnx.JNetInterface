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
	/// Internal class hash.
	/// </summary>
	[ExcludeFromCodeCoverage]
	internal String Hash => new CStringSequence(this.Name, this.ClassSignature).ToString();

	/// <summary>
	/// Indicates whether the current instance is valid within <paramref name="env"/> context.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the current instance is valid within <paramref name="env"/> context;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsValid(IEnvironment env);
	/// <summary>
	/// Retrieves the <see cref="IEnvironment"/> instance for current thread.
	/// </summary>
	/// <returns>The <see cref="IEnvironment"/> instance for current thread.</returns>
	[ExcludeFromCodeCoverage]
	internal IEnvironment? GetEnvironment() => this is JLocalObject jObj ? jObj.Environment : default;
}