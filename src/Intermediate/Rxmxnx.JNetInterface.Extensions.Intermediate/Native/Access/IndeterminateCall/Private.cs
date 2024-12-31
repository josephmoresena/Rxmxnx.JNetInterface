namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Private constructor.
	/// </summary>
	/// <param name="definition">Internal definition.</param>
	/// <param name="returnType">Return type signature.</param>
	private IndeterminateCall(JCallDefinition definition, CString returnType)
	{
		this.Definition = definition;
		this.ReturnType = returnType;
	}

	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult FunctionCall(JFunctionDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			IndeterminateResult result = new(out Span<Byte> bytes, signature);
			env.AccessFeature.CallPrimitiveFunction(bytes, jLocal, jClass, definition, nonVirtual, args);
			return result;
		}

		JLocalObject? jObject =
			env.AccessFeature.CallFunction<JLocalObject>(jLocal, jClass, definition, nonVirtual, args);
		return new(jObject, signature);
	}
	/// <summary>
	/// Invokes a static function on given <see cref="JClassObject"/> instance and returns its result.
	/// </summary>
	/// <param name="definition">A <see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Function arguments.</param>
	/// <returns>A <see cref="IndeterminateResult"/> instance.</returns>
	private IndeterminateResult StaticFunctionCall(JFunctionDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jClass.Environment;
		ReadOnlySpan<Byte> signature = this.ReturnType;

		if (signature.Length == 1)
		{
			IndeterminateResult result = new(out Span<Byte> bytes, signature);
			env.AccessFeature.CallPrimitiveStaticFunction(bytes, jClass, definition, args);
			return result;
		}

		JLocalObject? jObject = env.AccessFeature.CallStaticFunction<JLocalObject>(jClass, definition, args);
		return new(jObject, signature);
	}

	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jLocal">Target object.</param>
	/// <param name="jClass">Declaring call class.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Method arguments.</param>
	private static void MethodCall(JMethodDefinition definition, JLocalObject jLocal, JClassObject jClass,
		Boolean nonVirtual, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallMethod(jLocal, jClass, definition, nonVirtual, args);
	}
	/// <summary>
	/// Invokes a method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	private static void StaticMethodCall(JMethodDefinition definition, JClassObject jClass, ReadOnlySpan<IObject?> args)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallStaticMethod(jClass, definition, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	private static TObject NewCall<TObject>(JConstructorDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jClass, definition, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="definition">A <see cref="JConstructorDefinition"/> instance.</param>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
	private static TObject NewCall<TObject>(JConstructorDefinition definition, JConstructorObject jConstructor,
		ReadOnlySpan<IObject?> args) where TObject : JLocalObject, IClassType<TObject>
	{
		IEnvironment env = jConstructor.Environment;
		return env.AccessFeature.CallConstructor<TObject>(jConstructor, definition, args);
	}
	/// <summary>
	/// Creates a primitive <see cref="JFunctionDefinition"/> instance.
	/// </summary>
	/// <param name="functionName">UTF-8 function name.</param>
	/// <param name="returnTypeSignature">Return type signature.</param>
	/// <param name="args">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JFunctionDefinition"/> instance.</returns>
	private static JFunctionDefinition CreatePrimitiveFunction(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<Byte> returnTypeSignature, ReadOnlySpan<JArgumentMetadata> args)
		=> returnTypeSignature[0] switch
		{
			CommonNames.BooleanSignatureChar => JFunctionDefinition<JBoolean>.Create(functionName, args),
			CommonNames.ByteSignatureChar => JFunctionDefinition<JByte>.Create(functionName, args),
			CommonNames.CharSignatureChar => JFunctionDefinition<JChar>.Create(functionName, args),
			CommonNames.DoubleSignatureChar => JFunctionDefinition<JDouble>.Create(functionName, args),
			CommonNames.FloatSignatureChar => JFunctionDefinition<JFloat>.Create(functionName, args),
			CommonNames.IntSignatureChar => JFunctionDefinition<JInt>.Create(functionName, args),
			CommonNames.LongSignatureChar => JFunctionDefinition<JLong>.Create(functionName, args),
			_ => JFunctionDefinition<JShort>.Create(functionName, args),
		};
}