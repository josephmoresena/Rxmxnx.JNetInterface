namespace Rxmxnx.JNetInterface.Tests.Restricted;

public partial class AccessFeatureProxy
{
	JMethodId IAccessFeature.GetMethodId(JExecutableObject jExecutable)
	{
		IntPtr ptr = this.GetMethodId(jExecutable);
		return ptr.Transform<IntPtr, JMethodId>();
	}
	JFieldId IAccessFeature.GetFieldId(JFieldObject jField)
	{
		IntPtr ptr = this.GetFieldId(jField);
		return ptr.Transform<IntPtr, JFieldId>();
	}
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
		JFunctionDefinition definition, Boolean nonVirtual, IObject?[] args)
		=> bytes.WithSafeFixed((this, jLocal, jClass, definition, nonVirtual, args),
		                       AccessFeatureProxy.CallPrimitiveFunction);
	void IAccessFeature.CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass,
		JFunctionDefinition definition, IObject?[] args)
		=> bytes.WithSafeFixed((this, jClass, definition, args), AccessFeatureProxy.CallPrimitiveStaticFunction);

	private static void GetPrimitiveField(in IFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.GetPrimitiveField(mem, args.jLocal, args.jClass, args.definition);
	private static void GetPrimitiveStaticField(in IFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.GetPrimitiveStaticField(mem, args.jClass, args.definition);
	private static void SetPrimitiveField(in IReadOnlyFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.SetPrimitiveField(args.jClass, args.definition, mem);
	private static void SetPrimitiveStaticField(in IReadOnlyFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFieldDefinition definition) args)
		=> args.feature.SetPrimitiveStaticField(args.jClass, args.definition, mem);
	private static void CallPrimitiveFunction(in IFixedMemory mem,
		(AccessFeatureProxy feature, JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition, Boolean
			nonVirtual, IObject?[] args) args)
		=> args.feature.CallPrimitiveFunction(mem, args.jLocal, args.jClass, args.definition, args.nonVirtual,
		                                      args.args);
	private static void CallPrimitiveStaticFunction(in IFixedMemory mem,
		(AccessFeatureProxy feature, JClassObject jClass, JFunctionDefinition definition, IObject?[] args) args)
		=> args.feature.CallPrimitiveStaticFunction(mem, args.jClass, args.definition, args.args);
}