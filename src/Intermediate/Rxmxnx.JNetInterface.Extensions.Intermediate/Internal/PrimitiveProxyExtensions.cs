// ReSharper disable ConvertToExtensionBlock

namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal extensions class for proxy objects.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
internal static unsafe class PrimitiveProxyExtensions
{
	/// <summary>
	/// Invokes current function as typed primitive function.
	/// </summary>
	/// <param name="definition"><see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold a result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	public static void PrimitiveInvoke(this JFunctionDefinition definition, Span<Byte> bytes, JLocalObject jLocal,
		JClassObject jClass, Boolean nonVirtual, IObject?[] args)
	{
		delegate* <Span<Byte>, JFunctionDefinition, JLocalObject, JClassObject, Boolean, IObject?[], void> invoke =
			definition.Descriptor[^1] switch
			{
				CommonNames.BooleanSignatureChar => &Invoke<JBoolean>,
				CommonNames.ByteSignatureChar => &Invoke<JByte>,
				CommonNames.CharSignatureChar => &Invoke<JChar>,
				CommonNames.DoubleSignatureChar => &Invoke<JDouble>,
				CommonNames.FloatSignatureChar => &Invoke<JFloat>,
				CommonNames.IntSignatureChar => &Invoke<JInt>,
				CommonNames.LongSignatureChar => &Invoke<JLong>,
				CommonNames.ShortSignatureChar => &Invoke<JShort>,
				_ => throw new InvalidOperationException(IMessageResource.GetInstance()
				                                                         .InvalidPrimitiveDefinitionMessage),
			};
		invoke(bytes, definition, jLocal, jClass, nonVirtual, args);
		return;
		static void Invoke<TPrimitive>(Span<Byte> bytes, JFunctionDefinition definition, JLocalObject jLocal,
			JClassObject jClass, Boolean nonVirtual, IObject?[] args)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			IEnvironment env = jLocal.Environment;
			bytes.AsValue<TPrimitive>() = env.AccessFeature.CallFunction<TPrimitive>(
				jLocal, jClass, definition as JFunctionDefinition<TPrimitive> ?? new(definition), nonVirtual, args);
		}
	}
	/// <summary>
	/// Invokes the current function as a static-typed primitive function.
	/// </summary>
	/// <param name="definition"><see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold a result.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="args">Function arguments.</param>
	public static void PrimitiveStaticInvoke(this JFunctionDefinition definition, Span<Byte> bytes, JClassObject jClass,
		IObject?[] args)
	{
		delegate* <Span<Byte>, JFunctionDefinition, JClassObject, IObject?[], void> invoke =
			definition.Descriptor[^1] switch
			{
				CommonNames.BooleanSignatureChar => &Invoke<JBoolean>,
				CommonNames.ByteSignatureChar => &Invoke<JByte>,
				CommonNames.CharSignatureChar => &Invoke<JChar>,
				CommonNames.DoubleSignatureChar => &Invoke<JDouble>,
				CommonNames.FloatSignatureChar => &Invoke<JFloat>,
				CommonNames.IntSignatureChar => &Invoke<JInt>,
				CommonNames.LongSignatureChar => &Invoke<JLong>,
				CommonNames.ShortSignatureChar => &Invoke<JShort>,
				_ => throw new InvalidOperationException(IMessageResource.GetInstance()
				                                                         .InvalidPrimitiveDefinitionMessage),
			};
		invoke(bytes, definition, jClass, args);
		return;
		static void Invoke<TPrimitive>(Span<Byte> bytes, JFunctionDefinition definition, JClassObject jClass,
			IObject?[] args) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			IEnvironment env = jClass.Environment;
			bytes.AsValue<TPrimitive>() =
				env.AccessFeature.CallStaticFunction<TPrimitive>(
					jClass, definition as JFunctionDefinition<TPrimitive> ?? new(definition), args);
		}
	}
	/// <summary>
	/// Retrieves the current field as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold field value.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	public static void PrimitiveGet(this JFieldDefinition definition, Span<Byte> bytes, JLocalObject jLocal,
		JClassObject jClass)
	{
		delegate* <Span<Byte>, JFieldDefinition, JLocalObject, JClassObject, void> get =
			definition.Descriptor[^1] switch
			{
				CommonNames.BooleanSignatureChar => &Get<JBoolean>,
				CommonNames.ByteSignatureChar => &Get<JByte>,
				CommonNames.CharSignatureChar => &Get<JChar>,
				CommonNames.DoubleSignatureChar => &Get<JDouble>,
				CommonNames.FloatSignatureChar => &Get<JFloat>,
				CommonNames.IntSignatureChar => &Get<JInt>,
				CommonNames.LongSignatureChar => &Get<JLong>,
				CommonNames.ShortSignatureChar => &Get<JShort>,
				_ => throw new InvalidOperationException(IMessageResource.GetInstance()
				                                                         .InvalidPrimitiveDefinitionMessage),
			};
		get(bytes, definition, jLocal, jClass);
		return;
		static void Get<TPrimitive>(Span<Byte> bytes, JFieldDefinition definition, JLocalObject jLocal,
			JClassObject jClass) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			bytes.AsValue<TPrimitive>() =
				(definition as JFieldDefinition<TPrimitive> ?? new(definition)).Get(jLocal, jClass);
		}
	}
	/// <summary>
	/// Retrieves the current static field as a typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold field value.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	public static void PrimitiveStaticGet(this JFieldDefinition definition, Span<Byte> bytes, JClassObject jClass)
	{
		delegate* <Span<Byte>, JFieldDefinition, JClassObject, void> get = definition.Descriptor[^1] switch
		{
			CommonNames.BooleanSignatureChar => &Get<JBoolean>,
			CommonNames.ByteSignatureChar => &Get<JByte>,
			CommonNames.CharSignatureChar => &Get<JChar>,
			CommonNames.DoubleSignatureChar => &Get<JDouble>,
			CommonNames.FloatSignatureChar => &Get<JFloat>,
			CommonNames.IntSignatureChar => &Get<JInt>,
			CommonNames.LongSignatureChar => &Get<JLong>,
			CommonNames.ShortSignatureChar => &Get<JShort>,
			_ => throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage),
		};
		get(bytes, definition, jClass);
		return;
		static void Get<TPrimitive>(Span<Byte> bytes, JFieldDefinition definition, JClassObject jClass)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			bytes.AsValue<TPrimitive>() =
				(definition as JFieldDefinition<TPrimitive> ?? new(definition)).StaticGet(jClass);
		}
	}
	/// <summary>
	/// Sets the current field value as a typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> holding field value to set to.</param>
	public static void PrimitiveSet(this JFieldDefinition definition, JLocalObject jLocal, JClassObject jClass,
		ReadOnlySpan<Byte> bytes)
	{
		delegate* <ReadOnlySpan<Byte>, JFieldDefinition, JLocalObject, JClassObject, void> set =
			definition.Descriptor[^1] switch
			{
				CommonNames.BooleanSignatureChar => &Set<JBoolean>,
				CommonNames.ByteSignatureChar => &Set<JByte>,
				CommonNames.CharSignatureChar => &Set<JChar>,
				CommonNames.DoubleSignatureChar => &Set<JDouble>,
				CommonNames.FloatSignatureChar => &Set<JFloat>,
				CommonNames.IntSignatureChar => &Set<JInt>,
				CommonNames.LongSignatureChar => &Set<JLong>,
				CommonNames.ShortSignatureChar => &Set<JShort>,
				_ => throw new InvalidOperationException(IMessageResource.GetInstance()
				                                                         .InvalidPrimitiveDefinitionMessage),
			};
		set(bytes, definition, jLocal, jClass);
		return;
		static void Set<TPrimitive>(ReadOnlySpan<Byte> bytes, JFieldDefinition definition, JLocalObject jLocal,
			JClassObject jClass) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			(definition as JFieldDefinition<TPrimitive> ?? new(definition)).Set(
				jLocal, bytes.AsValue<TPrimitive>(), jClass);
		}
	}
	/// <summary>
	/// Sets the current static field value as a typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> holding field value to set to.</param>
	public static void PrimitiveStaticSet(this JFieldDefinition definition, JClassObject jClass,
		ReadOnlySpan<Byte> bytes)
	{
		delegate* <ReadOnlySpan<Byte>, JFieldDefinition, JClassObject, void> set = definition.Descriptor[^1] switch
		{
			CommonNames.BooleanSignatureChar => &Set<JBoolean>,
			CommonNames.ByteSignatureChar => &Set<JByte>,
			CommonNames.CharSignatureChar => &Set<JChar>,
			CommonNames.DoubleSignatureChar => &Set<JDouble>,
			CommonNames.FloatSignatureChar => &Set<JFloat>,
			CommonNames.IntSignatureChar => &Set<JInt>,
			CommonNames.LongSignatureChar => &Set<JLong>,
			CommonNames.ShortSignatureChar => &Set<JShort>,
			_ => throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage),
		};
		set(bytes, definition, jClass);
		return;
		static void Set<TPrimitive>(ReadOnlySpan<Byte> bytes, JFieldDefinition definition, JClassObject jClass)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			(definition as JFieldDefinition<TPrimitive> ?? new(definition)).StaticSet(
				jClass, bytes.AsValue<TPrimitive>());
		}
	}
	/// <summary>
	/// Normalize an argument array to standard form.
	/// </summary>
	/// <param name="args">Argument array.</param>
	/// <returns>Normalized argument array.</returns>
	public static IObject?[] Normalize(this IObject?[] args)
		=> Array.Exists(args, o => o is JPrimitiveObject) ? [.. args.Select(o => o.Normalize()),] : args;

	/// <summary>
	/// Normalize <see cref="IObject"/> instance.
	/// </summary>
	/// <param name="obj">A <see cref="IObject"/> instance.</param>
	/// <returns>Normalized <see cref="IObject"/> instance.</returns>
#if !PACKAGE
	[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
#endif
	private static IObject? Normalize(this IObject? obj)
	{
		if (obj is JPrimitiveObject jObject and not IPrimitiveType)
			return obj.ObjectSignature[0] switch
			{
				CommonNames.BooleanSignatureChar => jObject.AsPrimitive<JBoolean, Boolean>(),
				CommonNames.ByteSignatureChar => jObject.AsPrimitive<JByte, SByte>(),
				CommonNames.CharSignatureChar => jObject.AsPrimitive<JChar, Char>(),
				CommonNames.DoubleSignatureChar => jObject.AsPrimitive<JDouble, Double>(),
				CommonNames.FloatSignatureChar => jObject.AsPrimitive<JFloat, Single>(),
				CommonNames.IntSignatureChar => jObject.AsPrimitive<JInt, Int32>(),
				CommonNames.LongSignatureChar => jObject.AsPrimitive<JLong, Int64>(),
				CommonNames.ShortSignatureChar => jObject.AsPrimitive<JShort, Int16>(),
				_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
			};
		return obj;
	}
}