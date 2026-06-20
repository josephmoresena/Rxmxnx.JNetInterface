namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class PInvokeLibraryTests : LibraryBaseTests, IVirtualMachineLibraryType
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	static Boolean IVirtualMachineLibraryType.IsStatic => PInvokeLibraryTests.isStatic;

	[SkippableFact]
	internal void StaticTest()
	{
		try
		{
			PInvokeLibraryTests.isStatic = true;
			Skip.If(AotInfo.IsNativeAot);

			InvalidOperationException ex =
				Assert.Throws<InvalidOperationException>(JVirtualMachineLibrary.Create<PInvokeLibraryTests>);
			Assert.Contains("Native AOT", ex.Message);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
	}
	[Fact]
	internal void EmptyCreatedVms()
	{
		Int32 countEnter = 0;
		try
		{
			PInvokeLibraryTests.getCreatedVirtualMachines = Call;

			JVirtualMachineLibrary library = JVirtualMachineLibrary.Create<PInvokeLibraryTests>();

			Assert.Empty(library.GetCreatedVirtualMachines());
			Assert.Equal(1, countEnter);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
		return;
		JResult Call(ValPtr<JVirtualMachineRef> arr, Int32 arrSize, out Int32 count)
		{
			if (countEnter == 0)
				Assert.True(arr.IsZero);
			count = 0;
			countEnter++;
			return JResult.Ok;
		}
	}
	[Theory]
	[InlineData((Int32)JRuntimeVersion.SEd1)]
	[InlineData((Int32)JRuntimeVersion.SEd2)]
	[InlineData((Int32)JRuntimeVersion.SEd4)]
	[InlineData((Int32)JRuntimeVersion.J6)]
	[InlineData((Int32)JRuntimeVersion.J8)]
	[InlineData((Int32)JRuntimeVersion.J9)]
	[InlineData((Int32)JRuntimeVersion.J10)]
	[InlineData((Int32)JRuntimeVersion.J19)]
	[InlineData((Int32)JRuntimeVersion.J20)]
	[InlineData((Int32)JRuntimeVersion.J21)]
	[InlineData((Int32)JRuntimeVersion.J24)]
	internal void GetLatestSupportedVersionTest(Int32 jniVersion)
	{
		Int32 count = 0;
		try
		{
			PInvokeLibraryTests.getDefaultVirtualMachineInitArgs = Call;

			JVirtualMachineLibrary library = JVirtualMachineLibrary.Create<PInvokeLibraryTests>();

			if (jniVersion < (Int32)JRuntimeVersion.SEd2)
			{
				Assert.Throws<JavaVersionException>(() => library.GetLatestSupportedVersion());
				return;
			}

			Assert.Equal(jniVersion, library.GetLatestSupportedVersion());
			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(jniVersion switch
			{
				(Int32)JRuntimeVersion.SEd2 => 2,
				(Int32)JRuntimeVersion.SEd4 => 3,
				(Int32)JRuntimeVersion.J6 => 4,
				(Int32)JRuntimeVersion.J8 => 5,
				(Int32)JRuntimeVersion.J9 => 6,
				(Int32)JRuntimeVersion.J10 => 7,
				(Int32)JRuntimeVersion.J19 => 8,
				(Int32)JRuntimeVersion.J20 => 9,
				(Int32)JRuntimeVersion.J21 => 10,
				(Int32)JRuntimeVersion.J24 => 10,
				_ => 1,
			}, count);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
		return;
		unsafe JResult Call(ref VirtualMachineInitArgumentValue initValue)
		{
			try
			{
#if NET8_0_OR_GREATER
				ref readonly VirtualMachineInitOptionValue options =
#else
				ref VirtualMachineInitOptionValue options =
#endif
					ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);

				if (count == 0)
					Assert.Equal((Int32)JRuntimeVersion.SEd2, initValue.Version);
				count++;
#if NET8_0_OR_GREATER
				Assert.True(Unsafe.IsNullRef(in options));
#else
				Assert.True(Unsafe.IsNullRef(ref options));
#endif
				Assert.Equal(default, initValue.OptionsLength);
				Assert.Equal(default, initValue.IgnoreUnrecognized);

				if (initValue.Version > jniVersion)
					return JResult.VersionError;

				initValue = new()
				{
					Version = initValue.Version,
					OptionsLength = 0,
					Options = ReadOnlyValPtr<VirtualMachineInitOptionValue>.Zero,
					IgnoreUnrecognized = PInvokeLibraryTests.fixture.Create<Boolean>(),
				};
				return JResult.Ok;
			}
			catch (Exception ex)
			{
				LibraryBaseTests.NativeException = ex;
				return JResult.Ok;
			}
		}
	}
	[Theory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.InvalidArgumentsError)]
	[InlineData(JResult.MemoryError)]
	[InlineData(JResult.VersionError)]
	internal void CreateVirtualMachineTest(JResult result)
	{
		JVirtualMachineInitArg args = new((Int32)JRuntimeVersion.J6)
		{
			Options = new(PInvokeLibraryTests.fixture.CreateMany<String>()),
			IgnoreUnrecognized = PInvokeLibraryTests.fixture.Create<Boolean>(),
		};
		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		try
		{
			PInvokeLibraryTests.createVirtualMachine = Call;

			JVirtualMachineLibrary library = JVirtualMachineLibrary.Create<PInvokeLibraryTests>();

			if (result != JResult.Ok)
			{
				Assert.Equal(result,
				             Assert.Throws<JniException>(() => library.CreateVirtualMachine(args, out IEnvironment __))
				                   .Result);
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			using IInvokedVirtualMachine invoked = library.CreateVirtualMachine(args, out IEnvironment env);

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
		}
		finally
		{
			PInvokeLibraryTests.CleanUp();
		}
		return;
		unsafe JResult Call(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
			in VirtualMachineInitArgumentValue value)
		{
			try
			{
#if NET8_0_OR_GREATER
				ref readonly VirtualMachineInitOptionValue options =
#else
				ref VirtualMachineInitOptionValue options =
#endif
					ref Unsafe.AsRef<VirtualMachineInitOptionValue>(value.Options);
				Boolean nullOptions =
#if NET8_0_OR_GREATER
					Unsafe.IsNullRef(in options);
#else
					Unsafe.IsNullRef(ref options);
#endif

				Assert.Equal((Int32)JRuntimeVersion.J6, value.Version);
				Assert.Equal(args.Options.NonEmptyCount, value.OptionsLength);
				Assert.Equal(args.IgnoreUnrecognized, value.IgnoreUnrecognized);
				if (!nullOptions && (ReadOnlyValPtr<Byte>)options.OptionString is { IsZero: false, } optionString)
				{
#if NET8_0_OR_GREATER
					ref readonly Byte optionsBuffer =
#else
				ref Byte optionsBuffer =
#endif
						ref Unsafe.AsRef(in args.Options.GetPinnableReference());
#if NET8_0_OR_GREATER
					Assert.True(Unsafe.AreSame(in optionsBuffer, in optionString.Reference));
#else
					Assert.True(Unsafe.AreSame(ref optionsBuffer, ref Unsafe.AsRef(in optionString.Reference)));
#endif
				}
				else
				{
					Assert.Equal(0, value.OptionsLength);
				}

				if (result != JResult.Ok)
				{
					vmRef = default;
					envRef = default;
					return result;
				}

				vmRef = proxyEnv.VirtualMachine.Reference;
				envRef = proxyEnv.Reference;
				return JResult.Ok;
			}
			catch (Exception ex)
			{
				LibraryBaseTests.NativeException = ex;
				vmRef = default;
				envRef = default;
				return result;
			}
		}
	}

	private static void CleanUp()
	{
		LibraryBaseTests.NativeException = default;
		PInvokeLibraryTests.isStatic = false;
		PInvokeLibraryTests.getDefaultVirtualMachineInitArgs = default;
		PInvokeLibraryTests.createVirtualMachine = default;
		PInvokeLibraryTests.getCreatedVirtualMachines = default;
	}

	static JResult IVirtualMachineLibraryType.GetDefaultVirtualMachineInitArgs(
		ref VirtualMachineInitArgumentValue initArg)
		=> PInvokeLibraryTests.getDefaultVirtualMachineInitArgs?.Invoke(ref initArg) ?? JResult.DetachedThreadError;
	static JResult IVirtualMachineLibraryType.CreateVirtualMachine(out JVirtualMachineRef vmRef,
		out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue initArg)
	{
		if (PInvokeLibraryTests.createVirtualMachine is not null)
			return PInvokeLibraryTests.createVirtualMachine(out vmRef, out envRef, in initArg);
		Unsafe.SkipInit(out vmRef);
		Unsafe.SkipInit(out envRef);
		return JResult.DetachedThreadError;
	}
	static JResult IVirtualMachineLibraryType.GetCreatedVirtualMachines(ValPtr<JVirtualMachineRef> arr, Int32 arrSize,
		out Int32 count)
	{
		if (PInvokeLibraryTests.getCreatedVirtualMachines is not null)
			return PInvokeLibraryTests.getCreatedVirtualMachines.Invoke(arr, arrSize, out count);
		Unsafe.SkipInit(out count);
		return JResult.DetachedThreadError;
	}
}