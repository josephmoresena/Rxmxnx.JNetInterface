namespace Rxmxnx.JNetInterface.Tests;

public sealed unsafe partial class JVirtualMachineLibraryTests
{
	[ThreadStatic]
	private static Int32 count;
	[ThreadStatic]
	private static Int32 jniVersion;
	[ThreadStatic]
	private static Exception? nativeException;
	[ThreadStatic]
	private static JVirtualMachineInitArg? args;
	[ThreadStatic]
	private static JResult result;
	[ThreadStatic]
	private static NativeInterfaceProxy? proxyEnv;
	[ThreadStatic]
	private static ReadOnlyValPtr<VirtualMachineInitOptionValue> optionsPtr;
	[ThreadStatic]
	private static NativeInterfaceProxy[]? proxies;

	private static JResult GetDefaultVirtualMachineInitArgsForGetLatestSupportedVersionTest(
		ref VirtualMachineInitArgumentValue initValue)
	{
		try
		{
#if NET8_0_OR_GREATER
			ref readonly VirtualMachineInitOptionValue options =
#else
			ref VirtualMachineInitOptionValue options =
#endif
				ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);

			if (JVirtualMachineLibraryTests.count == 0)
				Assert.Equal(0x00010006, initValue.Version);
			JVirtualMachineLibraryTests.count++;
#if NET8_0_OR_GREATER
			Assert.True(Unsafe.IsNullRef(in options));
#else
			Assert.True(ref options);
#endif
			Assert.Equal(default, initValue.OptionsLength);
			Assert.Equal(default, initValue.IgnoreUnrecognized);

			if (initValue.Version > JVirtualMachineLibraryTests.jniVersion)
				return JResult.VersionError;

			initValue = new()
			{
				Version = initValue.Version,
				OptionsLength = 0,
				Options = ReadOnlyValPtr<VirtualMachineInitOptionValue>.Zero,
				IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
			};
			return JResult.Ok;
		}
		catch (Exception ex)
		{
			JVirtualMachineLibraryTests.nativeException = ex;
			return JResult.Ok;
		}
	}
	private static JResult CreateVirtualMachineForCreateVirtualMachineTest(out JVirtualMachineRef vmRef,
		out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue value)
	{
		if (JVirtualMachineLibraryTests.proxyEnv is null || JVirtualMachineLibraryTests.args is null)
		{
			vmRef = default;
			envRef = default;
			return JResult.DetachedThreadError;
		}
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
				!Unsafe.IsNullRef(in options);
#else
                !Unsafe.IsNullRef(ref options);
#endif

			Assert.Equal(0x00010006, value.Version);
			Assert.Equal(JVirtualMachineLibraryTests.args.Options.NonEmptyCount, value.OptionsLength);
			Assert.Equal(JVirtualMachineLibraryTests.args.IgnoreUnrecognized, value.IgnoreUnrecognized);
			if (!nullOptions && (ReadOnlyValPtr<Byte>)options.OptionString is { IsZero: false, } optionString)
			{
#if NET8_0_OR_GREATER
				ref readonly Byte optionsBuffer =
#else
                ref Byte optionsBuffer =
#endif
					ref Unsafe.AsRef(in JVirtualMachineLibraryTests.args.Options.GetPinnableReference());
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

			if (JVirtualMachineLibraryTests.result != JResult.Ok)
			{
				vmRef = default;
				envRef = default;
				return JVirtualMachineLibraryTests.result;
			}

			vmRef = JVirtualMachineLibraryTests.proxyEnv.VirtualMachine.Reference;
			envRef = JVirtualMachineLibraryTests.proxyEnv.Reference;
			return JResult.Ok;
		}
		catch (Exception ex)
		{
			JVirtualMachineLibraryTests.nativeException = ex;
			vmRef = default;
			envRef = default;
			return JVirtualMachineLibraryTests.result;
		}
	}
	private static JResult GetDefaultVirtualMachineInitArgsForGetDefaultArgumentTest(
		ref VirtualMachineInitArgumentValue initValue)
	{
		if (JVirtualMachineLibraryTests.args is null) return JResult.DetachedThreadError;
		try
		{
			ref readonly VirtualMachineInitOptionValue options =
				ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);

			Assert.InRange(initValue.Version, 0x00010006,
			               Math.Max(JVirtualMachineLibraryTests.args.Version, 0x00010006));
#if NET8_0_OR_GREATER
			Assert.True(Unsafe.IsNullRef(in options));
#else
			Assert.True(Unsafe.IsNullRef(ref Unsafe.AsRef(in options)));
#endif
			Assert.Equal(default, initValue.OptionsLength);
			Assert.Equal(default, initValue.IgnoreUnrecognized);

			initValue = new()
			{
				Version = initValue.Version,
				OptionsLength = JVirtualMachineLibraryTests.args.Options.Count,
				Options = JVirtualMachineLibraryTests.optionsPtr,
				IgnoreUnrecognized = JVirtualMachineLibraryTests.args.IgnoreUnrecognized,
			};
			return JVirtualMachineLibraryTests.result;
		}
		catch (Exception ex)
		{
			JVirtualMachineLibraryTests.nativeException = ex;
			return JVirtualMachineLibraryTests.result;
		}
	}
	private static JResult GetCreatedJavaVMs(ValPtr<JVirtualMachineRef> vmBuf, Int32 bufLen, out Int32 nVMs)
	{
		if (JVirtualMachineLibraryTests.proxies is null)
		{
			nVMs = 0;
			return JResult.DetachedThreadError;
		}
		try
		{
			nVMs = JVirtualMachineLibraryTests.proxies.Length;
			JVirtualMachineLibraryTests.count++;

			if (JVirtualMachineLibraryTests.count == 1)
			{
				Assert.True(Unsafe.IsNullRef(ref vmBuf.Reference));
				Assert.Equal(0, bufLen);
				return JResult.InvalidArgumentsError;
			}

			Span<JVirtualMachineRef> vmRefs = MemoryMarshal.CreateSpan(ref vmBuf.Reference, bufLen);
			Assert.Equal(JVirtualMachineLibraryTests.proxies.Length, vmRefs.Length);
			JVirtualMachineLibraryTests.proxies.Select(p => p.VirtualMachine.Reference).ToArray().CopyTo(vmRefs);
			nVMs = vmRefs.Length;
			return JVirtualMachineLibraryTests.result;
		}
		catch (Exception ex)
		{
			JVirtualMachineLibraryTests.nativeException = ex;
			nVMs = -1;
			return JVirtualMachineLibraryTests.result;
		}
	}
	private static void CleanUp()
	{
		JVirtualMachineLibraryTests.count = default;
		JVirtualMachineLibraryTests.jniVersion = default;
		JVirtualMachineLibraryTests.nativeException = default;
		JVirtualMachineLibraryTests.result = default;
		JVirtualMachineLibraryTests.proxyEnv = default;
		JVirtualMachineLibraryTests.optionsPtr = default;
		JVirtualMachineLibraryTests.proxies = default;
	}
}