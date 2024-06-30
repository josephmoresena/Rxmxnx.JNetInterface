namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3881,
                 Justification = CommonConstants.AbstractProxyJustification)]
public abstract class NativeInterfaceProxy
{
	public abstract Int32 GetVersion();

	public abstract JClassLocalRef DefineClass(ReadOnlyValPtr<Byte> className, JObjectLocalRef classLoader,
		IntPtr byteCode, Int32 byteLength);
	public abstract JClassLocalRef FindClass(ReadOnlyValPtr<Byte> className);
	public abstract JMethodId FromReflectedMethod(JObjectLocalRef executableRef);
	public abstract JFieldId FromReflectedField(JObjectLocalRef fieldRef);
	public abstract JObjectLocalRef ToReflectedMethod(JClassLocalRef classRef, JMethodId methodId, JBoolean isStatic);
	public abstract JClassLocalRef GetSuperclass(JClassLocalRef classRef);
	public abstract JBoolean IsAssignableFrom(JClassLocalRef classFromRef, JClassLocalRef classToRef);
	public abstract JObjectLocalRef ToReflectedField(JClassLocalRef classRef, JFieldId fieldId, JBoolean isStatic);

	public abstract JResult Throw(JThrowableLocalRef throwableRef);
	public abstract JResult ThrowNew(JClassLocalRef classRef, ReadOnlyValPtr<Byte> message);
	public abstract JThrowableLocalRef ExceptionOccurred();
	public abstract void ExceptionDescribe();
	public abstract void ExceptionClear();
	public abstract void FatalError(ReadOnlyValPtr<Byte> message);

	public abstract JResult PushLocalFrame(Int32 capacity);
	public abstract JObjectLocalRef PopLocalFrame(JObjectLocalRef localRef);
	public abstract JGlobalRef NewGlobalRef(JObjectLocalRef localRef);
	public abstract void DeleteGlobalRef(JGlobalRef globalRef);
	public abstract void DeleteLocalRef(JObjectLocalRef localRef);
	public abstract JBoolean IsSameObject(JObjectLocalRef localRef1, JObjectLocalRef localRef2);
	public abstract JObjectLocalRef NewLocalRef(JObjectLocalRef localRef);
	public abstract JResult EnsureLocalCapacity(Int32 capacity);
	public abstract JObjectLocalRef AllocObject(JClassLocalRef classRef);
	public abstract JObjectLocalRef NewObject(JClassLocalRef classRef, JMethodId constructorId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JClassLocalRef GetObjectClass(JObjectLocalRef localRef);
	public abstract JBoolean IsInstanceOf(JObjectLocalRef localRef, JClassLocalRef classRef);

	public abstract JMethodId GetMethodId(JClassLocalRef classRef, ReadOnlyValPtr<Byte> methodName,
		ReadOnlyValPtr<Byte> methodDescriptor);
	public abstract JObjectLocalRef CallObjectMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JBoolean CallBooleanMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JByte CallByteMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JChar CallCharMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JShort CallShortMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JInt CallIntMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JLong CallLongMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JFloat CallFloatMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JDouble CallDoubleMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract void CallVoidMethod(JObjectLocalRef localRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JObjectLocalRef CallNonVirtualObjectMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JBoolean CallNonVirtualBooleanMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JByte CallNonVirtualByteMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JChar CallNonVirtualCharMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JShort CallNonVirtualShortMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JInt CallNonVirtualIntMethod(JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JLong CallNonVirtualLongMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JFloat CallNonVirtualFloatMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract JDouble CallNonVirtualDoubleMethod(JObjectLocalRef localRef, JClassLocalRef classRef,
		JMethodId methodId, ReadOnlyValPtr<JValueWrapper> args);
	public abstract void CallNonVirtualVoidMethod(JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);

	public abstract JFieldId GetFieldId(JClassLocalRef classRef, ReadOnlyValPtr<Byte> fieldName,
		ReadOnlyValPtr<Byte> fieldDescriptor);
	public abstract JObjectLocalRef GetObjectField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JBoolean GetBooleanField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JByte GetByteField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JChar GetCharField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JShort GetShortField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JInt GetIntField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JLong GetLongField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JFloat GetFloatField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract JDouble GetDoubleField(JObjectLocalRef localRef, JFieldId fieldId);
	public abstract void SetObjectField(JObjectLocalRef localRef, JFieldId fieldId, JObjectLocalRef value);
	public abstract void SetBooleanField(JObjectLocalRef localRef, JFieldId fieldId, JBoolean value);
	public abstract void SetByteField(JObjectLocalRef localRef, JFieldId fieldId, JByte value);
	public abstract void SetCharField(JObjectLocalRef localRef, JFieldId fieldId, JChar value);
	public abstract void SetShortField(JObjectLocalRef localRef, JFieldId fieldId, JShort value);
	public abstract void SetIntField(JObjectLocalRef localRef, JFieldId fieldId, JInt value);
	public abstract void SetLongField(JObjectLocalRef localRef, JFieldId fieldId, JLong value);
	public abstract void SetFloatField(JObjectLocalRef localRef, JFieldId fieldId, JFloat value);
	public abstract void SetDoubleField(JObjectLocalRef localRef, JFieldId fieldId, JDouble value);

	public abstract JMethodId GetStaticMethodId(JClassLocalRef classRef, ReadOnlyValPtr<Byte> methodName,
		ReadOnlyValPtr<Byte> methodDescriptor);
	public abstract JObjectLocalRef CallStaticObjectMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JBoolean CallStaticBooleanMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JByte CallStaticByteMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JChar CallStaticCharMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JShort CallStaticShortMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JInt CallStaticIntMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JLong CallStaticLongMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JFloat CallStaticFloatMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract JDouble CallStaticDoubleMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);
	public abstract void CallStaticVoidMethod(JClassLocalRef classRef, JMethodId methodId,
		ReadOnlyValPtr<JValueWrapper> args);

	public abstract JFieldId GetStaticFieldId(JClassLocalRef classRef, ReadOnlyValPtr<Byte> fieldName,
		ReadOnlyValPtr<Byte> fieldDescriptor);
	public abstract JObjectLocalRef GetStaticObjectField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JBoolean GetStaticBooleanField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JByte GetStaticByteField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JChar GetStaticCharField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JShort GetStaticShortField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JInt GetStaticIntField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JLong GetStaticLongField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JFloat GetStaticFloatField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract JDouble GetStaticDoubleField(JClassLocalRef classRef, JFieldId fieldId);
	public abstract void SetStaticObjectField(JClassLocalRef classRef, JFieldId fieldId, JObjectLocalRef value);
	public abstract void SetStaticBooleanField(JClassLocalRef classRef, JFieldId fieldId, JBoolean value);
	public abstract void SetStaticByteField(JClassLocalRef classRef, JFieldId fieldId, JByte value);
	public abstract void SetStaticCharField(JClassLocalRef classRef, JFieldId fieldId, JChar value);
	public abstract void SetStaticShortField(JClassLocalRef classRef, JFieldId fieldId, JShort value);
	public abstract void SetStaticIntField(JClassLocalRef classRef, JFieldId fieldId, JInt value);
	public abstract void SetStaticLongField(JClassLocalRef classRef, JFieldId fieldId, JLong value);
	public abstract void SetStaticFloatField(JClassLocalRef classRef, JFieldId fieldId, JFloat value);
	public abstract void SetStaticDoubleField(JClassLocalRef classRef, JFieldId fieldId, JDouble value);

	public abstract JStringLocalRef NewString(ReadOnlyValPtr<Char> chars, Int32 length);
	public abstract Int32 GetStringLength(JStringLocalRef stringRef);
	public abstract ReadOnlyValPtr<Char> GetStringChars(JStringLocalRef stringRef, ValPtr<JBoolean> isCopy);
	public abstract void ReleaseStringChars(JStringLocalRef stringRef, ReadOnlyValPtr<Char> chars);

	public abstract JStringLocalRef NewStringUtf(ReadOnlyValPtr<Byte> chars);
	public abstract Int32 GetStringUtfLength(JStringLocalRef stringRef);
	public abstract ReadOnlyValPtr<Byte> GetStringUtfChars(JStringLocalRef stringRef, ValPtr<JBoolean> isCopy);
	public abstract void ReleaseStringUtfChars(JStringLocalRef stringRef, ReadOnlyValPtr<Byte> chars);

	public abstract Int32 GetArrayLength(JArrayLocalRef arrayRef);

	public abstract JObjectArrayLocalRef NewObjectArray(Int32 arrayLength, JClassLocalRef elementClass,
		JObjectLocalRef initialElement);
	public abstract JObjectLocalRef GetObjectArrayElement(JObjectArrayLocalRef arrayRef, Int32 index);
	public abstract void SetObjectArrayElement(JObjectArrayLocalRef arrayRef, Int32 index, JObjectLocalRef value);

	public abstract JBooleanArrayLocalRef NewBooleanArray(Int32 arrayLength);
	public abstract JByteArrayLocalRef NewByteArray(Int32 arrayLength);
	public abstract JCharArrayLocalRef NewCharArray(Int32 arrayLength);
	public abstract JShortArrayLocalRef NewShortArray(Int32 arrayLength);
	public abstract JIntArrayLocalRef NewIntArray(Int32 arrayLength);
	public abstract JLongArrayLocalRef NewLongArray(Int32 arrayLength);
	public abstract JFloatArrayLocalRef NewFloatArray(Int32 arrayLength);
	public abstract JDoubleArrayLocalRef NewDoubleArray(Int32 arrayLength);

	public abstract ValPtr<JBoolean> GetBooleanArrayElements(JBooleanArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JByte> GetByteArrayElements(JByteArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JChar> GetCharArrayElements(JCharArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JShort> GetShortArrayElements(JShortArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JInt> GetIntArrayElements(JIntArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JLong> GetLongArrayElements(JLongArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JFloat> GetFloatArrayElements(JFloatArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract ValPtr<JDouble> GetDoubleArrayElements(JDoubleArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);

	public abstract void ReleaseBooleanArrayElements(JBooleanArrayLocalRef arrayRef, ReadOnlyValPtr<JBoolean> elements,
		JReleaseMode mode);
	public abstract void ReleaseByteArrayElements(JByteArrayLocalRef arrayRef, ReadOnlyValPtr<JByte> elements,
		JReleaseMode mode);
	public abstract void ReleaseCharArrayElements(JCharArrayLocalRef arrayRef, ReadOnlyValPtr<JChar> elements,
		JReleaseMode mode);
	public abstract void ReleaseShortArrayElements(JShortArrayLocalRef arrayRef, ReadOnlyValPtr<JShort> elements,
		JReleaseMode mode);
	public abstract void ReleaseIntArrayElements(JIntArrayLocalRef arrayRef, ReadOnlyValPtr<JInt> elements,
		JReleaseMode mode);
	public abstract void ReleaseLongArrayElements(JLongArrayLocalRef arrayRef, ReadOnlyValPtr<JLong> elements,
		JReleaseMode mode);
	public abstract void ReleaseFloatArrayElements(JFloatArrayLocalRef arrayRef, ReadOnlyValPtr<JFloat> elements,
		JReleaseMode mode);
	public abstract void ReleaseDoubleArrayElements(JDoubleArrayLocalRef arrayRef, ReadOnlyValPtr<JDouble> elements,
		JReleaseMode mode);

	public abstract void GetBooleanArrayRegion(JBooleanArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JBoolean> buffer);
	public abstract void GetByteArrayRegion(JByteArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JByte> buffer);
	public abstract void GetCharArrayRegion(JCharArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JChar> buffer);
	public abstract void GetShortArrayRegion(JShortArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JShort> buffer);
	public abstract void GetIntArrayRegion(JIntArrayLocalRef arrayRef, Int32 start, Int32 count, ValPtr<JInt> buffer);
	public abstract void GetLongArrayRegion(JLongArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JLong> buffer);
	public abstract void GetFloatArrayRegion(JFloatArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JFloat> buffer);
	public abstract void GetDoubleArrayRegion(JDoubleArrayLocalRef arrayRef, Int32 start, Int32 count,
		ValPtr<JDouble> buffer);

	public abstract JResult RegisterNatives(JClassLocalRef classRef,
		ReadOnlyValPtr<NativeMethodValueWrapper> methodEntries, Int32 count);
	public abstract JResult UnregisterNatives(JClassLocalRef classRef);

	public abstract JResult MonitorEnter(JObjectLocalRef localRef);
	public abstract JResult MonitorExit(JObjectLocalRef localRef);

	public abstract JResult GetVirtualMachine(ValPtr<JVirtualMachineRef> vmRef);

	public abstract void GetStringRegion(JStringLocalRef stringRef, Int32 start, Int32 count, ValPtr<Char> buffer);
	public abstract void GetStringUtfRegion(JStringLocalRef stringRef, Int32 start, Int32 count, ValPtr<Byte> buffer);

	public abstract ValPtr<Byte> GetPrimitiveArrayCritical(JArrayLocalRef arrayRef, ValPtr<JBoolean> isCopy);
	public abstract void ReleasePrimitiveArrayCritical(JArrayLocalRef arrayRef, ValPtr<Byte> critical,
		JReleaseMode mode);

	public abstract ReadOnlyValPtr<Char> GetStringCritical(JStringLocalRef stringRef, ValPtr<JBoolean> isCopy);
	public abstract void ReleaseStringCritical(JStringLocalRef stringRef, ReadOnlyValPtr<Char> critical);

	public abstract JWeakRef NewWeakGlobalRef(JObjectLocalRef localRef);
	public abstract void DeleteWeakGlobalRef(JWeakRef weakRef);

	public abstract JBoolean ExceptionCheck();

	public abstract JObjectLocalRef NewDirectByteBuffer(IntPtr address, Int64 capacity);
	public abstract IntPtr GetDirectBufferAddress(JObjectLocalRef bufferRef);
	public abstract Int64 GetDirectBufferCapacity(JObjectLocalRef bufferRef);

	public abstract JReferenceType GetObjectRefType(JObjectLocalRef localRef);

	public abstract JObjectLocalRef GetModule(JClassLocalRef classRef);

	public abstract JBoolean IsVirtualThread(JObjectLocalRef threadRef);
}