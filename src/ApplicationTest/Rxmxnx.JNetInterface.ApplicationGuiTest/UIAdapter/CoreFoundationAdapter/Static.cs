using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class UIAdapter
{
	private sealed partial class CoreFoundationAdapter
	{
		[ThreadStatic]
		private static IntPtr loopRef;
		[ThreadStatic]
		private static IntPtr sourceRef;

		[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
		               EntryPoint = "CFRunLoopSourceCreate")]
		private static partial IntPtr RunLoopSourceCreate(IntPtr allocator, IntPtr order,
			ref RunLoopSourceContext context);
		[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
		               EntryPoint = "CFRunLoopAddSource")]
		private static partial void RunLoopAddSource(IntPtr runLoop, IntPtr source, IntPtr mode);
		[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
		               EntryPoint = "CFRunLoopRun")]
		private static partial void RunLoopRun();
		[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", EntryPoint = "CFRelease")]
		private static partial void ReleaseSource(IntPtr sourceRef);

		private static unsafe IntPtr GetCommonModes()
		{
			IntPtr handle = NativeLibrary.Load("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation");
			NativeLibrary.TryGetExport(handle, "kCFRunLoopCommonModes", out IntPtr symbol);
			return Unsafe.AsRef<IntPtr>(symbol.ToPointer());
		}

		[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
		               EntryPoint = "CFRunLoopGetCurrent")]
		private static partial IntPtr RunLoopGetCurrent();

		[UnmanagedCallersOnly]
		private static void DummyCallback(IntPtr _) { }
	}
}