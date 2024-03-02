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

		JReferenceObject jObject = jString;

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
		Assert.Equal(text.GetHashCode(), jString.GetHashCode());
		Assert.Equal(text.Length, jString.Length);
		Assert.Equal(utf8Text.Length, jString.Utf8Length);
		Assert.Equal(text, jString.Value);
		Assert.Equal(utf8Text, jString.GetUtf8Chars());
		Assert.Equal(text, jString.ToString());

		JStringObjectTests.ObjectMetadataTest(jString);
		JStringObjectTests.GetCharsTest(jString);
		JStringObjectTests.EqualityTest(jString);
		JStringObjectTests.ComparisionTest(jString);
		JStringObjectTests.EnumeratorTest(jString);
		JStringObjectTests.EnumerableTest<JStringObject, String>(jString);

		env.StringFeature.Received(1).GetUtf8Length(jString);
		env.StringFeature.Received(1).GetCopyUtf8(jString, Arg.Any<Memory<Byte>>());
		env.StringFeature.Received(initText ? 0 : 1).GetLength(jString);
		env.StringFeature.Received(initText ? 0 : 1).GetCopy(jString, Arg.Any<IFixedMemory<Char>>());
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStringObject>();
		String textValue = typeMetadata.ToString();
		VirtualMachineProxy vm = Substitute.For<VirtualMachineProxy>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JStringObjectTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		JGlobalRef globalRef = JStringObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, stringRef.Value, jStringClass);
		using JGlobal jGlobal = new(vm, new(jStringClass, IClassType.GetMetadata<JStringObject>()), !env.NoProxy,
		                            globalRef);

		Assert.StartsWith($"{nameof(JDataTypeMetadata)} {{", textValue);
		Assert.Contains(typeMetadata.ArgumentMetadata.ToSimplifiedString(), textValue);
		Assert.EndsWith($"{nameof(JDataTypeMetadata.Hash)} = {typeMetadata.Hash} }}", textValue);

		Assert.Equal(JTypeModifier.Final, typeMetadata.Modifier);
		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<JStringObject>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(JStringObject), typeMetadata.Type);
		Assert.Equal(JTypeKind.Class, typeMetadata.Kind);
		Assert.Equal(JStringObjectTests.className, typeMetadata.ClassName);
		Assert.Equal(JStringObjectTests.classSignature, typeMetadata.Signature);
		Assert.Equal(JStringObjectTests.arraySignature, typeMetadata.ArraySignature);
		Assert.Equal(JStringObjectTests.hash.ToString(), typeMetadata.Hash);
		Assert.Equal(JStringObjectTests.hash.ToString(), IDataType.GetHash<JStringObject>());
		Assert.Equal(IDataType.GetMetadata<JLocalObject>(), typeMetadata.BaseMetadata);
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JCharSequenceObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JStringObject>().Returns(jStringClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, stringRef.Value, jStringClass, false);

		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JStringObject jString0 =
			Assert.IsType<JStringObject>(typeMetadata.CreateInstance(jStringClass, stringRef.Value, true));
		using JStringObject jString1 = Assert.IsType<JStringObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JStringObject jString2 = Assert.IsType<JStringObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).IsAssignableTo<JStringObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}

	private static void ObjectMetadataTest(JStringObject jString)
	{
		StringObjectMetadata objectMetadata = Assert.IsType<StringObjectMetadata>(ILocalObject.CreateMetadata(jString));
		Assert.Equal(jString.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(jString.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(jString.Value, objectMetadata.Value);
		Assert.Equal(jString.Length, objectMetadata.Length);
		Assert.Equal(jString.Utf8Length, objectMetadata.Utf8Length);
	}
	private static void GetCharsTest(JStringObject jString)
	{
		String text = jString.Value;
		for (Int32 startIndex = 0; startIndex < text.Length; startIndex++)
		{
			Assert.Equal(text[startIndex..], jString.GetChars(startIndex));
			for (Int32 count = 0; count <= text.Length - startIndex; count++)
				Assert.Equal(text[startIndex..(startIndex + count)], jString.GetChars(startIndex, count));
		}
	}
	private static void ComparisionTest(JStringObject jString)
	{
		IComparable comp = Substitute.For<IComparable>();
		Int32 result = JStringObjectTests.fixture.Create<Int32>();
		JStringLocalRef stringRef1 = JStringObjectTests.fixture.Create<JStringLocalRef>();
		using JStringObject jString1 = new(jString.Class, stringRef1, jString.Value);
		IComparable jComp = jString;

		Assert.True(jString.Equals(jString1));
		Assert.True(jString.Equals((Object)jString1));

		comp.CompareTo(jString.Value).Returns(result);

		Assert.Equal(0, jString.CompareTo(jString.Value));
		Assert.Equal(0, jString.CompareTo(jString1));
		Assert.Equal(0, (jString as IComparable<IWrapper<String>>).CompareTo(IWrapper.CreateObject(jString.Value)));
		Assert.Equal(0, jComp.CompareTo(jString.Value));
		Assert.Equal(0, jComp.CompareTo(jString1));
		Assert.Equal(0, jComp.CompareTo(IWrapper.CreateObject(jString.Value)));
		Assert.Equal(0, jComp.CompareTo(jString1));
		Assert.Equal(-result, jComp.CompareTo(comp));
		Assert.Throws<ArgumentException>(() => jComp.CompareTo(new()));
	}
	private static void EqualityTest(JStringObject jString)
	{
		JStringLocalRef stringRef = jString.Reference;
		JStringLocalRef stringRef1 = NativeUtilities.Transform<IntPtr, JStringLocalRef>(~stringRef.Value.Pointer);
		using JLocalObject jLocal = new(jString.Class, stringRef.Value);
		using JLocalObject jLocal1 = new(jString.Class, stringRef1.Value);

		jString.Environment.IsSameObject(jString, jLocal1).Returns(true);

		Assert.True(jString.Value.GetHashCode().Equals(jString.GetHashCode()));
		Assert.True(jString.Equals(jString.Value));
		Assert.True(jString.Equals(jLocal));
		Assert.True(jString.Equals(jLocal1));
		Assert.True(jString.Equals((Object)jString.Value));
		Assert.True(jString.Equals(IWrapper.CreateObject(jString.Value)));
		Assert.True(jString.Equals((Object)jLocal));
		Assert.True((jString as IEquatable<IWrapper<String>>).Equals(IWrapper.CreateObject(jString.Value)));
		Assert.Equal(jString.Value.ToArray(), jString);

		jString.Environment.Received(0)
		       .IsSameObject(jString, Arg.Is<JLocalObject>(l => l.InternalReference.Equals(stringRef)));
		jString.Environment.Received(1)
		       .IsSameObject(jString, Arg.Is<JLocalObject>(l => l.InternalReference.Equals(stringRef1)));
	}
	private static void EnumeratorTest(JStringObject jString)
	{
		using CharEnumerator stringEnumerator = jString.Value.GetEnumerator();
		using CharEnumerator jStringEnumerator = jString.GetEnumerator();
		while (stringEnumerator.MoveNext() && jStringEnumerator.MoveNext())
			Assert.Equal(stringEnumerator.Current, jStringEnumerator.Current);
	}
	private static void EnumerableTest<TW, T>(TW enumerable) where TW : IEnumerable, IWrapper<T> where T : IEnumerable
	{
		IEnumerator wEnumerator = enumerable.GetEnumerator();
		IEnumerator tEnumerator = enumerable.Value.GetEnumerator();
		try
		{
			while (tEnumerator.MoveNext() && wEnumerator.MoveNext())
				Assert.Equal(tEnumerator.Current, wEnumerator.Current);
		}
		finally
		{
			(tEnumerator as IDisposable)?.Dispose();
			(wEnumerator as IDisposable)?.Dispose();
		}
	}
}