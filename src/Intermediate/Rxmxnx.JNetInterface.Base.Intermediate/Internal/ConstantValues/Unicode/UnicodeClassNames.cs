namespace Rxmxnx.JNetInterface.Internal.ConstantValues.Unicode;

/// <summary>
/// Unicode java classes names.
/// </summary>
internal static partial class UnicodeClassNames
{
	/// <inheritdoc cref="ClassNames.Object"/>
	[DefaultValue(ClassNames.Object)]
	public static readonly CString Object;
	/// <inheritdoc cref="ClassNames.ClassObject"/>
	[DefaultValue(ClassNames.ClassObject)]
	public static readonly CString ClassObject;

	/// <summary>
	/// Java class name of primitive <c>void</c> class.
	/// </summary>
	public static ReadOnlySpan<Byte> VoidPrimitive() => "void"u8;
	/// <inheritdoc cref="ClassNames.BooleanPrimitive"/>
	[DefaultValue(ClassNames.BooleanPrimitive)]
	public static partial ReadOnlySpan<Byte> BooleanPrimitive();
	/// <inheritdoc cref="ClassNames.BytePrimitive"/>
	[DefaultValue(ClassNames.BytePrimitive)]
	public static partial ReadOnlySpan<Byte> BytePrimitive();
	/// <inheritdoc cref="ClassNames.CharPrimitive"/>
	[DefaultValue(ClassNames.CharPrimitive)]
	public static partial ReadOnlySpan<Byte> CharPrimitive();
	/// <inheritdoc cref="ClassNames.DoublePrimitive"/>
	[DefaultValue(ClassNames.DoublePrimitive)]
	public static partial ReadOnlySpan<Byte> DoublePrimitive();
	/// <inheritdoc cref="ClassNames.FloatPrimitive"/>
	[DefaultValue(ClassNames.FloatPrimitive)]
	public static partial ReadOnlySpan<Byte> FloatPrimitive();
	/// <inheritdoc cref="ClassNames.IntPrimitive"/>
	[DefaultValue(ClassNames.IntPrimitive)]
	public static partial ReadOnlySpan<Byte> IntPrimitive();
	/// <inheritdoc cref="ClassNames.LongPrimitive"/>
	[DefaultValue(ClassNames.LongPrimitive)]
	public static partial ReadOnlySpan<Byte> LongPrimitive();
	/// <inheritdoc cref="ClassNames.ShortPrimitive"/>
	[DefaultValue(ClassNames.ShortPrimitive)]
	public static partial ReadOnlySpan<Byte> ShortPrimitive();

	/// <inheritdoc cref="ClassNames.ProxyObject"/>
	[DefaultValue(ClassNames.ProxyObject)]
	public static partial ReadOnlySpan<Byte> ProxyObject();
}