namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Helper class to use non-generic function pointers.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class NonGenericFunctionHelper
{
	/// <summary>
	/// Calls to <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void GetField(void* funcPtr, JEnvironmentRef envRef, IntPtr receiver, JFieldId fieldId,
		Int32 fieldSize, void* fieldPtr)
	{
		switch (fieldSize)
		{
			case 1:
				Unsafe.AsRef<Byte>(fieldPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Byte>)funcPtr)(envRef, receiver, fieldId);
				break;
			case 2:
				Unsafe.AsRef<UInt16>(fieldPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt16>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case 4:
				Unsafe.AsRef<UInt32>(fieldPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt32>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case 8:
				Unsafe.AsRef<UInt64>(fieldPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt64>)funcPtr)(
						envRef, receiver, fieldId);
				break;
		}
	}
	/// <summary>
	/// Calls <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void SetField(void* funcPtr, JEnvironmentRef envRef, IntPtr receiver, JFieldId fieldId,
		Int32 fieldSize, void* valPtr)
	{
		switch (fieldSize)
		{
			case 1:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Byte, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.AsRef<Byte>(valPtr));
				break;
			case 2:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt16, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.AsRef<UInt16>(valPtr));
				break;
			case 4:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt32, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.AsRef<UInt32>(valPtr));
				break;
			case 8:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt64, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.AsRef<UInt64>(valPtr));
				break;
		}
	}
	/// <summary>
	/// Calls CallNonVirtualMethod<c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static unsafe void CallNonVirtualMethod(void* funcPtr, JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args, Int32 valSize, void* valPtr)
	{
		switch (valSize)
		{
			case 1:
				Unsafe.AsRef<Byte>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Byte>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case 2:
				Unsafe.AsRef<UInt16>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, UInt16>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case 4:
				Unsafe.AsRef<UInt32>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, UInt32>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case 8:
				Unsafe.AsRef<UInt64>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, UInt64>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
		}
	}
	/// <summary>
	/// Calls CallMethod<c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static unsafe void CallMethod(void* funcPtr, JEnvironmentRef envRef, IntPtr receiver, JMethodId methodId,
		JValue* args, Int32 valSize, void* valPtr)
	{
		switch (valSize)
		{
			case 1:
				Unsafe.AsRef<Byte>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Byte>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case 2:
				Unsafe.AsRef<UInt16>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt16>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case 4:
				Unsafe.AsRef<UInt32>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt32>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case 8:
				Unsafe.AsRef<UInt64>(valPtr) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt64>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
		}
	}
}