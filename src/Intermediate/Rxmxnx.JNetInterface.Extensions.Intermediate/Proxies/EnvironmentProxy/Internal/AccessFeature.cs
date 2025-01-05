namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	TObject IAccessFeature.CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		ReadOnlySpan<IObject?> args)
		=> this.CallConstructor<TObject>(jClass, definition, args.ToArray());
	TObject IAccessFeature.CallConstructor<TObject>(JConstructorObject jConstructor, JConstructorDefinition definition,
		ReadOnlySpan<IObject?> args)
		=> this.CallConstructor<TObject>(jConstructor, definition, args.ToArray());
	TResult? IAccessFeature.CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args) where TResult : default
		=> this.CallStaticFunction<TResult>(jClass, definition, args.ToArray());
	TResult? IAccessFeature.CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args) where TResult : default
		=> this.CallStaticFunction<TResult>(jMethod, definition, args.ToArray());
	void IAccessFeature.CallStaticMethod(JClassObject jClass, JMethodDefinition definition, ReadOnlySpan<IObject?> args)
		=> this.CallStaticMethod(jClass, definition, args.ToArray());
	void IAccessFeature.CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition,
		ReadOnlySpan<IObject?> args)
		=> this.CallStaticMethod(jMethod, definition, args.ToArray());
	TResult? IAccessFeature.CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args) where TResult : default
		=> this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, args.ToArray());
	TResult? IAccessFeature.CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args) where TResult : default
		=> this.CallFunction<TResult>(jMethod, jLocal, definition, nonVirtual, args.ToArray());
	void IAccessFeature.CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> this.CallMethod(jLocal, jClass, definition, nonVirtual, args.ToArray());
	void IAccessFeature.CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> this.CallMethod(jMethod, jLocal, definition, nonVirtual, args.ToArray());
	TResult IAccessFeature.CallInternalStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args)
		=> this.CallStaticFunction<TResult>(jClass, definition, args.ToArray().Normalize())!;
	void IAccessFeature.CallInternalStaticMethod(JClassObject jClass, JMethodDefinition definition,
		ReadOnlySpan<IObject?> args)
		=> this.CallStaticMethod(jClass, definition, args.ToArray().Normalize());
	TResult IAccessFeature.CallInternalFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, args.ToArray().Normalize())!;
	void IAccessFeature.CallInternalMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> this.CallMethod(jLocal, jClass, definition, nonVirtual, args.ToArray().Normalize());

	void IAccessFeature.GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition)
		=> definition.PrimitiveGet(bytes, jLocal, jClass);
	void IAccessFeature.SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveSet(jLocal, jClass, bytes);
	void IAccessFeature.GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		=> definition.PrimitiveStaticGet(bytes, jClass);
	void IAccessFeature.SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> definition.PrimitiveStaticSet(jClass, bytes);

	void IAccessFeature.CallStaticPrimitiveFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, ReadOnlySpan<IObject?> args)
		=> definition.PrimitiveStaticInvoke(bytes, jClass, args.ToArray());
	void IAccessFeature.CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> definition.PrimitiveInvoke(bytes, jLocal, jClass, nonVirtual, args.ToArray());
}