namespace Rxmxnx.JNetInterface.Tests;

public sealed unsafe partial class JVirtualMachineLibraryTests
{
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

			if (LibraryBaseTests.Count == 0)
				Assert.Equal((Int32)JRuntimeVersion.SEd2, initValue.Version);
			LibraryBaseTests.Count++;
#if NET8_0_OR_GREATER
			Assert.True(Unsafe.IsNullRef(in options));
#else
			Assert.True(Unsafe.IsNullRef(ref options));
#endif
			Assert.Equal(default, initValue.OptionsLength);
			Assert.Equal(default, initValue.IgnoreUnrecognized);

			if (initValue.Version > LibraryBaseTests.JniVersion)
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
			LibraryBaseTests.NativeException = ex;
			return JResult.Ok;
		}
	}
	private static JResult CreateVirtualMachineForCreateVirtualMachineTest(out JVirtualMachineRef vmRef,
		out JEnvironmentRef envRef, in VirtualMachineInitArgumentValue value)
	{
		if (LibraryBaseTests.ProxyEnv is null || LibraryBaseTests.Args is null)
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
				Unsafe.IsNullRef(in options);
#else
				Unsafe.IsNullRef(ref options);
#endif

			Assert.Equal((Int32)JRuntimeVersion.J6, value.Version);
			Assert.Equal(LibraryBaseTests.Args.Options.NonEmptyCount, value.OptionsLength);
			Assert.Equal(LibraryBaseTests.Args.IgnoreUnrecognized, value.IgnoreUnrecognized);
			if (!nullOptions && (ReadOnlyValPtr<Byte>)options.OptionString is { IsZero: false, } optionString)
			{
#if NET8_0_OR_GREATER
				ref readonly Byte optionsBuffer =
#else
				ref Byte optionsBuffer =
#endif
					ref Unsafe.AsRef(in LibraryBaseTests.Args.Options.GetPinnableReference());
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

			if (LibraryBaseTests.Result != JResult.Ok)
			{
				vmRef = default;
				envRef = default;
				return LibraryBaseTests.Result;
			}

			vmRef = LibraryBaseTests.ProxyEnv.VirtualMachine.Reference;
			envRef = LibraryBaseTests.ProxyEnv.Reference;
			return JResult.Ok;
		}
		catch (Exception ex)
		{
			LibraryBaseTests.NativeException = ex;
			vmRef = default;
			envRef = default;
			return LibraryBaseTests.Result;
		}
	}
	private static JResult GetDefaultVirtualMachineInitArgsForGetDefaultArgumentTest(
		ref VirtualMachineInitArgumentValue initValue)
	{
		if (LibraryBaseTests.Args is null) return JResult.DetachedThreadError;
		try
		{
#if NET8_0_OR_GREATER
			ref readonly VirtualMachineInitOptionValue options =
#else
			ref VirtualMachineInitOptionValue options =
#endif
				ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);
			Boolean nullOptions =
#if NET8_0_OR_GREATER
				Unsafe.IsNullRef(in options);
#else
				Unsafe.IsNullRef(ref options);
#endif

			Assert.InRange(initValue.Version, (Int32)JRuntimeVersion.SEd2,
			               Math.Max(LibraryBaseTests.Args.Version, (Int32)JRuntimeVersion.SEd2));
			Assert.True(nullOptions);
			Assert.Equal(default, initValue.OptionsLength);
			Assert.Equal(default, initValue.IgnoreUnrecognized);

			initValue = new()
			{
				Version = initValue.Version,
				OptionsLength = LibraryBaseTests.Args.Options.Count,
				Options = LibraryBaseTests.OptionsPtr,
				IgnoreUnrecognized = LibraryBaseTests.Args.IgnoreUnrecognized,
			};
			return LibraryBaseTests.Result;
		}
		catch (Exception ex)
		{
			LibraryBaseTests.NativeException = ex;
			return LibraryBaseTests.Result;
		}
	}
	private static JResult GetCreatedJavaVMs(ValPtr<JVirtualMachineRef> vmBuf, Int32 bufLen, out Int32 nVMs)
	{
		if (LibraryBaseTests.Proxies is null)
		{
			nVMs = 0;
			return JResult.DetachedThreadError;
		}
		try
		{
			nVMs = LibraryBaseTests.Proxies.Length;
			LibraryBaseTests.Count++;

			if (LibraryBaseTests.Count == 1)
			{
				Assert.True(Unsafe.IsNullRef(ref vmBuf.Reference));
				Assert.Equal(0, bufLen);
				return JResult.InvalidArgumentsError;
			}

			Span<JVirtualMachineRef> vmRefs = MemoryMarshal.CreateSpan(ref vmBuf.Reference, bufLen);
			Assert.Equal(LibraryBaseTests.Proxies.Length, vmRefs.Length);
			LibraryBaseTests.Proxies.Select(p => p.VirtualMachine.Reference).ToArray().CopyTo(vmRefs);
			nVMs = vmRefs.Length;
			return LibraryBaseTests.Result;
		}
		catch (Exception ex)
		{
			LibraryBaseTests.NativeException = ex;
			nVMs = -1;
			return LibraryBaseTests.Result;
		}
	}
}