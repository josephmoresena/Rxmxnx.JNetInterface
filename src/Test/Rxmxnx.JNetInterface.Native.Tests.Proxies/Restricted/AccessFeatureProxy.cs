namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract partial class AccessFeatureProxy : IAccessFeature
{
	public abstract TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	public abstract TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	public abstract void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		TField? value) where TField : IObject, IDataType<TField>;
	public abstract void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition,
		TField? value) where TField : IDataType<TField>, IObject;
	public abstract TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	public abstract TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	public abstract void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IObject, IDataType<TField>;
	public abstract void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>, IObject;
	public abstract TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IDataType<TObject>;
	public abstract TObject CallConstructor<TObject>(JMethodObject jMethod, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IClassType<TObject>;
	public abstract TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	public abstract TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	public abstract void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args);
	public abstract void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, IObject?[] args);
	public abstract TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	public abstract TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	public abstract void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
	public abstract void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
	public abstract void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCallEntry> calls);
	public abstract void ClearNatives(JClassObject jClass);
	public abstract JCallDefinition GetDefinition(JStringObject memberName, JArrayObject<JClassObject> parameterTypes,
		JClassObject? returnType);
	public abstract JFieldDefinition GetDefinition(JStringObject memberName, JClassObject fieldType);
	public abstract JMethodObject GetReflectedMethod(JMethodDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
	public abstract JMethodObject GetReflectedFunction(JFunctionDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
	public abstract JConstructorObject GetReflectedConstructor(JConstructorDefinition definition,
		JClassObject declaringClass);
	public abstract JFieldObject GetReflectedField(JFieldDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
	public abstract void GetPrimitiveField(IFixedMemory bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition);
	public abstract void GetPrimitiveStaticField(IFixedMemory bytes, JClassObject jClass, JFieldDefinition definition);
	public abstract void SetPrimitiveField(JClassObject jClass, JFieldDefinition definition, IReadOnlyFixedMemory mem);
	public abstract void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		IReadOnlyFixedMemory mem);
	public abstract void CallPrimitiveFunction(IFixedMemory mem, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args);
	protected abstract void CallPrimitiveStaticFunction(IFixedMemory mem, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args);
	public abstract IntPtr GetMethodId(JExecutableObject jExecutable);
	public abstract IntPtr GetFieldId(JFieldObject jField);
}