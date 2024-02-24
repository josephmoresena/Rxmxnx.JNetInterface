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
		return fixture;
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