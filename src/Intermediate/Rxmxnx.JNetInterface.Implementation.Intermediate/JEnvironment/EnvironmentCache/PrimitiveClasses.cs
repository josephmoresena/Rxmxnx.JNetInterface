namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial class EnvironmentCache
	{
		JClassObject IClassFeature.VoidObject => this.GetClass<JVoidObject>();
		JClassObject IClassFeature.BooleanObject => this.GetClass<JBooleanObject>();
		JClassObject IClassFeature.ByteObject => this.GetClass<JByteObject>();
		JClassObject IClassFeature.CharacterObject => this.GetClass<JCharacterObject>();
		JClassObject IClassFeature.DoubleObject => this.GetClass<JDoubleObject>();
		JClassObject IClassFeature.FloatObject => this.GetClass<JFloatObject>();
		JClassObject IClassFeature.IntegerObject => this.GetClass<JIntegerObject>();
		JClassObject IClassFeature.LongObject => this.GetClass<JLongObject>();
		JClassObject IClassFeature.ShortObject => this.GetClass<JShortObject>();

		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> reference for primitive class.
		/// </summary>
		/// <param name="signature">Primitive signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef FindPrimitiveClass(Byte signature)
		{
			JClassObject wrapperClass = this.GetPrimitiveWrapperClass(signature);
			if (!JObject.IsNullOrDefault(wrapperClass))
			{
				JFieldDefinition fieldDefinition = NativeFunctionSetImpl.PrimitiveTypeDefinition;
				JObjectLocalRef localRef = this.GetStaticObjectField(wrapperClass, fieldDefinition);
				return JClassLocalRef.FromReference(in localRef);
			}

			JClassLocalRef classRef = this.FindMainClass(wrapperClass.Name, wrapperClass.ClassSignature);
			return this.FindPrimitiveClass(classRef, ClassNameHelper.GetPrimitiveClassName(signature));
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> reference for primitive class.
		/// </summary>
		/// <param name="wClassRef">Wrapper class reference.</param>
		/// <param name="className">Primitive class name.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef FindPrimitiveClass(JClassLocalRef wClassRef, String className)
		{
			try
			{
				JFieldId typeFieldId =
					this._env.GetStaticFieldId(NativeFunctionSetImpl.PrimitiveTypeDefinition, wClassRef, true);
				if (typeFieldId != default)
				{
					JObjectLocalRef localRef = this.GetStaticObjectField(wClassRef, typeFieldId, true);
					if (localRef != default) return JClassLocalRef.FromReference(in localRef);
				}
			}
			finally
			{
				this._env.DeleteLocalRef(wClassRef.Value);
			}

			this._env.DescribeException();
			this.ClearException();
			throw new NotSupportedException($"Primitive class {className} is not available for JNI access.");
		}

		/// <summary>
		/// Retrieves primitive class instance for <paramref name="className"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		/// <exception cref="ArgumentException">Non-primitive class.</exception>
		private JClassObject GetPrimitiveClass(ReadOnlySpan<Byte> className)
		{
			if (className.Length is < 3 or > 7)
				throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage);
			return className[0] switch
			{
				(Byte)'b' when "boolean"u8.SequenceEqual(className) => this.BooleanPrimitive,
				(Byte)'b' when "byte"u8.SequenceEqual(className) => this.BytePrimitive,
				(Byte)'c' when "char"u8.SequenceEqual(className) => this.CharPrimitive,
				(Byte)'d' when "double"u8.SequenceEqual(className) => this.DoublePrimitive,
				(Byte)'f' when "float"u8.SequenceEqual(className) => this.FloatPrimitive,
				(Byte)'i' when "int"u8.SequenceEqual(className) => this.IntPrimitive,
				(Byte)'l' when "long"u8.SequenceEqual(className) => this.LongPrimitive,
				(Byte)'s' when "short"u8.SequenceEqual(className) => this.ShortPrimitive,
				(Byte)'v' when "void"u8.SequenceEqual(className) => this.VoidPrimitive,
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
		}
	}
}