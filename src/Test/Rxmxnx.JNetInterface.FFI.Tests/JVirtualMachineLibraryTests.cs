[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Test class for <see cref="JVirtualMachineLibrary"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed unsafe class JVirtualMachineLibraryTests
{
	private const String NativeLibraryName = "JvmProxy";

	private static readonly String systemLibraryName = OperatingSystem.IsWindows() ? "user32" :
		OperatingSystem.IsMacOS() ? "/usr/lib/libSystem.B.dylib" : "libc";
	private static readonly String libraryPrefix = OperatingSystem.IsWindows() ? String.Empty : "lib";
	private static readonly String nativeExtension = OperatingSystem.IsWindows() ? ".dll" :
		OperatingSystem.IsMacOS() ? ".dylib" : ".so";
	private static readonly IFixture fixture = new Fixture().RegisterReferences();

	[SkippableFact]
	internal void SuccessTest()
	{
		String proxyPath = JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete);
		JVirtualMachineLibrary? library = JVirtualMachineLibrary.LoadLibrary(proxyPath);
		if (!NativeLibrary.TryLoad(proxyPath, out IntPtr handle))
		{
			Skip.If((library?.Handle).GetValueOrDefault() != IntPtr.Zero, "Invalid native library loading.");
			Assert.Null(library);
			return;
		}

		Assert.NotNull(library);
		Assert.Equal(handle, library.Handle);
		Assert.Equal(library.Handle, JVirtualMachineLibrary.Create(handle)?.Handle);
	}

	[Theory]
	[InlineData(JvmProxyType.NoCreate)]
	[InlineData(JvmProxyType.NoVMs)]
	[InlineData(JvmProxyType.NoCreateAndNoVMs)]
	internal void NullTest(JvmProxyType proxyType)
	{
		String proxyPath = JVirtualMachineLibraryTests.GetProxyPath(proxyType);
		JVirtualMachineLibrary? library = JVirtualMachineLibrary.LoadLibrary(proxyPath);
		Assert.Null(library);
		if (!NativeLibrary.TryLoad(proxyPath, out IntPtr handle)) return;
		Assert.Null(JVirtualMachineLibrary.Create(handle));
	}

	[Fact]
	internal void InvalidLibraryTest()
	{
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.systemLibraryName);
		Assert.Null(library);
	}

	[SkippableTheory]
	[InlineData(0x00010001)]
	[InlineData(0x00010002)]
	[InlineData(0x00010003)]
	[InlineData(0x00010004)]
	[InlineData(0x00010005)]
	[InlineData(0x00010006)]
	[InlineData(0x00010008)]
	[InlineData(0x00090000)]
	[InlineData(0x000a0000)]
	[InlineData(0x00130000)]
	[InlineData(0x00140000)]
	[InlineData(0x00150000)]
	[InlineData(0x00180000)]
	internal void GetLatestSupportedVersionTest(Int32 jniVersion)
	{
		Exception? nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		Int32 count = 0;

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs = GetDefaultVirtualMachineInitArgs;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (jniVersion < 0x00010006)
				Assert.Throws<JavaVersionException>(() => library.GetLatestSupportedVersion());
			else
				Assert.Equal(jniVersion, library.GetLatestSupportedVersion());

			if (nativeException is not null)
				throw new AggregateException(nativeException);
			Assert.Equal(jniVersion switch
			{
				0x00010006 => 2,
				0x00010008 => 3,
				0x00090000 => 4,
				0x000a0000 => 5,
				0x00130000 => 6,
				0x00140000 => 7,
				0x00150000 => 8,
				0x00180000 => 8,
				_ => 1,
			}, count);
		}
		finally
		{
			reset();
		}
		return;

		JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initValue)
		{
			try
			{
				ref readonly VirtualMachineInitOptionValue options =
					ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);

				if (count == 0)
					Assert.Equal(0x00010006, initValue.Version);
				count++;
				Assert.True(Unsafe.IsNullRef(in options));
				Assert.Equal(default, initValue.OptionsLength);
				Assert.Equal(default, initValue.IgnoreUnrecognized);

				if (initValue.Version > jniVersion)
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
				nativeException = ex;
				return JResult.Ok;
			}
		}
	}

	[SkippableTheory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.InvalidArgumentsError)]
	[InlineData(JResult.MemoryError)]
	[InlineData(JResult.VersionError)]
	internal void CreateVirtualMachineTest(JResult result)
	{
		Exception? nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineInitArg args = new(0x00010006)
		{
			Options = JVirtualMachineLibraryTests.CreateOptionsSequence(),
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};

		NativeInterfaceProxy proxyEnv = NativeInterfaceProxy.CreateProxy();
		CreateVirtualMachineDelegate createVirtualMachine = CreateVirtualMachine;
		try
		{
			reset();
			arrangeInvocation(default, createVirtualMachine.GetUnsafeFuncPtr(), default);

			if (result != JResult.Ok)
			{
				Assert.Equal(result,
				             Assert.Throws<JniException>(() => library.CreateVirtualMachine(args, out IEnvironment __))
				                   .Result);
				if (nativeException is not null)
					throw new AggregateException(nativeException);
				return;
			}

			using IInvokedVirtualMachine invoked = library.CreateVirtualMachine(args, out IEnvironment env);

			if (nativeException is not null)
				throw new AggregateException(nativeException);
			Assert.Equal(proxyEnv.Reference, env.Reference);
			Assert.Equal(proxyEnv.VirtualMachine.Reference, invoked.Reference);
		}
		finally
		{
			reset();
			JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference));
			proxyEnv.FinalizeProxy(true);
		}
		return;

		JResult CreateVirtualMachine(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
			in VirtualMachineInitArgumentValue value)
		{
			try
			{
				ref readonly VirtualMachineInitOptionValue options =
					ref Unsafe.AsRef<VirtualMachineInitOptionValue>(value.Options);

				Assert.Equal(0x00010006, value.Version);
				Assert.Equal(args.Options.NonEmptyCount, value.OptionsLength);
				Assert.Equal(args.IgnoreUnrecognized, value.IgnoreUnrecognized);
				if (!options.OptionString.IsZero)
					Assert.True(Unsafe.AreSame(in args.Options.GetPinnableReference(),
					                           in options.OptionString.Reference));
				else
					Assert.Equal(0, value.OptionsLength);

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
				nativeException = ex;
				vmRef = default;
				envRef = default;
				return result;
			}
		}
	}

	[SkippableTheory]
	[InlineData(0x00010001)]
	[InlineData(0x00010002)]
	[InlineData(0x00010003)]
	[InlineData(0x00010004)]
	[InlineData(0x00010005)]
	[InlineData(0x00010006)]
	[InlineData(0x00010008)]
	[InlineData(0x00090000)]
	[InlineData(0x000a0000)]
	[InlineData(0x00130000)]
	[InlineData(0x00140000)]
	[InlineData(0x00150000)]
	[InlineData(0x00180000)]
	[InlineData(0x00010001, JResult.Error)]
	[InlineData(0x00010002, JResult.Error)]
	[InlineData(0x00010003, JResult.Error)]
	[InlineData(0x00010004, JResult.Error)]
	[InlineData(0x00010005, JResult.Error)]
	[InlineData(0x00010006, JResult.Error)]
	[InlineData(0x00010008, JResult.Error)]
	[InlineData(0x00090000, JResult.Error)]
	[InlineData(0x000a0000, JResult.Error)]
	[InlineData(0x00130000, JResult.Error)]
	[InlineData(0x00140000, JResult.Error)]
	[InlineData(0x00150000, JResult.Error)]
	[InlineData(0x00180000, JResult.Error)]
	[InlineData(0x00010001, JResult.Ok, true)]
	[InlineData(0x00010002, JResult.Ok, true)]
	[InlineData(0x00010003, JResult.Ok, true)]
	[InlineData(0x00010004, JResult.Ok, true)]
	[InlineData(0x00010005, JResult.Ok, true)]
	[InlineData(0x00010006, JResult.Ok, true)]
	[InlineData(0x00010008, JResult.Ok, true)]
	[InlineData(0x00090000, JResult.Ok, true)]
	[InlineData(0x000a0000, JResult.Ok, true)]
	[InlineData(0x00130000, JResult.Ok, true)]
	[InlineData(0x00140000, JResult.Ok, true)]
	[InlineData(0x00150000, JResult.Ok, true)]
	[InlineData(0x00180000, JResult.Ok, true)]
	internal void GetDefaultArgumentTest(Int32 jniVersion, JResult result = JResult.Ok, Boolean useOptions = false)
	{
		Exception? nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineInitArg args = new(jniVersion)
		{
			Options = useOptions ? JVirtualMachineLibraryTests.CreateOptionsSequence() : CStringSequence.Empty,
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};
		using MemoryHandle emptyHandle = CString.Empty.TryPin(out _);
		using MemoryHandle seqHandle = args.Options.ToString().AsMemory().Pin();
		Span<VirtualMachineInitOptionValue> optionSpan = stackalloc VirtualMachineInitOptionValue[args.Options.Count];
		ReadOnlyValPtr<VirtualMachineInitOptionValue> optionsPtr =
			JVirtualMachineLibraryTests.GetOptionsPtr(optionSpan, args.Options);

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs = GetDefaultVirtualMachineInitArgs;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (result != JResult.Ok)
			{
				Assert.Throws<JniException>(() => library.GetDefaultArgument(jniVersion));
				if (nativeException is not null)
					throw new AggregateException(nativeException);
				return;
			}

			JVirtualMachineInitArg defaultValue = library.GetDefaultArgument(jniVersion);

			if (nativeException is not null)
				throw new AggregateException(nativeException);

			Assert.Equal(jniVersion < 0x00010006 ? 0x00010006 : jniVersion, defaultValue.Version);
			Assert.Equal(args.Options.NonEmptyCount, defaultValue.Options.Count);
			Assert.Equal(args.Options.ToString(), defaultValue.Options.ToString());
			Assert.Equal(!useOptions, Object.ReferenceEquals(args.Options, defaultValue.Options));
			Assert.Equal(args.IgnoreUnrecognized, defaultValue.IgnoreUnrecognized);
		}
		finally
		{
			reset();
		}
		return;

		JResult GetDefaultVirtualMachineInitArgs(ref VirtualMachineInitArgumentValue initValue)
		{
			try
			{
				ref readonly VirtualMachineInitOptionValue options =
					ref Unsafe.AsRef<VirtualMachineInitOptionValue>(initValue.Options);

				Assert.InRange(initValue.Version, 0x00010006, Math.Max(args.Version, 0x00010006));
				Assert.True(Unsafe.IsNullRef(in options));
				Assert.Equal(default, initValue.OptionsLength);
				Assert.Equal(default, initValue.IgnoreUnrecognized);

				initValue = new()
				{
					Version = initValue.Version,
					OptionsLength = args.Options.Count,
					Options = optionsPtr,
					IgnoreUnrecognized = args.IgnoreUnrecognized,
				};
				return result;
			}
			catch (Exception ex)
			{
				nativeException = ex;
				return result;
			}
		}
	}

	[Theory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.Ok, 1)]
	[InlineData(JResult.Error, 1)]
	[InlineData(JResult.Ok, 2)]
	[InlineData(JResult.Error, 2)]
	[InlineData(JResult.Ok, 3)]
	[InlineData(JResult.Error, 3)]
	[InlineData(JResult.Ok, -1)]
	[InlineData(JResult.Error, -1)]
	internal void GetCreatedJavaVMsTest(JResult result, Int32 createdVms = 0)
	{
		Exception? nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		NativeInterfaceProxy[] proxies = createdVms > 0 ? new NativeInterfaceProxy[createdVms] : [];
		Int32 count = 0;
		GetCreatedJavaVMsDelegate getCreatedJavaVMs = GetCreatedJavaVMs;

		reset();
		arrangeInvocation(default, default, getCreatedJavaVMs.GetUnsafeFuncPtr());

		for (Int32 i = 0; i < createdVms; i++)
			proxies[i] = NativeInterfaceProxy.CreateProxy();

		try
		{
			if (result != JResult.Ok && createdVms > 0)
			{
				Assert.Equal(result, Assert.Throws<JniException>(() => library.GetCreatedVirtualMachines()).Result);
				if (nativeException is not null)
					throw new AggregateException(nativeException);
				return;
			}

			IVirtualMachine[] vms = library.GetCreatedVirtualMachines();

			if (nativeException is not null)
				throw new AggregateException(nativeException);
			Assert.Equal(proxies.Length, vms.Length);
			Assert.Equal(proxies.Select(p => p.VirtualMachine.Reference), vms.Select(v => v.Reference));
		}
		finally
		{
			reset();
			Assert.Equal(createdVms > 0 ? 2 : 1, count);
			foreach (NativeInterfaceProxy proxyEnv in proxies)
			{
				JVirtualMachine.RemoveEnvironment(proxyEnv.VirtualMachine.Reference, proxyEnv.Reference);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				JVirtualMachine.RemoveVirtualMachine(proxyEnv.VirtualMachine.Reference);
				proxyEnv.FinalizeProxy(true);
			}
		}

		return;

		JResult GetCreatedJavaVMs(ValPtr<JVirtualMachineRef> vmBuf, Int32 bufLen, out Int32 nVMs)
		{
			try
			{
				nVMs = proxies.Length;
				count++;

				if (count == 1)
				{
					Assert.True(Unsafe.IsNullRef(ref vmBuf.Reference));
					Assert.Equal(0, bufLen);
					return JResult.InvalidArgumentsError;
				}

				Span<JVirtualMachineRef> vmRefs = MemoryMarshal.CreateSpan(ref vmBuf.Reference, bufLen);
				Assert.Equal(proxies.Length, vmRefs.Length);
				proxies.Select(p => p.VirtualMachine.Reference).ToArray().CopyTo(vmRefs);
				nVMs = vmRefs.Length;
				return result;
			}
			catch (Exception ex)
			{
				nativeException = ex;
				nVMs = -1;
				return result;
			}
		}
	}

	private static String GetProxyPath(JvmProxyType proxyType)
	{
		StringBuilder fileNameBuilder = new(50);
		fileNameBuilder.Append(JVirtualMachineLibraryTests.libraryPrefix);
		fileNameBuilder.Append(JVirtualMachineLibraryTests.NativeLibraryName);
		if (proxyType is not JvmProxyType.Complete) fileNameBuilder.Append($".{proxyType}");
		fileNameBuilder.Append($".{RuntimeInformation.ProcessArchitecture}".ToLowerInvariant());
		fileNameBuilder.Append(JVirtualMachineLibraryTests.nativeExtension);
		return fileNameBuilder.ToString();
	}
	private static CStringSequence CreateOptionsSequence()
	{
		Span<Int32> sizes = stackalloc Int32[4];
		sizes[0] = Random.Shared.Next(0, 5);
		sizes[1] = Random.Shared.Next(0, 5);
		sizes[2] = Random.Shared.Next(0, 5);
		sizes[3] = Random.Shared.Next(0, 5);
		return new(JVirtualMachineLibraryTests.fixture.CreateMany<String>(sizes[0])
		                                      .Concat(Enumerable.Repeat(String.Empty, sizes[1]))
		                                      .Concat(JVirtualMachineLibraryTests.fixture.CreateMany<String>(sizes[2]))
		                                      .Concat(Enumerable.Repeat(String.Empty, sizes[3])));
	}
	private static ReadOnlyValPtr<VirtualMachineInitOptionValue> GetOptionsPtr(
		Span<VirtualMachineInitOptionValue> optionsAlloc, CStringSequence options)
	{
		Int32 index = 0;
		foreach (ReadOnlySpan<Byte> value in options.CreateView())
		{
			optionsAlloc[index] = new(value.GetUnsafeValPtr());
			index++;
		}
		return optionsAlloc.GetUnsafeValPtr();
	}
	private static delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>,
		FuncPtr<CreateVirtualMachineDelegate>, FuncPtr<GetCreatedJavaVMsDelegate>, void> GetProxyMethods(
			JVirtualMachineLibrary library, out delegate* unmanaged<void> reset)
	{
		reset = (delegate* unmanaged<void>)NativeLibrary.GetExport(library.Handle, "Reset");
		return (delegate* unmanaged< FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>,
			FuncPtr<CreateVirtualMachineDelegate>, FuncPtr<GetCreatedJavaVMsDelegate>, void>)NativeLibrary.GetExport(
			library.Handle, "ArrangeInvocation");
	}

	[Flags]
	internal enum JvmProxyType : Byte
	{
		Complete,
		NoCreate = 0x1,
		NoVMs = 0x2,
		NoCreateAndNoVMs = JvmProxyType.NoCreate | JvmProxyType.NoVMs,
	}

	private delegate JResult GetDefaultVirtualMachineInitArgsDelegate(ref VirtualMachineInitArgumentValue initValue);

	private delegate JResult CreateVirtualMachineDelegate(out JVirtualMachineRef vmRef, out JEnvironmentRef envRef,
		in VirtualMachineInitArgumentValue value);

	private delegate JResult GetCreatedJavaVMsDelegate(ValPtr<JVirtualMachineRef> vmBuf, Int32 bufLen, out Int32 nVMs);
}