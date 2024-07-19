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
			JFieldDefinition fieldDefinition = NativeFunctionSetImpl.PrimitiveTypeDefinition;
			if (!JObject.IsNullOrDefault(wrapperClass))
			{
				JObjectLocalRef localRef = this.GetStaticObjectField(wrapperClass, fieldDefinition);
				return JClassLocalRef.FromReference(in localRef);
			}

			JClassLocalRef classRef = this.FindMainClass(new(wrapperClass));
			try
			{
				JFieldId typeFieldId = this._env.GetStaticFieldId(fieldDefinition, classRef, true);
				if (typeFieldId != default)
				{
					JObjectLocalRef localRef = this.GetStaticObjectField(classRef, typeFieldId, true);
					if (localRef != default) return JClassLocalRef.FromReference(in localRef);
				}
			}
			finally
			{
				this._env.DeleteLocalRef(classRef.Value);
			}

			(this._env as IEnvironment).DescribeException();
			this.ClearException();
			throw new NotSupportedException(
				$"Primitive class {ClassNameHelper.GetPrimitiveClassName(signature)} is not available for JNI access.");
		}
		/// <summary>
		/// Retrieves primitive class instance for <paramref name="className"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		/// <exception cref="ArgumentException">Non-primitive class.</exception>
		private JClassObject GetPrimitiveClass(ReadOnlySpan<Byte> className)
			=> className.Length switch
			{
				3 => className[0] == 0x69 /*i*/ ?
					this.IntPrimitive :
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
				4 => className[0] switch
				{
					0x62 //b
						=> this.BooleanPrimitive,
					0x63 //c
						=> this.CharPrimitive,
					0x6C //l
						=> this.LongPrimitive,
					_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
				},
				5 => className[0] switch
				{
					0x66 //f
						=> this.FloatPrimitive,
					0x73 //l
						=> this.ShortPrimitive,
					_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
				},
				6 => className[0] == 0x64 /*d*/ ?
					this.DoublePrimitive :
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
				7 => className[0] == 0x62 /*b*/ ?
					this.BooleanPrimitive :
					throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
				_ => throw new ArgumentException(CommonConstants.InvalidPrimitiveTypeMessage),
			};
	}
}