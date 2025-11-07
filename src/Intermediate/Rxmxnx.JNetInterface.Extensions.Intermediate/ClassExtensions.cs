namespace Rxmxnx.JNetInterface;

#if !PACKAGE
/// <summary>
/// Set of class extensions.
/// </summary>
public static class ClassExtensions
#else
public partial class JClassObject
#endif
{
#if PACKAGE
	/// <summary>
	/// Retrieves the runtime <see cref="JDataTypeMetadata"/> instance from current class.
	/// </summary>
	/// <returns>A <see cref="JDataTypeMetadata"/> instance.</returns>
	public JDataTypeMetadata GetRuntimeClassMetadata() => JClassObject.GetRuntimeClassMetadata(jClass);
#endif

	/// <summary>
	/// Retrieves the runtime <see cref="JDataTypeMetadata"/> instance from <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JDataTypeMetadata"/> instance.</returns>
#if !PACKAGE
	public static JDataTypeMetadata GetRuntimeClassMetadata(this JClassObject jClass)
#else
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static JDataTypeMetadata GetRuntimeClassMetadata(JClassObject jClass)
#endif
		=> jClass.GetInformation().Hash switch
		{
			ClassNameHelper.BooleanPrimitiveHash => IDataType.GetMetadata<JBoolean>(),
			ClassNameHelper.BytePrimitiveHash => IDataType.GetMetadata<JByte>(),
			ClassNameHelper.CharPrimitiveHash => IDataType.GetMetadata<JChar>(),
			ClassNameHelper.DoublePrimitiveHash => IDataType.GetMetadata<JDouble>(),
			ClassNameHelper.FloatPrimitiveHash => IDataType.GetMetadata<JFloat>(),
			ClassNameHelper.IntPrimitiveHash => IDataType.GetMetadata<JInt>(),
			ClassNameHelper.LongPrimitiveHash => IDataType.GetMetadata<JLong>(),
			ClassNameHelper.ShortPrimitiveHash => IDataType.GetMetadata<JShort>(),
			ClassNameHelper.VoidPrimitiveHash => JPrimitiveTypeMetadata.VoidMetadata,
			_ => jClass.Environment.ClassFeature.GetTypeMetadata(jClass),
		};
}