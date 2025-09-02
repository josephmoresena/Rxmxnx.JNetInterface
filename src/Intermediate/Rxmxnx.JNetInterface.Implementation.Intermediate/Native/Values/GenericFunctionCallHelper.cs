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
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Byte>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, SByte>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, UInt16>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Double>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Single>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Int32>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Int64>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, Int16>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, void*, IntPtr>)funcPtr)(
						envRef.Pointer, receiver, methodId.Pointer, args);
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
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Byte >)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, SByte>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, UInt16>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Double >)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Single>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Int32>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Int64>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, Int16>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void*, IntPtr>)funcPtr)(
						envRef.Pointer, localRef.Pointer, classRef.Pointer, methodId.Pointer, args);
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
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Byte>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JByte:
				Unsafe.As<Byte, SByte>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, SByte>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JChar:
				Unsafe.As<Byte, UInt16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, UInt16>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JDouble:
				Unsafe.As<Byte, Double>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Double>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JFloat:
				Unsafe.As<Byte, Single>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Single>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JInt:
				Unsafe.As<Byte, Int32>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int32>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JLong:
				Unsafe.As<Byte, Int64>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int64>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			case JNativeType.JShort:
				Unsafe.As<Byte, Int16>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int16>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
				break;
			default:
				Unsafe.As<Byte, IntPtr>(ref refValue) =
					((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr>)funcPtr)(
						envRef.Pointer, receiver, fieldId.Pointer);
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
				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Byte, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Byte>(ref refValue));
				break;
			case JNativeType.JByte:

				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, SByte, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, SByte>(ref refValue));
				break;
			case JNativeType.JChar:

				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, UInt16, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, UInt16>(ref refValue));
				break;
			case JNativeType.JDouble:

				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Double, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Double>(ref refValue));
				break;
			case JNativeType.JFloat:

				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Single, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Single>(ref refValue));
				break;
			case JNativeType.JInt:

				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int32, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Int32>(ref refValue));
				break;
			case JNativeType.JLong:
				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int64, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Int64>(ref refValue));
				break;
			case JNativeType.JShort:
				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, Int16, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, Int16>(ref refValue));
				break;
			default:
				((delegate* unmanaged<IntPtr, IntPtr, IntPtr, IntPtr, void>)funcPtr)(
					envRef.Pointer, receiver, fieldId.Pointer, Unsafe.As<Byte, IntPtr>(ref refValue));
				break;
		}
	}
}