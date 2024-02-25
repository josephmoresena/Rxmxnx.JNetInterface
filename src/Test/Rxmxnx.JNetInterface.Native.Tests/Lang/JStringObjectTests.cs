namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
public sealed class JStringObjectTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly CString className = new(UnicodeClassNames.StringObject);
	private static readonly CString classSignature = CString.Concat("L"u8, JStringObjectTests.className, ";"u8);
	private static readonly CString arraySignature = CString.Concat("["u8, JStringObjectTests.classSignature);
	private static readonly CStringSequence hash = new(JStringObjectTests.className, JStringObjectTests.classSignature,
	                                                   JStringObjectTests.arraySignature);

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	[InlineData(true, true)]
	[InlineData(false, true)]
	internal void ConstructorClassTest(Boolean isProxy, Boolean initText = false)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		String text = JStringObjectTests.fixture.Create<String>();
		CString utf8Text = (CString)text;
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, typeMetadata);
		using JStringObject jString = new(jStringClass, stringRef, initText ? text : default);
		using JLocalObject jLocal = new(jStringClass, stringRef.Value);
		JReferenceObject jObject = jString;

		env.IsSameObject(jString, jLocal).Returns(true);
		env.StringFeature.GetLength(Arg.Is<JStringObject>(ss => jObject.Equals(ss))).Returns(text.Length);
		env.StringFeature.GetUtf8Length(Arg.Is<JStringObject>(ss => jObject.Equals(ss))).Returns(utf8Text.Length);
		env.StringFeature
		   .When(s => s.GetCopyUtf8(Arg.Is<JStringObject>(ss => jObject.Equals(ss)), Arg.Any<Memory<Byte>>())).Do(c =>
		   {
			   Memory<Byte> mem = (Memory<Byte>)c[1];
			   utf8Text.AsSpan().CopyTo(mem.Span);
		   });
		env.StringFeature
		   .When(s => s.GetCopy(Arg.Is<JStringObject>(ss => jObject.Equals(ss)), Arg.Any<IFixedMemory<Char>>())).Do(c =>
		   {
			   IFixedMemory<Char> mem = (IFixedMemory<Char>)c[1];
			   text.CopyTo(mem.Values);
		   });

		Assert.Equal(isProxy, jString.IsProxy);
		Assert.Equal(JStringObjectTests.className, jString.ObjectClassName);
		Assert.Equal(JStringObjectTests.classSignature, jString.ObjectSignature);
		Assert.Equal(stringRef, jString.Reference);
		Assert.Equal(initText, text.GetHashCode().Equals(jString.GetHashCode()));
		Assert.Equal(!initText, stringRef.GetHashCode().Equals(jString.GetHashCode()));
		Assert.Equal(text.Length, jString.Length);
		Assert.Equal(utf8Text.Length, jString.Utf8Length);
		Assert.Equal(text, jString.Value);
		Assert.Equal(utf8Text, jString.GetUtf8Chars());
		Assert.Equal(text, jString.ToString());
		Assert.True(text.GetHashCode().Equals(jString.GetHashCode()));
		Assert.True(jString.Equals((Object)text));
		Assert.True(jString.Equals(IWrapper.CreateObject(text)));
		Assert.True(jString.Equals((Object)jLocal));
		Assert.True((jString as IEquatable<IWrapper<String>>).Equals(IWrapper.CreateObject(text)));
		Assert.Equal(text.ToArray(), jString);

		env.StringFeature.Received(1).GetUtf8Length(jString);
		env.StringFeature.Received(1).GetCopyUtf8(jString, Arg.Any<Memory<Byte>>());
		env.StringFeature.Received(initText ? 0 : 1).GetLength(jString);
		env.StringFeature.Received(initText ? 0 : 1).GetCopy(jString, Arg.Any<IFixedMemory<Char>>());
	}
}