[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Test class for <see cref="JVirtualMachineLibrary"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed unsafe partial class JVirtualMachineLibraryTests
{
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

	[SkippableTheory]
	[InlineData(JvmProxyType.NoCreate)]
	[InlineData(JvmProxyType.NoVMs)]
	[InlineData(JvmProxyType.NoCreateAndNoVMs)]
	internal void NullTest(JvmProxyType proxyType)
	{
		String proxyPath = JVirtualMachineLibraryTests.GetProxyPath(proxyType);
		JVirtualMachineLibrary? library = JVirtualMachineLibrary.LoadLibrary(proxyPath);
		if (proxyType.HasFlag(JvmProxyType.NoCreate))
		{
			Assert.Null(library);
			if (!NativeLibrary.TryLoad(proxyPath, out IntPtr handle)) return;
			Assert.Null(JVirtualMachineLibrary.Create(handle));
		}
		else if (proxyType.HasFlag(JvmProxyType.NoVMs))
		{
			if (!NativeLibrary.TryLoad(proxyPath, out IntPtr _))
			{
				Skip.If((library?.Handle).GetValueOrDefault() != IntPtr.Zero, "Invalid native library loading.");
				Assert.Null(library);
				return;
			}

			Assert.NotNull(library);
			Assert.Throws<JniException>(() => library.GetCreatedVirtualMachines());
		}
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
	internal void GetLatestSupportedVersionTest(Int32 jniVersionTest)
	{
		JVirtualMachineLibraryTests.nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		JVirtualMachineLibraryTests.jniVersion = jniVersionTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineLibraryTests.count = 0;

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs = JVirtualMachineLibraryTests
			.GetDefaultVirtualMachineInitArgsForGetLatestSupportedVersionTest;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (JVirtualMachineLibraryTests.jniVersion < 0x00010006)
				Assert.Throws<JavaVersionException>(() => library.GetLatestSupportedVersion());
			else
				Assert.Equal(JVirtualMachineLibraryTests.jniVersion, library.GetLatestSupportedVersion());

			if (JVirtualMachineLibraryTests.nativeException is not null)
				throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
			Assert.Equal(JVirtualMachineLibraryTests.jniVersion switch
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
			}, JVirtualMachineLibraryTests.count);
		}
		finally
		{
			reset();
			JVirtualMachineLibraryTests.CleanUp();
		}
	}

	[SkippableTheory]
	[InlineData(JResult.Ok)]
	[InlineData(JResult.Error)]
	[InlineData(JResult.InvalidArgumentsError)]
	[InlineData(JResult.MemoryError)]
	[InlineData(JResult.VersionError)]
	internal void CreateVirtualMachineTest(JResult resultTest)
	{
		JVirtualMachineLibraryTests.nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		JVirtualMachineLibraryTests.result = resultTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineLibraryTests.args = new(0x00010006)
		{
			Options = JVirtualMachineLibraryTests.CreateOptionsSequence(),
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};

		JVirtualMachineLibraryTests.proxyEnv = NativeInterfaceProxy.CreateProxy();
		CreateVirtualMachineDelegate createVirtualMachine =
			JVirtualMachineLibraryTests.CreateVirtualMachineForCreateVirtualMachineTest;
		try
		{
			reset();
			arrangeInvocation(default, createVirtualMachine.GetUnsafeFuncPtr(), default);

			if (JVirtualMachineLibraryTests.result != JResult.Ok)
			{
				Assert.Equal(JVirtualMachineLibraryTests.result,
				             Assert.Throws<JniException>(() => library.CreateVirtualMachine(
					                                         JVirtualMachineLibraryTests.args, out IEnvironment __))
				                   .Result);
				if (JVirtualMachineLibraryTests.nativeException is not null)
					throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
				return;
			}

			using IInvokedVirtualMachine invoked =
				library.CreateVirtualMachine(JVirtualMachineLibraryTests.args, out IEnvironment env);

			if (JVirtualMachineLibraryTests.nativeException is not null)
				throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
			Assert.Equal(JVirtualMachineLibraryTests.proxyEnv.Reference, env.Reference);
			Assert.Equal(JVirtualMachineLibraryTests.proxyEnv.VirtualMachine.Reference, invoked.Reference);
		}
		finally
		{
			reset();
			JVirtualMachine.RemoveEnvironment(JVirtualMachineLibraryTests.proxyEnv.VirtualMachine.Reference,
			                                  JVirtualMachineLibraryTests.proxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(
				JVirtualMachine.RemoveVirtualMachine(JVirtualMachineLibraryTests.proxyEnv.VirtualMachine.Reference));
			JVirtualMachineLibraryTests.proxyEnv.FinalizeProxy(true);
			JVirtualMachineLibraryTests.CleanUp();
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
	internal void GetDefaultArgumentTest(Int32 jniVersionTest, JResult resultTest = JResult.Ok,
		Boolean useOptions = false)
	{
		JVirtualMachineLibraryTests.nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		JVirtualMachineLibraryTests.jniVersion = jniVersionTest;
		JVirtualMachineLibraryTests.result = resultTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineLibraryTests.args = new(JVirtualMachineLibraryTests.jniVersion)
		{
			Options = useOptions ? JVirtualMachineLibraryTests.CreateOptionsSequence() : CStringSequence.Empty,
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};
		using MemoryHandle seqHandle = JVirtualMachineLibraryTests.args.Options.ToString().AsMemory().Pin();
		Span<VirtualMachineInitOptionValue> optionSpan =
			stackalloc VirtualMachineInitOptionValue[JVirtualMachineLibraryTests.args.Options.Count];
		JVirtualMachineLibraryTests.optionsPtr =
			JVirtualMachineLibraryTests.GetOptionsPtr(optionSpan, JVirtualMachineLibraryTests.args.Options);

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs =
			JVirtualMachineLibraryTests.GetDefaultVirtualMachineInitArgsForGetDefaultArgumentTest;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (JVirtualMachineLibraryTests.result != JResult.Ok)
			{
				Assert.Throws<JniException>(() => library.GetDefaultArgument(JVirtualMachineLibraryTests.jniVersion));
				if (JVirtualMachineLibraryTests.nativeException is not null)
					throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
				return;
			}

			JVirtualMachineInitArg defaultValue = library.GetDefaultArgument(JVirtualMachineLibraryTests.jniVersion);

			if (JVirtualMachineLibraryTests.nativeException is not null)
				throw new AggregateException(JVirtualMachineLibraryTests.nativeException);

			Assert.Equal(
				JVirtualMachineLibraryTests.jniVersion < 0x00010006 ?
					0x00010006 :
					JVirtualMachineLibraryTests.jniVersion, defaultValue.Version);
			Assert.Equal(JVirtualMachineLibraryTests.args.Options.NonEmptyCount, defaultValue.Options.Count);
			Assert.Equal(JVirtualMachineLibraryTests.args.Options.ToString(), defaultValue.Options.ToString());
			Assert.Equal(!useOptions,
			             Object.ReferenceEquals(JVirtualMachineLibraryTests.args.Options, defaultValue.Options));
			Assert.Equal(JVirtualMachineLibraryTests.args.IgnoreUnrecognized, defaultValue.IgnoreUnrecognized);
		}
		finally
		{
			reset();
			JVirtualMachineLibraryTests.CleanUp();
		}
	}

	[SkippableTheory]
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
	internal void GetCreatedJavaVMsTest(JResult resultTest, Int32 createdVms = 0)
	{
		JVirtualMachineLibraryTests.nativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		GetCreatedJavaVMsDelegate getCreatedJavaVMs = JVirtualMachineLibraryTests.GetCreatedJavaVMs;

		JVirtualMachineLibraryTests.proxies = createdVms > 0 ? new NativeInterfaceProxy[createdVms] : [];
		JVirtualMachineLibraryTests.count = 0;
		JVirtualMachineLibraryTests.result = resultTest;

		reset();
		arrangeInvocation(default, default, getCreatedJavaVMs.GetUnsafeFuncPtr());

		for (Int32 i = 0; i < createdVms; i++)
			JVirtualMachineLibraryTests.proxies[i] = NativeInterfaceProxy.CreateProxy();

		try
		{
			if (JVirtualMachineLibraryTests.result != JResult.Ok && createdVms > 0)
			{
				Assert.Equal(JVirtualMachineLibraryTests.result,
				             Assert.Throws<JniException>(() => library.GetCreatedVirtualMachines()).Result);
				if (JVirtualMachineLibraryTests.nativeException is not null)
					throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
				return;
			}

			IVirtualMachine[] vms = library.GetCreatedVirtualMachines();

			if (JVirtualMachineLibraryTests.nativeException is not null)
				throw new AggregateException(JVirtualMachineLibraryTests.nativeException);
			Assert.Equal(JVirtualMachineLibraryTests.proxies.Length, vms.Length);
			Assert.Equal(JVirtualMachineLibraryTests.proxies.Select(p => p.VirtualMachine.Reference),
			             vms.Select(v => v.Reference));
		}
		finally
		{
			reset();
			Assert.Equal(createdVms > 0 ? 2 : 1, JVirtualMachineLibraryTests.count);
			foreach (NativeInterfaceProxy proxyEnvT in JVirtualMachineLibraryTests.proxies)
			{
				JVirtualMachine.RemoveEnvironment(proxyEnvT.VirtualMachine.Reference, proxyEnvT.Reference);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				JVirtualMachine.RemoveVirtualMachine(proxyEnvT.VirtualMachine.Reference);
				proxyEnvT.FinalizeProxy(true);
			}
			JVirtualMachineLibraryTests.CleanUp();
		}
	}
}