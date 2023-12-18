namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Internal extensions class.
/// </summary>
internal static class InternalExtensions
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
		if (definition.Return == typeof(Byte))
			bytes.AsValue<JBoolean>() =
				JFunctionDefinition<JBoolean>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(SByte))
			bytes.AsValue<JByte>() =
				JFunctionDefinition<JByte>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Char))
			bytes.AsValue<JChar>() =
				JFunctionDefinition<JChar>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Double))
			bytes.AsValue<JDouble>() =
				JFunctionDefinition<JDouble>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Single))
			bytes.AsValue<JFloat>() =
				JFunctionDefinition<JFloat>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Int32))
			bytes.AsValue<JInt>() = JFunctionDefinition<JInt>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Int64))
			bytes.AsValue<JLong>() =
				JFunctionDefinition<JLong>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		else if (definition.Return == typeof(Int16))
			bytes.AsValue<JShort>() =
				JFunctionDefinition<JShort>.Invoke(new(definition), jLocal, jClass, nonVirtual, args);
		throw new InvalidOperationException("Definition is not primitive.");
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
		if (definition.Return == typeof(Byte))
			bytes.AsValue<JBoolean>() = JFunctionDefinition<JBoolean>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(SByte))
			bytes.AsValue<JByte>() = JFunctionDefinition<JByte>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Char))
			bytes.AsValue<JChar>() = JFunctionDefinition<JChar>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Double))
			bytes.AsValue<JDouble>() = JFunctionDefinition<JDouble>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Single))
			bytes.AsValue<JFloat>() = JFunctionDefinition<JFloat>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Int32))
			bytes.AsValue<JInt>() = JFunctionDefinition<JInt>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Int64))
			bytes.AsValue<JLong>() = JFunctionDefinition<JLong>.StaticInvoke(new(definition), jClass, args);
		else if (definition.Return == typeof(Int16))
			bytes.AsValue<JShort>() = JFunctionDefinition<JShort>.StaticInvoke(new(definition), jClass, args);
		throw new InvalidOperationException("Definition is not primitive.");
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
		if (definition.Return == typeof(Byte))
			bytes.AsValue<JBoolean>() = new JFieldDefinition<JBoolean>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(SByte))
			bytes.AsValue<JByte>() = new JFieldDefinition<JByte>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Char))
			bytes.AsValue<JChar>() = new JFieldDefinition<JChar>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Double))
			bytes.AsValue<JDouble>() = new JFieldDefinition<JDouble>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Single))
			bytes.AsValue<JFloat>() = new JFieldDefinition<JFloat>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Int32))
			bytes.AsValue<JInt>() = new JFieldDefinition<JInt>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Int64))
			bytes.AsValue<JLong>() = new JFieldDefinition<JLong>(definition).Get(jLocal, jClass);
		else if (definition.Return == typeof(Int16))
			bytes.AsValue<JShort>() = new JFieldDefinition<JShort>(definition).Get(jLocal, jClass);
		throw new InvalidOperationException("Definition is not primitive.");
	}
	/// <summary>
	/// Retrieves current static field as typed primitive field.
	/// </summary>
	/// <param name="definition"><see cref="JFieldDefinition"/> instance.</param>
	/// <param name="bytes"><see cref="Span{T}"/> to hold field value.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	public static void PrimitiveStaticGet(this JFieldDefinition definition, Span<Byte> bytes, JClassObject jClass)
	{
		if (definition.Return == typeof(Byte))
			bytes.AsValue<JBoolean>() = new JFieldDefinition<JBoolean>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(SByte))
			bytes.AsValue<JByte>() = new JFieldDefinition<JByte>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Char))
			bytes.AsValue<JChar>() = new JFieldDefinition<JChar>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Double))
			bytes.AsValue<JDouble>() = new JFieldDefinition<JDouble>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Single))
			bytes.AsValue<JFloat>() = new JFieldDefinition<JFloat>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Int32))
			bytes.AsValue<JInt>() = new JFieldDefinition<JInt>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Int64))
			bytes.AsValue<JLong>() = new JFieldDefinition<JLong>(definition).StaticGet(jClass);
		else if (definition.Return == typeof(Int16))
			bytes.AsValue<JShort>() = new JFieldDefinition<JShort>(definition).StaticGet(jClass);
		throw new InvalidOperationException("Definition is not primitive.");
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
		if (definition.Return == typeof(Byte))
			new JFieldDefinition<JBoolean>(definition).Set(jLocal, bytes.AsValue<JBoolean>(), jClass);
		else if (definition.Return == typeof(SByte))
			new JFieldDefinition<JByte>(definition).Set(jLocal, bytes.AsValue<JByte>(), jClass);
		else if (definition.Return == typeof(Char))
			new JFieldDefinition<JChar>(definition).Set(jLocal, bytes.AsValue<JChar>(), jClass);
		else if (definition.Return == typeof(Double))
			new JFieldDefinition<JDouble>(definition).Set(jLocal, bytes.AsValue<JDouble>(), jClass);
		else if (definition.Return == typeof(Single))
			new JFieldDefinition<JFloat>(definition).Set(jLocal, bytes.AsValue<JFloat>(), jClass);
		else if (definition.Return == typeof(Int32))
			new JFieldDefinition<JInt>(definition).Set(jLocal, bytes.AsValue<JInt>(), jClass);
		else if (definition.Return == typeof(Int64))
			new JFieldDefinition<JLong>(definition).Set(jLocal, bytes.AsValue<JLong>(), jClass);
		else if (definition.Return == typeof(Int16))
			new JFieldDefinition<JShort>(definition).Set(jLocal, bytes.AsValue<JShort>(), jClass);
		throw new InvalidOperationException("Definition is not primitive.");
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
		if (definition.Return == typeof(Byte))
			new JFieldDefinition<JBoolean>(definition).StaticSet(jClass, bytes.AsValue<JBoolean>());
		else if (definition.Return == typeof(SByte))
			new JFieldDefinition<JByte>(definition).StaticSet(jClass, bytes.AsValue<JByte>());
		else if (definition.Return == typeof(Char))
			new JFieldDefinition<JChar>(definition).StaticSet(jClass, bytes.AsValue<JChar>());
		else if (definition.Return == typeof(Double))
			new JFieldDefinition<JDouble>(definition).StaticSet(jClass, bytes.AsValue<JDouble>());
		else if (definition.Return == typeof(Single))
			new JFieldDefinition<JFloat>(definition).StaticSet(jClass, bytes.AsValue<JFloat>());
		else if (definition.Return == typeof(Int32))
			new JFieldDefinition<JInt>(definition).StaticSet(jClass, bytes.AsValue<JInt>());
		else if (definition.Return == typeof(Int64))
			new JFieldDefinition<JLong>(definition).StaticSet(jClass, bytes.AsValue<JLong>());
		else if (definition.Return == typeof(Int16))
			new JFieldDefinition<JShort>(definition).StaticSet(jClass, bytes.AsValue<JShort>());
		throw new InvalidOperationException("Definition is not primitive.");
	}
	/// <summary>
	/// Normalize argument array to standard form.
	/// </summary>
	/// <param name="args">Argument array.</param>
	/// <returns>Normalized argument array.</returns>
	public static IObject?[] Normalize(this IObject?[] args)
		=> args.Any(o => o is JPrimitiveObject) ? args.Select(o => o.Normalize()).ToArray() : args;
	/// <summary>
	/// Normalize <see cref="IObject"/> instance.
	/// </summary>
	/// <param name="obj">A <see cref="IObject"/> instance.</param>
	/// <returns>Normalized <see cref="IObject"/> instance.</returns>
	public static IObject? Normalize(this IObject? obj)
	{
		if (obj is JPrimitiveObject jObject and not IPrimitiveType)
			return obj.ObjectSignature[0] switch
			{
				0x90 => //Z
					jObject.AsPrimitive<JBoolean, Boolean>(),
				0x66 => //B
					jObject.AsPrimitive<JByte, SByte>(),
				0x67 => //C
					jObject.AsPrimitive<JChar, Char>(),
				0x68 => //D
					jObject.AsPrimitive<JDouble, Double>(),
				0x70 => //F
					jObject.AsPrimitive<JFloat, Single>(),
				0x73 => //I
					jObject.AsPrimitive<JInt, Int32>(),
				0x74 => //J
					jObject.AsPrimitive<JLong, Int64>(),
				0x83 => //S
					jObject.AsPrimitive<JShort, Int16>(),
				_ => throw new InvalidOperationException("Object is not primitive."),
			};
		return obj;
	}
}