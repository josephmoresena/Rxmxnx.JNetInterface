namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores initial classes.
/// </summary>
internal abstract class MainClasses<TClass> where TClass : JReferenceObject
{
	/// <summary>
	/// Class for <see cref="JClassObject"/>
	/// </summary>
	public abstract TClass ClassObject { get; }
	/// <summary>
	/// Class for <see cref="JThrowableObject"/>
	/// </summary>
	public abstract TClass ThrowableObject { get; }
	/// <summary>
	/// Class for <see cref="JStackTraceElementObject"/>
	/// </summary>
	public abstract TClass StackTraceElementObject { get; }

	/// <summary>
	/// Class for Java <c>void</c> type.
	/// </summary>
	public abstract TClass VoidPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JBoolean"/>.
	/// </summary>
	public abstract TClass BooleanPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JByte"/>.
	/// </summary>
	public abstract TClass BytePrimitive { get; }
	/// <summary>
	/// Class for <see cref="JChar"/>.
	/// </summary>
	public abstract TClass CharPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JDouble"/>.
	/// </summary>
	public abstract TClass DoublePrimitive { get; }
	/// <summary>
	/// Class for <see cref="JFloat"/>.
	/// </summary>
	public abstract TClass FloatPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JInt"/>.
	/// </summary>
	public abstract TClass IntPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JLong"/>.
	/// </summary>
	public abstract TClass LongPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JShort"/>.
	/// </summary>
	public abstract TClass ShortPrimitive { get; }
}