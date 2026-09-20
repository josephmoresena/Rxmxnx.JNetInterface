namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	public void CallStaticPrimitiveFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args = default)
	{
#if !NET9_0_OR_GREATER
		LegacyStaticPrimitiveFunctionCall call = new(this, jClass, definition);
		call.WithSafeFixed(bytes, args);
#else
		this.CallStaticPrimitiveFunction<LegacyCallArgument>(bytes, jClass, definition, new(args));
#endif
	}
	public void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
		JFunctionDefinition definition, Boolean nonVirtual, ReadOnlySpan<IObject?> args = default)
	{
#if !NET9_0_OR_GREATER
		LegacyInstancePrimitiveFunctionCall call = new(this, jLocal, jClass, definition, nonVirtual);
		call.WithSafeFixed(bytes, args);
#else
		this.CallPrimitiveFunction<LegacyCallArgument>(bytes, jLocal, jClass, definition, nonVirtual, new(args));
#endif
	}
	public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IDataType<TObject>
	{
#if !NET9_0_OR_GREATER
		LegacyConstructorCall<TObject> call = new(this, jClass, definition);
		args.WithSafeFixed(call, out TObject result);
		return result;
#else
		return this.CallConstructor<TObject, LegacyCallArgument>(jClass, definition, new(args));
#endif
	}
#if !NET8_0_OR_GREATER || ANDROID
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public TObject CallConstructor<TObject>(JConstructorObject jConstructor, JConstructorDefinition definition,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
#if !NET9_0_OR_GREATER
		LegacyReflectedConstructorCall<TObject> call = new(this, jConstructor, definition);
		args.WithSafeFixed(call, out TObject result);
		return result;
#else
		return this.CallConstructor<TObject, LegacyCallArgument>(jConstructor, definition, new(args));
#endif
	}
	public TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
	{
#if !NET9_0_OR_GREATER
		LegacyStaticFunctionCall<TResult> call = new(this, jClass, definition);
		args.WithSafeFixed(call, out TResult? result);
		return result;
#else
		return this.CallStaticFunction<TResult, LegacyCallArgument>(jClass, definition, new(args));
#endif
	}
	public TResult? CallStaticFunction<TResult>(JMethodObject jMethod, JFunctionDefinition definition,
		ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
	{
#if !NET9_0_OR_GREATER
		LegacyReflectedStaticFunctionCall<TResult> call = new(this, jMethod, definition);
		args.WithSafeFixed(call, out TResult? result);
		return result;
#else
		return this.CallStaticFunction<TResult, LegacyCallArgument>(jMethod, definition, new(args));
#endif
	}
	public void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, ReadOnlySpan<IObject?> args)
	{
#if !NET9_0_OR_GREATER
		LegacyStaticMethodCall call = new(this, jClass, definition);
		args.WithSafeFixed(call);
#else
		this.CallStaticMethod<LegacyCallArgument>(jClass, definition, new(args));
#endif
	}
	public void CallStaticMethod(JMethodObject jMethod, JMethodDefinition definition, ReadOnlySpan<IObject?> args)
	{
#if !NET9_0_OR_GREATER
		LegacyReflectedStaticMethodCall call = new(this, jMethod, definition);
		args.WithSafeFixed(call);
#else
		this.CallStaticMethod<LegacyCallArgument>(jMethod, definition, new(args));
#endif
	}
	public TResult? CallFunction<TResult>(JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
	{
#if !NET9_0_OR_GREATER
		LegacyInstanceFunctionCall<TResult> call = new(this, jLocal, jClass, definition, nonVirtual);
		args.WithSafeFixed(call, out TResult? result);
		return result;
#else
		return this.CallFunction<TResult, LegacyCallArgument>(jLocal, jClass, definition, nonVirtual, new(args));
#endif
	}
	public TResult? CallFunction<TResult>(JMethodObject jMethod, JLocalObject jLocal, JFunctionDefinition definition,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args) where TResult : IDataType<TResult>
	{
#if !NET9_0_OR_GREATER
		LegacyReflectedInstanceFunctionCall<TResult> call = new(this, jMethod, jLocal, definition, nonVirtual);
		args.WithSafeFixed(call, out TResult? result);
		return result;
#else
		return this.CallFunction<TResult, LegacyCallArgument>(jMethod, jLocal, definition, nonVirtual, new(args));
#endif
	}
	public void CallMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition, Boolean nonVirtual,
		ReadOnlySpan<IObject?> args)
	{
#if !NET9_0_OR_GREATER
		LegacyInstanceMethodCall call = new(this, jLocal, jClass, definition, nonVirtual);
		args.WithSafeFixed(call);
#else
		this.CallMethod<LegacyCallArgument>(jLocal, jClass, definition, nonVirtual, new(args));
#endif
	}
	public void CallMethod(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition, Boolean nonVirtual,
		ReadOnlySpan<IObject?> args)
	{
#if !NET9_0_OR_GREATER
		LegacyReflectedInstanceMethodCall call = new(this, jMethod, jLocal, definition, nonVirtual);
		args.WithSafeFixed(call);
#else
		this.CallMethod<LegacyCallArgument>(jMethod, jLocal, definition, nonVirtual, new(args));
#endif
	}
}