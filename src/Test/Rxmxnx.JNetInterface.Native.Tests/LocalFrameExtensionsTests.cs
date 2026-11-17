namespace Rxmxnx.JNetInterface.Tests;

/// <summary>
/// Covers <see cref="LocalFrameExtensions"/> through the proxy's <see cref="IEnvironment.WithFrame"/> callbacks.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class LocalFrameExtensionsTests
{
	private const Int32 frameCapacity = 7;

	[Fact]
	internal void StructActionRunsWithAnEmptyObjectSpan()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		LocalFrameExtensionsTests.InvokeCallbacks(env);
		Tracker tracker = new();
		TrackAction action = new(tracker);

		env.WithFrame(LocalFrameExtensionsTests.frameCapacity, action);
		action.WithFrame(env, LocalFrameExtensionsTests.frameCapacity);

		Assert.Equal(2, tracker.Calls);
		Assert.Equal(0, tracker.Length);
		Assert.Same(env, tracker.Environment);
	}
	[Fact]
	internal void ClassActionRunsWithAnEmptyObjectSpan()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		LocalFrameExtensionsTests.InvokeCallbacks(env);
		Tracker tracker = new();

		env.WithFrame(LocalFrameExtensionsTests.frameCapacity, new TrackClassAction(tracker));

		Assert.Equal(1, tracker.Calls);
		Assert.Equal(0, tracker.Length);
		Assert.Same(env, tracker.Environment);
	}
	[Fact]
	internal void StructFunctionReturnsTheCallbackResult()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		LocalFrameExtensionsTests.InvokeCallbacks(env);
		Tracker tracker = new();
		TrackFunction function = new(tracker);

		env.WithFrame(LocalFrameExtensionsTests.frameCapacity, function, out Int32 fromEnvironment);
		function.WithFrame(env, LocalFrameExtensionsTests.frameCapacity, out Int32 fromFunction);

		Assert.Equal(TrackFunction.Result, fromEnvironment);
		Assert.Equal(TrackFunction.Result, fromFunction);
		Assert.Equal(2, tracker.Calls);
		Assert.Equal(0, tracker.Length);
		Assert.Same(env, tracker.Environment);
	}
	[Fact]
	internal void ClassFunctionReturnsTheCallbackResult()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		LocalFrameExtensionsTests.InvokeCallbacks(env);
		Tracker tracker = new();

		env.WithFrame(LocalFrameExtensionsTests.frameCapacity, new TrackClassFunction(tracker), out Int32 result);

		Assert.Equal(TrackFunction.Result, result);
		Assert.Equal(1, tracker.Calls);
		Assert.Equal(0, tracker.Length);
		Assert.Same(env, tracker.Environment);
	}
	[Fact]
	internal void NullEnvironmentOrActionDoesNotRunTheCallback()
	{
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		LocalFrameExtensionsTests.InvokeCallbacks(env);
		Tracker tracker = new();
		TrackAction action = new(tracker);
		TrackFunction function = new(tracker);
		IEnvironment? missing = null;

		missing.WithFrame(LocalFrameExtensionsTests.frameCapacity, action);
		env.WithFrame(LocalFrameExtensionsTests.frameCapacity, (TrackClassAction?)null);
		action.WithFrame(missing, LocalFrameExtensionsTests.frameCapacity);
		missing.WithFrame(LocalFrameExtensionsTests.frameCapacity, function, out Int32 _);
		env.WithFrame<TrackClassFunction, Int32>(LocalFrameExtensionsTests.frameCapacity, null, out Int32 _);
		function.WithFrame(missing, LocalFrameExtensionsTests.frameCapacity, out Int32 _);

		Assert.Equal(0, tracker.Calls);
		env.DidNotReceiveWithAnyArgs().WithFrame(default, Arg.Any<ValueTuple<IntPtr, Type>>(),
		                                         Arg.Any<Action<ValueTuple<IntPtr, Type>>>());
	}

	private static void InvokeCallbacks(EnvironmentProxy env)
	{
		env.When(e => e.WithFrame(Arg.Any<Int32>(), Arg.Any<ValueTuple<IntPtr, Type>>(),
		                          Arg.Any<Action<ValueTuple<IntPtr, Type>>>()))
		   .Do(call => call.Arg<Action<ValueTuple<IntPtr, Type>>>()(call.Arg<ValueTuple<IntPtr, Type>>()));
		env.WithFrame(Arg.Any<Int32>(), Arg.Any<ValueTuple<IntPtr, Type>>(),
		              Arg.Any<Func<ValueTuple<IntPtr, Type>, Int32>>())
		   .Returns(call => call.Arg<Func<ValueTuple<IntPtr, Type>, Int32>>()(call.Arg<ValueTuple<IntPtr, Type>>()));
	}

	private sealed class Tracker
	{
		public Int32 Calls { get; set; }
		public Int32 Length { get; set; } = -1;
		public IEnvironment? Environment { get; set; }
	}

	private readonly struct TrackAction(Tracker tracker) : IFrameAction
	{
		public void Accept(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			tracker.Calls++;
			tracker.Length = objects.Length;
			tracker.Environment = env;
		}
	}

	private sealed class TrackClassAction(Tracker tracker) : IFrameAction
	{
		public void Accept(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			tracker.Calls++;
			tracker.Length = objects.Length;
			tracker.Environment = env;
		}
	}

	private readonly struct TrackFunction(Tracker tracker) : IFrameFunction<Int32>
	{
		public const Int32 Result = 42;

		public Int32 Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			tracker.Calls++;
			tracker.Length = objects.Length;
			tracker.Environment = env;
			return TrackFunction.Result;
		}
	}

	private sealed class TrackClassFunction(Tracker tracker) : IFrameFunction<Int32>
	{
		public Int32 Apply(IEnvironment env, ReadOnlySpan<JReferenceObject?> objects)
		{
			tracker.Calls++;
			tracker.Length = objects.Length;
			tracker.Environment = env;
			return TrackFunction.Result;
		}
	}
}