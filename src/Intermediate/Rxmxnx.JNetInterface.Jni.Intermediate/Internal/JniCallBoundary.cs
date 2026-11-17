#if !PACKAGE
using System.Runtime.ExceptionServices;

namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Slot for a managed exception captured at an unmanaged JNI entry during tests.
/// </summary>
internal static class JniCallBoundary
{
	[ThreadStatic]
	internal static Exception? Pending;

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowIfPending()
	{
		Exception? pending = JniCallBoundary.Pending;
		if (pending is null) return;
		JniCallBoundary.Pending = default;
		ExceptionDispatchInfo.Capture(pending).Throw();
	}
}
#endif