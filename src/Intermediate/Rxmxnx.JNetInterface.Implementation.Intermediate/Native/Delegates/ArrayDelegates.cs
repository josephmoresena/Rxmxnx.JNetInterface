namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate Int32 GetArrayLengthDelegate(JEnvironmentRef env, JArrayLocalRef array);

internal delegate JObjectArrayLocalRef NewObjectArrayDelegate(JEnvironmentRef env, Int32 length, JClassLocalRef jClass,
	JObjectLocalRef init);

internal delegate JObjectLocalRef GetObjectArrayElementDelegate(JEnvironmentRef env, JObjectArrayLocalRef arrayRef,
	Int32 index);

internal delegate void SetObjectArrayElementDelegate(JEnvironmentRef env, JObjectArrayLocalRef arrayRef, Int32 index,
	JObjectLocalRef obj);

internal delegate JBooleanArrayLocalRef NewBooleanArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JByteArrayLocalRef NewByteArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JCharArrayLocalRef NewCharArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JShortArrayLocalRef NewShortArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JIntArrayLocalRef NewIntArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JLongArrayLocalRef NewLongArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JFloatArrayLocalRef NewFloatArrayDelegate(JEnvironmentRef env, Int32 length);
internal delegate JDoubleArrayLocalRef NewDoubleArrayDelegate(JEnvironmentRef env, Int32 length);

internal delegate ValPtr<Byte> GetBooleanArrayElementsDelegate(JEnvironmentRef env, JBooleanArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<SByte> GetByteArrayElementsDelegate(JEnvironmentRef env, JByteArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Char> GetCharArrayElementsDelegate(JEnvironmentRef env, JCharArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int16> GetShortArrayElementsDelegate(JEnvironmentRef env, JShortArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int32> GetIntArrayElementsDelegate(JEnvironmentRef env, JIntArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Int64> GetLongArrayElementsDelegate(JEnvironmentRef env, JLongArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Single> GetFloatArrayElementsDelegate(JEnvironmentRef env, JFloatArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ValPtr<Double> GetDoubleArrayElementsDelegate(JEnvironmentRef env, JDoubleArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleaseBooleanArrayElementsDelegate(JEnvironmentRef env, JBooleanArrayLocalRef arrayRef,
	ReadOnlyValPtr<Byte> elements0, JReleaseMode mode);

internal delegate void ReleaseByteArrayElementsDelegate(JEnvironmentRef env, JByteArrayLocalRef arrayRef,
	ReadOnlyValPtr<SByte> elements0, JReleaseMode mode);

internal delegate void ReleaseCharArrayElementsDelegate(JEnvironmentRef env, JCharArrayLocalRef arrayRef,
	ReadOnlyValPtr<Char> elements0, JReleaseMode mode);

internal delegate void ReleaseShortArrayElementsDelegate(JEnvironmentRef env, JShortArrayLocalRef arrayRef,
	ReadOnlyValPtr<Int16> elements0, JReleaseMode mode);

internal delegate void ReleaseIntArrayElementsDelegate(JEnvironmentRef env, JIntArrayLocalRef arrayRef,
	ReadOnlyValPtr<Int32> elements0, JReleaseMode mode);

internal delegate void ReleaseLongArrayElementsDelegate(JEnvironmentRef env, JLongArrayLocalRef arrayRef,
	ReadOnlyValPtr<Int64> elements0, JReleaseMode mode);

internal delegate void ReleaseFloatArrayElementsDelegate(JEnvironmentRef env, JFloatArrayLocalRef arrayRef,
	ReadOnlyValPtr<Single> elements0, JReleaseMode mode);

internal delegate void ReleaseDoubleArrayElementsDelegate(JEnvironmentRef env, JDoubleArrayLocalRef arrayRef,
	ReadOnlyValPtr<Double> elements0, JReleaseMode mode);

internal delegate void GetBooleanArrayRegionDelegate(JEnvironmentRef env, JBooleanArrayLocalRef arrayRef,
	Int32 startIndex, Int32 length, ValPtr<Byte> buffer0);

internal delegate void GetByteArrayRegionDelegate(JEnvironmentRef env, JByteArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<SByte> buffer0);

internal delegate void GetCharArrayRegionDelegate(JEnvironmentRef env, JCharArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Char> buffer0);

internal delegate void GetShortArrayRegionDelegate(JEnvironmentRef env, JShortArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int16> buffer0);

internal delegate void GetIntArrayRegionDelegate(JEnvironmentRef env, JIntArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int32> buffer0);

internal delegate void GetLongArrayRegionDelegate(JEnvironmentRef env, JLongArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Int64> buffer0);

internal delegate void GetFloatArrayRegionDelegate(JEnvironmentRef env, JFloatArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ValPtr<Single> buffer0);

internal delegate void GetDoubleArrayRegionDelegate(JEnvironmentRef env, JDoubleArrayLocalRef arrayRef,
	Int32 startIndex, Int32 length, ValPtr<Double> buffer0);

internal delegate void SetBooleanArrayRegionDelegate(JEnvironmentRef env, JBooleanArrayLocalRef arrayRef,
	Int32 startIndex, Int32 length, ReadOnlyValPtr<Byte> buffer0);

internal delegate void SetByteArrayRegionDelegate(JEnvironmentRef env, JByteArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<SByte> buffer0);

internal delegate void SetCharArrayRegionDelegate(JEnvironmentRef env, JCharArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Char> buffer0);

internal delegate void SetShortArrayRegionDelegate(JEnvironmentRef env, JShortArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int16> buffer0);

internal delegate void SetIntArrayRegionDelegate(JEnvironmentRef env, JIntArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int32> buffer0);

internal delegate void SetLongArrayRegionDelegate(JEnvironmentRef env, JLongArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Int64> buffer0);

internal delegate void SetFloatArrayRegionDelegate(JEnvironmentRef env, JFloatArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ReadOnlyValPtr<Single> buffer0);

internal delegate void SetDoubleArrayRegionDelegate(JEnvironmentRef env, JDoubleArrayLocalRef arrayRef,
	Int32 startIndex, Int32 length, ReadOnlyValPtr<Double> buffer0);

internal delegate ValPtr<Byte> GetPrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleasePrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	IntPtr elements, JReleaseMode mode);