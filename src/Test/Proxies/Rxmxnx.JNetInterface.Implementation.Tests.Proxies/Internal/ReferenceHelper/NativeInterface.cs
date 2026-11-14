// ReSharper disable DuplicatedSequentialIfBodies

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
			SetBooleanArrayRegion = &ReferenceHelper.SetBooleanArrayRegion,
			SetByteArrayRegion = &ReferenceHelper.SetByteArrayRegion,
			SetCharArrayRegion = &ReferenceHelper.SetCharArrayRegion,
			SetShortArrayRegion = &ReferenceHelper.SetShortArrayRegion,
			SetIntArrayRegion = &ReferenceHelper.SetIntArrayRegion,
			SetLongArrayRegion = &ReferenceHelper.SetLongArrayRegion,
			SetFloatArrayRegion = &ReferenceHelper.SetFloatArrayRegion,
			SetDoubleArrayRegion = &ReferenceHelper.SetDoubleArrayRegion,
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
			GetStringUtfLongLength = &ReferenceHelper.GetStringUtfLongLength,
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
			SetBooleanArrayRegion = &ReferenceHelper.SetBooleanArrayRegion,
			SetByteArrayRegion = &ReferenceHelper.SetByteArrayRegion,
			SetCharArrayRegion = &ReferenceHelper.SetCharArrayRegion,
			SetShortArrayRegion = &ReferenceHelper.SetShortArrayRegion,
			SetIntArrayRegion = &ReferenceHelper.SetIntArrayRegion,
			SetLongArrayRegion = &ReferenceHelper.SetLongArrayRegion,
			SetFloatArrayRegion = &ReferenceHelper.SetFloatArrayRegion,
			SetDoubleArrayRegion = &ReferenceHelper.SetDoubleArrayRegion,
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
			GetStringUtfLongLength = &ReferenceHelper.GetStringUtfLongLength,
		},
	];

	[UnmanagedCallersOnly]
	private static Int32 GetVersion(JEnvironmentRef envRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetVersion();
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JClassLocalRef DefineClass(JEnvironmentRef envRef, Byte* className, JObjectLocalRef classLoader,
		IntPtr byteCode, Int32 byteLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .DefineClass((ReadOnlyValPtr<Byte>)(IntPtr)className, classLoader, byteCode,
			                                   byteLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JClassLocalRef FindClass(JEnvironmentRef envRef, Byte* className)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (!proxy.UseDefaultClassRef) return proxy.FindClass((ReadOnlyValPtr<Byte>)(IntPtr)className);
			return proxy.GetMainClassLocalRef(className) ?? proxy.FindClass((ReadOnlyValPtr<Byte>)(IntPtr)className);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JMethodId FromReflectedMethod(JEnvironmentRef envRef, JObjectLocalRef executableRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).FromReflectedMethod(executableRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JFieldId FromReflectedField(JEnvironmentRef envRef, JObjectLocalRef fieldRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).FromReflectedField(fieldRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JBoolean isStatic)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).ToReflectedMethod(classRef, methodId, isStatic);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetSuperclass(JEnvironmentRef envRef, JClassLocalRef classRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetSuperclass(classRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean IsAssignableFrom(JEnvironmentRef envRef, JClassLocalRef classFromRef,
		JClassLocalRef classToRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).IsAssignableFrom(classFromRef, classToRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef ToReflectedField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean isStatic)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).ToReflectedField(classRef, fieldId, isStatic);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JResult Throw(JEnvironmentRef envRef, JThrowableLocalRef throwableRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).Throw(throwableRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult ThrowNew(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* message)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).ThrowNew(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)message);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
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
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).ExceptionDescribe();
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ExceptionClear(JEnvironmentRef envRef)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).ExceptionClear();
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void FatalError(JEnvironmentRef envRef, Byte* message)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).FatalError((ReadOnlyValPtr<Byte>)(IntPtr)message);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JResult PushLocalFrame(JEnvironmentRef envRef, Int32 capacity)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).PushLocalFrame(capacity);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef PopLocalFrame(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).PopLocalFrame(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JGlobalRef NewGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (!proxy.UseDefaultClassRef) return proxy.NewGlobalRef(localRef);
			return proxy.GetMainClassGlobalRef(new(localRef)) ?? proxy.NewGlobalRef(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void DeleteGlobalRef(JEnvironmentRef envRef, JGlobalRef globalRef)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).DeleteGlobalRef(globalRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void DeleteLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).DeleteLocalRef(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean IsSameObject(JEnvironmentRef envRef, JObjectLocalRef localRef1, JObjectLocalRef localRef2)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).IsSameObject(localRef1, localRef2);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewLocalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (!proxy.UseDefaultClassRef) return proxy.NewLocalRef(localRef);
			if (proxy.VirtualMachine.ClassGlobalRef.Value == localRef)
				return proxy.ClassLocalRef.Value;
			if (proxy.VirtualMachine.ThrowableGlobalRef.Value == localRef)
				return proxy.ThrowableLocalRef.Value;
			if (proxy.VirtualMachine.StackTraceElementGlobalRef.Value == localRef)
				return proxy.StackTraceElementLocalRef.Value;
			if (proxy.VirtualMachine.NumberGlobalRef.Value == localRef)
				return proxy.NumberObjectLocalRef.Value;

			if (proxy.VirtualMachine.VoidGlobalRef.Value == localRef)
				return proxy.VoidObjectLocalRef.Value;
			if (proxy.VirtualMachine.BooleanGlobalRef.Value == localRef)
				return proxy.BooleanObjectLocalRef.Value;
			if (proxy.VirtualMachine.ByteGlobalRef.Value == localRef)
				return proxy.ByteObjectLocalRef.Value;
			if (proxy.VirtualMachine.CharacterGlobalRef.Value == localRef)
				return proxy.CharacterObjectLocalRef.Value;
			if (proxy.VirtualMachine.DoubleGlobalRef.Value == localRef)
				return proxy.DoubleObjectLocalRef.Value;
			if (proxy.VirtualMachine.FloatGlobalRef.Value == localRef)
				return proxy.FloatObjectLocalRef.Value;
			if (proxy.VirtualMachine.IntegerGlobalRef.Value == localRef)
				return proxy.IntegerObjectLocalRef.Value;
			if (proxy.VirtualMachine.LongGlobalRef.Value == localRef)
				return proxy.LongObjectLocalRef.Value;
			if (proxy.VirtualMachine.ShortGlobalRef.Value == localRef)
				return proxy.ShortObjectLocalRef.Value;

			if (proxy.VirtualMachine.VoidPGlobalRef.Value == localRef)
				return proxy.VoidPrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.BooleanPGlobalRef.Value == localRef)
				return proxy.BooleanPrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.BytePGlobalRef.Value == localRef)
				return proxy.BytePrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.CharPGlobalRef.Value == localRef)
				return proxy.CharPrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.DoublePGlobalRef.Value == localRef)
				return proxy.DoublePrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.FloatPGlobalRef.Value == localRef)
				return proxy.FloatPrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.IntPGlobalRef.Value == localRef)
				return proxy.IntPrimitiveLocalRef.Value;
			if (proxy.VirtualMachine.LongPGlobalRef.Value == localRef)
				return proxy.LongPrimitiveLocalRef.Value;
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (proxy.VirtualMachine.ShortPGlobalRef.Value == localRef)
				return proxy.ShortPrimitiveLocalRef.Value;
			return proxy.NewLocalRef(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult EnsureLocalCapacity(JEnvironmentRef envRef, Int32 capacity)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).EnsureLocalCapacity(capacity);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef AllocObject(JEnvironmentRef envRef, JClassLocalRef classRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).AllocObject(classRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef NewObject(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId constructorId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .NewObject(classRef, constructorId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JClassLocalRef GetObjectClass(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetObjectClass(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean IsInstanceOf(JEnvironmentRef envRef, JObjectLocalRef localRef, JClassLocalRef classRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).IsInstanceOf(localRef, classRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JMethodId GetMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			return (proxy.UseDefaultClassRef ? proxy.GetMainMethodId(classRef, methodName) : default) ??
				proxy.GetMethodId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName,
				                  (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallObjectMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean CallBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallBooleanMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte CallByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallByteMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar CallCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallCharMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort CallShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallShortMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt CallIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallIntMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong CallLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallLongMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Single CallFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallFloatMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Double CallDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallDoubleMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void CallVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).CallVoidMethod(localRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallNonVirtualObjectMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualObjectMethod(localRef, classRef, methodId,
			                                                  (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean CallNonVirtualBooleanMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualBooleanMethod(localRef, classRef, methodId,
			                                                   (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte CallNonVirtualByteMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualByteMethod(localRef, classRef, methodId,
			                                                (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar CallNonVirtualCharMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualCharMethod(localRef, classRef, methodId,
			                                                (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort CallNonVirtualShortMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualShortMethod(localRef, classRef, methodId,
			                                                 (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt CallNonVirtualIntMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualIntMethod(localRef, classRef, methodId,
			                                               (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong CallNonVirtualLongMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualLongMethod(localRef, classRef, methodId,
			                                                (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Single CallNonVirtualFloatMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualFloatMethod(localRef, classRef, methodId,
			                                                 (ReadOnlyValPtr<JValue>)(IntPtr)args).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Double CallNonVirtualDoubleMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallNonVirtualDoubleMethod(localRef, classRef, methodId,
			                                                  (ReadOnlyValPtr<JValue>)(IntPtr)args).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void CallNonVirtualVoidMethod(JEnvironmentRef envRef, JObjectLocalRef localRef,
		JClassLocalRef classRef, JMethodId methodId, JValue* args)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .CallNonVirtualVoidMethod(localRef, classRef, methodId,
			                                         (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JFieldId GetFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
			                                                   (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetObjectField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean GetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetBooleanField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte GetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetByteField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar GetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetCharField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort GetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetShortField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt GetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetIntField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong GetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetLongField(localRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Single GetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetFloatField(localRef, fieldId).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Double GetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetDoubleField(localRef, fieldId).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void SetObjectField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JObjectLocalRef value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetObjectField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetBooleanField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId,
		JBoolean value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetBooleanField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetByteField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JByte value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetByteField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetCharField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JChar value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetCharField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetShortField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JShort value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetShortField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetIntField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JInt value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetIntField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetLongField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, JLong value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetLongField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetFloatField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, Single value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetFloatField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetDoubleField(JEnvironmentRef envRef, JObjectLocalRef localRef, JFieldId fieldId, Double value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetDoubleField(localRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JMethodId GetStaticMethodId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* methodName,
		Byte* methodDescriptor)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			return (proxy.UseDefaultClassRef ? proxy.GetMainStaticMethodId(classRef, methodName) : default) ??
				proxy.GetStaticMethodId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)methodName,
				                        (ReadOnlyValPtr<Byte>)(IntPtr)methodDescriptor);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef CallStaticObjectMethod(JEnvironmentRef envRef, JClassLocalRef classRef,
		JMethodId methodId, JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticObjectMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean CallStaticBooleanMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticBooleanMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte CallStaticByteMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticByteMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar CallStaticCharMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticCharMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort CallStaticShortMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticShortMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt CallStaticIntMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticIntMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong CallStaticLongMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticLongMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Single CallStaticFloatMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticFloatMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args)
			                      .Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Double CallStaticDoubleMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .CallStaticDoubleMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args)
			                      .Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void CallStaticVoidMethod(JEnvironmentRef envRef, JClassLocalRef classRef, JMethodId methodId,
		JValue* args)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .CallStaticVoidMethod(classRef, methodId, (ReadOnlyValPtr<JValue>)(IntPtr)args);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JFieldId GetStaticFieldId(JEnvironmentRef envRef, JClassLocalRef classRef, Byte* fieldName,
		Byte* fieldDescriptor)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			return (proxy.UseDefaultClassRef ? proxy.GetMainStaticFieldId(classRef, fieldName) : default) ??
				proxy.GetStaticFieldId(classRef, (ReadOnlyValPtr<Byte>)(IntPtr)fieldName,
				                       (ReadOnlyValPtr<Byte>)(IntPtr)fieldDescriptor);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef,
		JFieldId fieldId)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (!proxy.UseDefaultClassRef) return proxy.GetStaticObjectField(classRef, fieldId);
			return proxy.GetPrimitiveClass(classRef, fieldId)?.Value ?? proxy.GetStaticObjectField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JBoolean GetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticBooleanField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte GetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticByteField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar GetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticCharField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort GetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticShortField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt GetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticIntField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong GetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticLongField(classRef, fieldId);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Single GetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticFloatField(classRef, fieldId).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Double GetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStaticDoubleField(classRef, fieldId).Value;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticObjectField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JObjectLocalRef value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticObjectField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticBooleanField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JBoolean value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticBooleanField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticByteField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JByte value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticByteField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticCharField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JChar value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticCharField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticShortField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JShort value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticShortField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticIntField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId, JInt value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticIntField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticLongField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		JLong value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticLongField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticFloatField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		Single value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticFloatField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetStaticDoubleField(JEnvironmentRef envRef, JClassLocalRef classRef, JFieldId fieldId,
		Double value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetStaticDoubleField(classRef, fieldId, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewString(JEnvironmentRef envRef, Char* chars, Int32 length)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewString((ValPtr<Char>)(IntPtr)chars, length);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Int32 GetStringLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStringLength(stringRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Char* GetStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
	{
		try
		{
			return (Char*)ReferenceHelper.GetProxy(envRef).GetStringChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
			                             .Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseStringChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* chars)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).ReleaseStringChars(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)chars);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JStringLocalRef NewStringUtf(JEnvironmentRef envRef, Byte* chars)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewStringUtf((ReadOnlyValPtr<Byte>)(IntPtr)chars);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Int32 GetStringUtfLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStringUtfLength(stringRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Byte* GetStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
	{
		try
		{
			return (Byte*)ReferenceHelper.GetProxy(envRef)
			                             .GetStringUtfChars(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseStringUtfChars(JEnvironmentRef envRef, JStringLocalRef stringRef, Byte* chars)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).ReleaseStringUtfChars(stringRef, (ReadOnlyValPtr<Byte>)(IntPtr)chars);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static Int32 GetArrayLength(JEnvironmentRef envRef, JArrayLocalRef arrayRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetArrayLength(arrayRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JObjectArrayLocalRef NewObjectArray(JEnvironmentRef envRef, Int32 arrayLength,
		JClassLocalRef elementClass, JObjectLocalRef initialElement)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewObjectArray(arrayLength, elementClass, initialElement);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef,
		Int32 index)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetObjectArrayElement(arrayRef, index);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void SetObjectArrayElement(JEnvironmentRef envRef, JObjectArrayLocalRef arrayRef, Int32 index,
		JObjectLocalRef value)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).SetObjectArrayElement(arrayRef, index, value);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JBooleanArrayLocalRef NewBooleanArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewBooleanArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByteArrayLocalRef NewByteArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewByteArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JCharArrayLocalRef NewCharArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewCharArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShortArrayLocalRef NewShortArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewShortArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JIntArrayLocalRef NewIntArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewIntArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLongArrayLocalRef NewLongArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewLongArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JFloatArrayLocalRef NewFloatArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewFloatArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JDoubleArrayLocalRef NewDoubleArray(JEnvironmentRef envRef, Int32 arrayLength)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewDoubleArray(arrayLength);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JBoolean* GetBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* isCopy)
	{
		try
		{
			return (JBoolean*)ReferenceHelper.GetProxy(envRef)
			                                 .GetBooleanArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy)
			                                 .Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JByte* GetByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JByte*)ReferenceHelper.GetProxy(envRef)
			                              .GetByteArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JChar* GetCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JChar*)ReferenceHelper.GetProxy(envRef)
			                              .GetCharArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JShort* GetShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JShort*)ReferenceHelper.GetProxy(envRef)
			                               .GetShortArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JInt* GetIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JInt*)ReferenceHelper.GetProxy(envRef)
			                             .GetIntArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JLong* GetLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JLong*)ReferenceHelper.GetProxy(envRef)
			                              .GetLongArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JFloat* GetFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (JFloat*)ReferenceHelper.GetProxy(envRef)
			                               .GetFloatArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JDouble* GetDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JBoolean* isCopy)
	{
		try
		{
			return (JDouble*)ReferenceHelper.GetProxy(envRef)
			                                .GetDoubleArrayElements(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static void ReleaseBooleanArrayElements(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef,
		JBoolean* elements, JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseBooleanArrayElements(arrayRef, (ReadOnlyValPtr<JBoolean>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseByteArrayElements(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, JByte* elements,
		JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseByteArrayElements(arrayRef, (ReadOnlyValPtr<JByte>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseCharArrayElements(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, JChar* elements,
		JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseCharArrayElements(arrayRef, (ReadOnlyValPtr<JChar>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseShortArrayElements(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef,
		JShort* elements, JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseShortArrayElements(arrayRef, (ReadOnlyValPtr<JShort>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseIntArrayElements(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, JInt* elements,
		JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseIntArrayElements(arrayRef, (ReadOnlyValPtr<JInt>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseLongArrayElements(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, JLong* elements,
		JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseLongArrayElements(arrayRef, (ReadOnlyValPtr<JLong>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseFloatArrayElements(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef,
		JFloat* elements, JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseFloatArrayElements(arrayRef, (ReadOnlyValPtr<JFloat>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseDoubleArrayElements(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef,
		JDouble* elements, JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleaseDoubleArrayElements(arrayRef, (ReadOnlyValPtr<JDouble>)(IntPtr)elements, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static void GetBooleanArrayRegion(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef, Int32 start,
		Int32 count, JBoolean* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .GetBooleanArrayRegion(arrayRef, start, count, (ValPtr<JBoolean>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetByteArrayRegion(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, Int32 start,
		Int32 count, JByte* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetByteArrayRegion(arrayRef, start, count, (ValPtr<JByte>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetCharArrayRegion(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, Int32 start,
		Int32 count, JChar* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetCharArrayRegion(arrayRef, start, count, (ValPtr<JChar>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetShortArrayRegion(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, Int32 start,
		Int32 count, JShort* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .GetShortArrayRegion(arrayRef, start, count, (ValPtr<JShort>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetIntArrayRegion(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, Int32 start, Int32 count,
		JInt* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetIntArrayRegion(arrayRef, start, count, (ValPtr<JInt>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetLongArrayRegion(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, Int32 start,
		Int32 count, JLong* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetLongArrayRegion(arrayRef, start, count, (ValPtr<JLong>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetFloatArrayRegion(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, Int32 start,
		Int32 count, JFloat* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .GetFloatArrayRegion(arrayRef, start, count, (ValPtr<JFloat>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetDoubleArrayRegion(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef, Int32 start,
		Int32 count, JDouble* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .GetDoubleArrayRegion(arrayRef, start, count, (ValPtr<JDouble>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static void SetBooleanArrayRegion(JEnvironmentRef envRef, JBooleanArrayLocalRef arrayRef, Int32 start,
		Int32 count, JBoolean* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetBooleanArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JBoolean>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetByteArrayRegion(JEnvironmentRef envRef, JByteArrayLocalRef arrayRef, Int32 start,
		Int32 count, JByte* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetByteArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JByte>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetCharArrayRegion(JEnvironmentRef envRef, JCharArrayLocalRef arrayRef, Int32 start,
		Int32 count, JChar* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetCharArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JChar>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetShortArrayRegion(JEnvironmentRef envRef, JShortArrayLocalRef arrayRef, Int32 start,
		Int32 count, JShort* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetShortArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JShort>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetIntArrayRegion(JEnvironmentRef envRef, JIntArrayLocalRef arrayRef, Int32 start, Int32 count,
		JInt* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetIntArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JInt>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetLongArrayRegion(JEnvironmentRef envRef, JLongArrayLocalRef arrayRef, Int32 start,
		Int32 count, JLong* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetLongArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JLong>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetFloatArrayRegion(JEnvironmentRef envRef, JFloatArrayLocalRef arrayRef, Int32 start,
		Int32 count, JFloat* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetFloatArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JFloat>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void SetDoubleArrayRegion(JEnvironmentRef envRef, JDoubleArrayLocalRef arrayRef, Int32 start,
		Int32 count, JDouble* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .SetDoubleArrayRegion(arrayRef, start, count, (ReadOnlyValPtr<JDouble>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JResult RegisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef,
		NativeMethodValue* methodEntries, Int32 count)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef)
			                      .RegisterNatives(classRef, (ReadOnlyValPtr<NativeMethodValue>)(IntPtr)methodEntries,
			                                       count);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult UnregisterNatives(JEnvironmentRef envRef, JClassLocalRef classRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).UnregisterNatives(classRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JResult MonitorEnter(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).MonitorEnter(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static JResult MonitorExit(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).MonitorExit(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JResult GetVirtualMachine(JEnvironmentRef envRef, JVirtualMachineRef* vmRef)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (proxy is { UseVirtualMachineRef: false, })
				return proxy.GetVirtualMachine((ValPtr<JVirtualMachineRef>)(IntPtr)vmRef);

			Unsafe.AsRef<JVirtualMachineRef>(vmRef) = proxy.VirtualMachine.Reference;
			return JResult.Ok;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static void GetStringRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Char* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetStringRegion(stringRef, start, count, (ValPtr<Char>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}
	[UnmanagedCallersOnly]
	private static void GetStringUtfRegion(JEnvironmentRef envRef, JStringLocalRef stringRef, Int32 start, Int32 count,
		Byte* buffer)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).GetStringUtfRegion(stringRef, start, count, (ValPtr<Byte>)(IntPtr)buffer);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static Byte* GetPrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, JBoolean* isCopy)
	{
		try
		{
			return (Byte*)ReferenceHelper.GetProxy(envRef)
			                             .GetPrimitiveArrayCritical(arrayRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleasePrimitiveArrayCritical(JEnvironmentRef envRef, JArrayLocalRef arrayRef, Byte* critical,
		JReleaseMode mode)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef)
			               .ReleasePrimitiveArrayCritical(arrayRef, (ValPtr<Byte>)(IntPtr)critical, mode);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static Char* GetStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, JBoolean* isCopy)
	{
		try
		{
			return (Char*)ReferenceHelper.GetProxy(envRef)
			                             .GetStringCritical(stringRef, (ValPtr<JBoolean>)(IntPtr)isCopy).Pointer;
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void ReleaseStringCritical(JEnvironmentRef envRef, JStringLocalRef stringRef, Char* critical)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).ReleaseStringCritical(stringRef, (ReadOnlyValPtr<Char>)(IntPtr)critical);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

	[UnmanagedCallersOnly]
	private static JWeakRef NewWeakGlobalRef(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewWeakGlobalRef(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static void DeleteWeakGlobalRef(JEnvironmentRef envRef, JWeakRef weakRef)
	{
		try
		{
			ReferenceHelper.GetProxy(envRef).DeleteWeakGlobalRef(weakRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
		}
	}

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
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).NewDirectByteBuffer(address, capacity);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static IntPtr GetDirectBufferAddress(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetDirectBufferAddress(bufferRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
	[UnmanagedCallersOnly]
	private static Int64 GetDirectBufferCapacity(JEnvironmentRef envRef, JObjectLocalRef bufferRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetDirectBufferCapacity(bufferRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JReferenceType GetObjectRefType(JEnvironmentRef envRef, JObjectLocalRef localRef)
	{
		try
		{
			NativeInterfaceProxy proxy = ReferenceHelper.GetProxy(envRef);
			if (!proxy.UseDefaultClassRef) return proxy.GetObjectRefType(localRef);
			if (proxy.VirtualMachine.ClassGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ThrowableGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.StackTraceElementGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.NumberGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.SystemGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;

			if (proxy.VirtualMachine.VoidGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.BooleanGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ByteGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.CharacterGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.DoubleGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.FloatGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.IntegerGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.LongGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.ShortGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;

			if (proxy.VirtualMachine.VoidPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.BooleanPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.BytePGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.CharPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.DoublePGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.FloatPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.IntPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			if (proxy.VirtualMachine.LongPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (proxy.VirtualMachine.ShortPGlobalRef.Value == localRef)
				return JReferenceType.GlobalRefType;
			return proxy.GetObjectRefType(localRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JObjectLocalRef GetModule(JEnvironmentRef envRef, JClassLocalRef classRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetModule(classRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static JBoolean IsVirtualThread(JEnvironmentRef envRef, JObjectLocalRef threadRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).IsVirtualThread(threadRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}

	[UnmanagedCallersOnly]
	private static Int64 GetStringUtfLongLength(JEnvironmentRef envRef, JStringLocalRef stringRef)
	{
		try
		{
			return ReferenceHelper.GetProxy(envRef).GetStringUtfLongLength(stringRef);
		}
		catch (Exception ex)
		{
			JniUnmanagedFailure.Capture(ex);
			return default;
		}
	}
}