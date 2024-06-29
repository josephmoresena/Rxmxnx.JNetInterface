using Rxmxnx.JNetInterface.Native.Identifiers;
using Rxmxnx.JNetInterface.Native.Values.Functions;
using Rxmxnx.JNetInterface.Primitives;

namespace Rxmxnx.JNetInterface.Tests.Internal;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
internal unsafe struct InternalNativeInterface
{
#pragma warning disable CS0169
	private readonly JNativeInterface.ComReserved _reserved;
#pragma warning restore CS0169

	public delegate* unmanaged<JEnvironmentRef, Int32> GetVersion;

	public delegate* unmanaged<JEnvironmentRef, ReadOnlyValPtr<Byte>, JObjectLocalRef, IntPtr, Int32, JClassLocalRef>
		DefineClass;
	public delegate* unmanaged<JEnvironmentRef, ValPtr<Byte>, JClassLocalRef> FindClass;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId> FromReflectedMethod;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId> FromReflectedField;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JMethodId, JBoolean, JObjectLocalRef> ToReflectedMethod;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JClassLocalRef> GetSuperclass;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JClassLocalRef, JBoolean> IsAssignableFrom;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JFieldId, JBoolean, JObjectLocalRef> ToReflectedField;

	public delegate* unmanaged<JEnvironmentRef, JThrowableLocalRef, JResult> Throw;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, JResult> ThrowNew;
	public delegate* unmanaged<JEnvironmentRef, JThrowableLocalRef> ExceptionOccurred;
	public delegate* unmanaged<JEnvironmentRef, void> ExceptionDescribe;
	public delegate* unmanaged<JEnvironmentRef, void> ExceptionClear;
	public delegate* unmanaged<JEnvironmentRef, ReadOnlyValPtr<Byte>, void> FatalError;

	public delegate* unmanaged<JEnvironmentRef, Int32, JResult> PushLocalFrame;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef> PopLocalFrame;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JGlobalRef> NewGlobalRef;
	public delegate* unmanaged<JEnvironmentRef, JGlobalRef, void> DeleteGlobalRef;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, void> DeleteLocalRef;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef, JBoolean> IsSameObject;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JObjectLocalRef> NewLocalRef;
	public delegate* unmanaged<JEnvironmentRef, Int32, JResult> EnsureLocalCapacity;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JObjectLocalRef> AllocObject;
#pragma warning disable CS0169
	private readonly MethodOffset _newObjectOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JObjectLocalRef>
		NewObject;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef> GetObjectClass;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JBoolean> IsInstanceOf;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, ReadOnlyValPtr<Byte>, JMethodId>
		GetMethodId;
#pragma warning disable CS0169
	private readonly MethodOffset _callObjectMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JObjectLocalRef>
		CallObjectMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callBooleanMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JBoolean>
		CallBooleanMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callByteMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JByte>
		CallByteMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callCharMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JChar>
		CallCharMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callShortMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JShort>
		CallShortMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callIntMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JInt> CallIntMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callLongMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JLong>
		CallLongMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callFloatMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JFloat>
		CallFloatMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callDoubleMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JDouble>
		CallDoubleMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callVoidMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, void>
		CallVoidMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualObjectMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JObjectLocalRef> CallNonVirtualObjectMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualBooleanMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JBoolean> CallNonVirtualBooleanMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualByteMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JByte> CallNonVirtualByteMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualCharMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JChar> CallNonVirtualCharMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualShortMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JShort > CallNonVirtualShortMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualIntMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JInt
		> CallNonVirtualIntMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualLongMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JLong> CallNonVirtualLongMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualFloatMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JFloat > CallNonVirtualFloatMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualDoubleMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>,
		JDouble> CallNonVirtualDoubleMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callNonVirtualVoidMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JClassLocalRef, JMethodId, ReadOnlyValPtr<JValue>, void
		> CallNonVirtualVoidMethod;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, ReadOnlyValPtr<Byte>, JFieldId>
		GetFieldId;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JObjectLocalRef> GetObjectField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JBoolean> GetBooleanField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JByte> GetByteField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JChar> GetCharField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JShort> GetShortField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JInt> GetIntField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JLong> GetLongField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JFloat> GetFloatField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JDouble> GetDoubleField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JObjectLocalRef, void> SetObjectField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JBoolean, void> SetBooleanField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JByte, void> SetByteField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JChar, void> SetCharField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JShort, void> SetShortField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JInt, void> SetIntField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JLong> SetLongField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JFloat, void> SetFloatField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JDouble, void> SetDoubleField;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, ReadOnlyValPtr<Byte>, JMethodId>
		GetStaticMethodId;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticObjectMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JObjectLocalRef>
		CallStaticObjectMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticBooleanMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JBoolean>
		CallStaticBooleanMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticByteMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JByte>
		CallStaticByteMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticCharMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JChar>
		CallStaticCharMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticShortMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JShort>
		CallStaticShortMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticIntMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JInt>
		CallStaticIntMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticLongMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JLong>
		CallStaticLongMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticFloatMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JFloat>
		CallStaticFloatMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticDoubleMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, JDouble>
		CallStaticDoubleMethod;
#pragma warning disable CS0169
	private readonly MethodOffset _callStaticVoidMethodOffset;
#pragma warning restore CS0169
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JMethodId, ReadOnlyValPtr<JValue>, void>
		CallStaticVoidMethod;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<Byte>, ReadOnlyValPtr<Byte>, JFieldId>
		GetStaticFieldId;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JObjectLocalRef> GetStaticObjectField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JBoolean> GetStaticBooleanField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JByte> GetStaticByteField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JChar> GetStaticCharField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JShort> GetStaticShortField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JInt> GetStaticIntField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JLong> GetStaticLongField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JFloat> GetStaticFloatField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JDouble> GetStaticDoubleField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JObjectLocalRef, void> SetStaticObjectField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JBoolean, void> SetStaticBooleanField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JByte, void> SetStaticByteField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JChar, void> SetStaticCharField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JShort, void> SetStaticShortField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JInt, void> SetStaticIntField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JLong> SetStaticLongField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JFloat, void> SetStaticFloatField;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JFieldId, JDouble, void> SetStaticDoubleField;

	public delegate* unmanaged<JEnvironmentRef, Char*, Int32, JStringLocalRef> NewString;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32> GetStringLength;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ValPtr<JBoolean>, ReadOnlyValPtr<Char>> GetStringChars;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ReadOnlyValPtr<Char>, void> ReleaseStringChars;

	public delegate* unmanaged<JEnvironmentRef, Byte*, JStringLocalRef> NewStringUtf;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32> GetStringUtfLength;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ValPtr<JBoolean>, ReadOnlyValPtr<Char>>
		GetStringUtfChars;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ReadOnlyValPtr<Char>, void> ReleaseStringUtfChars;

	public delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, Int32> GetArrayLength;

	public delegate* unmanaged<JEnvironmentRef, Int32, JClassLocalRef, JObjectLocalRef, JObjectArrayLocalRef>
		NewObjectArray;
	public delegate* unmanaged<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef> GetObjectArrayElement;
	public delegate* unmanaged<JEnvironmentRef, JObjectArrayLocalRef, Int32, JObjectLocalRef, void>
		SetObjectArrayElement;

	public delegate* unmanaged<JEnvironmentRef, Int32, JBooleanArrayLocalRef> NewBooleanArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JByteArrayLocalRef> NewByteArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JCharArrayLocalRef> NewCharArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JShortArrayLocalRef> NewShortArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JIntArrayLocalRef> NewIntArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JLongArrayLocalRef> NewLongArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JFloatArrayLocalRef> NewFloatArray;
	public delegate* unmanaged<JEnvironmentRef, Int32, JDoubleArrayLocalRef> NewDoubleArray;

	public delegate* unmanaged<JEnvironmentRef, JBooleanArrayLocalRef, ValPtr<JBoolean>, ValPtr<JBoolean>>
		GetBooleanArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JByteArrayLocalRef, ValPtr<JBoolean>, ValPtr<JByte>>
		GetByteArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JCharArrayLocalRef, ValPtr<JBoolean>, ValPtr<JChar>>
		GetCharArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JShortArrayLocalRef, ValPtr<JBoolean>, ValPtr<JShort>>
		GetShortArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JIntArrayLocalRef, ValPtr<JBoolean>, ValPtr<JInt>> GetIntArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JLongArrayLocalRef, ValPtr<JBoolean>, ValPtr<JLong>>
		GetLongArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JFloatArrayLocalRef, ValPtr<JBoolean>, ValPtr<JFloat>>
		GetFloatArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JDoubleArrayLocalRef, ValPtr<JBoolean>, ValPtr<JDouble>>
		GetDoubleArrayElements;

	public delegate* unmanaged<JEnvironmentRef, JBooleanArrayLocalRef, ReadOnlyValPtr<JBoolean>, JReleaseMode, void>
		ReleaseBooleanArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JByteArrayLocalRef, ReadOnlyValPtr<JByte>, JReleaseMode, void>
		ReleaseByteArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JCharArrayLocalRef, ReadOnlyValPtr<JChar>, JReleaseMode, void>
		ReleaseCharArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JShortArrayLocalRef, ReadOnlyValPtr<JShort>, JReleaseMode, void>
		ReleaseShortArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JIntArrayLocalRef, ReadOnlyValPtr<JInt>, JReleaseMode, void>
		ReleaseIntArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JLongArrayLocalRef, ReadOnlyValPtr<JLong>, JReleaseMode, void>
		ReleaseLongArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JFloatArrayLocalRef, ReadOnlyValPtr<JFloat>, JReleaseMode, void>
		ReleaseFloatArrayElements;
	public delegate* unmanaged<JEnvironmentRef, JDoubleArrayLocalRef, ReadOnlyValPtr<JDouble>, JReleaseMode, void>
		ReleaseDoubleArrayElements;

	public delegate* unmanaged<JEnvironmentRef, JBooleanArrayLocalRef, Int32, Int32, ValPtr<JBoolean>, void>
		GetBooleanArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JByteArrayLocalRef, Int32, Int32, ValPtr<JByte>, void>
		GetByteArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JCharArrayLocalRef, Int32, Int32, ValPtr<JChar>, void>
		GetCharArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JShortArrayLocalRef, Int32, Int32, ValPtr<JShort>, void>
		GetShortArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JIntArrayLocalRef, Int32, Int32, ValPtr<JInt>, void> GetIntArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JLongArrayLocalRef, Int32, Int32, ValPtr<JLong>, void>
		GetLongArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JFloatArrayLocalRef, Int32, Int32, ValPtr<JFloat>, void>
		GetFloatArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JDoubleArrayLocalRef, Int32, Int32, ValPtr<JDouble>, void>
		GetDoubleArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JBooleanArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JBoolean>, void>
		SetBooleanArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JByteArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JByte>, void>
		SetByteArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JCharArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JChar>, void>
		SetCharArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JShortArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JShort>, void>
		SetShortArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JIntArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JInt>, void>
		SetIntArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JLongArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JLong>, void>
		SetLongArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JFloatArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JFloat>, void>
		SetFloatArrayRegion;
	public delegate* unmanaged<JEnvironmentRef, JDoubleArrayLocalRef, Int32, Int32, ReadOnlyValPtr<JDouble>, void>
		SetDoubleArrayRegion;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, ReadOnlyValPtr<NativeMethodValue>, Int32, JResult>
		RegisterNatives;
	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JResult> UnregisterNatives;

	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JResult> MonitorEnter;
	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JResult> MonitorExit;

	public delegate* unmanaged<JEnvironmentRef, ValPtr<JVirtualMachineRef>, JResult> GetVirtualMachine;

	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32, Int32, ReadOnlyValPtr<Char>, void>
		GetStringRegion;
	public delegate* unmanaged<JEnvironmentRef, JStringLocalRef, Int32, Int32, ReadOnlyValPtr<Byte>, void>
		GetStringUtfRegion;

	public delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, ValPtr<JBoolean>, ValPtr<Byte>>
		GetPrimitiveArrayCritical;
	public delegate* unmanaged<JEnvironmentRef, JArrayLocalRef, ValPtr<Byte>, JReleaseMode, void>
		ReleasePrimitiveArrayCritical;

	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ValPtr<JBoolean>, ReadOnlyValPtr<Char>>
		GetStringCritical;
	public readonly delegate* unmanaged<JEnvironmentRef, JStringLocalRef, ReadOnlyValPtr<Char>, void>
		ReleaseStringCritical;

	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JWeakRef> NewWeakGlobalRef;
	public readonly delegate* unmanaged<JEnvironmentRef, JWeakRef, void> DeleteWeakGlobalRef;

	public readonly delegate* unmanaged<JEnvironmentRef, JBoolean> ExceptionCheck;

	public readonly delegate* unmanaged<JEnvironmentRef, IntPtr, Int64, JObjectLocalRef> NewDirectByteBuffer;
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, IntPtr> GetDirectBufferAddress;
	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, Int64> GetDirectBufferCapacity;

	public readonly delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JReferenceType> GetObjectRefType;

	public delegate* unmanaged<JEnvironmentRef, JClassLocalRef, JObjectLocalRef> GetModule;

	public delegate* unmanaged<JEnvironmentRef, JObjectLocalRef, JBoolean> IsVirtualThread;
}