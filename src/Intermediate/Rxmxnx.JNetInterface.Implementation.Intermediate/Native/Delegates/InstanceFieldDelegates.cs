namespace Rxmxnx.JNetInterface.Native.Delegates;

internal delegate JObjectLocalRef GetObjectFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Byte GetBooleanFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate SByte GetByteFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Char GetCharFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Int16 GetShortFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Int32 GetIntFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Int64 GetLongFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Single GetFloatFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);
internal delegate Double GetDoubleFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField);

internal delegate void SetObjectFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField,
	JObjectLocalRef value);

internal delegate void SetBooleanFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Byte value);
internal delegate void SetByteFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, SByte value);
internal delegate void SetCharFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Char value);
internal delegate void SetShortFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Int16 value);
internal delegate void SetIntFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Int32 value);
internal delegate void SetLongFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Int64 value);
internal delegate void SetFloatFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Single value);
internal delegate void SetDoubleFieldDelegate(JEnvironmentRef env, JObjectLocalRef obj, JFieldId jField, Double value);