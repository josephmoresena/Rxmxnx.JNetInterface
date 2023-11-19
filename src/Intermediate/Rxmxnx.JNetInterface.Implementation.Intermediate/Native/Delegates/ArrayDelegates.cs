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

internal delegate ref readonly Byte GetBooleanArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly SByte GetByteArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Char GetCharArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Int16 GetShortArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Int32 GetIntArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Int64 GetLongArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Single GetFloatArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate ref readonly Double GetDoubleArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleaseBooleanArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in Byte elements0, JReleaseMode mode);

internal delegate void ReleaseByteArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in SByte elements0, JReleaseMode mode);

internal delegate void ReleaseCharArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, in Char elements0,
	JReleaseMode mode);

internal delegate void ReleaseShortArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in Int16 elements0, JReleaseMode mode);

internal delegate void ReleaseIntArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, in Int32 elements0,
	JReleaseMode mode);

internal delegate void ReleaseLongArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in Int64 elements0, JReleaseMode mode);

internal delegate void ReleaseFloatArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in Single elements0, JReleaseMode mode);

internal delegate void ReleaseDoubleArrayElementsDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	in Double elements0, JReleaseMode mode);

internal delegate void GetBooleanArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Byte buffer0);

internal delegate void GetByteArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref SByte buffer0);

internal delegate void GetCharArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Char buffer0);

internal delegate void GetShortArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Int16 buffer0);

internal delegate void GetIntArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Int32 buffer0);

internal delegate void GetLongArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Int64 buffer0);

internal delegate void GetFloatArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Single buffer0);

internal delegate void GetDoubleArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, ref Double buffer0);

internal delegate void SetBooleanArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Byte buffer0);

internal delegate void SetByteArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in SByte buffer0);

internal delegate void SetCharArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Char buffer0);

internal delegate void SetShortArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Int16 buffer0);

internal delegate void SetIntArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Int32 buffer0);

internal delegate void SetLongArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Int64 buffer0);

internal delegate void SetFloatArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Single buffer0);

internal delegate void SetDoubleArrayRegionDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef, Int32 startIndex,
	Int32 length, in Double buffer0);

internal delegate IntPtr GetPrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	out Byte isCopy);

internal delegate void ReleasePrimitiveArrayCriticalDelegate(JEnvironmentRef env, JArrayLocalRef arrayRef,
	IntPtr elements, JReleaseMode mode);