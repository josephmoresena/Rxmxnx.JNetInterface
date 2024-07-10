namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public static class ProxyTestsExtensions
{
	public static TFixture RegisterReferences<TFixture>(this TFixture fixture) where TFixture : IFixture
	{
		fixture.Register<IFixture, JObjectLocalRef>(ProxyTestsExtensions.CreateReference<JObjectLocalRef>);
		fixture.Register<IFixture, JClassLocalRef>(ProxyTestsExtensions.CreateReference<JClassLocalRef>);
		fixture.Register<IFixture, JStringLocalRef>(ProxyTestsExtensions.CreateReference<JStringLocalRef>);
		fixture.Register<IFixture, JArrayLocalRef>(ProxyTestsExtensions.CreateReference<JArrayLocalRef>);
		fixture.Register<IFixture, JThrowableLocalRef>(ProxyTestsExtensions.CreateReference<JThrowableLocalRef>);
		fixture.Register<IFixture, JGlobalRef>(ProxyTestsExtensions.CreateReference<JGlobalRef>);
		fixture.Register<IFixture, JWeakRef>(ProxyTestsExtensions.CreateReference<JWeakRef>);
		fixture.Register<IFixture, JFieldId>(ProxyTestsExtensions.CreateReference<JFieldId>);
		fixture.Register<IFixture, JMethodId>(ProxyTestsExtensions.CreateReference<JMethodId>);

		fixture.Register<IFixture, JObjectArrayLocalRef>(ProxyTestsExtensions.CreateReference<JObjectArrayLocalRef>);
		fixture.Register<IFixture, JBooleanArrayLocalRef>(ProxyTestsExtensions.CreateReference<JBooleanArrayLocalRef>);
		fixture.Register<IFixture, JByteArrayLocalRef>(ProxyTestsExtensions.CreateReference<JByteArrayLocalRef>);
		fixture.Register<IFixture, JCharArrayLocalRef>(ProxyTestsExtensions.CreateReference<JCharArrayLocalRef>);
		fixture.Register<IFixture, JDoubleArrayLocalRef>(ProxyTestsExtensions.CreateReference<JDoubleArrayLocalRef>);
		fixture.Register<IFixture, JFloatArrayLocalRef>(ProxyTestsExtensions.CreateReference<JFloatArrayLocalRef>);
		fixture.Register<IFixture, JLongArrayLocalRef>(ProxyTestsExtensions.CreateReference<JLongArrayLocalRef>);
		fixture.Register<IFixture, JShortArrayLocalRef>(ProxyTestsExtensions.CreateReference<JShortArrayLocalRef>);
		return fixture;
	}
	public static String ToSimplifiedString(this JArgumentMetadata argumentMetadata)
		=> $"{{ {nameof(JArgumentMetadata.Signature)} = {argumentMetadata.Signature}, {nameof(JArgumentMetadata.Size)} = {argumentMetadata.Size} }}";
	public static String ToPrintableHash(this JReferenceTypeMetadata typeMetadata)
		=> $"{ITypeInformation.GetPrintableHash(typeMetadata.Hash, out String lastChar)}{lastChar}";
	public static JStackTraceElementObject CreateStackTrace(this StackTraceInfo info, JClassObject jClass,
		JObjectLocalRef localRef = default)
	{
		JReferenceTypeMetadata metadata = IClassType.GetMetadata<JStackTraceElementObject>();
		JStackTraceElementObject element = (JStackTraceElementObject)metadata.CreateInstance(jClass, localRef);
		StackTraceElementObjectMetadata objectMetadata = new(new(jClass)) { Information = info, };
		ILocalObject.ProcessMetadata(element, objectMetadata);
		return element;
	}

	private static T CreateReference<T>(IFixture fixture) where T : unmanaged, IFixedPointer
	{
		Span<Byte> bytes = stackalloc Byte[IntPtr.Size];
		Int32 idx = 0;
		foreach (Byte value in fixture.CreateMany<Byte>(bytes.Length))
		{
			bytes[idx] = value;
			idx++;
		}
		return bytes.AsValue<T>();
	}
}