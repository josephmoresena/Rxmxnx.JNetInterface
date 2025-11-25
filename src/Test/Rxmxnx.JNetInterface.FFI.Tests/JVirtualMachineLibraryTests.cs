[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Test class for <see cref="JVirtualMachineLibrary"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed unsafe partial class JVirtualMachineLibraryTests : LibraryBaseTests
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
	internal void GetLatestSupportedVersionTest(Int32 jniVersionTest)
	{
		LibraryBaseTests.NativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		LibraryBaseTests.JniVersion = jniVersionTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		LibraryBaseTests.Count = 0;

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs = JVirtualMachineLibraryTests
			.GetDefaultVirtualMachineInitArgsForGetLatestSupportedVersionTest;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (LibraryBaseTests.JniVersion < (Int32)JRuntimeVersion.SEd2)
				Assert.Throws<JavaVersionException>(() => library.GetLatestSupportedVersion());
			else
				Assert.Equal(LibraryBaseTests.JniVersion, library.GetLatestSupportedVersion());

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(LibraryBaseTests.JniVersion switch
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
			}, LibraryBaseTests.Count);
		}
		finally
		{
			reset();
			LibraryBaseTests.CleanUp();
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
		LibraryBaseTests.NativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		LibraryBaseTests.Result = resultTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		LibraryBaseTests.Args = new((Int32)JRuntimeVersion.J6)
		{
			Options = JVirtualMachineLibraryTests.CreateOptionsSequence(),
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};

		LibraryBaseTests.ProxyEnv = NativeInterfaceProxy.CreateProxy();
		CreateVirtualMachineDelegate createVirtualMachine =
			JVirtualMachineLibraryTests.CreateVirtualMachineForCreateVirtualMachineTest;
		try
		{
			reset();
			arrangeInvocation(default, createVirtualMachine.GetUnsafeFuncPtr(), default);

			if (LibraryBaseTests.Result != JResult.Ok)
			{
				Assert.Equal(LibraryBaseTests.Result,
				             Assert.Throws<JniException>(() => library.CreateVirtualMachine(
					                                         LibraryBaseTests.Args, out IEnvironment __)).Result);
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			using IInvokedVirtualMachine invoked =
				library.CreateVirtualMachine(LibraryBaseTests.Args, out IEnvironment env);

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(LibraryBaseTests.ProxyEnv.Reference, env.Reference);
			Assert.Equal(LibraryBaseTests.ProxyEnv.VirtualMachine.Reference, invoked.Reference);
		}
		finally
		{
			reset();
			JVirtualMachine.RemoveEnvironment(LibraryBaseTests.ProxyEnv.VirtualMachine.Reference,
			                                  LibraryBaseTests.ProxyEnv.Reference);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Assert.False(JVirtualMachine.RemoveVirtualMachine(LibraryBaseTests.ProxyEnv.VirtualMachine.Reference));
			LibraryBaseTests.ProxyEnv.FinalizeProxy(true);
			LibraryBaseTests.CleanUp();
		}
	}

	[SkippableTheory]
	[InlineData((Int32)JRuntimeVersion.SEd1)]
	[InlineData((Int32)JRuntimeVersion.SEd2)]
	[InlineData((Int32)JRuntimeVersion.SEd3)]
	[InlineData((Int32)JRuntimeVersion.SEd4)]
	[InlineData((Int32)JRuntimeVersion.J5)]
	[InlineData((Int32)JRuntimeVersion.J6)]
	[InlineData((Int32)JRuntimeVersion.J8)]
	[InlineData((Int32)JRuntimeVersion.J9)]
	[InlineData((Int32)JRuntimeVersion.J10)]
	[InlineData((Int32)JRuntimeVersion.J19)]
	[InlineData((Int32)JRuntimeVersion.J20)]
	[InlineData((Int32)JRuntimeVersion.J21)]
	[InlineData((Int32)JRuntimeVersion.J24)]
	[InlineData((Int32)JRuntimeVersion.SEd1, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.SEd2, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.SEd3, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.SEd4, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J5, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J6, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J8, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J9, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J10, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J19, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J20, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J21, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.J24, JResult.Error)]
	[InlineData((Int32)JRuntimeVersion.SEd1, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.SEd2, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.SEd3, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.SEd4, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J5, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J6, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J8, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J9, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J10, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J19, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J20, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J21, JResult.Ok, true)]
	[InlineData((Int32)JRuntimeVersion.J24, JResult.Ok, true)]
	internal void GetDefaultArgumentTest(Int32 jniVersionTest, JResult resultTest = JResult.Ok,
		Boolean useOptions = false)
	{
		LibraryBaseTests.NativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		LibraryBaseTests.JniVersion = jniVersionTest;
		LibraryBaseTests.Result = resultTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		LibraryBaseTests.Args = new(LibraryBaseTests.JniVersion)
		{
			Options = useOptions ? JVirtualMachineLibraryTests.CreateOptionsSequence() : CStringSequence.Empty,
			IgnoreUnrecognized = JVirtualMachineLibraryTests.fixture.Create<Boolean>(),
		};
		using MemoryHandle seqHandle = LibraryBaseTests.Args.Options.ToString().AsMemory().Pin();
		Span<VirtualMachineInitOptionValue> optionSpan =
			stackalloc VirtualMachineInitOptionValue[LibraryBaseTests.Args.Options.Count];
		LibraryBaseTests.OptionsPtr =
			JVirtualMachineLibraryTests.GetOptionsPtr(optionSpan, LibraryBaseTests.Args.Options);

		GetDefaultVirtualMachineInitArgsDelegate getDefaultVirtualMachineInitArgs =
			JVirtualMachineLibraryTests.GetDefaultVirtualMachineInitArgsForGetDefaultArgumentTest;
		try
		{
			reset();
			arrangeInvocation(getDefaultVirtualMachineInitArgs.GetUnsafeFuncPtr(), default, default);

			if (LibraryBaseTests.Result != JResult.Ok)
			{
				Assert.Throws<JniException>(() => library.GetDefaultArgument(LibraryBaseTests.JniVersion));
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			JVirtualMachineInitArg defaultValue = library.GetDefaultArgument(LibraryBaseTests.JniVersion);

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);

			Assert.Equal(
				LibraryBaseTests.JniVersion < (Int32)JRuntimeVersion.SEd2 ?
					(Int32)JRuntimeVersion.SEd2 :
					LibraryBaseTests.JniVersion, defaultValue.Version);
			Assert.Equal(LibraryBaseTests.Args.Options.NonEmptyCount, defaultValue.Options.Count);
			Assert.Equal(LibraryBaseTests.Args.Options.ToString(), defaultValue.Options.ToString());
			Assert.Equal(!useOptions, Object.ReferenceEquals(LibraryBaseTests.Args.Options, defaultValue.Options));
			Assert.Equal(LibraryBaseTests.Args.IgnoreUnrecognized, defaultValue.IgnoreUnrecognized);
		}
		finally
		{
			reset();
			LibraryBaseTests.CleanUp();
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
		LibraryBaseTests.NativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		GetCreatedJavaVMsDelegate getCreatedJavaVMs = JVirtualMachineLibraryTests.GetCreatedJavaVMs;

		LibraryBaseTests.Proxies = createdVms > 0 ? new NativeInterfaceProxy[createdVms] : [];
		LibraryBaseTests.Count = 0;
		LibraryBaseTests.Result = resultTest;

		reset();
		arrangeInvocation(default, default, getCreatedJavaVMs.GetUnsafeFuncPtr());

		for (Int32 i = 0; i < createdVms; i++)
			LibraryBaseTests.Proxies[i] = NativeInterfaceProxy.CreateProxy();

		try
		{
			if (LibraryBaseTests.Result != JResult.Ok && createdVms > 0)
			{
				Assert.Equal(LibraryBaseTests.Result,
				             Assert.Throws<JniException>(() => library.GetCreatedVirtualMachines()).Result);
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			IVirtualMachine[] vms = library.GetCreatedVirtualMachines();

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(LibraryBaseTests.Proxies.Length, vms.Length);
			Assert.Equal(LibraryBaseTests.Proxies.Select(p => p.VirtualMachine.Reference),
			             vms.Select(v => v.Reference));
		}
		finally
		{
			reset();
			Assert.Equal(createdVms > 0 ? 2 : 1, LibraryBaseTests.Count);
			foreach (NativeInterfaceProxy proxyEnvT in LibraryBaseTests.Proxies)
			{
				JVirtualMachine.RemoveEnvironment(proxyEnvT.VirtualMachine.Reference, proxyEnvT.Reference);
				GC.Collect();
				GC.WaitForPendingFinalizers();
				JVirtualMachine.RemoveVirtualMachine(proxyEnvT.VirtualMachine.Reference);
				proxyEnvT.FinalizeProxy(true);
			}
			LibraryBaseTests.CleanUp();
		}
	}
}