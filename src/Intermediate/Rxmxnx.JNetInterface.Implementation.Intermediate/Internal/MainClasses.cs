namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores initial classes.
/// </summary>
internal abstract record MainClasses<TClass> where TClass : JReferenceObject
{
	/// <summary>
	/// Class for <see cref="JClassObject"/>
	/// </summary>
	public TClass ClassObject { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JThrowableObject"/>
	/// </summary>
	public TClass ThrowableObject { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JStackTraceElementObject"/>
	/// </summary>
	public TClass StackTraceElementObject { get; protected init; } = default!;

	/// <summary>
	/// Class for Java <c>void</c> type.
	/// </summary>
	public TClass VoidPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JBoolean"/>.
	/// </summary>
	public TClass BooleanPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JByte"/>.
	/// </summary>
	public TClass BytePrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JChar"/>.
	/// </summary>
	public TClass CharPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JDouble"/>.
	/// </summary>
	public TClass DoublePrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JFloat"/>.
	/// </summary>
	public TClass FloatPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JInt"/>.
	/// </summary>
	public TClass IntPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JLong"/>.
	/// </summary>
	public TClass LongPrimitive { get; protected init; } = default!;
	/// <summary>
	/// Class for <see cref="JShort"/>.
	/// </summary>
	public TClass ShortPrimitive { get; protected init; } = default!;
}