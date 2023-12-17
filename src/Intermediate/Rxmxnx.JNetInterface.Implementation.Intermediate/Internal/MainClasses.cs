namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores initial classes.
/// </summary>
internal abstract class MainClasses<TClass> where TClass : JReferenceObject
{
	/// <summary>
	/// Object for <see cref="JClassObject"/>
	/// </summary>
	public TClass ClassObject { get; protected init; } = default!;
	/// <summary>
	/// Object for <see cref="JThrowableObject"/>
	/// </summary>
	public TClass ThrowableObject { get; protected init; } = default!;
	/// <summary>
	/// Object for <see cref="JStackTraceElementObject"/>
	/// </summary>
	public TClass StackTraceElementObject { get; protected init; } = default!;
}