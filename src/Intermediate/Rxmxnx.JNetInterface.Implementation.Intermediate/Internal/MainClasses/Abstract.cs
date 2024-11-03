namespace Rxmxnx.JNetInterface.Internal;

internal abstract partial class MainClasses<TClass> where TClass : JReferenceObject
{
	/// <summary>
	/// Class for Java <see cref="JVoidObject"/>.
	/// </summary>
	public abstract TClass VoidObject { get; }
	/// <summary>
	/// Class for <see cref="JBooleanObject"/>.
	/// </summary>
	public abstract TClass BooleanObject { get; }
	/// <summary>
	/// Class for <see cref="JByteObject"/>.
	/// </summary>
	public abstract TClass ByteObject { get; }
	/// <summary>
	/// Class for <see cref="JCharacterObject"/>.
	/// </summary>
	public abstract TClass CharacterObject { get; }
	/// <summary>
	/// Class for <see cref="JDoubleObject"/>.
	/// </summary>
	public abstract TClass DoubleObject { get; }
	/// <summary>
	/// Class for <see cref="JFloatObject"/>.
	/// </summary>
	public abstract TClass FloatObject { get; }
	/// <summary>
	/// Class for <see cref="JIntegerObject"/>.
	/// </summary>
	public abstract TClass IntegerObject { get; }
	/// <summary>
	/// Class for <see cref="JLongObject"/>.
	/// </summary>
	public abstract TClass LongObject { get; }
	/// <summary>
	/// Class for <see cref="JShortObject"/>.
	/// </summary>
	public abstract TClass ShortObject { get; }
}