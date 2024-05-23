namespace Rxmxnx.JNetInterface.Native.Values;

internal partial struct NativeInterface
{
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.DefineClass"/>
	/// </summary>
	public static readonly JniMethodInfo DefineClassInfo = new() { Name = nameof(ClassFunctionSet.DefineClass), };
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.FindClass"/>
	/// </summary>
	public static readonly JniMethodInfo FindClassInfo = new() { Name = nameof(ClassFunctionSet.FindClass), };
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.FromReflectedMethod"/>
	/// </summary>
	public static readonly JniMethodInfo FromReflectedMethodInfo =
		new() { Name = nameof(ClassFunctionSet.FromReflectedMethod), };
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.FromReflectedField"/>
	/// </summary>
	public static readonly JniMethodInfo FromReflectedFieldInfo = new()
	{
		Name = nameof(ClassFunctionSet.FromReflectedField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.ToReflectedMethod"/>
	/// </summary>
	public static readonly JniMethodInfo ToReflectedMethodInfo = new()
	{
		Name = nameof(ClassFunctionSet.ToReflectedMethod), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.GetSuperclass"/>
	/// </summary>
	public static readonly JniMethodInfo GetSuperclassInfo = new() { Name = nameof(ClassFunctionSet.GetSuperclass), };
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.IsAssignableFrom"/>
	/// </summary>
	public static readonly JniMethodInfo IsAssignableFromInfo =
		new() { Name = nameof(ClassFunctionSet.IsAssignableFrom), };
	/// <summary>
	/// Information of <see cref="ClassFunctionSet.ToReflectedField"/>
	/// </summary>
	public static readonly JniMethodInfo ToReflectedFieldInfo =
		new() { Name = nameof(ClassFunctionSet.ToReflectedField), };
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.Throw"/>
	/// </summary>
	public static readonly JniMethodInfo ThrowInfo =
		new() { Name = nameof(ErrorFunctionSet.Throw), Level = JniSafetyLevels.ErrorSafe, };
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.ThrowNew"/>
	/// </summary>
	public static readonly JniMethodInfo ThrowNewInfo =
		new() { Name = nameof(ErrorFunctionSet.ThrowNew), Level = JniSafetyLevels.ErrorSafe, };
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.ExceptionOccurred"/>
	/// </summary>
	public static readonly JniMethodInfo ExceptionOccurredInfo = new()
	{
		Name = nameof(ErrorFunctionSet.ExceptionOccurred), Level = JniSafetyLevels.ErrorSafe,
	};
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.ExceptionDescribe"/>
	/// </summary>
	public static readonly JniMethodInfo ExceptionDescribeInfo = new()
	{
		Name = nameof(ErrorFunctionSet.ExceptionDescribe),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.ExceptionClear"/>
	/// </summary>
	public static readonly JniMethodInfo ExceptionClearInfo = new()
	{
		Name = nameof(ErrorFunctionSet.ExceptionClear),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ErrorFunctionSet.FatalError"/>
	/// </summary>
	public static readonly JniMethodInfo FatalErrorInfo = new() { Name = nameof(ErrorFunctionSet.FatalError), };
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.PushLocalFrame"/>
	/// </summary>
	public static readonly JniMethodInfo PushLocalFrameInfo =
		new() { Name = nameof(ReferenceFunctionSet.PushLocalFrame), };
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.PopLocalFrame"/>
	/// </summary>
	public static readonly JniMethodInfo PopLocalFrameInfo = new()
	{
		Name = nameof(ReferenceFunctionSet.PopLocalFrame),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.NewGlobalRef"/>
	/// </summary>
	public static readonly JniMethodInfo NewGlobalRefInfo = new() { Name = nameof(ReferenceFunctionSet.NewGlobalRef), };
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.DeleteGlobalRef"/>
	/// </summary>
	public static readonly JniMethodInfo DeleteGlobalRefInfo = new()
	{
		Name = nameof(ReferenceFunctionSet.DeleteGlobalRef),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.DeleteLocalRef"/>
	/// </summary>
	public static readonly JniMethodInfo DeleteLocalRefInfo = new()
	{
		Name = nameof(ReferenceFunctionSet.DeleteLocalRef),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.IsSameObject"/>
	/// </summary>
	public static readonly JniMethodInfo IsSameObjectInfo = new()
	{
		Name = nameof(ReferenceFunctionSet.IsSameObject), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.NewLocalRef"/>
	/// </summary>
	public static readonly JniMethodInfo NewLocalRefInfo = new() { Name = nameof(ReferenceFunctionSet.NewLocalRef), };
	/// <summary>
	/// Information of <see cref="ReferenceFunctionSet.EnsureLocalCapacity"/>
	/// </summary>
	public static readonly JniMethodInfo EnsureLocalCapacityInfo = new()
	{
		Name = nameof(ReferenceFunctionSet.EnsureLocalCapacity), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ObjectFunctionSet.AllocObject"/>
	/// </summary>
	public static readonly JniMethodInfo AllocObjectInfo = new() { Name = nameof(ObjectFunctionSet.AllocObject), };
	/// <summary>
	/// Information of <see cref="ObjectFunctionSet.NewObject"/>
	/// </summary>
	public static readonly JniMethodInfo NewObjectInfo = new() { Name = nameof(ObjectFunctionSet.NewObject), };
	/// <summary>
	/// Information of <see cref="ObjectFunctionSet.GetObjectClass"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectClassInfo = new()
	{
		Name = nameof(ObjectFunctionSet.GetObjectClass), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ObjectFunctionSet.IsInstanceOf"/>
	/// </summary>
	public static readonly JniMethodInfo IsInstanceOfInfo = new()
	{
		Name = nameof(ObjectFunctionSet.IsInstanceOf), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.GetMethodId"/>
	/// </summary>
	public static readonly JniMethodInfo GetMethodIdInfo = new()
	{
		Name = "GetMethodID", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallObjectMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallObjectMethodInfo = new()
	{
		Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallObjectMethod),
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallBooleanMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallBooleanMethodInfo = new()
	{
		Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallBooleanMethod),
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallByteMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallByteMethodInfo =
		new() { Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallByteMethod), };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallCharMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallCharMethodInfo =
		new() { Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallCharMethod), };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallShortMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallShortMethodInfo = new()
	{
		Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallShortMethod),
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallIntMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallIntMethodInfo =
		new() { Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallIntMethod), };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallLongMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallLongMethodInfo =
		new() { Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallLongMethod), };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallFloatMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallFloatMethodInfo = new()
	{
		Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallShortMethod),
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallDoubleMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallDoubleMethodInfo = new()
	{
		Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallDoubleMethod),
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JObjectLocalRef}.CallVoidMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallVoidMethodInfo =
		new() { Name = nameof(MethodFunctionSet<JObjectLocalRef>.CallVoidMethod), };
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualObjectMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualObjectMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualObjectMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualBooleanMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualBooleanMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualBooleanMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualByteMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualByteMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualByteMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualCharMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualCharMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualCharMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualShortMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualShortMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualShortMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualIntMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualIntMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualIntMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualLongMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualLongMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualLongMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualFloatMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualFloatMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualFloatMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualDoubleMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualDoubleMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualDoubleMethod),
	};
	/// <summary>
	/// Information of see cref="MethodFunctionSet.CallNonVirtualVoidMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallNonVirtualVoidMethodInfo = new()
	{
		Name = nameof(NonVirtualMethodFunctionSet.CallNonVirtualVoidMethod),
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetFieldId"/>
	/// </summary>
	public static readonly JniMethodInfo GetFieldIdInfo = new()
	{
		Name = "GetFieldID", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetObjectField"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectFieldInfo =
		new() { Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetObjectField), };
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetBooleanField"/>
	/// </summary>
	public static readonly JniMethodInfo GetBooleanFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetBooleanField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo GetByteFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetByteField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo GetCharFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetCharField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetShortField"/>
	/// </summary>
	public static readonly JniMethodInfo GetShortFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetShortField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetIntField"/>
	/// </summary>
	public static readonly JniMethodInfo GetIntFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetIntField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetLongField"/>
	/// </summary>
	public static readonly JniMethodInfo GetLongFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetLongField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo GetFloatFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetFloatField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.GetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo GetDoubleFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.GetDoubleField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetObjectField"/>
	/// </summary>
	public static readonly JniMethodInfo SetObjectFieldInfo =
		new() { Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetObjectField), };
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetBooleanField"/>
	/// </summary>
	public static readonly JniMethodInfo SetBooleanFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetBooleanField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo SetByteFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetByteField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo SetCharFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetCharField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetShortField"/>
	/// </summary>
	public static readonly JniMethodInfo SetShortFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetShortField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetIntField"/>
	/// </summary>
	public static readonly JniMethodInfo SetIntFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetIntField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetLongField"/>
	/// </summary>
	public static readonly JniMethodInfo SetLongFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetLongField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo SetFloatFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetFloatField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JObjectLocalRef}.SetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo SetDoubleFieldInfo = new()
	{
		Name = nameof(FieldFunctionSet<JObjectLocalRef>.SetDoubleField), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.GetMethodId"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticMethodIdInfo = new()
	{
		Name = "GetStaticMethodID", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallObjectMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticObjectMethodInfo = new() { Name = "CallStaticObjectMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallBooleanMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticBooleanMethodInfo = new() { Name = "CallStaticBooleanMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallByteMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticByteMethodInfo = new() { Name = "CallStaticByteMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallCharMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticCharMethodInfo = new() { Name = "CallStaticCharMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallShortMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticShortMethodInfo = new() { Name = "CallStaticShortMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallIntMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticIntMethodInfo = new() { Name = "CallStaticIntMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallLongMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticLongMethodInfo = new() { Name = "CallStaticLongMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallFloatMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticFloatMethodInfo = new() { Name = "CallStaticFloatMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallDoubleMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticDoubleMethodInfo = new() { Name = "CallStaticDoubleMethod", };
	/// <summary>
	/// Information of <see cref="MethodFunctionSet{JClassLocalRef}.CallVoidMethod"/>
	/// </summary>
	public static readonly JniMethodInfo CallStaticVoidMethodInfo = new() { Name = "CallStaticVoidMethod", };
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetFieldId"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticFieldIdInfo = new()
	{
		Name = "GetStaticFieldID", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetObjectField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticObjectFieldInfo = new() { Name = "GetStaticObjectField", };
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetBooleanField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticBooleanFieldInfo = new()
	{
		Name = "GetStaticBooleanField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticByteFieldInfo = new()
	{
		Name = "GetStaticByteField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticCharFieldInfo = new()
	{
		Name = "GetStaticCharField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetShortField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticShortFieldInfo = new()
	{
		Name = "GetStaticShortField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetIntField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticIntFieldInfo = new()
	{
		Name = "GetStaticIntField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetLongField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticLongFieldInfo = new()
	{
		Name = "GetStaticLongField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticFloatFieldInfo = new()
	{
		Name = "GetStaticFloatField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo GetStaticDoubleFieldInfo = new()
	{
		Name = "GetStaticDoubleField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetObjectField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticObjectFieldInfo = new() { Name = "SetStaticObjectField", };
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.GetBooleanField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticBooleanFieldInfo = new()
	{
		Name = "SetStaticBooleanField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticByteFieldInfo = new()
	{
		Name = "SetStaticByteField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetByteField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticCharFieldInfo = new()
	{
		Name = "SetStaticCharField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetShortField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticShortFieldInfo = new()
	{
		Name = "SetStaticShortField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetIntField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticIntFieldInfo = new()
	{
		Name = "SetStaticIntField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetLongField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticLongFieldInfo = new()
	{
		Name = "SetStaticLongField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticFloatFieldInfo = new()
	{
		Name = "SetStaticFloatField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="FieldFunctionSet{JClassLocalRef}.SetFloatField"/>
	/// </summary>
	public static readonly JniMethodInfo SetStaticDoubleFieldInfo = new()
	{
		Name = "SetStaticDoubleField", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet.NewString"/>
	/// </summary>
	public static readonly JniMethodInfo NewStringInfo = new()
	{
		Name = nameof(StringFunctionSet.NewString), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.GetStringLength"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringLengthInfo = new()
	{
		Name = nameof(StringFunctionSet<Char>.GetStringLength), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.GetStringChars"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringCharsInfo = new()
	{
		Name = nameof(StringFunctionSet<Char>.GetStringChars), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.ReleaseStringChars"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseStringCharsInfo = new()
	{
		Name = nameof(StringFunctionSet<Byte>.ReleaseStringChars),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet.NewStringUtf"/>
	/// </summary>
	public static readonly JniMethodInfo NewStringUtfInfo = new()
	{
		Name = "NewStringUTF", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.GetStringLength"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringUtfLengthInfo = new()
	{
		Name = "GetStringUTFLength", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.GetStringChars"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringUtfCharsInfo = new()
	{
		Name = "GetStringUTFChars", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringFunctionSet{TChar}.ReleaseStringChars"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseStringUtfCharsInfo = new()
	{
		Name = "ReleaseStringUTFChars", Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ArrayFunctionSet.GetArrayLength"/>
	/// </summary>
	public static readonly JniMethodInfo GetArrayLengthInfo = new()
	{
		Name = nameof(ArrayFunctionSet.GetArrayLength), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ObjectArrayFunctionSet.NewObjectArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewArrayObjectInfo = new()
	{
		Name = nameof(ObjectArrayFunctionSet.NewObjectArray),
	};
	/// <summary>
	/// Information of <see cref="ObjectArrayFunctionSet.GetObjectArrayElement"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectArrayElementInfo = new()
	{
		Name = nameof(ObjectArrayFunctionSet.GetObjectArrayElement),
	};
	/// <summary>
	/// Information of <see cref="ObjectArrayFunctionSet.SetObjectArrayElement"/>
	/// </summary>
	public static readonly JniMethodInfo SetObjectArrayElementInfo = new()
	{
		Name = nameof(ObjectArrayFunctionSet.SetObjectArrayElement),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewBooleanArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewBooleanArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewBooleanArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewByteArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewByteArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewByteArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewCharArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewCharArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewCharArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewShortArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewShortArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewShortArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewIntArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewIntArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewIntArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewLongArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewLongArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewLongArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewFloatArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewFloatArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewFloatArray),
	};
	/// <summary>
	/// Information of <see cref="NewPrimitiveArrayFunctionSet.NewDoubleArray"/>
	/// </summary>
	public static readonly JniMethodInfo NewDoubleArrayInfo = new()
	{
		Name = nameof(NewPrimitiveArrayFunctionSet.NewDoubleArray),
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetBooleanArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetBooleanArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetBooleanArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetByteArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetByteArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetByteArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetCharArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetCharArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetCharArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetShortArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetShortArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetShortArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetIntArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetIntArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetIntArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetLongArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetLongArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetLongArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetFloatArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetFloatArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetFloatArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetPrimitiveArrayElementsFunctionSet.GetDoubleArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo GetDoubleArrayElementsInfo = new()
	{
		Name = nameof(GetPrimitiveArrayElementsFunctionSet.GetDoubleArrayElements),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseBooleanArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseBooleanArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseBooleanArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseByteArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseByteArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseByteArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseCharArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseCharArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseCharArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseShortArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseShortArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseShortArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseIntArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseIntArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseIntArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseLongArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseLongArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseLongArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseFloatArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseFloatArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseFloatArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="ReleasePrimitiveArrayElementsFunctionSet.ReleaseDoubleArrayElements"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseDoubleArrayElementsInfo = new()
	{
		Name = nameof(ReleasePrimitiveArrayElementsFunctionSet.ReleaseDoubleArrayElements),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetBooleanArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetBooleanArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetBooleanArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetByteArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetByteArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetByteArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetCharArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetCharArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetCharArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetShortArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetShortArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetShortArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetIntArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetIntArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetIntArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetLongArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetLongArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetLongArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetFloatArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetFloatArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetFloatArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.GetDoubleArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetDoubleArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.GetDoubleArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetBooleanArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetBooleanArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetBooleanArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetByteArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetByteArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetByteArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetCharArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetCharArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetCharArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetShortArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetShortArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetShortArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetIntArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetIntArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetIntArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetLongArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetLongArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetLongArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetFloatArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetFloatArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetFloatArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayRegionFunctionSet.SetDoubleArrayRegion"/>
	/// </summary>
	public static readonly JniMethodInfo SetDoubleArrayRegionInfo = new()
	{
		Name = nameof(PrimitiveArrayRegionFunctionSet.SetDoubleArrayRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeRegistryFunctionSet.RegisterNatives"/>
	/// </summary>
	public static readonly JniMethodInfo RegisterNativesInfo = new()
	{
		Name = nameof(NativeRegistryFunctionSet.RegisterNatives),
	};
	/// <summary>
	/// Information of <see cref="NativeRegistryFunctionSet.UnregisterNatives"/>
	/// </summary>
	public static readonly JniMethodInfo UnregisterNativesInfo = new()
	{
		Name = nameof(NativeRegistryFunctionSet.UnregisterNatives),
	};
	/// <summary>
	/// Information of <see cref="MonitorFunctionSet.MonitorEnter"/>
	/// </summary>
	public static readonly JniMethodInfo MonitorEnterInfo = new() { Name = nameof(MonitorFunctionSet.MonitorEnter), };
	/// <summary>
	/// Information of <see cref="MonitorFunctionSet.MonitorExit"/>
	/// </summary>
	public static readonly JniMethodInfo MonitorExitInfo = new()
	{
		Name = nameof(MonitorFunctionSet.MonitorExit),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeInterface.GetVirtualMachineInfo"/>
	/// </summary>
	public static readonly JniMethodInfo GetVirtualMachineInfo = new()
	{
		Name = "GetJavaVM", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetStringRegionFunction{Char}.GetStringRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringRegionInfo = new()
	{
		Name = nameof(GetStringRegionFunction<Char>.GetStringRegion), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="GetStringRegionFunction{Byte}.GetStringRegion"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringUtfRegionInfo = new()
	{
		Name = "GetStringUTFRegion", Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayCriticalFunctionSet.GetPrimitiveArrayCritical"/>
	/// </summary>
	public static readonly JniMethodInfo GetPrimitiveArrayCriticalInfo = new()
	{
		Name = nameof(PrimitiveArrayCriticalFunctionSet.GetPrimitiveArrayCritical),
		Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="PrimitiveArrayCriticalFunctionSet.ReleasePrimitiveArrayCritical"/>
	/// </summary>
	public static readonly JniMethodInfo ReleasePrimitiveArrayCriticalInfo = new()
	{
		Name = nameof(PrimitiveArrayCriticalFunctionSet.ReleasePrimitiveArrayCritical),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringCriticalFunctionSet.GetStringCritical"/>
	/// </summary>
	public static readonly JniMethodInfo GetStringCriticalInfo = new()
	{
		Name = nameof(StringCriticalFunctionSet.GetStringCritical), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="StringCriticalFunctionSet.ReleaseStringCritical"/>
	/// </summary>
	public static readonly JniMethodInfo ReleaseStringCriticalInfo = new()
	{
		Name = nameof(StringCriticalFunctionSet.ReleaseStringCritical),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="WeakReferenceFunctionSet.NewWeakGlobalRef"/>
	/// </summary>
	public static readonly JniMethodInfo NewWeakGlobalRefInfo =
		new() { Name = nameof(WeakReferenceFunctionSet.NewWeakGlobalRef), };
	/// <summary>
	/// Information of <see cref="WeakReferenceFunctionSet.DeleteWeakGlobalRef"/>
	/// </summary>
	public static readonly JniMethodInfo DeleteWeakGlobalRefInfo = new()
	{
		Name = nameof(WeakReferenceFunctionSet.DeleteWeakGlobalRef),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeInterface.ExceptionCheck"/>
	/// </summary>
	public static readonly JniMethodInfo ExceptionCheckInfo = new()
	{
		Name = nameof(NativeInterface.ExceptionCheck),
		Level = JniSafetyLevels.ErrorSafe | JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NioFunctionSet.NewDirectByteBuffer"/>
	/// </summary>
	public static readonly JniMethodInfo NewDirectByteBufferInfo = new()
	{
		Name = nameof(NioFunctionSet.NewDirectByteBuffer), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NioFunctionSet.GetDirectBufferAddress"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferAddressInfo = new()
	{
		Name = nameof(NioFunctionSet.GetDirectBufferAddress), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NioFunctionSet.GetDirectBufferCapacity"/>
	/// </summary>
	public static readonly JniMethodInfo GetDirectBufferCapacityInfo = new()
	{
		Name = nameof(NioFunctionSet.GetDirectBufferCapacity), Level = JniSafetyLevels.CriticalSafe,
	};
	/// <summary>
	/// Information of <see cref="NativeInterface.GetObjectRefType"/>
	/// </summary>
	public static readonly JniMethodInfo GetObjectRefTypeInfo = new()
	{
		Name = nameof(NativeInterface.GetObjectRefType), Level = JniSafetyLevels.CriticalSafe,
	};
}