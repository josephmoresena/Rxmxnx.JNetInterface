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
		fixture.Register<IFixture, JIntArrayLocalRef>(ProxyTestsExtensions.CreateReference<JIntArrayLocalRef>);
		fixture.Register<IFixture, JLongArrayLocalRef>(ProxyTestsExtensions.CreateReference<JLongArrayLocalRef>);
		fixture.Register<IFixture, JShortArrayLocalRef>(ProxyTestsExtensions.CreateReference<JShortArrayLocalRef>);
		return fixture;
	}
	public static String ToSimplifiedString(this JArgumentMetadata argumentMetadata)
		=> $"{{ {nameof(JArgumentMetadata.Signature)} = {argumentMetadata.Signature}, {nameof(JArgumentMetadata.Size)} = {argumentMetadata.Size} }}";
	public static String ToPrintableHash(this JReferenceTypeMetadata typeMetadata)
		=> $"{InfoSequenceBase.GetPrintableHash(typeMetadata.Hash, out String lastChar)}{lastChar}";
	internal static IFixedPointer.IDisposable GetFixedPointer(this InfoSequenceBase info)
		=> info.ToString().AsMemory().GetFixedContext();
	internal static IFixedPointer.IDisposable GetFixedPointer(this TypeInfoSequence info,
		out IFixedPointer.IDisposable nameCtx)
	{
		Int32 nameLength = info.Name.Length + 1;
		String uName = String.Create(nameLength / 2 + nameLength % 2, info.Name, (s, n) =>
		{
			Span<Byte> bytes = s.AsBytes();
			ReadOnlySpan<Byte> source = n.AsSpan();
			for (Int32 i = 0; i < source.Length; i++) bytes[i] = source[i] != (Byte)'/' ? source[i] : (Byte)'.';
		});
		nameCtx = uName.AsMemory().GetFixedContext();
		return info.ToString().AsMemory().GetFixedContext();
	}
	public static JStackTraceElementObject CreateStackTrace(this StackTraceInfo info, JClassObject jClass,
		JObjectLocalRef localRef = default)
	{
		JReferenceTypeMetadata metadata = IClassType.GetMetadata<JStackTraceElementObject>();
		JStackTraceElementObject element = (JStackTraceElementObject)metadata.CreateInstance(jClass, localRef);
		StackTraceElementObjectMetadata objectMetadata = new(new(jClass)) { Information = info, };
		ILocalObject.ProcessMetadata(element, objectMetadata);
		return element;
	}
	public static ReadOnlySpan<Byte> GetByteSpan(this ReadOnlyValPtr<Byte> valPtr, Int32 maxLength = 100)
	{
		Int32 length = 0;
		while ((valPtr + length).Reference != default && length < maxLength) length++;
		return valPtr.Pointer.GetUnsafeReadOnlySpan<Byte>(length);
	}
	public static TPrimitive CreatePrimitive<TPrimitive>(this IFixture fixture)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		Span<TPrimitive> result = stackalloc TPrimitive[1];
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		switch (primitiveTypeMetadata.Signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				result.AsValues<TPrimitive, Boolean>()[0] = fixture.Create<Boolean>();
				break;
			case CommonNames.ByteSignatureChar:
				result.AsValues<TPrimitive, SByte>()[0] = fixture.Create<SByte>();
				break;
			case CommonNames.CharSignatureChar:
				result.AsValues<TPrimitive, Char>()[0] = fixture.Create<Char>();
				break;
			case CommonNames.DoubleSignatureChar:
				result.AsValues<TPrimitive, Double>()[0] = fixture.Create<Double>();
				break;
			case CommonNames.FloatSignatureChar:
				result.AsValues<TPrimitive, Single>()[0] = fixture.Create<Single>();
				break;
			case CommonNames.IntSignatureChar:
				result.AsValues<TPrimitive, Int32>()[0] = fixture.Create<Int32>();
				break;
			case CommonNames.LongSignatureChar:
				result.AsValues<TPrimitive, Int64>()[0] = fixture.Create<Int64>();
				break;
			case CommonNames.ShortSignatureChar:
				result.AsValues<TPrimitive, Int16>()[0] = fixture.Create<Int16>();
				break;
		}
		return result[0];
	}
	public static TPrimitive[] CreatePrimitiveArray<TPrimitive>(this IFixture fixture, Int32 length)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		TPrimitive[] result = new TPrimitive[length];
		JPrimitiveTypeMetadata primitiveTypeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		switch (primitiveTypeMetadata.Signature[0])
		{
			case CommonNames.BooleanSignatureChar:
				fixture.CreateMany<Boolean>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.ByteSignatureChar:
				fixture.CreateMany<SByte>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.CharSignatureChar:
				fixture.CreateMany<Char>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.DoubleSignatureChar:
				fixture.CreateMany<Double>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.FloatSignatureChar:
				fixture.CreateMany<Single>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.IntSignatureChar:
				fixture.CreateMany<Int32>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.LongSignatureChar:
				fixture.CreateMany<Int64>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
			case CommonNames.ShortSignatureChar:
				fixture.CreateMany<Int16>(length).ToArray().AsSpan().AsBytes().CopyTo(result.AsSpan().AsBytes());
				break;
		}
		return result;
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