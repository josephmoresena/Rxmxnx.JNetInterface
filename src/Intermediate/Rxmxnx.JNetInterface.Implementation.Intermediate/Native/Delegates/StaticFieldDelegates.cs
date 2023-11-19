namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef GetStaticObjectFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass,
	JFieldId jField);

internal delegate Byte GetStaticBooleanFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate SByte GetStaticByteFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Char GetStaticCharFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Int16 GetStaticShortFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Int32 GetStaticIntFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Int64 GetStaticLongFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Single GetStaticFloatFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);
internal delegate Double GetStaticDoubleFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField);

internal delegate void SetStaticObjectFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	JObjectLocalRef value);

internal delegate void SetStaticBooleanFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Byte value);

internal delegate void SetStaticByteFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	SByte value);

internal delegate void SetStaticCharFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Char value);

internal delegate void SetStaticShortFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Int16 value);

internal delegate void SetStaticIntFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Int32 value);

internal delegate void SetStaticLongFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Int64 value);

internal delegate void SetStaticFloatFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Single value);

internal delegate void SetStaticDoubleFieldDelegate(JEnvironmentRef env, JClassLocalRef jclass, JFieldId jField,
	Double value);