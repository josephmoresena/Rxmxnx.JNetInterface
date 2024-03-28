namespace Rxmxnx.JNetInterface;

/// <summary>
/// Stores the information of a <see cref="JStackTraceElementObject"/> instance.
/// </summary>
public sealed record StackTraceInfo
{
	/// <summary>
	/// The fully qualified name of the class containing the execution point.
	/// </summary>
	public String ClassName { get; init; }
	/// <summary>
	/// The name of the source file containing the execution point
	/// </summary>
	public String FileName { get; init; }
	/// <summary>
	/// The line number of the source line containing the execution point
	/// </summary>
	public Int32 LineNumber { get; init; }
	/// <summary>
	/// The name of the method containing the execution point.
	/// </summary>
	public String MethodName { get; init; }
	/// <summary>
	/// Indicates whether the method containing the execution point is a native method.
	/// </summary>
	public Boolean NativeMethod { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public StackTraceInfo()
	{
		this.ClassName = default!;
		this.FileName = default!;
		this.MethodName = default!;
	}
}