namespace Rxmxnx.JNetInterface.Tests.Internal;

/// <summary>
/// Stores a managed exception thrown from an <see cref="System.Runtime.InteropServices.UnmanagedCallersOnlyAttribute"/>
/// JNI entry so <see cref="JniCallBoundary.ThrowIfPending"/> can throw it after the call returns.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class JniUnmanagedFailure
{
	/// <summary>
	/// Stores <paramref name="exception"/> in <see cref="JniCallBoundary.Pending"/>.
	/// </summary>
	/// <param name="exception">Exception captured at the unmanaged entry.</param>
	public static void Capture(Exception exception) => JniCallBoundary.Pending ??= exception;
}