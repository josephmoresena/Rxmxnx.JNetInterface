namespace Rxmxnx.JNetInterface.Tests.Native;

[ExcludeFromCodeCoverage]
public sealed class ReferenceObjectTests
{
	private static readonly IFixture fixture = new Fixture();

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	internal void Test(Boolean isProxy)
	{
		CString className = (CString)ReferenceObjectTests.fixture.Create<String>();
		CString signature = (CString)ReferenceObjectTests.fixture.Create<String>();
		Byte[] value = ReferenceObjectTests.fixture.CreateMany<Byte>(IntPtr.Size).ToArray();
		Span<Byte> bytes = stackalloc Byte[IntPtr.Size * 3];
		Span<JValue> span = stackalloc JValue[3];
		ReferenceObjectProxy proxy = new(className, signature, isProxy);
		IDisposable synchronizer = Substitute.For<IDisposable>();
		Int32 idx = IntPtr.Size * 2;

		proxy.AsSpanEvent += () => value.AsSpan();
		proxy.GetSynchronizerEvent += () => synchronizer;
		proxy.InstanceOfEvent += (t, o) => t == typeof(DataTypeProxy) && Object.ReferenceEquals(proxy, o);
		proxy.SetAssignableToEvent +=
			(t, o) => Assert.True(t == typeof(DataTypeProxy) && Object.ReferenceEquals(proxy, o));

		(proxy as IObject).CopyTo(bytes);
		(proxy as IObject).CopyTo(bytes, ref idx);
		(proxy as IObject).CopyTo(span, 1);

		Assert.NotEqual(0, proxy.Id);
		Assert.True(proxy.InstanceOf<DataTypeProxy>());
		Assert.Equal(isProxy, proxy.IsProxy);
		Assert.Equal(value.AsSpan().AsValue<JObjectLocalRef>(), proxy.As<JObjectLocalRef>());
		Assert.Equal(value.AsSpan().AsValue<JObjectLocalRef>(), proxy.To<JObjectLocalRef>());
		Assert.True(Unsafe.AreSame(in value.AsSpan().AsValue<JObjectLocalRef>(), in proxy.As<JObjectLocalRef>()));
		Assert.Equal(value.AsSpan().AsValue<IntPtr>() == IntPtr.Zero, proxy.IsDefault);
		Assert.Equal(value.AsSpan().AsValue<IntPtr>() == IntPtr.Zero, proxy.IsDefaultInstance());
		Assert.Equal(synchronizer, proxy.Synchronize());

		Assert.Equal(bytes.AsValues<Byte, IntPtr>()[0], value.AsSpan().AsValue<IntPtr>());
		Assert.Equal(idx, bytes.Length);
		Assert.Equal(bytes.AsValues<Byte, IntPtr>()[2], value.AsSpan().AsValue<IntPtr>());
		Assert.Equal(span[1], JValue.Create(value.AsSpan().AsValue<IntPtr>()));

		Assert.False(proxy.Equals(null));
		Assert.False(proxy.Equals(ReferenceObjectTests.fixture.Create<String>()));
		Assert.False(proxy.Equals(new ObjectProxy()));
		switch (IntPtr.Size)
		{
			case sizeof(Int32):
				Assert.False(proxy.Equals(
					             new PrimitiveObjectProxy<Int32>(proxy.ObjectClassName, proxy.ObjectSignature,
					                                             value.AsSpan().AsValue<Int32>())));
				break;
			case sizeof(Int64):
				Assert.False(proxy.Equals(
					             new PrimitiveObjectProxy<Int64>(proxy.ObjectClassName, proxy.ObjectSignature,
					                                             value.AsSpan().AsValue<Int64>())));
				break;
		}

		ReferenceObjectTests.EqualityTest(proxy, false, false);
		ReferenceObjectTests.EqualityTest(proxy, false, true);
		ReferenceObjectTests.EqualityTest(proxy, true, false);
		ReferenceObjectTests.EqualityTest(proxy, true, true);
		Assert.Equal(value.AsSpan().AsValue<IntPtr>() == IntPtr.Zero, JObject.IsNullOrDefault(proxy));
		ReferenceObjectTests.ViewTest(proxy);

		ReferenceObjectTests.EqualityTest(proxy, false, false);
		ReferenceObjectTests.EqualityTest(proxy, false, true);
		ReferenceObjectTests.EqualityTest(proxy, true, false);
		ReferenceObjectTests.EqualityTest(proxy, true, true);
		Assert.True(JObject.IsNullOrDefault(proxy));
		Assert.True(JObject.IsNullOrDefault(null));
		ReferenceObjectTests.ViewTest(proxy);
	}

	private static void EqualityTest(JReferenceObject jObject, Boolean isCopy, Boolean isProxy)
	{
		ReferenceObjectProxy proxy = new(jObject.ObjectClassName, jObject.ObjectClassName, isProxy);
		ReferenceObjectProxy proxy2 = new(jObject);
		IDisposable synchronizer = Substitute.For<IDisposable>();
		Byte[] value = new Byte[IntPtr.Size];

		if (isCopy) (jObject as IObject).CopyTo(value);

		proxy.AsSpanEvent += () => value.AsSpan();
		proxy.GetSynchronizerEvent += () => synchronizer;
		proxy.InstanceOfEvent += (t, _) => t == typeof(DataTypeProxy);

		Boolean equals = proxy.IsProxy == jObject.IsProxy && proxy.IsDefault == jObject.IsDefault &&
			(isCopy || proxy.IsDefault);

		Assert.Equal(equals, jObject.Equals(proxy));
		Assert.Equal(equals, jObject == proxy);
		Assert.Equal(!equals, jObject != proxy);

		Assert.NotEqual(jObject.Id, proxy.Id);
		Assert.NotEqual(jObject.Id, proxy2.Id);
		Assert.Equal(jObject.IsProxy, proxy2.IsProxy);
	}
	private static void ViewTest(JReferenceObject jObject)
	{
		ViewProxy view = new(jObject);
		Assert.Equal(jObject.IsDefault, view.IsDefault);
		Assert.Equal(jObject.Id, view.Id);
		Assert.Equal(jObject, view.Object);
		Assert.Equal(jObject, (view as IViewObject).Object);
		Assert.Equal(jObject, (view as IWrapper<JReferenceObject>).Value);
		Assert.Equal(jObject.ObjectClassName, view.ObjectClassName);
		Assert.Equal(jObject.ObjectSignature, view.ObjectSignature);
		Assert.Equal(jObject.InstanceOf<DataTypeProxy>(), view.InstanceOf<DataTypeProxy>());
		Assert.Equal(jObject.Synchronize(), view.Synchronize());
		Assert.True(jObject.Equals(view));
		Assert.Equal(JObject.IsNullOrDefault(jObject), JObject.IsNullOrDefault(view));
		view.SetAssignableTo<DataTypeProxy>(default);

		if (!jObject.IsDefault)
		{
			view.ClearValue();
			Assert.True(jObject.IsDefault);
		}
	}

	private abstract class DataTypeProxy : JReferenceObject, IDataType<DataTypeProxy>
	{
		protected DataTypeProxy(Boolean isProxy) : base(isProxy) { }
		protected DataTypeProxy(JReferenceObject jObject) : base(jObject) { }
	}

	private sealed class ObjectProxy : JObject
	{
		public override CString ObjectClassName => CString.Empty;
		public override CString ObjectSignature => CString.Empty;
		public override Boolean Equals(JObject? other) => true;
		private protected override void CopyTo(Span<Byte> span, ref Int32 offset) { }
		private protected override void CopyTo(Span<JValue> span, Int32 index) { }
	}

	private sealed class ViewProxy(JReferenceObject jObject) : JReferenceObject.View<JReferenceObject>(jObject);
}