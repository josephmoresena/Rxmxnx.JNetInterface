namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JStringLocalRef NewStringDelegate(JEnvironmentRef env, ReadOnlyValPtr<Char> chars0, Int32 length);
internal delegate Int32 GetStringLengthDelegate(JEnvironmentRef env, JStringLocalRef jString);

internal delegate ReadOnlyValPtr<Char> GetStringCharsDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringCharsDelegate(JEnvironmentRef env, JStringLocalRef jString, ReadOnlyValPtr<Char> chars0);
internal delegate JStringLocalRef NewStringUtfDelegate(JEnvironmentRef env, ReadOnlyValPtr<Byte> utf8Chars0);
internal delegate Int32 GetStringUtfLengthDelegate(JEnvironmentRef env, JStringLocalRef jString);

internal delegate ReadOnlyValPtr<Byte> GetStringUtfCharsDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringUtfCharsDelegate(JEnvironmentRef env, JStringLocalRef jString, ReadOnlyValPtr<Byte> utf8Chars0);

internal delegate void GetStringRegionDelegate(JEnvironmentRef env, JStringLocalRef jString, Int32 startIndex,
	Int32 length, ValPtr<Char> buffer0);

internal delegate void GetStringUtfRegionDelegate(JEnvironmentRef env, JStringLocalRef jString, Int32 startIndex,
	Int32 length, ValPtr<Byte> buffer0);

internal delegate ReadOnlyValPtr<Char> GetStringCriticalDelegate(JEnvironmentRef env, JStringLocalRef jString,
	out Byte isCopy);

internal delegate void ReleaseStringCriticalDelegate(JEnvironmentRef env, JStringLocalRef jString, ReadOnlyValPtr<Char> chars0);