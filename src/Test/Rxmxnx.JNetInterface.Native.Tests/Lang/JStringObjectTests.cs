namespace Rxmxnx.JNetInterface.Tests.Lang;

[ExcludeFromCodeCoverage]
[SuppressMessage("Usage", "xUnit1046:Avoid using TheoryDataRow arguments that are not serializable")]
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
	[InlineData(true, null)]
	[InlineData(false, null)]
	internal void ConstructorClassTest(Boolean isProxy, Boolean? initText = false)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStringObject>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment(isProxy);
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		String text = JStringObjectTests.fixture.Create<String>();
		CString utf8Text = (CString)text;
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, typeMetadata);
		using JStringObject jString = initText.HasValue ?
			new(jStringClass, stringRef, initText.Value ? text : default) :
			new(jStringClass, stringRef, utf8Text.Length);

		JReferenceObject jObject = jString;

		env.ClassFeature.GetClass<JStringObject>().Returns(jStringClass);
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

		env.StringFeature.Received(initText.HasValue ? 1 : 0).GetUtf8Length(jString);
		env.StringFeature.Received(1).GetCopyUtf8(jString, Arg.Any<Memory<Byte>>());
		env.StringFeature.Received(initText.GetValueOrDefault() ? 0 : 1).GetLength(jString);
		env.StringFeature.Received(initText.GetValueOrDefault() ? 0 : 1)
		   .GetCopy(jString, Arg.Any<IFixedMemory<Char>>());

		using JStringObject jStringClone = JFakeObject<JStringObject>.Clone(jString);
		Assert.Equal(jString, jStringClone);

		env.ClassFeature.Received(1).GetClass<JStringObject>();
	}
	[Fact]
	internal void ProcessMetadataTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		String value = JStringObjectTests.fixture.Create<String>();
		CString utf8Value = (CString)value;
		using JClassObject jClass = new(env);
		using JClassObject jStringClass = new(jClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef);
		StringObjectMetadata stringMetadata = new(new(jStringClass))
		{
			Value = value, Length = value.Length, Utf8Length = utf8Value.Length,
		};

		ILocalObject.ProcessMetadata(jString, stringMetadata);

		Assert.Equal(stringMetadata.ObjectClassName, jString.ObjectClassName);
		Assert.Equal(stringMetadata.ObjectSignature, jString.ObjectSignature);
		Assert.Equal(stringMetadata.Value, jString.Value);
		Assert.Equal(stringMetadata.Length, jString.Length);
		Assert.Equal(stringMetadata.Utf8Length, jString.Utf8Length);

		Assert.Equal(stringMetadata.Value, jString.ToString());
	}
	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void MetadataTest(Boolean disposeParse)
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<JStringObject>();
		String textValue = typeMetadata.ToString();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		VirtualMachineProxy vm = env.VirtualMachine;
		ThreadProxy thread = ThreadProxy.CreateEnvironment(env);
		JClassLocalRef classRef = JStringObjectTests.fixture.Create<JClassLocalRef>();
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		JGlobalRef globalRef = JStringObjectTests.fixture.Create<JGlobalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, typeMetadata, classRef);
		using JLocalObject jLocal = new(env, stringRef.Value, jStringClass);
		using JGlobal jGlobal = new(vm, new(jStringClass, IClassType.GetMetadata<JStringObject>()), globalRef);

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
		Assert.IsType<JFunctionDefinition<JStringObject>>(typeMetadata.CreateFunctionDefinition("functionName"u8, []));
		Assert.IsType<JFieldDefinition<JStringObject>>(typeMetadata.CreateFieldDefinition("fieldName"u8));
		Assert.Equal(typeof(JLocalObject), EnvironmentProxy.GetFamilyType<JStringObject>());
		Assert.Equal(JTypeKind.Class, EnvironmentProxy.GetKind<JStringObject>());
		Assert.Contains(IInterfaceType.GetMetadata<JSerializableObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JCharSequenceObject>(), typeMetadata.Interfaces);
		Assert.Contains(IInterfaceType.GetMetadata<JComparableObject>(), typeMetadata.Interfaces);
		Assert.DoesNotContain(JFakeInterfaceObject.TypeMetadata, typeMetadata.Interfaces);

		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JSerializableObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JCharSequenceObject>()));
		Assert.True(typeMetadata.Interfaces.Contains(IInterfaceType.GetMetadata<JComparableObject>()));
		Assert.False(typeMetadata.Interfaces.Contains(JFakeInterfaceObject.TypeMetadata));

		vm.InitializeThread(Arg.Any<CString?>(), Arg.Any<JGlobalBase?>(), Arg.Any<Int32>()).ReturnsForAnyArgs(thread);
		env.ClassFeature.GetClass<JStringObject>().Returns(jStringClass);
		env.ReferenceFeature.Received(1).GetLifetime(jLocal, stringRef.Value, jStringClass, false);

		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JLocalObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JSerializableObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JCharSequenceObject>()));
		Assert.True(typeMetadata.TypeOf(IReferenceType.GetMetadata<JComparableObject>()));
		Assert.Null(typeMetadata.ParseInstance(default));
		Assert.Null(typeMetadata.ParseInstance(env, default));
		Assert.Null(typeMetadata.CreateException(jGlobal));

		using JStringObject jString0 =
			Assert.IsType<JStringObject>(typeMetadata.CreateInstance(jStringClass, stringRef.Value, true));
		using JStringObject jString1 = Assert.IsType<JStringObject>(typeMetadata.ParseInstance(jLocal, disposeParse));
		using JStringObject jString2 = Assert.IsType<JStringObject>(typeMetadata.ParseInstance(env, jGlobal));

		env.ClassFeature.Received(0).IsInstanceOf<JStringObject>(Arg.Any<JReferenceObject>());

		using IFixedPointer.IDisposable fPtr = (typeMetadata as ITypeInformation).GetClassNameFixedPointer();
		Assert.Equal(fPtr.Pointer, typeMetadata.ClassName.AsSpan().GetUnsafeIntPtr());
	}
	[Fact]
	internal void CreateTest()
	{
		String textValue = JStringObjectTests.fixture.Create<String>();
		CString utf8Value = (CString)textValue;
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, textValue);

		env.StringFeature.Create(textValue).Returns(jString);
		env.StringFeature.Create(utf8Value).Returns(jString);

		Assert.Null(JStringObject.Create(env, default(String)));
		Assert.Equal(jString, JStringObject.Create(env, textValue));
		Assert.Equal(jString, JStringObject.Create(env, textValue.AsSpan()));
		Assert.Null(JStringObject.Create(env, default(CString)));
		Assert.Equal(jString, JStringObject.Create(env, utf8Value));
		Assert.Equal(jString, JStringObject.Create(env, utf8Value.AsSpan()));

		env.StringFeature.Received(2).Create(textValue);
		env.StringFeature.Received(2).Create(utf8Value);
	}
	[Fact]
	internal void OperatorsTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		String value0 = JStringObjectTests.fixture.Create<String>();
		String value1 = JStringObjectTests.fixture.Create<String>();
		JStringLocalRef stringRef0 = JStringObjectTests.fixture.Create<JStringLocalRef>();
		JStringLocalRef stringRef1 = JStringObjectTests.fixture.Create<JStringLocalRef>();

		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString0 = new(jStringClass, stringRef0, value0);
		using JStringObject jString1 = new(jStringClass, stringRef1, value1);

		Assert.Equal(value0 == value1, jString0 == value1);
		Assert.Equal(value0 != value1, jString0 != value1);
		Assert.Equal(value0 == value1, jString0 == jString1);
		Assert.Equal(value0 != value1, jString0 != jString1);
	}
	[Fact]
	internal void MemoryTest()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		JStringLocalRef stringRef = JStringObjectTests.fixture.Create<JStringLocalRef>();
		String value = JStringObjectTests.fixture.Create<String>();
		using JClassObject jClassClass = new(env);
		using JClassObject jStringClass = new(jClassClass, IClassType.GetMetadata<JStringObject>());
		using JStringObject jString = new(jStringClass, stringRef, value);

		JStringObjectTests.GetNativeCharsTest(jString);
		JStringObjectTests.GetCriticalCharsTest(jString);
		JStringObjectTests.GetNativeUtf8CharsTest(jString);
	}

	private static void GetNativeCharsTest(JStringObject jString)
	{
		using IReadOnlyFixedMemory<Char>.IDisposable chars = jString.Value.AsMemory().GetFixedContext();
		IReadOnlyFixedContext<Byte> binarySequenceMemory = chars.AsBinaryContext();
		INativeMemoryAdapter sequenceMemory = Substitute.For<INativeMemoryAdapter>();
		JReleaseMode releaseMode = JStringObjectTests.fixture.Create<JReleaseMode>();

		sequenceMemory.GetReadOnlyContext(Arg.Any<JNativeMemory<Char>>()).Returns(binarySequenceMemory);
		jString.Environment.StringFeature.GetSequence(jString, Arg.Any<JMemoryReferenceKind>()).Returns(sequenceMemory);

		JNativeMemory<Char> sequence = jString.GetNativeChars();
		try
		{
			IReadOnlyFixedContext<Char> charsContext = sequence.GetContext();

			Assert.Equal(chars.Pointer, sequence.Pointer);
			Assert.Equal(sequence.Copy, sequence.Copy);
			Assert.Equal(sequence.Critical, sequenceMemory.Critical);
			Assert.False(sequence.Disposed.Value);
			Assert.True(chars.Values.SequenceEqual(sequence.Values));
			Assert.Equal(binarySequenceMemory, sequence.GetBinaryContext());

			Assert.Equal(chars.Pointer, charsContext.Pointer);
			Assert.True(chars.Values.SequenceEqual(charsContext.Values));
			Assert.Equal(chars.ValuePointer, charsContext.ValuePointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out _).Pointer);

			sequence.ReleaseMode = releaseMode;
			Assert.Equal(releaseMode, sequence.ReleaseMode);
		}
		finally
		{
			sequence.Dispose();
		}
		sequence.Dispose();
		sequenceMemory.Received(1).GetReadOnlyContext(sequence);
		sequenceMemory.Received(1).Release(releaseMode);
	}
	private static void GetCriticalCharsTest(JStringObject jString)
	{
		using IReadOnlyFixedMemory<Char>.IDisposable chars = jString.Value.AsMemory().GetFixedContext();
		IReadOnlyFixedContext<Byte> binarySequenceMemory = chars.AsBinaryContext();
		INativeMemoryAdapter sequenceMemory = Substitute.For<INativeMemoryAdapter>();
		JReleaseMode releaseMode = JStringObjectTests.fixture.Create<JReleaseMode>();

		sequenceMemory.GetReadOnlyContext(Arg.Any<JNativeMemory<Char>>()).Returns(binarySequenceMemory);
		jString.Environment.StringFeature.GetCriticalSequence(jString, Arg.Any<JMemoryReferenceKind>())
		       .Returns(sequenceMemory);

		JNativeMemory<Char> sequence = jString.GetCriticalChars();
		try
		{
			IReadOnlyFixedContext<Char> charsContext = sequence.GetContext();

			Assert.Equal(chars.Pointer, sequence.Pointer);
			Assert.Equal(sequence.Copy, sequence.Copy);
			Assert.Equal(sequence.Critical, sequenceMemory.Critical);
			Assert.False(sequence.Disposed.Value);
			Assert.True(chars.Values.SequenceEqual(sequence.Values));
			Assert.True(chars.Bytes.SequenceEqual((sequence as IReadOnlyFixedMemory<Byte>).Values));
			Assert.True(chars.Bytes.SequenceEqual((sequence as IReadOnlyFixedMemory<Byte>).Bytes));
			Assert.Equal(binarySequenceMemory, sequence.GetBinaryContext());

			Assert.Equal(chars.Pointer, charsContext.Pointer);
			Assert.True(chars.Values.SequenceEqual(charsContext.Values));
			Assert.Equal(chars.ValuePointer, charsContext.ValuePointer);
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).Transformation<Byte>(out _).Pointer,
			             binarySequenceMemory.Transformation<Byte>(out _).Pointer);

			sequence.ReleaseMode = releaseMode;
			Assert.Equal(releaseMode, sequence.ReleaseMode);
		}
		finally
		{
			sequence.Dispose();
		}
		sequence.Dispose();
		sequenceMemory.Received(1).GetReadOnlyContext(sequence);
		sequenceMemory.Received(1).Release(releaseMode);
	}
	private static void GetNativeUtf8CharsTest(JStringObject jString)
	{
		using IReadOnlyFixedMemory<Byte>.IDisposable utf8Chars =
			Encoding.UTF8.GetBytes(jString.Value).AsMemory().GetFixedContext();
		INativeMemoryAdapter sequenceMemory = Substitute.For<INativeMemoryAdapter>();
		JReleaseMode releaseMode = JStringObjectTests.fixture.Create<JReleaseMode>();

		sequenceMemory.GetReadOnlyContext(Arg.Any<JNativeMemory<Byte>>()).Returns(utf8Chars);
		jString.Environment.StringFeature.GetUtf8Sequence(jString, Arg.Any<JMemoryReferenceKind>())
		       .Returns(sequenceMemory);

		JNativeMemory<Byte> sequence = jString.GetNativeUtf8Chars();
		try
		{
			Assert.Equal(sequence, ((IReadOnlyFixedMemory)sequence).AsBinaryContext());
			Assert.Equal(utf8Chars.Pointer, sequence.Pointer);
			Assert.Equal(sequence.Copy, sequence.Copy);
			Assert.Equal(sequence.Critical, sequenceMemory.Critical);
			Assert.False(sequence.Disposed.Value);
			Assert.True(utf8Chars.Values.SequenceEqual(sequence.Values));
			Assert.True(utf8Chars.Values.SequenceEqual((sequence as IReadOnlyFixedMemory<Byte>).Bytes));
			Assert.Equal((IReadOnlyFixedContext<Byte>)utf8Chars, sequence.GetBinaryContext());
			Assert.Equal((sequence as IReadOnlyFixedContext<Byte>).Transformation<Byte>(out _).Pointer,
			             utf8Chars.Pointer);

			sequence.ReleaseMode = releaseMode;
			Assert.Equal(releaseMode, sequence.ReleaseMode);
		}
		finally
		{
			sequence.Dispose();
		}
		sequence.Dispose();
		sequenceMemory.Received(1).GetReadOnlyContext(sequence);
		sequenceMemory.Received(1).Release(releaseMode);
	}

	private static void ObjectMetadataTest(JStringObject jString)
	{
		StringObjectMetadata objectMetadata = Assert.IsType<StringObjectMetadata>(ILocalObject.CreateMetadata(jString));
		Assert.Equal(jString.ObjectClassName, objectMetadata.ObjectClassName);
		Assert.Equal(jString.ObjectSignature, objectMetadata.ObjectSignature);
		Assert.Equal(jString.Value, objectMetadata.Value);
		Assert.Equal(jString.Length, objectMetadata.Length);
		Assert.Equal(jString.Utf8Length, objectMetadata.Utf8Length);

		JSerializableObject jSerializable = jString.CastTo<JSerializableObject>();
		JCharSequenceObject jCharSequence = jString.CastTo<JCharSequenceObject>();
		JComparableObject jComparableO = jString.CastTo<JComparableObject>();

		Assert.Equal(jString.Id, jSerializable.Id);
		Assert.Equal(jString.Id, jCharSequence.Id);
		Assert.Equal(jString.Id, jComparableO.Id);

		Assert.Equal(jString, jSerializable.Object);
		Assert.Equal(jString, jCharSequence.Object);
		Assert.Equal(jString, jComparableO.Object);

		Assert.True(Object.ReferenceEquals(jString, jString.CastTo<JLocalObject>()));
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
		       .IsSameObject(
			       jString, Arg.Is<JLocalObject>(l => (l as ILocalObject).InternalReference.Equals(stringRef1)));
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