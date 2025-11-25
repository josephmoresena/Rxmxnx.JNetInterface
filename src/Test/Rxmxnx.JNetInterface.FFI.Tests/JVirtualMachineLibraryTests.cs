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

			if (JVirtualMachineLibraryTests.jniVersion < (Int32)JRuntimeVersion.SEd2)
				Assert.Throws<JavaVersionException>(() => library.GetLatestSupportedVersion());
			else
				Assert.Equal(JVirtualMachineLibraryTests.jniVersion, library.GetLatestSupportedVersion());

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
			Assert.Equal(JVirtualMachineLibraryTests.jniVersion switch
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
		LibraryBaseTests.NativeException = default;
		JVirtualMachineLibrary? library =
			JVirtualMachineLibrary.LoadLibrary(JVirtualMachineLibraryTests.GetProxyPath(JvmProxyType.Complete));

		Skip.If(library is null, "JVM library not loaded.");

		JVirtualMachineLibraryTests.result = resultTest;

		delegate* unmanaged<FuncPtr<GetDefaultVirtualMachineInitArgsDelegate>, FuncPtr<CreateVirtualMachineDelegate>,
			FuncPtr<GetCreatedJavaVMsDelegate>, void> arrangeInvocation =
				JVirtualMachineLibraryTests.GetProxyMethods(library, out delegate* unmanaged<void> reset);
		JVirtualMachineLibraryTests.args = new((Int32)JRuntimeVersion.J6)
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
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			using IInvokedVirtualMachine invoked =
				library.CreateVirtualMachine(JVirtualMachineLibraryTests.args, out IEnvironment env);

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
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
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			JVirtualMachineInitArg defaultValue = library.GetDefaultArgument(JVirtualMachineLibraryTests.jniVersion);

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);

			Assert.Equal(
				JVirtualMachineLibraryTests.jniVersion < (Int32)JRuntimeVersion.SEd2 ?
					(Int32)JRuntimeVersion.SEd2 :
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
		LibraryBaseTests.NativeException = default;
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
				if (LibraryBaseTests.NativeException is not null)
					throw new AggregateException(LibraryBaseTests.NativeException);
				return;
			}

			IVirtualMachine[] vms = library.GetCreatedVirtualMachines();

			if (LibraryBaseTests.NativeException is not null)
				throw new AggregateException(LibraryBaseTests.NativeException);
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