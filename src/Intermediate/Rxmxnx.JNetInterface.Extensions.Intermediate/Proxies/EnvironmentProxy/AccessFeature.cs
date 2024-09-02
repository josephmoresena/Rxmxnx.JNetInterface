namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <inheritdoc/>
	public abstract TField? GetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition)
		where TField : IDataType<TField>;
	/// <inheritdoc/>
	public abstract TField? GetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract void SetField<TField>(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		TField? value) where TField : IDataType<TField>;
	/// <inheritdoc/>
	public abstract void SetField<TField>(JFieldObject jField, JLocalObject jLocal, JFieldDefinition definition,
		TField? value) where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
		where TField : IDataType<TField>;
	/// <inheritdoc/>
	public abstract TField? GetStaticField<TField>(JFieldObject jField, JFieldDefinition definition)
		where TField : IDataType<TField>, IObject;
	/// <inheritdoc/>
	public abstract void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>;
	/// <inheritdoc/>
	public abstract void SetStaticField<TField>(JFieldObject jField, JFieldDefinition definition, TField? value)
		where TField : IDataType<TField>, IObject;
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
	/// <inheritdoc/>
	public abstract JMethodId GetMethodId(JExecutableObject jExecutable);
	/// <inheritdoc/>
	public abstract JFieldId GetFieldId(JFieldObject jField);
	/// <inheritdoc cref="IAccessFeature.CallConstructor{TObject}(JClassObject, JConstructorDefinition, ReadOnlySpan{IObject})"/>
	public abstract TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IDataType<TObject>;
	/// <inheritdoc
	///     cref="IAccessFeature.CallConstructor{TObject}(JConstructorObject, JConstructorDefinition, ReadOnlySpan{IObject})"/>
	public abstract TObject CallConstructor<TObject>(JConstructorObject jConstructor, JConstructorDefinition definition,
		IObject?[] args) where TObject : JLocalObject, IClassType<TObject>;
	/// <inheritdoc cref="IAccessFeature.CallConstructor{TObject}(JClassObject, JConstructorDefinition, ReadOnlySpan{IObject})"/>
	public abstract TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc
	///     cref="IAccessFeature.CallStaticFunction{TObject}(JMethodObject, JFunctionDefinition, ReadOnlySpan{IObject})"/>
	public abstract TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
		IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc cref="IAccessFeature.CallStaticMethod(JClassObject, JMethodDefinition, ReadOnlySpan{IObject})"/>
	public abstract void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args);
	/// <inheritdoc cref="IAccessFeature.CallStaticMethod(JMethodObject, JMethodDefinition, ReadOnlySpan{IObject})"/>
	public abstract void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, IObject?[] args);
	/// <inheritdoc
	///     cref="IAccessFeature.CallFunction{TObject}(JLocalObject, JClassObject, JFunctionDefinition, Boolean, ReadOnlySpan{IObject})"/>
	public abstract TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc
	///     cref="IAccessFeature.CallFunction{TObject}(JMethodObject, JLocalObject, JFunctionDefinition, Boolean, ReadOnlySpan{IObject})"/>
	public abstract TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args) where TResult : IDataType<TResult>;
	/// <inheritdoc
	///     cref="IAccessFeature.CallMethod(JLocalObject, JClassObject, JMethodDefinition, Boolean, ReadOnlySpan{IObject})"/>
	public abstract void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
	/// <inheritdoc
	///     cref="IAccessFeature.CallMethod(JMethodObject, JLocalObject, JMethodDefinition, Boolean, ReadOnlySpan{IObject})"/>
	public abstract void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
		Boolean nonVirtual, IObject?[] args);
}