namespace Rxmxnx.JNetInterface.Tests;

public unsafe partial class NativeInterfaceHelper
{
	[UnmanagedCallersOnly]
	private static Int32 GetVersion(JEnvironmentRef envRef) => NativeInterfaceHelper.proxies[envRef].GetVersion();

	[UnmanagedCallersOnly]
	private static JClassLocalRef DefineClass(JEnvironmentRef envRef, Byte* className, JObjectLocalRef classLoader,
		IntPtr byteCode, Int32 byteLength)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .DefineClass((ReadOnlyValPtr<Byte>)(IntPtr)className, classLoader, byteCode,
		                                     byteLength);
	[UnmanagedCallersOnly]
	private static JClassLocalRef FindClass(JEnvironmentRef envRef, Byte* className)
		=> NativeInterfaceHelper.proxies[envRef].FindClass((ReadOnlyValPtr<Byte>)(IntPtr)className);
	[UnmanagedCallersOnly]
	private static JMethodId FromReflectedMethod(JEnvironmentRef envRef, JObjectLocalRef executableRef)
		=> NativeInterfaceHelper.proxies[envRef].FromReflectedMethod(executableRef);
	[UnmanagedCallersOnly]
	private static JFieldId FromReflectedField(JEnvironmentRef envRef, JObjectLocalRef fieldRef)
		=> NativeInterfaceHelper.proxies[envRef].FromReflectedField(fieldRef);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JBoolean isStatic)
		=> NativeInterfaceHelper.proxies[envRef].ToReflectedMethod(classRef, methodId, isStatic);
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetSuperclass(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> NativeInterfaceHelper.proxies[envRef].GetSuperclass(classRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsAssignableFrom(JEnvironmentRef envRef, JClassLocalRef classFromRef,
		JClassLocalRef classToRef)
		=> NativeInterfaceHelper.proxies[envRef].IsAssignableFrom(classFromRef, classToRef);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean isStatic)
		=> NativeInterfaceHelper.proxies[envRef].ToReflectedField(classRef, fieldId, isStatic);

	[UnmanagedCallersOnly]
	private static JResult Throw(JEnvironmentRef envRef, JThrowableLocalRef throwableRef)
		=> NativeInterfaceHelper.proxies[envRef].Throw(throwableRef);
	[UnmanagedCallersOnly]
	private static JResult ThrowNew(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* message)
		=> NativeInterfaceHelper.proxies[envRef].ThrowNew(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)message);
	[UnmanagedCallersOnly]
	private static JThrowableLocalRef ExceptionOccurred(JEnvironmentRef envRef)
		=> NativeInterfaceHelper.proxies[envRef].ExceptionOccurred();
	[UnmanagedCallersOnly]
	private static void ExceptionDescribe(JEnvironmentRef envRef)
		=> NativeInterfaceHelper.proxies[envRef].ExceptionDescribe();
	[UnmanagedCallersOnly]
	private static void ExceptionClear(JEnvironmentRef envRef)
		=> NativeInterfaceHelper.proxies[envRef].ExceptionClear();
	[UnmanagedCallersOnly]
	private static void FatalError(JEnvironmentRef envRef, Byte* message)
		=> NativeInterfaceHelper.proxies[envRef].FatalError((ReadOnlyValPtr<Byte>)(IntPtr)message);

	[UnmanagedCallersOnly]
	private static JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
		=> NativeInterfaceHelper.proxies[envRef].PushLocalFrame(capacity);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].PopLocalFrame(localRef);
	[UnmanagedCallersOnly]
	private static JGlobalRef NewGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].NewGlobalRef(localRef);
	[UnmanagedCallersOnly]
	private static void DeleteGlobalRef(JEnvironmentRef envRef, JGlobalRef globalRef)
		=> NativeInterfaceHelper.proxies[envRef].DeleteGlobalRef(globalRef);
	[UnmanagedCallersOnly]
	private static void DeleteLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].DeleteLocalRef(localRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsSameObject(JEnvironmentRef envRef, JObjectLocalRef localRef1, JObjectLocalRef localRef2)
		=> NativeInterfaceHelper.proxies[envRef].IsSameObject(localRef1, localRef2);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].NewLocalRef(localRef);
	[UnmanagedCallersOnly]
	private static JResult EnsureLocalCapacity(JEnvironmentRef envRef, Int32 capacity)
		=> NativeInterfaceHelper.proxies[envRef].EnsureLocalCapacity(capacity);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef AllocObject(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> NativeInterfaceHelper.proxies[envRef].AllocObject(classRef);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewObject(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId constructorId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .NewObject(classRef, constructorId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetObjectClass(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].GetObjectClass(localRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsInstanceOf(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef)
		=> NativeInterfaceHelper.proxies[envRef].IsInstanceOf(localRef, classRef);

	[UnmanagedCallersOnly]
	private static JMethodId GetMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
		=> NativeInterfaceHelper.proxies[envRef].GetMethodId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName,
		                                                     (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallObjectMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JBoolean CallBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallBooleanMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JByte CallByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallByteMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JChar CallCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallCharMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JShort CallShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallShortMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JInt CallIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallIntMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JLong CallLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallLongMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JFloat CallFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallFloatMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JDouble CallDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallDoubleMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallVoidMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallNonVirtualObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualObjectMethod(localRef, classRef, methodId,
		                                                    (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JBoolean CallNonVirtualBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualBooleanMethod(localRef, classRef, methodId,
		                                                     (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JByte CallNonVirtualByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualByteMethod(localRef, classRef, methodId,
		                                                  (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JChar CallNonVirtualCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualCharMethod(localRef, classRef, methodId,
		                                                  (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JShort CallNonVirtualShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualShortMethod(localRef, classRef, methodId,
		                                                   (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JInt CallNonVirtualIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualIntMethod(localRef, classRef, methodId,
		                                                 (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JLong CallNonVirtualLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualLongMethod(localRef, classRef, methodId,
		                                                  (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JFloat CallNonVirtualFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualFloatMethod(localRef, classRef, methodId,
		                                                   (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JDouble CallNonVirtualDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualDoubleMethod(localRef, classRef, methodId,
		                                                    (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallNonVirtualVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallNonVirtualVoidMethod(localRef, classRef, methodId,
		                                                  (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);

	[UnmanagedCallersOnly]
	private static JFieldId GetFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
		=> NativeInterfaceHelper.proxies[envRef].GetFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
		                                                    (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetObjectField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JBoolean GetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetBooleanField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JByte GetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetByteField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JChar GetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetCharField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JShort GetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetShortField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JInt GetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetIntField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JLong GetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetLongField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JFloat GetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetFloatField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static JDouble GetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetDoubleField(localRef, fieldId);
	[UnmanagedCallersOnly]
	private static void SetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JObjectLocalRef value)
		=> NativeInterfaceHelper.proxies[envRef].SetObjectField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JBoolean value)
		=> NativeInterfaceHelper.proxies[envRef].SetBooleanField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JByte value)
		=> NativeInterfaceHelper.proxies[envRef].SetByteField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JChar value)
		=> NativeInterfaceHelper.proxies[envRef].SetCharField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JShort value)
		=> NativeInterfaceHelper.proxies[envRef].SetShortField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JInt value)
		=> NativeInterfaceHelper.proxies[envRef].SetIntField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JLong value)
		=> NativeInterfaceHelper.proxies[envRef].SetLongField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JFloat value)
		=> NativeInterfaceHelper.proxies[envRef].SetFloatField(localRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JDouble value)
		=> NativeInterfaceHelper.proxies[envRef].SetDoubleField(localRef, fieldId, value);

	[UnmanagedCallersOnly]
	private static JMethodId GetStaticMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticMethodId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName,
		                                                           (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallStaticObjectMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticObjectMethod(classRef, methodId,
		                                                (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JBoolean CallStaticBooleanMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticBooleanMethod(classRef, methodId,
		                                                 (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JByte CallStaticByteMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticByteMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JChar CallStaticCharMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticCharMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JShort CallStaticShortMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticShortMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JInt CallStaticIntMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticIntMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JLong CallStaticLongMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticLongMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JFloat CallStaticFloatMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticFloatMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JDouble CallStaticDoubleMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticDoubleMethod(classRef, methodId,
		                                                (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallStaticVoidMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .CallStaticVoidMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);

	[UnmanagedCallersOnly]
	private static JFieldId GetStaticFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
		                                                          (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef,
		JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticObjectField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JBoolean GetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticBooleanField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JByte GetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticByteField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JChar GetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticCharField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JShort GetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticShortField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JInt GetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticIntField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JLong GetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticLongField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JFloat GetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticFloatField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static JDouble GetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> NativeInterfaceHelper.proxies[envRef].GetStaticDoubleField(classRef, fieldId);
	[UnmanagedCallersOnly]
	private static void SetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JObjectLocalRef value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticObjectField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticBooleanField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JByte value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticByteField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JChar value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticCharField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JShort value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticShortField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId, JInt value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticIntField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JLong value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticLongField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JFloat value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticFloatField(classRef, fieldId, value);
	[UnmanagedCallersOnly]
	private static void SetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JDouble value)
		=> NativeInterfaceHelper.proxies[envRef].SetStaticDoubleField(classRef, fieldId, value);

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewString(JEnvironmentRef envRef, Char* chars, Int32 length)
		=> NativeInterfaceHelper.proxies[envRef].NewString((ValPtr<Char>)(IntPtr)chars, length);
	[UnmanagedCallersOnly]
	private static Int32 GetStringLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> NativeInterfaceHelper.proxies[envRef].GetStringLength(stringRef);
	[UnmanagedCallersOnly]
	private static Char* GetStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Char*)NativeInterfaceHelper.proxies[envRef].GetStringChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                               .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* chars)
		=> NativeInterfaceHelper.proxies[envRef].ReleaseStringChars(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)chars);

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewStringUtf(JEnvironmentRef envRef, Byte* chars)
		=> NativeInterfaceHelper.proxies[envRef].NewStringUtf((ReadOnlyValPtr<Byte>)(IntPtr)chars);
	[UnmanagedCallersOnly]
	private static Int32 GetStringUtfLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> NativeInterfaceHelper.proxies[envRef].GetStringUtfLength(stringRef);
	[UnmanagedCallersOnly]
	private static Byte* GetStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Byte*)NativeInterfaceHelper.proxies[envRef].GetStringUtfChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                               .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Byte* chars)
		=> NativeInterfaceHelper.proxies[envRef].ReleaseStringUtfChars(stringRef, (ReadOnlyValPtr<Byte>)(IntPtr)chars);

	[UnmanagedCallersOnly]
	private static Int32 GetArrayLength(JEnvironmentRef envRef, JArrayLocalRef arrayRef)
		=> NativeInterfaceHelper.proxies[envRef].GetArrayLength(arrayRef);

	[UnmanagedCallersOnly]
	private static JObjectArrayLocalRef NewObjectArray(JEnvironmentRef envRef, Int32 arrayLength,
		JClassLocalRef elementClass, JObjectLocalRef initialElement)
		=> NativeInterfaceHelper.proxies[envRef].NewObjectArray(arrayLength, elementClass, initialElement);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef,
		Int32 index)
		=> NativeInterfaceHelper.proxies[envRef].GetObjectArrayElement(arrayRef, index);
	[UnmanagedCallersOnly]
	private static void SetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef, Int32 index,
		JObjectLocalRef value)
		=> NativeInterfaceHelper.proxies[envRef].SetObjectArrayElement(arrayRef, index, value);

	[UnmanagedCallersOnly]
	private static JBooleanArrayLocalRef NewBooleanArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewBooleanArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JByteArrayLocalRef NewByteArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewByteArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JCharArrayLocalRef NewCharArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewCharArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JShortArrayLocalRef NewShortArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewShortArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JIntArrayLocalRef NewIntArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewIntArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JLongArrayLocalRef NewLongArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewLongArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JFloatArrayLocalRef NewFloatArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewFloatArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JDoubleArrayLocalRef NewDoubleArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> NativeInterfaceHelper.proxies[envRef].NewDoubleArray(arrayLength);

	[UnmanagedCallersOnly]
	private static JBoolean* GetBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* isCopy)
		=> (JBoolean*)NativeInterfaceHelper.proxies[envRef]
		                                   .GetBooleanArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JByte* GetByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JByte*)NativeInterfaceHelper.proxies[envRef]
		                                .GetByteArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JChar* GetCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JChar*)NativeInterfaceHelper.proxies[envRef]
		                                .GetCharArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JShort* GetShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JShort*)NativeInterfaceHelper.proxies[envRef]
		                                 .GetShortArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JInt* GetIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JInt*)NativeInterfaceHelper.proxies[envRef].GetIntArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                               .Pointer;
	[UnmanagedCallersOnly]
	private static JLong* GetLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JLong*)NativeInterfaceHelper.proxies[envRef]
		                                .GetLongArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JFloat* GetFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JFloat*)NativeInterfaceHelper.proxies[envRef]
		                                 .GetFloatArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JDouble* GetDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JBoolean* isCopy)
		=> (JDouble*)NativeInterfaceHelper.proxies[envRef]
		                                  .GetDoubleArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;

	[UnmanagedCallersOnly]
	private static void ReleaseBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* elements, JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseBooleanArrayElements(arrayRef, (ReadOnlyValPtr<JBoolean>)(IntPtr)elements,
		                                                     mode);
	[UnmanagedCallersOnly]
	private static void ReleaseByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JByte* elements,
		JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseByteArrayElements(arrayRef, (ReadOnlyValPtr<JByte>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JChar* elements,
		JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseCharArrayElements(arrayRef, (ReadOnlyValPtr<JChar>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef,
		JShort* elements, JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseShortArrayElements(arrayRef, (ReadOnlyValPtr<JShort>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JInt* elements,
		JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseIntArrayElements(arrayRef, (ReadOnlyValPtr<JInt>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JLong* elements,
		JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseLongArrayElements(arrayRef, (ReadOnlyValPtr<JLong>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef,
		JFloat* elements, JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseFloatArrayElements(arrayRef, (ReadOnlyValPtr<JFloat>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JDouble* elements, JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseDoubleArrayElements(arrayRef, (ReadOnlyValPtr<JDouble>)(IntPtr)elements, mode);

	[UnmanagedCallersOnly]
	private static void GetBooleanArrayRegion(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef, Int32 start,
		Int32 count, JBoolean* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetBooleanArrayRegion(arrayRef, start, count, (ValPtr<JBoolean>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetByteArrayRegion(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, Int32 start,
		Int32 count, JByte* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetByteArrayRegion(arrayRef, start, count, (ValPtr<JByte>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetCharArrayRegion(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, Int32 start,
		Int32 count, JChar* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetCharArrayRegion(arrayRef, start, count, (ValPtr<JChar>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetShortArrayRegion(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, Int32 start,
		Int32 count, JShort* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetShortArrayRegion(arrayRef, start, count, (ValPtr<JShort>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetIntArrayRegion(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, Int32 start, Int32 count,
		JInt* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetIntArrayRegion(arrayRef, start, count, (ValPtr<JInt>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetLongArrayRegion(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, Int32 start,
		Int32 count, JLong* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetLongArrayRegion(arrayRef, start, count, (ValPtr<JLong>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetFloatArrayRegion(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, Int32 start,
		Int32 count, JFloat* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetFloatArrayRegion(arrayRef, start, count, (ValPtr<JFloat>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetDoubleArrayRegion(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef, Int32 start,
		Int32 count, JDouble* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetDoubleArrayRegion(arrayRef, start, count, (ValPtr<JDouble>)(IntPtr)buffer);

	[UnmanagedCallersOnly]
	private static JResult RegisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef,
		NativeMethodValueWrapper* methodEntries, Int32 count)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .RegisterNatives(
			                        classRef, (ReadOnlyValPtr<NativeMethodValueWrapper>)(IntPtr)methodEntries, count);
	[UnmanagedCallersOnly]
	private static JResult UnregisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> NativeInterfaceHelper.proxies[envRef].UnregisterNatives(classRef);

	[UnmanagedCallersOnly]
	private static JResult MonitorEnter(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].MonitorEnter(localRef);
	[UnmanagedCallersOnly]
	private static JResult MonitorExit(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].MonitorEnter(localRef);

	[UnmanagedCallersOnly]
	private static JResult GetVirtualMachine(JEnvironmentRef envRef, JVirtualMachineRef* vmRef)
		=> NativeInterfaceHelper.proxies[envRef].GetVirtualMachine((ValPtr<JVirtualMachineRef>)(IntPtr)vmRef);

	[UnmanagedCallersOnly]
	private static void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Char* buffer)
		=> NativeInterfaceHelper.proxies[envRef].GetStringRegion(stringRef, start, count, (ValPtr<Char>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetStringUtfRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Byte* buffer)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .GetStringUtfRegion(stringRef, start, count, (ValPtr<Byte>)(IntPtr)buffer);

	[UnmanagedCallersOnly]
	private static Byte* GetPrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (Byte*)NativeInterfaceHelper.proxies[envRef]
		                               .GetPrimitiveArrayCritical(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static void ReleasePrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, Byte* critical,
		JReleaseMode mode)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleasePrimitiveArrayCritical(arrayRef, (ValPtr<Byte>)(IntPtr)critical, mode);

	[UnmanagedCallersOnly]
	private static Char* GetStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Char*)NativeInterfaceHelper.proxies[envRef].GetStringCritical(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                               .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* critical)
		=> NativeInterfaceHelper.proxies[envRef]
		                        .ReleaseStringCritical(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)critical);

	[UnmanagedCallersOnly]
	private static JWeakRef NewWeakGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].NewWeakGlobalRef(localRef);
	[UnmanagedCallersOnly]
	private static void DeleteWeakGlobalRef(JEnvironmentRef envRef, JWeakRef weakRef)
		=> NativeInterfaceHelper.proxies[envRef].DeleteWeakGlobalRef(weakRef);

	[UnmanagedCallersOnly]
	private static JBoolean ExceptionCheck(JEnvironmentRef envRef)
		=> NativeInterfaceHelper.proxies[envRef].ExceptionCheck();

	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewDirectByteBuffer(JEnvironmentRef envRef, IntPtr address, Int64 capacity)
		=> NativeInterfaceHelper.proxies[envRef].NewDirectByteBuffer(address, capacity);
	[UnmanagedCallersOnly]
	private static IntPtr GetDirectBufferAddress(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
		=> NativeInterfaceHelper.proxies[envRef].GetDirectBufferAddress(bufferRef);
	[UnmanagedCallersOnly]
	private static Int64 GetDirectBufferCapacity(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
		=> NativeInterfaceHelper.proxies[envRef].GetDirectBufferCapacity(bufferRef);

	[UnmanagedCallersOnly]
	private static JReferenceType GetObjectRefType(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> NativeInterfaceHelper.proxies[envRef].GetObjectRefType(localRef);

	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetModule(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> NativeInterfaceHelper.proxies[envRef].GetModule(classRef);

	[UnmanagedCallersOnly]
	private static JBoolean IsVirtualThread(JEnvironmentRef envRef, JObjectLocalRef threadRef)
		=> NativeInterfaceHelper.proxies[envRef].IsVirtualThread(threadRef);
}