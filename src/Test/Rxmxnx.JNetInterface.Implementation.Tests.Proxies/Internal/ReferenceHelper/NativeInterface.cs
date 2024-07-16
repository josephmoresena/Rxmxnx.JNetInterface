namespace Rxmxnx.JNetInterface.Tests.Internal;

[ExcludeFromCodeCoverage]
internal unsafe partial class ReferenceHelper
{
	public static readonly InternalNativeInterface[] NativeInterface =
	[
		new()
		{
			GetVersion = &ReferenceHelper.GetVersion,
			DefineClass = &ReferenceHelper.DefineClass,
			FindClass = &ReferenceHelper.FindClass,
			FromReflectedMethod = &ReferenceHelper.FromReflectedMethod,
			FromReflectedField = &ReferenceHelper.FromReflectedField,
			ToReflectedMethod = &ReferenceHelper.ToReflectedMethod,
			GetSuperclass = &ReferenceHelper.GetSuperclass,
			IsAssignableFrom = &ReferenceHelper.IsAssignableFrom,
			ToReflectedField = &ReferenceHelper.ToReflectedField,
			Throw = &ReferenceHelper.Throw,
			ThrowNew = &ReferenceHelper.ThrowNew,
			ExceptionOccurred = &ReferenceHelper.ExceptionOccurred,
			ExceptionDescribe = &ReferenceHelper.ExceptionDescribe,
			ExceptionClear = &ReferenceHelper.ExceptionClear,
			FatalError = &ReferenceHelper.FatalError,
			PushLocalFrame = &ReferenceHelper.PushLocalFrame,
			PopLocalFrame = &ReferenceHelper.PopLocalFrame,
			NewGlobalRef = &ReferenceHelper.NewGlobalRef,
			DeleteGlobalRef = &ReferenceHelper.DeleteGlobalRef,
			DeleteLocalRef = &ReferenceHelper.DeleteLocalRef,
			IsSameObject = &ReferenceHelper.IsSameObject,
			NewLocalRef = &ReferenceHelper.NewLocalRef,
			EnsureLocalCapacity = &ReferenceHelper.EnsureLocalCapacity,
			AllocObject = &ReferenceHelper.AllocObject,
			NewObject = &ReferenceHelper.NewObject,
			GetObjectClass = &ReferenceHelper.GetObjectClass,
			IsInstanceOf = &ReferenceHelper.IsInstanceOf,
			GetMethodId = &ReferenceHelper.GetMethodId,
			CallObjectMethod = &ReferenceHelper.CallObjectMethod,
			CallBooleanMethod = &ReferenceHelper.CallBooleanMethod,
			CallByteMethod = &ReferenceHelper.CallByteMethod,
			CallCharMethod = &ReferenceHelper.CallCharMethod,
			CallShortMethod = &ReferenceHelper.CallShortMethod,
			CallIntMethod = &ReferenceHelper.CallIntMethod,
			CallLongMethod = &ReferenceHelper.CallLongMethod,
			CallFloatMethod = &ReferenceHelper.CallFloatMethod,
			CallDoubleMethod = &ReferenceHelper.CallDoubleMethod,
			CallVoidMethod = &ReferenceHelper.CallVoidMethod,
			CallNonVirtualObjectMethod = &ReferenceHelper.CallNonVirtualObjectMethod,
			CallNonVirtualBooleanMethod = &ReferenceHelper.CallNonVirtualBooleanMethod,
			CallNonVirtualByteMethod = &ReferenceHelper.CallNonVirtualByteMethod,
			CallNonVirtualCharMethod = &ReferenceHelper.CallNonVirtualCharMethod,
			CallNonVirtualShortMethod = &ReferenceHelper.CallNonVirtualShortMethod,
			CallNonVirtualIntMethod = &ReferenceHelper.CallNonVirtualIntMethod,
			CallNonVirtualLongMethod = &ReferenceHelper.CallNonVirtualLongMethod,
			CallNonVirtualFloatMethod = &ReferenceHelper.CallNonVirtualFloatMethod,
			CallNonVirtualDoubleMethod = &ReferenceHelper.CallNonVirtualDoubleMethod,
			CallNonVirtualVoidMethod = &ReferenceHelper.CallNonVirtualVoidMethod,
			GetFieldId = &ReferenceHelper.GetFieldId,
			GetObjectField = &ReferenceHelper.GetObjectField,
			GetBooleanField = &ReferenceHelper.GetBooleanField,
			GetByteField = &ReferenceHelper.GetByteField,
			GetCharField = &ReferenceHelper.GetCharField,
			GetShortField = &ReferenceHelper.GetShortField,
			GetIntField = &ReferenceHelper.GetIntField,
			GetLongField = &ReferenceHelper.GetLongField,
			GetFloatField = &ReferenceHelper.GetFloatField,
			GetDoubleField = &ReferenceHelper.GetDoubleField,
			SetObjectField = &ReferenceHelper.SetObjectField,
			SetBooleanField = &ReferenceHelper.SetBooleanField,
			SetByteField = &ReferenceHelper.SetByteField,
			SetCharField = &ReferenceHelper.SetCharField,
			SetShortField = &ReferenceHelper.SetShortField,
			SetIntField = &ReferenceHelper.SetIntField,
			SetLongField = &ReferenceHelper.SetLongField,
			SetFloatField = &ReferenceHelper.SetFloatField,
			SetDoubleField = &ReferenceHelper.SetDoubleField,
			GetStaticMethodId = &ReferenceHelper.GetStaticMethodId,
			CallStaticObjectMethod = &ReferenceHelper.CallStaticObjectMethod,
			CallStaticBooleanMethod = &ReferenceHelper.CallStaticBooleanMethod,
			CallStaticByteMethod = &ReferenceHelper.CallStaticByteMethod,
			CallStaticCharMethod = &ReferenceHelper.CallStaticCharMethod,
			CallStaticShortMethod = &ReferenceHelper.CallStaticShortMethod,
			CallStaticIntMethod = &ReferenceHelper.CallStaticIntMethod,
			CallStaticLongMethod = &ReferenceHelper.CallStaticLongMethod,
			CallStaticFloatMethod = &ReferenceHelper.CallStaticFloatMethod,
			CallStaticDoubleMethod = &ReferenceHelper.CallStaticDoubleMethod,
			CallStaticVoidMethod = &ReferenceHelper.CallStaticVoidMethod,
			GetStaticFieldId = &ReferenceHelper.GetStaticFieldId,
			GetStaticObjectField = &ReferenceHelper.GetStaticObjectField,
			GetStaticBooleanField = &ReferenceHelper.GetStaticBooleanField,
			GetStaticByteField = &ReferenceHelper.GetStaticByteField,
			GetStaticCharField = &ReferenceHelper.GetStaticCharField,
			GetStaticShortField = &ReferenceHelper.GetStaticShortField,
			GetStaticIntField = &ReferenceHelper.GetStaticIntField,
			GetStaticLongField = &ReferenceHelper.GetStaticLongField,
			GetStaticFloatField = &ReferenceHelper.GetStaticFloatField,
			GetStaticDoubleField = &ReferenceHelper.GetStaticDoubleField,
			SetStaticObjectField = &ReferenceHelper.SetStaticObjectField,
			SetStaticBooleanField = &ReferenceHelper.SetStaticBooleanField,
			SetStaticByteField = &ReferenceHelper.SetStaticByteField,
			SetStaticCharField = &ReferenceHelper.SetStaticCharField,
			SetStaticShortField = &ReferenceHelper.SetStaticShortField,
			SetStaticIntField = &ReferenceHelper.SetStaticIntField,
			SetStaticLongField = &ReferenceHelper.SetStaticLongField,
			SetStaticFloatField = &ReferenceHelper.SetStaticFloatField,
			SetStaticDoubleField = &ReferenceHelper.SetStaticDoubleField,
			NewString = &ReferenceHelper.NewString,
			GetStringLength = &ReferenceHelper.GetStringLength,
			GetStringChars = &ReferenceHelper.GetStringChars,
			ReleaseStringChars = &ReferenceHelper.ReleaseStringChars,
			NewStringUtf = &ReferenceHelper.NewStringUtf,
			GetStringUtfLength = &ReferenceHelper.GetStringUtfLength,
			GetStringUtfChars = &ReferenceHelper.GetStringUtfChars,
			ReleaseStringUtfChars = &ReferenceHelper.ReleaseStringUtfChars,
			GetArrayLength = &ReferenceHelper.GetArrayLength,
			NewObjectArray = &ReferenceHelper.NewObjectArray,
			GetObjectArrayElement = &ReferenceHelper.GetObjectArrayElement,
			SetObjectArrayElement = &ReferenceHelper.SetObjectArrayElement,
			NewBooleanArray = &ReferenceHelper.NewBooleanArray,
			NewByteArray = &ReferenceHelper.NewByteArray,
			NewCharArray = &ReferenceHelper.NewCharArray,
			NewShortArray = &ReferenceHelper.NewShortArray,
			NewIntArray = &ReferenceHelper.NewIntArray,
			NewLongArray = &ReferenceHelper.NewLongArray,
			NewFloatArray = &ReferenceHelper.NewFloatArray,
			NewDoubleArray = &ReferenceHelper.NewDoubleArray,
			GetBooleanArrayElements = &ReferenceHelper.GetBooleanArrayElements,
			GetByteArrayElements = &ReferenceHelper.GetByteArrayElements,
			GetCharArrayElements = &ReferenceHelper.GetCharArrayElements,
			GetShortArrayElements = &ReferenceHelper.GetShortArrayElements,
			GetIntArrayElements = &ReferenceHelper.GetIntArrayElements,
			GetLongArrayElements = &ReferenceHelper.GetLongArrayElements,
			GetFloatArrayElements = &ReferenceHelper.GetFloatArrayElements,
			GetDoubleArrayElements = &ReferenceHelper.GetDoubleArrayElements,
			ReleaseBooleanArrayElements = &ReferenceHelper.ReleaseBooleanArrayElements,
			ReleaseByteArrayElements = &ReferenceHelper.ReleaseByteArrayElements,
			ReleaseCharArrayElements = &ReferenceHelper.ReleaseCharArrayElements,
			ReleaseShortArrayElements = &ReferenceHelper.ReleaseShortArrayElements,
			ReleaseIntArrayElements = &ReferenceHelper.ReleaseIntArrayElements,
			ReleaseLongArrayElements = &ReferenceHelper.ReleaseLongArrayElements,
			ReleaseFloatArrayElements = &ReferenceHelper.ReleaseFloatArrayElements,
			ReleaseDoubleArrayElements = &ReferenceHelper.ReleaseDoubleArrayElements,
			GetBooleanArrayRegion = &ReferenceHelper.GetBooleanArrayRegion,
			GetByteArrayRegion = &ReferenceHelper.GetByteArrayRegion,
			GetCharArrayRegion = &ReferenceHelper.GetCharArrayRegion,
			GetShortArrayRegion = &ReferenceHelper.GetShortArrayRegion,
			GetIntArrayRegion = &ReferenceHelper.GetIntArrayRegion,
			GetLongArrayRegion = &ReferenceHelper.GetLongArrayRegion,
			GetFloatArrayRegion = &ReferenceHelper.GetFloatArrayRegion,
			GetDoubleArrayRegion = &ReferenceHelper.GetDoubleArrayRegion,
			RegisterNatives = &ReferenceHelper.RegisterNatives,
			UnregisterNatives = &ReferenceHelper.UnregisterNatives,
			MonitorEnter = &ReferenceHelper.MonitorEnter,
			MonitorExit = &ReferenceHelper.MonitorExit,
			GetVirtualMachine = &ReferenceHelper.GetVirtualMachine,
			GetStringRegion = &ReferenceHelper.GetStringRegion,
			GetStringUtfRegion = &ReferenceHelper.GetStringUtfRegion,
			GetPrimitiveArrayCritical = &ReferenceHelper.GetPrimitiveArrayCritical,
			ReleasePrimitiveArrayCritical = &ReferenceHelper.ReleasePrimitiveArrayCritical,
			GetStringCritical = &ReferenceHelper.GetStringCritical,
			ReleaseStringCritical = &ReferenceHelper.ReleaseStringCritical,
			NewWeakGlobalRef = &ReferenceHelper.NewWeakGlobalRef,
			DeleteWeakGlobalRef = &ReferenceHelper.DeleteWeakGlobalRef,
			ExceptionCheck = &ReferenceHelper.ExceptionCheck,
			NewDirectByteBuffer = &ReferenceHelper.NewDirectByteBuffer,
			GetDirectBufferAddress = &ReferenceHelper.GetDirectBufferAddress,
			GetDirectBufferCapacity = &ReferenceHelper.GetDirectBufferCapacity,
			GetObjectRefType = &ReferenceHelper.GetObjectRefType,
			GetModule = &ReferenceHelper.GetModule,
			IsVirtualThread = &ReferenceHelper.IsVirtualThread,
		},
		new()
		{
			GetVersion = &ReferenceHelper.GetVersion,
			DefineClass = &ReferenceHelper.DefineClass,
			FindClass = &ReferenceHelper.FindClass,
			FromReflectedMethod = &ReferenceHelper.FromReflectedMethod,
			FromReflectedField = &ReferenceHelper.FromReflectedField,
			ToReflectedMethod = &ReferenceHelper.ToReflectedMethod,
			GetSuperclass = &ReferenceHelper.GetSuperclass,
			IsAssignableFrom = &ReferenceHelper.IsAssignableFrom,
			ToReflectedField = &ReferenceHelper.ToReflectedField,
			Throw = &ReferenceHelper.Throw,
			ThrowNew = &ReferenceHelper.ThrowNew,
			ExceptionOccurred = &ReferenceHelper.ExceptionOccurred,
			ExceptionDescribe = &ReferenceHelper.ExceptionDescribe,
			ExceptionClear = &ReferenceHelper.ExceptionClear,
			FatalError = &ReferenceHelper.FatalError,
			PushLocalFrame = &ReferenceHelper.PushLocalFrame,
			PopLocalFrame = &ReferenceHelper.PopLocalFrame,
			NewGlobalRef = &ReferenceHelper.NewGlobalRef,
			DeleteGlobalRef = &ReferenceHelper.DeleteGlobalRef,
			DeleteLocalRef = &ReferenceHelper.DeleteLocalRef,
			IsSameObject = &ReferenceHelper.IsSameObject,
			NewLocalRef = &ReferenceHelper.NewLocalRef,
			EnsureLocalCapacity = &ReferenceHelper.EnsureLocalCapacity,
			AllocObject = &ReferenceHelper.AllocObject,
			NewObject = &ReferenceHelper.NewObject,
			GetObjectClass = &ReferenceHelper.GetObjectClass,
			IsInstanceOf = &ReferenceHelper.IsInstanceOf,
			GetMethodId = &ReferenceHelper.GetMethodId,
			CallObjectMethod = &ReferenceHelper.CallObjectMethod,
			CallBooleanMethod = &ReferenceHelper.CallBooleanMethod,
			CallByteMethod = &ReferenceHelper.CallByteMethod,
			CallCharMethod = &ReferenceHelper.CallCharMethod,
			CallShortMethod = &ReferenceHelper.CallShortMethod,
			CallIntMethod = &ReferenceHelper.CallIntMethod,
			CallLongMethod = &ReferenceHelper.CallLongMethod,
			CallFloatMethod = &ReferenceHelper.CallFloatMethod,
			CallDoubleMethod = &ReferenceHelper.CallDoubleMethod,
			CallVoidMethod = &ReferenceHelper.CallVoidMethod,
			CallNonVirtualObjectMethod = &ReferenceHelper.CallNonVirtualObjectMethod,
			CallNonVirtualBooleanMethod = &ReferenceHelper.CallNonVirtualBooleanMethod,
			CallNonVirtualByteMethod = &ReferenceHelper.CallNonVirtualByteMethod,
			CallNonVirtualCharMethod = &ReferenceHelper.CallNonVirtualCharMethod,
			CallNonVirtualShortMethod = &ReferenceHelper.CallNonVirtualShortMethod,
			CallNonVirtualIntMethod = &ReferenceHelper.CallNonVirtualIntMethod,
			CallNonVirtualLongMethod = &ReferenceHelper.CallNonVirtualLongMethod,
			CallNonVirtualFloatMethod = &ReferenceHelper.CallNonVirtualFloatMethod,
			CallNonVirtualDoubleMethod = &ReferenceHelper.CallNonVirtualDoubleMethod,
			CallNonVirtualVoidMethod = &ReferenceHelper.CallNonVirtualVoidMethod,
			GetFieldId = &ReferenceHelper.GetFieldId,
			GetObjectField = &ReferenceHelper.GetObjectField,
			GetBooleanField = &ReferenceHelper.GetBooleanField,
			GetByteField = &ReferenceHelper.GetByteField,
			GetCharField = &ReferenceHelper.GetCharField,
			GetShortField = &ReferenceHelper.GetShortField,
			GetIntField = &ReferenceHelper.GetIntField,
			GetLongField = &ReferenceHelper.GetLongField,
			GetFloatField = &ReferenceHelper.GetFloatField,
			GetDoubleField = &ReferenceHelper.GetDoubleField,
			SetObjectField = &ReferenceHelper.SetObjectField,
			SetBooleanField = &ReferenceHelper.SetBooleanField,
			SetByteField = &ReferenceHelper.SetByteField,
			SetCharField = &ReferenceHelper.SetCharField,
			SetShortField = &ReferenceHelper.SetShortField,
			SetIntField = &ReferenceHelper.SetIntField,
			SetLongField = &ReferenceHelper.SetLongField,
			SetFloatField = &ReferenceHelper.SetFloatField,
			SetDoubleField = &ReferenceHelper.SetDoubleField,
			GetStaticMethodId = &ReferenceHelper.GetStaticMethodId,
			CallStaticObjectMethod = &ReferenceHelper.CallStaticObjectMethod,
			CallStaticBooleanMethod = &ReferenceHelper.CallStaticBooleanMethod,
			CallStaticByteMethod = &ReferenceHelper.CallStaticByteMethod,
			CallStaticCharMethod = &ReferenceHelper.CallStaticCharMethod,
			CallStaticShortMethod = &ReferenceHelper.CallStaticShortMethod,
			CallStaticIntMethod = &ReferenceHelper.CallStaticIntMethod,
			CallStaticLongMethod = &ReferenceHelper.CallStaticLongMethod,
			CallStaticFloatMethod = &ReferenceHelper.CallStaticFloatMethod,
			CallStaticDoubleMethod = &ReferenceHelper.CallStaticDoubleMethod,
			CallStaticVoidMethod = &ReferenceHelper.CallStaticVoidMethod,
			GetStaticFieldId = &ReferenceHelper.GetStaticFieldId,
			GetStaticObjectField = &ReferenceHelper.GetStaticObjectField,
			GetStaticBooleanField = &ReferenceHelper.GetStaticBooleanField,
			GetStaticByteField = &ReferenceHelper.GetStaticByteField,
			GetStaticCharField = &ReferenceHelper.GetStaticCharField,
			GetStaticShortField = &ReferenceHelper.GetStaticShortField,
			GetStaticIntField = &ReferenceHelper.GetStaticIntField,
			GetStaticLongField = &ReferenceHelper.GetStaticLongField,
			GetStaticFloatField = &ReferenceHelper.GetStaticFloatField,
			GetStaticDoubleField = &ReferenceHelper.GetStaticDoubleField,
			SetStaticObjectField = &ReferenceHelper.SetStaticObjectField,
			SetStaticBooleanField = &ReferenceHelper.SetStaticBooleanField,
			SetStaticByteField = &ReferenceHelper.SetStaticByteField,
			SetStaticCharField = &ReferenceHelper.SetStaticCharField,
			SetStaticShortField = &ReferenceHelper.SetStaticShortField,
			SetStaticIntField = &ReferenceHelper.SetStaticIntField,
			SetStaticLongField = &ReferenceHelper.SetStaticLongField,
			SetStaticFloatField = &ReferenceHelper.SetStaticFloatField,
			SetStaticDoubleField = &ReferenceHelper.SetStaticDoubleField,
			NewString = &ReferenceHelper.NewString,
			GetStringLength = &ReferenceHelper.GetStringLength,
			GetStringChars = &ReferenceHelper.GetStringChars,
			ReleaseStringChars = &ReferenceHelper.ReleaseStringChars,
			NewStringUtf = &ReferenceHelper.NewStringUtf,
			GetStringUtfLength = &ReferenceHelper.GetStringUtfLength,
			GetStringUtfChars = &ReferenceHelper.GetStringUtfChars,
			ReleaseStringUtfChars = &ReferenceHelper.ReleaseStringUtfChars,
			GetArrayLength = &ReferenceHelper.GetArrayLength,
			NewObjectArray = &ReferenceHelper.NewObjectArray,
			GetObjectArrayElement = &ReferenceHelper.GetObjectArrayElement,
			SetObjectArrayElement = &ReferenceHelper.SetObjectArrayElement,
			NewBooleanArray = &ReferenceHelper.NewBooleanArray,
			NewByteArray = &ReferenceHelper.NewByteArray,
			NewCharArray = &ReferenceHelper.NewCharArray,
			NewShortArray = &ReferenceHelper.NewShortArray,
			NewIntArray = &ReferenceHelper.NewIntArray,
			NewLongArray = &ReferenceHelper.NewLongArray,
			NewFloatArray = &ReferenceHelper.NewFloatArray,
			NewDoubleArray = &ReferenceHelper.NewDoubleArray,
			GetBooleanArrayElements = &ReferenceHelper.GetBooleanArrayElements,
			GetByteArrayElements = &ReferenceHelper.GetByteArrayElements,
			GetCharArrayElements = &ReferenceHelper.GetCharArrayElements,
			GetShortArrayElements = &ReferenceHelper.GetShortArrayElements,
			GetIntArrayElements = &ReferenceHelper.GetIntArrayElements,
			GetLongArrayElements = &ReferenceHelper.GetLongArrayElements,
			GetFloatArrayElements = &ReferenceHelper.GetFloatArrayElements,
			GetDoubleArrayElements = &ReferenceHelper.GetDoubleArrayElements,
			ReleaseBooleanArrayElements = &ReferenceHelper.ReleaseBooleanArrayElements,
			ReleaseByteArrayElements = &ReferenceHelper.ReleaseByteArrayElements,
			ReleaseCharArrayElements = &ReferenceHelper.ReleaseCharArrayElements,
			ReleaseShortArrayElements = &ReferenceHelper.ReleaseShortArrayElements,
			ReleaseIntArrayElements = &ReferenceHelper.ReleaseIntArrayElements,
			ReleaseLongArrayElements = &ReferenceHelper.ReleaseLongArrayElements,
			ReleaseFloatArrayElements = &ReferenceHelper.ReleaseFloatArrayElements,
			ReleaseDoubleArrayElements = &ReferenceHelper.ReleaseDoubleArrayElements,
			GetBooleanArrayRegion = &ReferenceHelper.GetBooleanArrayRegion,
			GetByteArrayRegion = &ReferenceHelper.GetByteArrayRegion,
			GetCharArrayRegion = &ReferenceHelper.GetCharArrayRegion,
			GetShortArrayRegion = &ReferenceHelper.GetShortArrayRegion,
			GetIntArrayRegion = &ReferenceHelper.GetIntArrayRegion,
			GetLongArrayRegion = &ReferenceHelper.GetLongArrayRegion,
			GetFloatArrayRegion = &ReferenceHelper.GetFloatArrayRegion,
			GetDoubleArrayRegion = &ReferenceHelper.GetDoubleArrayRegion,
			RegisterNatives = &ReferenceHelper.RegisterNatives,
			UnregisterNatives = &ReferenceHelper.UnregisterNatives,
			MonitorEnter = &ReferenceHelper.MonitorEnter,
			MonitorExit = &ReferenceHelper.MonitorExit,
			GetVirtualMachine = &ReferenceHelper.GetVirtualMachine,
			GetStringRegion = &ReferenceHelper.GetStringRegion,
			GetStringUtfRegion = &ReferenceHelper.GetStringUtfRegion,
			GetPrimitiveArrayCritical = &ReferenceHelper.GetPrimitiveArrayCritical,
			ReleasePrimitiveArrayCritical = &ReferenceHelper.ReleasePrimitiveArrayCritical,
			GetStringCritical = &ReferenceHelper.GetStringCritical,
			ReleaseStringCritical = &ReferenceHelper.ReleaseStringCritical,
			NewWeakGlobalRef = &ReferenceHelper.NewWeakGlobalRef,
			DeleteWeakGlobalRef = &ReferenceHelper.DeleteWeakGlobalRef,
			ExceptionCheck = &ReferenceHelper.ExceptionCheck,
			NewDirectByteBuffer = &ReferenceHelper.NewDirectByteBuffer,
			GetDirectBufferAddress = &ReferenceHelper.GetDirectBufferAddress,
			GetDirectBufferCapacity = &ReferenceHelper.GetDirectBufferCapacity,
			GetObjectRefType = &ReferenceHelper.GetObjectRefType,
			GetModule = &ReferenceHelper.GetModule,
			IsVirtualThread = &ReferenceHelper.IsVirtualThread,
		},
	];

	[UnmanagedCallersOnly]
	private static Int32 GetVersion(JEnvironmentRef envRef) => ReferenceHelper.GetProxy(envRef).GetVersion();

	[UnmanagedCallersOnly]
	private static JClassLocalRef DefineClass(JEnvironmentRef envRef, Byte* className, JObjectLocalRef classLoader,
		IntPtr byteCode, Int32 byteLength)
		=> ReferenceHelper.GetProxy(envRef)
		                  .DefineClass((ReadOnlyValPtr<Byte>)(IntPtr)className, classLoader, byteCode, byteLength);
	[UnmanagedCallersOnly]
	private static JClassLocalRef FindClass(JEnvironmentRef envRef, Byte* className)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (!proxy.UseDefaultClassRef) proxy.FindClass((ReadOnlyValPtr<Byte>)(IntPtr)className);
		return proxy.GetMainClassLocalRef(className) ?? proxy.FindClass((ReadOnlyValPtr<Byte>)(IntPtr)className);
	}
	[UnmanagedCallersOnly]
	private static JMethodId FromReflectedMethod(JEnvironmentRef envRef, JObjectLocalRef executableRef)
		=> ReferenceHelper.GetProxy(envRef).FromReflectedMethod(executableRef);
	[UnmanagedCallersOnly]
	private static JFieldId FromReflectedField(JEnvironmentRef envRef, JObjectLocalRef fieldRef)
		=> ReferenceHelper.GetProxy(envRef).FromReflectedField(fieldRef);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JBoolean isStatic)
		=> ReferenceHelper.GetProxy(envRef).ToReflectedMethod(classRef, methodId, isStatic);
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetSuperclass(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> ReferenceHelper.GetProxy(envRef).GetSuperclass(classRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsAssignableFrom(JEnvironmentRef envRef, JClassLocalRef classFromRef,
		JClassLocalRef classToRef)
		=> ReferenceHelper.GetProxy(envRef).IsAssignableFrom(classFromRef, classToRef);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean isStatic)
		=> ReferenceHelper.GetProxy(envRef).ToReflectedField(classRef, fieldId, isStatic);

	[UnmanagedCallersOnly]
	private static JResult Throw(JEnvironmentRef envRef, JThrowableLocalRef throwableRef)
		=> ReferenceHelper.GetProxy(envRef).Throw(throwableRef);
	[UnmanagedCallersOnly]
	private static JResult ThrowNew(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* message)
		=> ReferenceHelper.GetProxy(envRef).ThrowNew(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)message);
	[UnmanagedCallersOnly]
	private static JThrowableLocalRef ExceptionOccurred(JEnvironmentRef envRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).ExceptionOccurred();
		}
		catch (Exception)
		{
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void ExceptionDescribe(JEnvironmentRef envRef)
		=> ReferenceHelper.GetProxy(envRef).ExceptionDescribe();
	[UnmanagedCallersOnly]
	private static void ExceptionClear(JEnvironmentRef envRef) => ReferenceHelper.GetProxy(envRef).ExceptionClear();
	[UnmanagedCallersOnly]
	private static void FatalError(JEnvironmentRef envRef, Byte* message)
		=> ReferenceHelper.GetProxy(envRef).FatalError((ReadOnlyValPtr<Byte>)(IntPtr)message);

	[UnmanagedCallersOnly]
	private static JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
		=> ReferenceHelper.GetProxy(envRef).PushLocalFrame(capacity);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).PopLocalFrame(localRef);
	[UnmanagedCallersOnly]
	private static JGlobalRef NewGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (!proxy.UseDefaultClassRef) return proxy.NewGlobalRef(localRef);
		return proxy.GetMainClassGlobalRef(JClassLocalRef.FromReference(in localRef)) ?? proxy.NewGlobalRef(localRef);
	}
	[UnmanagedCallersOnly]
	private static void DeleteGlobalRef(JEnvironmentRef envRef, JGlobalRef globalRef)
		=> ReferenceHelper.GetProxy(envRef).DeleteGlobalRef(globalRef);
	[UnmanagedCallersOnly]
	private static void DeleteLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).DeleteLocalRef(localRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsSameObject(JEnvironmentRef envRef, JObjectLocalRef localRef1, JObjectLocalRef localRef2)
		=> ReferenceHelper.GetProxy(envRef).IsSameObject(localRef1, localRef2);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (proxy.UseDefaultClassRef)
		{
			if (proxy.VirtualMachine.ClassGlobalRef.Value == localRef)
				return proxy.ClassLocalRef.Value;
			if (proxy.VirtualMachine.ThrowableGlobalRef.Value == localRef)
				return proxy.ThrowableLocalRef.Value;
			if (proxy.VirtualMachine.StackTraceElementGlobalRef.Value == localRef)
				return proxy.StackTraceObjectLocalRef.Value;
			if (proxy.VirtualMachine.VoidGlobalRef.Value == localRef)
				return proxy.VoidPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.BooleanGlobalRef.Value == localRef)
				return proxy.BooleanPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.ByteGlobalRef.Value == localRef)
				return proxy.BytePrimitiveClassRef.Value;
			if (proxy.VirtualMachine.CharGlobalRef.Value == localRef)
				return proxy.CharPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.DoubleGlobalRef.Value == localRef)
				return proxy.DoublePrimitiveClassRef.Value;
			if (proxy.VirtualMachine.FloatGlobalRef.Value == localRef)
				return proxy.FloatPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.IntGlobalRef.Value == localRef)
				return proxy.IntPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.LongGlobalRef.Value == localRef)
				return proxy.LongPrimitiveClassRef.Value;
			if (proxy.VirtualMachine.ShortGlobalRef.Value == localRef)
				return proxy.ShortPrimitiveClassRef.Value;
		}
		return proxy.NewLocalRef(localRef);
	}
	[UnmanagedCallersOnly]
	private static JResult EnsureLocalCapacity(JEnvironmentRef envRef, Int32 capacity)
		=> ReferenceHelper.GetProxy(envRef).EnsureLocalCapacity(capacity);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef AllocObject(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> ReferenceHelper.GetProxy(envRef).AllocObject(classRef);
	private static JObjectLocalRef NewObject(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId constructorId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .NewObject(classRef, constructorId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetObjectClass(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).GetObjectClass(localRef);
	[UnmanagedCallersOnly]
	private static JBoolean IsInstanceOf(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef)
		=> ReferenceHelper.GetProxy(envRef).IsInstanceOf(localRef, classRef);

	[UnmanagedCallersOnly]
	private static JMethodId GetMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		return (proxy.UseDefaultClassRef ? proxy.GetMainMethodId(classRef, methodName) : default) ?? proxy.GetMethodId(
			classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName, (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
	}
	private static JObjectLocalRef CallObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallObjectMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JBoolean CallBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallBooleanMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JByte CallByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallByteMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JChar CallCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallCharMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JShort CallShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallShortMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JInt CallIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallIntMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JLong CallLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallLongMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JFloat CallFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallFloatMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JDouble CallDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallDoubleMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallVoidMethod(localRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JObjectLocalRef CallNonVirtualObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualObjectMethod(localRef, classRef, methodId,
		                                              (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JBoolean CallNonVirtualBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualBooleanMethod(localRef, classRef, methodId,
		                                               (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JByte CallNonVirtualByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualByteMethod(localRef, classRef, methodId,
		                                            (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JChar CallNonVirtualCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualCharMethod(localRef, classRef, methodId,
		                                            (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JShort CallNonVirtualShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualShortMethod(localRef, classRef, methodId,
		                                             (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JInt CallNonVirtualIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualIntMethod(localRef, classRef, methodId,
		                                           (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JLong CallNonVirtualLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualLongMethod(localRef, classRef, methodId,
		                                            (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JFloat CallNonVirtualFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualFloatMethod(localRef, classRef, methodId,
		                                             (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JDouble CallNonVirtualDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualDoubleMethod(localRef, classRef, methodId,
		                                              (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallNonVirtualVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallNonVirtualVoidMethod(localRef, classRef, methodId,
		                                            (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);

	[UnmanagedCallersOnly]
	private static JFieldId GetFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
		=> ReferenceHelper.GetProxy(envRef).GetFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
		                                               (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
	private static JObjectLocalRef GetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetObjectField(localRef, fieldId);
	private static JBoolean GetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetBooleanField(localRef, fieldId);
	private static JByte GetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetByteField(localRef, fieldId);
	private static JChar GetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetCharField(localRef, fieldId);
	private static JShort GetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetShortField(localRef, fieldId);
	private static JInt GetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetIntField(localRef, fieldId);
	private static JLong GetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetLongField(localRef, fieldId);
	private static JFloat GetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetFloatField(localRef, fieldId);
	private static JDouble GetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetDoubleField(localRef, fieldId);
	private static void SetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JObjectLocalRef value)
		=> ReferenceHelper.GetProxy(envRef).SetObjectField(localRef, fieldId, value);
	private static void SetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JBoolean value)
		=> ReferenceHelper.GetProxy(envRef).SetBooleanField(localRef, fieldId, value);
	private static void SetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JByte value)
		=> ReferenceHelper.GetProxy(envRef).SetByteField(localRef, fieldId, value);
	private static void SetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JChar value)
		=> ReferenceHelper.GetProxy(envRef).SetCharField(localRef, fieldId, value);
	private static void SetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JShort value)
		=> ReferenceHelper.GetProxy(envRef).SetShortField(localRef, fieldId, value);
	private static void SetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JInt value)
		=> ReferenceHelper.GetProxy(envRef).SetIntField(localRef, fieldId, value);
	private static void SetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JLong value)
		=> ReferenceHelper.GetProxy(envRef).SetLongField(localRef, fieldId, value);
	private static void SetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JFloat value)
		=> ReferenceHelper.GetProxy(envRef).SetFloatField(localRef, fieldId, value);
	private static void SetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JDouble value)
		=> ReferenceHelper.GetProxy(envRef).SetDoubleField(localRef, fieldId, value);

	[UnmanagedCallersOnly]
	private static JMethodId GetStaticMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
		=> ReferenceHelper.GetProxy(envRef).GetStaticMethodId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName,
		                                                      (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
	private static JObjectLocalRef CallStaticObjectMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticObjectMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JBoolean CallStaticBooleanMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticBooleanMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JByte CallStaticByteMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticByteMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JChar CallStaticCharMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticCharMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JShort CallStaticShortMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticShortMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JInt CallStaticIntMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticIntMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JLong CallStaticLongMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticLongMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JFloat CallStaticFloatMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticFloatMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	private static JDouble CallStaticDoubleMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticDoubleMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);
	[UnmanagedCallersOnly]
	private static void CallStaticVoidMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValueWrapper* args)
		=> ReferenceHelper.GetProxy(envRef)
		                  .CallStaticVoidMethod(classRef, methodId, (ReadOnlyValPtr<JValueWrapper>)(IntPtr)args);

	[UnmanagedCallersOnly]
	private static JFieldId GetStaticFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		return (proxy.UseDefaultClassRef ? proxy.GetPrimitiveWrapperClassTypeField(classRef, fieldName) : default) ??
			proxy.GetStaticFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
			                       (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
	}
	private static JObjectLocalRef GetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef,
		JFieldId fieldId)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (!proxy.UseDefaultClassRef) return proxy.GetStaticObjectField(classRef, fieldId);
		return proxy.GetPrimitiveClass(classRef, fieldId)?.Value ?? proxy.GetStaticObjectField(classRef, fieldId);
	}
	private static JBoolean GetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticBooleanField(classRef, fieldId);
	private static JByte GetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticByteField(classRef, fieldId);
	private static JChar GetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticCharField(classRef, fieldId);
	private static JShort GetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticShortField(classRef, fieldId);
	private static JInt GetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticIntField(classRef, fieldId);
	private static JLong GetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticLongField(classRef, fieldId);
	private static JFloat GetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticFloatField(classRef, fieldId);
	private static JDouble GetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
		=> ReferenceHelper.GetProxy(envRef).GetStaticDoubleField(classRef, fieldId);
	private static void SetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JObjectLocalRef value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticObjectField(classRef, fieldId, value);
	private static void SetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticBooleanField(classRef, fieldId, value);
	private static void SetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JByte value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticByteField(classRef, fieldId, value);
	private static void SetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JChar value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticCharField(classRef, fieldId, value);
	private static void SetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JShort value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticShortField(classRef, fieldId, value);
	private static void SetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId, JInt value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticIntField(classRef, fieldId, value);
	private static void SetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JLong value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticLongField(classRef, fieldId, value);
	private static void SetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JFloat value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticFloatField(classRef, fieldId, value);
	private static void SetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JDouble value)
		=> ReferenceHelper.GetProxy(envRef).SetStaticDoubleField(classRef, fieldId, value);

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewString(JEnvironmentRef envRef, Char* chars, Int32 length)
		=> ReferenceHelper.GetProxy(envRef).NewString((ValPtr<Char>)(IntPtr)chars, length);
	[UnmanagedCallersOnly]
	private static Int32 GetStringLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> ReferenceHelper.GetProxy(envRef).GetStringLength(stringRef);
	[UnmanagedCallersOnly]
	private static Char* GetStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Char*)ReferenceHelper.GetProxy(envRef).GetStringChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* chars)
		=> ReferenceHelper.GetProxy(envRef).ReleaseStringChars(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)chars);

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewStringUtf(JEnvironmentRef envRef, Byte* chars)
		=> ReferenceHelper.GetProxy(envRef).NewStringUtf((ReadOnlyValPtr<Byte>)(IntPtr)chars);
	[UnmanagedCallersOnly]
	private static Int32 GetStringUtfLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
		=> ReferenceHelper.GetProxy(envRef).GetStringUtfLength(stringRef);
	[UnmanagedCallersOnly]
	private static Byte* GetStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Byte*)ReferenceHelper.GetProxy(envRef).GetStringUtfChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                         .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Byte* chars)
		=> ReferenceHelper.GetProxy(envRef).ReleaseStringUtfChars(stringRef, (ReadOnlyValPtr<Byte>)(IntPtr)chars);

	[UnmanagedCallersOnly]
	private static Int32 GetArrayLength(JEnvironmentRef envRef, JArrayLocalRef arrayRef)
		=> ReferenceHelper.GetProxy(envRef).GetArrayLength(arrayRef);

	[UnmanagedCallersOnly]
	private static JObjectArrayLocalRef NewObjectArray(JEnvironmentRef envRef, Int32 arrayLength,
		JClassLocalRef elementClass, JObjectLocalRef initialElement)
		=> ReferenceHelper.GetProxy(envRef).NewObjectArray(arrayLength, elementClass, initialElement);
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef,
		Int32 index)
		=> ReferenceHelper.GetProxy(envRef).GetObjectArrayElement(arrayRef, index);
	[UnmanagedCallersOnly]
	private static void SetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef, Int32 index,
		JObjectLocalRef value)
		=> ReferenceHelper.GetProxy(envRef).SetObjectArrayElement(arrayRef, index, value);

	[UnmanagedCallersOnly]
	private static JBooleanArrayLocalRef NewBooleanArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewBooleanArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JByteArrayLocalRef NewByteArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewByteArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JCharArrayLocalRef NewCharArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewCharArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JShortArrayLocalRef NewShortArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewShortArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JIntArrayLocalRef NewIntArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewIntArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JLongArrayLocalRef NewLongArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewLongArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JFloatArrayLocalRef NewFloatArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewFloatArray(arrayLength);
	[UnmanagedCallersOnly]
	private static JDoubleArrayLocalRef NewDoubleArray(JEnvironmentRef envRef, Int32 arrayLength)
		=> ReferenceHelper.GetProxy(envRef).NewDoubleArray(arrayLength);

	[UnmanagedCallersOnly]
	private static JBoolean* GetBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* isCopy)
		=> (JBoolean*)ReferenceHelper.GetProxy(envRef)
		                             .GetBooleanArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
	[UnmanagedCallersOnly]
	private static JByte* GetByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JByte*)ReferenceHelper.GetProxy(envRef).GetByteArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                          .Pointer;
	[UnmanagedCallersOnly]
	private static JChar* GetCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JChar*)ReferenceHelper.GetProxy(envRef).GetCharArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                          .Pointer;
	[UnmanagedCallersOnly]
	private static JShort* GetShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JShort*)ReferenceHelper.GetProxy(envRef).GetShortArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                           .Pointer;
	[UnmanagedCallersOnly]
	private static JInt* GetIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JInt*)ReferenceHelper.GetProxy(envRef).GetIntArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                         .Pointer;
	[UnmanagedCallersOnly]
	private static JLong* GetLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JLong*)ReferenceHelper.GetProxy(envRef).GetLongArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                          .Pointer;
	[UnmanagedCallersOnly]
	private static JFloat* GetFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (JFloat*)ReferenceHelper.GetProxy(envRef).GetFloatArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                           .Pointer;
	[UnmanagedCallersOnly]
	private static JDouble* GetDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JBoolean* isCopy)
		=> (JDouble*)ReferenceHelper.GetProxy(envRef).GetDoubleArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                            .Pointer;

	[UnmanagedCallersOnly]
	private static void ReleaseBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* elements, JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseBooleanArrayElements(arrayRef, (ReadOnlyValPtr<JBoolean>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JByte* elements,
		JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseByteArrayElements(arrayRef, (ReadOnlyValPtr<JByte>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JChar* elements,
		JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseCharArrayElements(arrayRef, (ReadOnlyValPtr<JChar>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef,
		JShort* elements, JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseShortArrayElements(arrayRef, (ReadOnlyValPtr<JShort>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JInt* elements,
		JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseIntArrayElements(arrayRef, (ReadOnlyValPtr<JInt>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JLong* elements,
		JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseLongArrayElements(arrayRef, (ReadOnlyValPtr<JLong>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef,
		JFloat* elements, JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseFloatArrayElements(arrayRef, (ReadOnlyValPtr<JFloat>)(IntPtr)elements, mode);
	[UnmanagedCallersOnly]
	private static void ReleaseDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JDouble* elements, JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleaseDoubleArrayElements(arrayRef, (ReadOnlyValPtr<JDouble>)(IntPtr)elements, mode);

	[UnmanagedCallersOnly]
	private static void GetBooleanArrayRegion(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef, Int32 start,
		Int32 count, JBoolean* buffer)
		=> ReferenceHelper.GetProxy(envRef)
		                  .GetBooleanArrayRegion(arrayRef, start, count, (ValPtr<JBoolean>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetByteArrayRegion(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, Int32 start,
		Int32 count, JByte* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetByteArrayRegion(arrayRef, start, count, (ValPtr<JByte>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetCharArrayRegion(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, Int32 start,
		Int32 count, JChar* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetCharArrayRegion(arrayRef, start, count, (ValPtr<JChar>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetShortArrayRegion(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, Int32 start,
		Int32 count, JShort* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetShortArrayRegion(arrayRef, start, count, (ValPtr<JShort>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetIntArrayRegion(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, Int32 start, Int32 count,
		JInt* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetIntArrayRegion(arrayRef, start, count, (ValPtr<JInt>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetLongArrayRegion(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, Int32 start,
		Int32 count, JLong* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetLongArrayRegion(arrayRef, start, count, (ValPtr<JLong>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetFloatArrayRegion(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, Int32 start,
		Int32 count, JFloat* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetFloatArrayRegion(arrayRef, start, count, (ValPtr<JFloat>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetDoubleArrayRegion(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef, Int32 start,
		Int32 count, JDouble* buffer)
		=> ReferenceHelper.GetProxy(envRef)
		                  .GetDoubleArrayRegion(arrayRef, start, count, (ValPtr<JDouble>)(IntPtr)buffer);

	[UnmanagedCallersOnly]
	private static JResult RegisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef,
		NativeMethodValueWrapper* methodEntries, Int32 count)
		=> ReferenceHelper.GetProxy(envRef)
		                  .RegisterNatives(classRef, (ReadOnlyValPtr<NativeMethodValueWrapper>)(IntPtr)methodEntries,
		                                   count);
	[UnmanagedCallersOnly]
	private static JResult UnregisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> ReferenceHelper.GetProxy(envRef).UnregisterNatives(classRef);

	[UnmanagedCallersOnly]
	private static JResult MonitorEnter(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).MonitorEnter(localRef);
	[UnmanagedCallersOnly]
	private static JResult MonitorExit(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).MonitorEnter(localRef);

	[UnmanagedCallersOnly]
	private static JResult GetVirtualMachine(JEnvironmentRef envRef, JVirtualMachineRef* vmRef)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (proxy is { UseVirtualMachineRef: false, })
			return proxy.GetVirtualMachine((ValPtr<JVirtualMachineRef>)(IntPtr)vmRef);

		Unsafe.AsRef<JVirtualMachineRef>(vmRef) = proxy.VirtualMachine.Reference;
		return JResult.Ok;
	}

	[UnmanagedCallersOnly]
	private static void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Char* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetStringRegion(stringRef, start, count, (ValPtr<Char>)(IntPtr)buffer);
	[UnmanagedCallersOnly]
	private static void GetStringUtfRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Byte* buffer)
		=> ReferenceHelper.GetProxy(envRef).GetStringUtfRegion(stringRef, start, count, (ValPtr<Byte>)(IntPtr)buffer);

	[UnmanagedCallersOnly]
	private static Byte* GetPrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, JBoolean* isCopy)
		=> (Byte*)ReferenceHelper.GetProxy(envRef).GetPrimitiveArrayCritical(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                         .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleasePrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, Byte* critical,
		JReleaseMode mode)
		=> ReferenceHelper.GetProxy(envRef)
		                  .ReleasePrimitiveArrayCritical(arrayRef, (ValPtr<Byte>)(IntPtr)critical, mode);

	[UnmanagedCallersOnly]
	private static Char* GetStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
		=> (Char*)ReferenceHelper.GetProxy(envRef).GetStringCritical(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
		                         .Pointer;
	[UnmanagedCallersOnly]
	private static void ReleaseStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* critical)
		=> ReferenceHelper.GetProxy(envRef).ReleaseStringCritical(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)critical);

	[UnmanagedCallersOnly]
	private static JWeakRef NewWeakGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
		=> ReferenceHelper.GetProxy(envRef).NewWeakGlobalRef(localRef);
	[UnmanagedCallersOnly]
	private static void DeleteWeakGlobalRef(JEnvironmentRef envRef, JWeakRef weakRef)
		=> ReferenceHelper.GetProxy(envRef).DeleteWeakGlobalRef(weakRef);

	[UnmanagedCallersOnly]
	private static JBoolean ExceptionCheck(JEnvironmentRef envRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).ExceptionCheck();
		}
		catch (Exception)
		{
			return false;
		}
	}

	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewDirectByteBuffer(JEnvironmentRef envRef, IntPtr address, Int64 capacity)
		=> ReferenceHelper.GetProxy(envRef).NewDirectByteBuffer(address, capacity);
	[UnmanagedCallersOnly]
	private static IntPtr GetDirectBufferAddress(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
		=> ReferenceHelper.GetProxy(envRef).GetDirectBufferAddress(bufferRef);
	[UnmanagedCallersOnly]
	private static Int64 GetDirectBufferCapacity(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
		=> ReferenceHelper.GetProxy(envRef).GetDirectBufferCapacity(bufferRef);

	[UnmanagedCallersOnly]
	private static JReferenceType GetObjectRefType(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
		if (proxy.UseDefaultClassRef)
		{
			if (proxy.VirtualMachine.ClassGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ThrowableGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.StackTraceElementGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.VoidGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.BooleanGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ByteGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.CharGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.DoubleGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.FloatGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.IntGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.LongGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ShortGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
		}
		return proxy.GetObjectRefType(localRef);
	}

	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetModule(JEnvironmentRef envRef, JClassLocalRef classRef)
		=> ReferenceHelper.GetProxy(envRef).GetModule(classRef);

	[UnmanagedCallersOnly]
	private static JBoolean IsVirtualThread(JEnvironmentRef envRef, JObjectLocalRef threadRef)
		=> ReferenceHelper.GetProxy(envRef).IsVirtualThread(threadRef);
}