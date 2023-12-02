namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate Int32 GetArrayLengthDelegate(JEnvironmentRef env, JArrayLocalRef array);

internal delegate JArrayLocalRef NewObjectArrayDelegate(JEnvironmentRef env, Int32 length, JClassLocalRef jClass,
	JObjectLocalRef init);

internal delegate JObjectLocalRef GetObjectArrayElementDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	Int32 index);

internal delegate void SetObjectArrayElementDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 index,
	JObjectLocalRef obj);

internal delegate JArrayLocalRef NewBooleanArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewByteArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewCharArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewShortArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewIntArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewLongArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewFloatArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JArrayLocalRef NewDoubleArrayDelegate(JEnvironmentRef env, Int32 length);

internal delegate ValPtr<Byte> GetBooleanArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<SByte> GetByteArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Char> GetCharArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int16> GetShortArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int32> GetIntArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int64> GetLongArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Single> GetFloatArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Double> GetDoubleArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleaseBooleanArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<Byte> elements0, JReleaseMode mode);

internal delegate void ReleaseByteArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<SByte> elements0, JReleaseMode mode);

internal delegate void ReleaseCharArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, ReadOnlyValPtr<Char> elements0,
	JReleaseMode mode);

internal delegate void ReleaseShortArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<Int16> elements0, JReleaseMode mode);

internal delegate void ReleaseIntArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, ReadOnlyValPtr<Int32> elements0,
	JReleaseMode mode);

internal delegate void ReleaseLongArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<Int64> elements0, JReleaseMode mode);

internal delegate void ReleaseFloatArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<Single> elements0, JReleaseMode mode);

internal delegate void ReleaseDoubleArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	ReadOnlyValPtr<Double> elements0, JReleaseMode mode);

internal delegate void GetBooleanArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Byte> buffer0);

internal delegate void GetByteArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<SByte> buffer0);

internal delegate void GetCharArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Char> buffer0);

internal delegate void GetShortArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int16> buffer0);

internal delegate void GetIntArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int32> buffer0);

internal delegate void GetLongArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int64> buffer0);

internal delegate void GetFloatArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Single> buffer0);

internal delegate void GetDoubleArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Double> buffer0);

internal delegate void SetBooleanArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Byte> buffer0);

internal delegate void SetByteArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<SByte> buffer0);

internal delegate void SetCharArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Char> buffer0);

internal delegate void SetShortArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int16> buffer0);

internal delegate void SetIntArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int32> buffer0);

internal delegate void SetLongArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int64> buffer0);

internal delegate void SetFloatArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Single> buffer0);

internal delegate void SetDoubleArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Double> buffer0);

internal delegate IntPtr GetPrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleasePrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	IntPtr elements, JReleaseMode mode);