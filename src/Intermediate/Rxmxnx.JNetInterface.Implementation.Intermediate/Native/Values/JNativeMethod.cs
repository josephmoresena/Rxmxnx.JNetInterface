﻿namespace Rxmxnx.JNetInterface.Native.Values;

internal readonly struct JNativeMethod
{
	internal IntPtr Name { get; init; }
	internal IntPtr Signature { get; init; }
	internal IntPtr Pointer { get; init; }
}