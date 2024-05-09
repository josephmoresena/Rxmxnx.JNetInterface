namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	/// <inheritdoc/>
	public abstract TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		TField? value) where TField : IObject, IDataType<TField>;
	/// <inheritdoc/>
	public abstract void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition,
		TField? value) where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
		where TField : IObject, IDataType<TField>;
	/// <inheritdoc/>
	public abstract TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IObject, IDataType<TField>;
	/// <inheritdoc/>
	public abstract void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IDataType<TObject>;
	/// <inheritdoc/>
	public abstract TObject CallConstructor<TObject>(JConstructorObject jConstructor, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IClassType<TObject>;
	/// <inheritdoc/>
	public abstract TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc/>
	public abstract TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc/>
	public abstract void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args);
	/// <inheritdoc/>
	public abstract void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, IObject?[] args);
	/// <inheritdoc/>
	public abstract TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc/>
	public abstract TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc/>
	public abstract void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
	/// <inheritdoc/>
	public abstract void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
	/// <inheritdoc/>
	public abstract void RegisterNatives(JClassObject jClass, IReadOnlyList<JNativeCallEntry> calls);
	/// <inheritdoc/>
	public abstract void ClearNatives(JClassObject jClass);
	/// <inheritdoc/>
	public abstract JCallDefinition GetDefinition(JStringObject memberName, JArrayObject<JClassObject> parameterTypes,
		JClassObject? returnType);
	/// <inheritdoc/>
	public abstract JFieldDefinition GetDefinition(JStringObject memberName, JClassObject fieldType);
	/// <inheritdoc/>
	public abstract JMethodObject GetReflectedMethod(JMethodDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
	/// <inheritdoc/>
	public abstract JMethodObject GetReflectedFunction(JFunctionDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
	/// <inheritdoc/>
	public abstract JConstructorObject GetReflectedConstructor(JConstructorDefinition definition,
		JClassObject declaringClass);
	/// <inheritdoc/>
	public abstract JFieldObject GetReflectedField(JFieldDefinition definition, JClassObject declaringClass,
		Boolean isStatic);
}