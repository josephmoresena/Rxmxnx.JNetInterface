namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal extensions class for proxy objects.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class PrimitiveProxyExtensions
{
	/// <summary>
	/// Invokes current function as typed primitive function.
	/// </summary>
	/// <param name="definition"><see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold result.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">Function arguments.</param>
	public static void PrimitiveInvoke(this JFunctionDefinition definition, Span<Byte> bytes, JLocalObject jLocal,
		JClassObject jClass, Boolean nonVirtual, IObject?[] args)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				bytes.AsValue<JBoolean>() = JFunctionDefinition<JBoolean>.Invoke(
					definition as JFunctionDefinition<JBoolean> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.ByteSignatureChar:
				bytes.AsValue<JByte>() = JFunctionDefinition<JByte>.Invoke(
					definition as JFunctionDefinition<JByte> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.CharSignatureChar:
				bytes.AsValue<JChar>() = JFunctionDefinition<JChar>.Invoke(
					definition as JFunctionDefinition<JChar> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.DoubleSignatureChar:
				bytes.AsValue<JDouble>() = JFunctionDefinition<JDouble>.Invoke(
					definition as JFunctionDefinition<JDouble> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.FloatSignatureChar:
				bytes.AsValue<JFloat>() = JFunctionDefinition<JFloat>.Invoke(
					definition as JFunctionDefinition<JFloat> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.IntSignatureChar:
				bytes.AsValue<JInt>() = JFunctionDefinition<JInt>.Invoke(
					definition as JFunctionDefinition<JInt> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.LongSignatureChar:
				bytes.AsValue<JLong>() = JFunctionDefinition<JLong>.Invoke(
					definition as JFunctionDefinition<JLong> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			case CommonNames.ShortSignatureChar:
				bytes.AsValue<JShort>() = JFunctionDefinition<JShort>.Invoke(
					definition as JFunctionDefinition<JShort> ?? new(definition), jLocal, jClass, nonVirtual, args);
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Invokes current function as static typed primitive function.
	/// </summary>
	/// <param name="definition"><see cref="JFunctionDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold result.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="args">Function arguments.</param>
	public static void PrimitiveStaticInvoke(this JFunctionDefinition definition, Span<Byte> bytes, JClassObject jClass,
		IObject?[] args)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				bytes.AsValue<JBoolean>() =
					JFunctionDefinition<JBoolean>.StaticInvoke(
						definition as JFunctionDefinition<JBoolean> ?? new(definition), jClass, args);
				break;
			case CommonNames.ByteSignatureChar:
				bytes.AsValue<JByte>() =
					JFunctionDefinition<JByte>.StaticInvoke(definition as JFunctionDefinition<JByte> ?? new(definition),
					                                        jClass, args);
				break;
			case CommonNames.CharSignatureChar:
				bytes.AsValue<JChar>() =
					JFunctionDefinition<JChar>.StaticInvoke(definition as JFunctionDefinition<JChar> ?? new(definition),
					                                        jClass, args);
				break;
			case CommonNames.DoubleSignatureChar:
				bytes.AsValue<JDouble>() =
					JFunctionDefinition<JDouble>.StaticInvoke(
						definition as JFunctionDefinition<JDouble> ?? new(definition), jClass, args);
				break;
			case CommonNames.FloatSignatureChar:
				bytes.AsValue<JFloat>() =
					JFunctionDefinition<JFloat>.StaticInvoke(
						definition as JFunctionDefinition<JFloat> ?? new(definition), jClass, args);
				break;
			case CommonNames.IntSignatureChar:
				bytes.AsValue<JInt>() =
					JFunctionDefinition<JInt>.StaticInvoke(definition as JFunctionDefinition<JInt> ?? new(definition),
					                                       jClass, args);
				break;
			case CommonNames.LongSignatureChar:
				bytes.AsValue<JLong>() =
					JFunctionDefinition<JLong>.StaticInvoke(definition as JFunctionDefinition<JLong> ?? new(definition),
					                                        jClass, args);
				break;
			case CommonNames.ShortSignatureChar:
				bytes.AsValue<JShort>() =
					JFunctionDefinition<JShort>.StaticInvoke(
						definition as JFunctionDefinition<JShort> ?? new(definition), jClass, args);
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Retrieves current field as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold field value.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	public static void PrimitiveGet(this JFieldDefinition definition, Span<Byte> bytes, JLocalObject jLocal,
		JClassObject jClass)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				bytes.AsValue<JBoolean>() =
					(definition as JFieldDefinition<JBoolean> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.ByteSignatureChar:
				bytes.AsValue<JByte>() = (definition as JFieldDefinition<JByte> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.CharSignatureChar:
				bytes.AsValue<JChar>() = (definition as JFieldDefinition<JChar> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.DoubleSignatureChar:
				bytes.AsValue<JDouble>() =
					(definition as JFieldDefinition<JDouble> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.FloatSignatureChar:
				bytes.AsValue<JFloat>() =
					(definition as JFieldDefinition<JFloat> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.IntSignatureChar:
				bytes.AsValue<JInt>() = (definition as JFieldDefinition<JInt> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.LongSignatureChar:
				bytes.AsValue<JLong>() = (definition as JFieldDefinition<JLong> ?? new(definition)).Get(jLocal, jClass);
				break;
			case CommonNames.ShortSignatureChar:
				bytes.AsValue<JShort>() =
					(definition as JFieldDefinition<JShort> ?? new(definition)).Get(jLocal, jClass);
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Retrieves current static field as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold field value.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	public static void PrimitiveStaticGet(this JFieldDefinition definition, Span<Byte> bytes, JClassObject jClass)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				bytes.AsValue<JBoolean>() =
					(definition as JFieldDefinition<JBoolean> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.ByteSignatureChar:
				bytes.AsValue<JByte>() = (definition as JFieldDefinition<JByte> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.CharSignatureChar:
				bytes.AsValue<JChar>() = (definition as JFieldDefinition<JChar> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.DoubleSignatureChar:
				bytes.AsValue<JDouble>() =
					(definition as JFieldDefinition<JDouble> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.FloatSignatureChar:
				bytes.AsValue<JFloat>() = (definition as JFieldDefinition<JFloat> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.IntSignatureChar:
				bytes.AsValue<JInt>() = (definition as JFieldDefinition<JInt> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.LongSignatureChar:
				bytes.AsValue<JLong>() = (definition as JFieldDefinition<JLong> ?? new(definition)).StaticGet(jClass);
				break;
			case CommonNames.ShortSignatureChar:
				bytes.AsValue<JShort>() = (definition as JFieldDefinition<JShort> ?? new(definition)).StaticGet(jClass);
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Sets current field value as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> holding field value to set to.</param>
	public static void PrimitiveSet(this JFieldDefinition definition, JLocalObject jLocal, JClassObject jClass,
		ReadOnlySpan<Byte> bytes)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				(definition as JFieldDefinition<JBoolean> ?? new(definition)).Set(
					jLocal, bytes.AsValue<JBoolean>(), jClass);
				break;
			case CommonNames.ByteSignatureChar:
				(definition as JFieldDefinition<JByte> ?? new(definition)).Set(jLocal, bytes.AsValue<JByte>(), jClass);
				break;
			case CommonNames.CharSignatureChar:
				(definition as JFieldDefinition<JChar> ?? new(definition)).Set(jLocal, bytes.AsValue<JChar>(), jClass);
				break;
			case CommonNames.DoubleSignatureChar:
				(definition as JFieldDefinition<JDouble> ?? new(definition)).Set(
					jLocal, bytes.AsValue<JDouble>(), jClass);
				break;
			case CommonNames.FloatSignatureChar:
				(definition as JFieldDefinition<JFloat> ?? new(definition)).Set(
					jLocal, bytes.AsValue<JFloat>(), jClass);
				break;
			case CommonNames.IntSignatureChar:
				(definition as JFieldDefinition<JInt> ?? new(definition)).Set(jLocal, bytes.AsValue<JInt>(), jClass);
				break;
			case CommonNames.LongSignatureChar:
				(definition as JFieldDefinition<JLong> ?? new(definition)).Set(jLocal, bytes.AsValue<JLong>(), jClass);
				break;
			case CommonNames.ShortSignatureChar:
				(definition as JFieldDefinition<JShort> ?? new(definition)).Set(
					jLocal, bytes.AsValue<JShort>(), jClass);
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Sets current static field value as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> holding field value to set to.</param>
	public static void PrimitiveStaticSet(this JFieldDefinition definition, JClassObject jClass,
		ReadOnlySpan<Byte> bytes)
	{
		switch (definition.Descriptor[^1])
		{
			case CommonNames.BooleanSignatureChar:
				(definition as JFieldDefinition<JBoolean> ?? new(definition)).StaticSet(
					jClass, bytes.AsValue<JBoolean>());
				break;
			case CommonNames.ByteSignatureChar:
				(definition as JFieldDefinition<JByte> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JByte>());
				break;
			case CommonNames.CharSignatureChar:
				(definition as JFieldDefinition<JChar> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JChar>());
				break;
			case CommonNames.DoubleSignatureChar:
				(definition as JFieldDefinition<JDouble> ?? new(definition))
					.StaticSet(jClass, bytes.AsValue<JDouble>());
				break;
			case CommonNames.FloatSignatureChar:
				(definition as JFieldDefinition<JFloat> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JFloat>());
				break;
			case CommonNames.IntSignatureChar:
				(definition as JFieldDefinition<JInt> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JInt>());
				break;
			case CommonNames.LongSignatureChar:
				(definition as JFieldDefinition<JLong> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JLong>());
				break;
			case CommonNames.ShortSignatureChar:
				(definition as JFieldDefinition<JShort> ?? new(definition)).StaticSet(jClass, bytes.AsValue<JShort>());
				break;
			default:
				throw new InvalidOperationException(IMessageResource.GetInstance().InvalidPrimitiveDefinitionMessage);
		}
	}
	/// <summary>
	/// Normalize argument array to standard form.
	/// </summary>
	/// <param name="args">Argument array.</param>
	/// <returns>Normalized argument array.</returns>
	public static IObject?[] Normalize(this IObject?[] args)
		=> Array.Exists(args, o => o is JPrimitiveObject) ? args.Select(o => o.Normalize()).ToArray() : args;

	/// <summary>
	/// Normalize <see cref="IObject"/> instance.
	/// </summary>
	/// <param name="obj">A <see cref="IObject"/> instance.</param>
	/// <returns>Normalized <see cref="IObject"/> instance.</returns>
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