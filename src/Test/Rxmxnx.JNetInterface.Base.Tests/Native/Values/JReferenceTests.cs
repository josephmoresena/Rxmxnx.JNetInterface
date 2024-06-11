namespace Rxmxnx.JNetInterface.Tests.Native.Values;

[ExcludeFromCodeCoverage]
public sealed class JReferenceTests
{
	private static readonly IFixture fixture = new Fixture();

	[Fact]
	internal void ObjectLocalRefTest() => JReferenceTests.Test<JObjectLocalRef>();
	[Fact]
	internal void GlobalRefTest() => JReferenceTests.GlobalTest<JGlobalRef>();
	[Fact]
	internal void WeakRefTest() => JReferenceTests.GlobalTest<JWeakRef>();

	[Fact]
	internal void ClassLocalRefTest() => JReferenceTests.ObjectReferenceTest<JClassLocalRef>();
	[Fact]
	internal void ThrowableLocalRefTest() => JReferenceTests.ObjectReferenceTest<JThrowableLocalRef>();
	[Fact]
	internal void StringLocalRefTest() => JReferenceTests.ObjectReferenceTest<JStringLocalRef>();
	[Fact]
	internal void ArrayLocalRefTest() => JReferenceTests.ObjectReferenceTest<JArrayLocalRef>();

	[Fact]
	internal void JBooleanArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JBooleanArrayLocalRef>();
	[Fact]
	internal void JByteArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JByteArrayLocalRef>();
	[Fact]
	internal void JCharArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JCharArrayLocalRef>();
	[Fact]
	internal void JDoubleArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JDoubleArrayLocalRef>();
	[Fact]
	internal void JFloatArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JFloatArrayLocalRef>();
	[Fact]
	internal void JIntArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JIntArrayLocalRef>();
	[Fact]
	internal void JLongArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JLongArrayLocalRef>();
	[Fact]
	internal void JObjetArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JObjectArrayLocalRef>();
	[Fact]
	internal void JShortArrayLocalRefTest() => JReferenceTests.ArrayReferenceTest<JShortArrayLocalRef>();

	private static void Test<TPointer>()
		where TPointer : unmanaged, IFixedPointer, INativeType, IEqualityOperators<TPointer, TPointer, Boolean>,
		IEquatable<TPointer>
	{
		TPointer ref1 = JReferenceTests.CreatePointer<TPointer>();
		TPointer ref2 = JReferenceTests.CreatePointer<TPointer>();
		TPointer ref3 = ref1;
		JGlobalRef globalRef = NativeUtilities.Transform<TPointer, JGlobalRef>(in ref1);
		JWeakRef weakRef = NativeUtilities.Transform<TPointer, JWeakRef>(in ref1);

		Assert.True(ref1.Equals(IWrapper.Create(ref1)));
		Assert.Equal(ref3 is not JWeakRef, ref1.Equals(globalRef));
		Assert.Equal(ref3 is not JGlobalRef, ref1.Equals(weakRef));
		Assert.Equal(ref1.GetHashCode(), ref1.Pointer.GetHashCode());
		Assert.NotEqual(ref1, ref2);

		Assert.False(ref1.Equals(ref2));
		Assert.True(ref1.Equals(ref3));
		Assert.False(ref1.Equals((Object)ref2));
		Assert.True(ref1.Equals((Object)ref3));
		Assert.False(ref1.Equals((Int64)ref1.Pointer));

		Assert.False(ref1 == ref2);
		Assert.True(ref1 == ref3);
		Assert.True(ref1 != ref2);
		Assert.False(ref1 != ref3);

		Assert.Equal(IntPtr.Zero, new TPointer().Pointer);
	}
	private static void GlobalTest<TGlobalRef>()
		where TGlobalRef : unmanaged, IFixedPointer, INativeType, IEqualityOperators<TGlobalRef, TGlobalRef, Boolean>,
		IEquatable<TGlobalRef>, IObjectGlobalReferenceType
	{
		JReferenceTests.Test<TGlobalRef>();
		TGlobalRef ref1 = JReferenceTests.CreatePointer<TGlobalRef>();
		TGlobalRef ref2 = JReferenceTests.CreatePointer<TGlobalRef>();

		Assert.Equal(ref1.Value == ref2.Value, ref1 == ref2);
		Assert.True(ref1.Value.Equals(ref1));
	}
	private static void ObjectReferenceTest<TObjectRef>()
		where TObjectRef : unmanaged, IObjectReferenceType<TObjectRef>,
		IEqualityOperators<TObjectRef, TObjectRef, Boolean>, IEquatable<TObjectRef>,
		IEqualityOperators<TObjectRef, JObjectLocalRef, Boolean>
	{
		JReferenceTests.Test<TObjectRef>();
		TObjectRef ref1 = JReferenceTests.CreatePointer<TObjectRef>();
		TObjectRef ref2 = JReferenceTests.CreatePointer<TObjectRef>();
		TObjectRef defRef = default;

		Assert.True(defRef.IsDefault);
		Assert.Equal(ref1.Pointer == IntPtr.Zero, ref1.IsDefault);

		JGlobalRef globalRef = NativeUtilities.Transform<TObjectRef, JGlobalRef>(in ref1);
		JWeakRef weakRef = NativeUtilities.Transform<TObjectRef, JWeakRef>(in ref1);

		Assert.True(ref1.Equals((Object)ref1));
		Assert.True(ref1.Equals(IWrapper.Create(ref1)));
		Assert.True(ref1.Equals(globalRef));
		Assert.True(ref1.Equals(ref1.Value));
		Assert.True(ref1.Equals(IWrapper.Create(ref1.Value)));
		Assert.True(ref1.Equals(JClassLocalRef.FromReference(in globalRef)));
		Assert.True(ref1.Equals(JClassLocalRef.FromReference(in weakRef)));
		Assert.False(ref1.Equals(ref1.Pointer));
		Assert.True(ref1.Value.Equals(ref1));

		Assert.Equal(ref1.Pointer == ref2.Pointer, ref1.Equals(ref2));
		Assert.Equal(ref1.Pointer == IntPtr.Size, ref1.Equals(defRef));

		Assert.True(ref1 == ref1.Value);
		Assert.False(ref1 != ref1.Value);
		Assert.Equal(ref1.Pointer == ref2.Pointer, ref1 == ref2);
		Assert.Equal(ref1.Pointer != ref2.Pointer, ref1 != ref2);
		Assert.Equal(ref1.Pointer == IntPtr.Size, ref1 == defRef.Value);
		Assert.Equal(ref1.Pointer != IntPtr.Size, ref1 != defRef.Value);

		Assert.Equal(ref1, TObjectRef.FromReference(ref1.Value));
		Assert.Equal(ref2, TObjectRef.FromReference(ref2.Value));
	}
	private static void ArrayReferenceTest<TArrayRef>()
		where TArrayRef : unmanaged, IArrayReferenceType, IObjectReferenceType<TArrayRef>,
		IEqualityOperators<TArrayRef, TArrayRef, Boolean>, IEquatable<TArrayRef>,
		IEqualityOperators<TArrayRef, JObjectLocalRef, Boolean>, IEqualityOperators<TArrayRef, JArrayLocalRef, Boolean>,
		IEquatable<JArrayLocalRef>
	{
		JReferenceTests.ObjectReferenceTest<TArrayRef>();
		TArrayRef ref1 = JReferenceTests.CreatePointer<TArrayRef>();
		TArrayRef ref2 = JReferenceTests.CreatePointer<TArrayRef>();
		TArrayRef defRef = default;

		JArrayLocalRef arrRef = ref1.ArrayValue;

		Assert.True(defRef.IsDefault);
		Assert.Equal(ref1.Pointer == IntPtr.Zero, ref1.IsDefault);

		Assert.True(ref1.Equals((Object)ref1));
		Assert.True(ref1.Equals(ref1.ArrayValue));
		Assert.True(ref1.Equals((Object)ref1.ArrayValue));
		Assert.True(ref1.Equals(JObjectArrayLocalRef.FromReference(in arrRef)));
		Assert.True(ref1.ArrayValue.Equals(ref1));

		Assert.Equal(ref1.Pointer == ref2.Pointer, ref1.Equals(ref2));
		Assert.Equal(ref1.Pointer == IntPtr.Size, ref1.Equals(defRef));

		Assert.True(ref1 == ref1.ArrayValue);
		Assert.False(ref1 != ref1.ArrayValue);
		Assert.Equal(ref1.Pointer == ref2.Pointer, ref1 == ref2);
		Assert.Equal(ref1.Pointer != ref2.Pointer, ref1 != ref2);
		Assert.Equal(ref1.Pointer == IntPtr.Size, ref1 == defRef.ArrayValue);
		Assert.Equal(ref1.Pointer != IntPtr.Size, ref1 != defRef.ArrayValue);
	}
	private static TPointer CreatePointer<TPointer>()
		where TPointer : unmanaged, IFixedPointer, INativeType, IEqualityOperators<TPointer, TPointer, Boolean>,
		IEquatable<TPointer>
	{
		IntPtr ptr1 = JReferenceTests.fixture.CreateMany<Byte>(IntPtr.Size).ToArray().AsSpan().AsValue<IntPtr>();
		return NativeUtilities.Transform<IntPtr, TPointer>(in ptr1);
	}
}