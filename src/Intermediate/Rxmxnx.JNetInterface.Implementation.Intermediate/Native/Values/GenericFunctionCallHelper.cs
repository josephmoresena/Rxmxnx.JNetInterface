namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Helper class to use function pointers in generic function calls.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class GenericFunctionCallHelper
{
	/// <summary>
	/// Indicates whether generic JNI calls should be done with managed function pointers.
	/// </summary>
	public static readonly Boolean UseManagedGenericPointers =
		RuntimeInformation.ProcessArchitecture is Architecture.Arm;

	/// <summary>
	/// Calls Call&lt;type&gt;Method<c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void CallMethod(void* funcPtr, JNativeType resultType, JEnvironmentRef envRef, IntPtr receiver,
		JMethodId methodId, JValue* args, ref Byte refValue)
	{
		switch (resultType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, JBoolean>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JBoolean>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, JByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JByte>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, JChar>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JChar>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, JDouble>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JDouble>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, JFloat>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JFloat>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, JFloat>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JInt>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, JLong>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JLong>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, JShort>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JShort>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			default:
				Unsafe.As<Byte, JObjectLocalRef>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, JObjectLocalRef>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
		}
	}
	/// <summary>
	/// Calls CallNonVirtual&lt;type&gt;Method<c>A</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void CallNonVirtualMethod(void* funcPtr, JNativeType resultType, JEnvironmentRef envRef,
		JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId, JValue* args, ref Byte refValue)
	{
		switch (resultType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, JBoolean>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JBoolean
						>)funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, JByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JByte>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, JChar>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JChar>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, JDouble>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JDouble
						>)funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, JFloat>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JFloat>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, JInt>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JInt>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, JLong>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JLong>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, JShort>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, JShort>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			default:
				Unsafe.As<Byte, JObjectLocalRef>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*,
						JObjectLocalRef>)funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
		}
	}
	/// <summary>
	/// Calls <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void GetField(void* funcPtr, JNativeType fieldType, JEnvironmentRef envRef, IntPtr receiver,
		JFieldId fieldId, ref Byte refValue)
	{
		switch (fieldType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, JBoolean>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JBoolean>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, JByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JByte>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, JChar>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JChar>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, JDouble>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JDouble>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, JFloat>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JFloat>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, JInt>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JInt>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, JLong>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JLong>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, JShort>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JShort>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			default:
				Unsafe.As<Byte, JObjectLocalRef>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef>)funcPtr)(
						envRef, receiver, fieldId);
				break;
		}
	}
	/// <summary>
	/// Calls <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
	[ExcludeFromCodeCoverage]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void SetField(void* funcPtr, JNativeType fieldType, JEnvironmentRef envRef, IntPtr receiver,
		JFieldId fieldId, ref Byte refValue)
	{
		switch (fieldType)
		{
			case JNativeType.JBoolean:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JBoolean, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JBoolean>(ref refValue));
				break;
			case JNativeType.JByte:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JByte, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JByte>(ref refValue));
				break;
			case JNativeType.JChar:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JChar, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JChar>(ref refValue));
				break;
			case JNativeType.JDouble:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JDouble, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JDouble>(ref refValue));
				break;
			case JNativeType.JFloat:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JFloat, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JFloat>(ref refValue));
				break;
			case JNativeType.JInt:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JInt, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JInt>(ref refValue));
				break;
			case JNativeType.JLong:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JLong, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JLong>(ref refValue));
				break;
			case JNativeType.JShort:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JShort, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JShort>(ref refValue));
				break;
			default:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, JObjectLocalRef, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, JObjectLocalRef>(ref refValue));
				break;
		}
	}
}