namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class AccessFeatureProxy
{
	void IAccessFeature.GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFieldDefinition definition)
		=> bytes.WithSafeFixed((this, jLocal, jClass, definition), AccessFeatureProxy.GetPrimitiveField);
	void IAccessFeature.GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		=> bytes.WithSafeFixed((this, jClass, definition), AccessFeatureProxy.GetPrimitiveStaticField);

	void IAccessFeature.SetPrimitiveField(JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> bytes.WithSafeFixed((this, jLocal, jClass, definition), AccessFeatureProxy.SetPrimitiveField);
	void IAccessFeature.SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes)
		=> bytes.WithSafeFixed((this, jClass, definition), AccessFeatureProxy.SetPrimitiveStaticField);

	void IAccessFeature.CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args)
		=> bytes.WithSafeFixed((this, jLocal, jClass, definition, nonVirtual, args.ToArray()),
		                       AccessFeatureProxy.CallPrimitiveFunction);
	void IAccessFeature.CallStaticPrimitiveFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, ReadOnlySpan<IObject?> args)
		=> bytes.WithSafeFixed((this, jClass, definition, args.ToArray()),
		                       AccessFeatureProxy.CallStaticPrimitiveFunction);

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

	private static void GetPrimitiveField(in IFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.GetPrimitiveField(mem, args.jLocal, args.jClass, args.definition);
	private static void GetPrimitiveStaticField(in IFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.GetPrimitiveStaticField(mem, args.jClass, args.definition);
	private static void SetPrimitiveField(in IReadOnlyFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.SetPrimitiveField(args.jLocal, args.jClass, args.definition, mem);
	private static void SetPrimitiveStaticField(in IReadOnlyFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.SetPrimitiveStaticField(args.jClass, args.definition, mem);
	private static void CallPrimitiveFunction(in IFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition, Boolean
			nonVirtual, IObject?[] args) args)
		=> args.feature.CallPrimitiveFunction(mem, args.jLocal, args.jClass, args.definition, args.nonVirtual,
		                                      args.args);
	private static void CallStaticPrimitiveFunction(in IFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFunctionDefinition definition, IObject?[] args) args)
		=> args.feature.CallStaticPrimitiveFunction(mem, args.jClass, args.definition, args.args);
}