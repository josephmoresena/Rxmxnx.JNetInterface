namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JResult ThrowDelegate(JEnvironmentRef env, JThrowableLocalRef obj);
internal delegate JResult ThrowNewDelegate(JEnvironmentRef env, JClassLocalRef jClass, ReadOnlyValPtr<Byte> messageChars0);
internal delegate JThrowableLocalRef ExceptionOccurredDelegate(JEnvironmentRef env);
internal delegate void ExceptionDescribeDelegate(JEnvironmentRef env);
internal delegate void ExceptionClearDelegate(JEnvironmentRef env);
internal delegate void FatalErrorDelegate(JEnvironmentRef env, ReadOnlyValPtr<Byte> messageChars0);
internal delegate Byte ExceptionCheckDelegate(JEnvironmentRef env);