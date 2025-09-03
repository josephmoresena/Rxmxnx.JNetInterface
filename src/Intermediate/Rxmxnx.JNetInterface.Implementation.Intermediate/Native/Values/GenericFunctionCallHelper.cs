namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// Helper class to use function pointers in generic function calls.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal static class GenericFunctionCallHelper
{
	/// <summary>
	/// Calls Call&lt;type&gt;Method<c>A</c> function.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void CallMethod(void* funcPtr, JNativeType resultType, JEnvironmentRef envRef, IntPtr receiver,
		JMethodId methodId, JValue* args, ref Byte refValue)
	{
		switch (resultType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, Byte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Byte>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, SByte>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, UInt16>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Double>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Single>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int32>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int64>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, Int16>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JMethodId, JValue*, IntPtr>)funcPtr)(
						envRef, receiver, methodId, args);
				break;
		}
	}
	/// <summary>
	/// Calls CallNonVirtual&lt;type&gt;Method<c>A</c> function.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !PACKAGE
	[ExcludeFromCodeCoverage]
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS107,
	                 Justification = CommonConstants.PrimitiveCallJustification)]
#endif
	public static unsafe void CallNonVirtualMethod(void* funcPtr, JNativeType resultType, JEnvironmentRef envRef,
		JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId, JValue* args, ref Byte refValue)
	{
		switch (resultType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, Byte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Byte >)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, SByte>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, UInt16>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Double
						>)funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Single>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Int32>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Int64>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, Int16>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, JValue*, IntPtr>)
						funcPtr)(envRef, localRef, classRef, methodId, args);
				break;
		}
	}
	/// <summary>
	/// Calls <c>Get&lt;type&gt;Field</c> function.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void GetField(void* funcPtr, JNativeType fieldType, JEnvironmentRef envRef, IntPtr receiver,
		JFieldId fieldId, ref Byte refValue)
	{
		switch (fieldType)
		{
			case JNativeType.JBoolean:
				Unsafe.As<Byte, Byte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Byte>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, SByte>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt16>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Double>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Single>)funcPtr)(
						envRef, receiver, fieldId);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int32>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int64>)funcPtr)(envRef, receiver, fieldId);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int16>)funcPtr)(envRef, receiver, fieldId);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, IntPtr>)funcPtr)(
						envRef, receiver, fieldId);
				break;
		}
	}
	/// <summary>
	/// Calls <c>Set&lt;type&gt;Field</c> function.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe void SetField(void* funcPtr, JNativeType fieldType, JEnvironmentRef envRef, IntPtr receiver,
		JFieldId fieldId, ref Byte refValue)
	{
		switch (fieldType)
		{
			case JNativeType.JBoolean:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Byte, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Byte>(ref refValue));
				break;
			case JNativeType.JByte:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, SByte, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, SByte>(ref refValue));
				break;
			case JNativeType.JChar:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, UInt16, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, UInt16>(ref refValue));
				break;
			case JNativeType.JDouble:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Double, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Double>(ref refValue));
				break;
			case JNativeType.JFloat:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Single, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Single>(ref refValue));
				break;
			case JNativeType.JInt:

				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int32, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Int32>(ref refValue));
				break;
			case JNativeType.JLong:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int64, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Int64>(ref refValue));
				break;
			case JNativeType.JShort:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, Int16, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, Int16>(ref refValue));
				break;
			default:
				((delegate* unmanaged<JEnvironmentRef, IntPtr, JFieldId, IntPtr, void>)funcPtr)(
					envRef, receiver, fieldId, Unsafe.As<Byte, IntPtr>(ref refValue));
				break;
		}
	}
}