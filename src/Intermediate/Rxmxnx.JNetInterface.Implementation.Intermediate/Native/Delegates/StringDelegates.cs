namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JStringLocalRef NewStringDelegate(JEnvironmentRef env, in Char chars0, Int32 length);
internal delegate Int32 GetStringLengthDelegate(JEnvironmentRef env, JStringLocalRef jString);

internal delegate ref readonly Char GetStringCharsDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringCharsDelegate(JEnvironmentRef env, JStringLocalRef jString, in Char chars0);
internal delegate JStringLocalRef NewStringUtfDelegate(JEnvironmentRef env, in Byte utf8Chars0);
internal delegate Int32 GetStringUtfLengthDelegate(JEnvironmentRef env, JStringLocalRef jString);

internal delegate ref readonly Byte GetStringUtfCharsDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringUtfCharsDelegate(JEnvironmentRef env, JStringLocalRef jString, in Byte utf8Chars0);

internal delegate void GetStringRegionDelegate(JEnvironmentRef env, JStringLocalRef jString, Int32 startIndex,
	Int32 length, ref Char buffer0);

internal delegate void GetStringUtfRegionDelegate(JEnvironmentRef env, JStringLocalRef jString, Int32 startIndex,
	Int32 length, ref Byte buffer0);

internal delegate ref readonly Char GetStringCriticalDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringCriticalDelegate(JEnvironmentRef env, JStringLocalRef jString, in Char chars0);