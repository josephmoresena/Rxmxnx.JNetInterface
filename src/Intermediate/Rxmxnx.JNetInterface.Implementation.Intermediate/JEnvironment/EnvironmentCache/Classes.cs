namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache
	{
		/// <summary>
		/// Class cache cache.
		/// </summary>
		public ClassCache GetClassCache() => this._classes;
		/// <summary>
		/// Retrieves the <see cref="JClassObject"/> according to <paramref name="classRef"/>.
		/// </summary>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <param name="keepReference">Indicates whether class reference should be assigned to created object.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		public JClassObject GetClass(JClassLocalRef classRef, Boolean keepReference)
		{
			using JStringObject jString = JClassObject.GetClassName(this._env, classRef, out Boolean isPrimitive);
			using JNativeMemory<Byte> utf8Text = jString.GetNativeUtf8Chars();
			JClassObject jClass = isPrimitive ?
				this.GetPrimitiveClass(utf8Text.Values) :
				this.GetClass(utf8Text.Values, keepReference ? classRef : default);
			if (keepReference && jClass.InternalReference == default) jClass.SetValue(classRef);
			return jClass;
		}
		/// <inheritdoc cref="JEnvironment.LoadClass(JClassObject?)"/>
		public void LoadClass(JClassObject? jClass)
		{
			if (jClass is null) return;
			this._classes[jClass.Hash] = jClass;
			this.VirtualMachine.LoadGlobal(jClass);
		}
		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> reference for primitive class.
		/// </summary>
		/// <param name="signature">Primitive signature.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public JClassLocalRef FindPrimitiveClass(Byte signature)
		{
			using JClassObject wrapperClass = signature switch
			{
				UnicodePrimitiveSignatures.BooleanSignatureChar => this.GetClass<JBooleanObject>(),
				UnicodePrimitiveSignatures.ByteSignatureChar => this.GetClass<JByteObject>(),
				UnicodePrimitiveSignatures.CharSignatureChar => this.GetClass<JCharacterObject>(),
				UnicodePrimitiveSignatures.DoubleSignatureChar => this.GetClass<JDoubleObject>(),
				UnicodePrimitiveSignatures.FloatSignatureChar => this.GetClass<JFloatObject>(),
				UnicodePrimitiveSignatures.IntSignatureChar => this.GetClass<JIntegerObject>(),
				UnicodePrimitiveSignatures.LongSignatureChar => this.GetClass<JLongObject>(),
				UnicodePrimitiveSignatures.ShortSignatureChar => this.GetClass<JShortObject>(),
				_ => this.GetClass<JVoidObject>(),
			};
			JObjectLocalRef localRef =
				this.GetStaticObjectField(wrapperClass, InternalFunctionCache.PrimitiveTypeDefinition);
			return NativeUtilities.Transform<JObjectLocalRef, JClassLocalRef>(in localRef);
		}

		/// <summary>
		/// Load main classes.
		/// </summary>
		private void LoadMainClasses()
		{
			this.Register(this.ClassObject);
			this.Register(this.ThrowableObject);
			this.Register(this.StackTraceElementObject);

			this.Register(this.BooleanPrimitive);
			this.Register(this.BytePrimitive);
			this.Register(this.CharPrimitive);
			this.Register(this.DoublePrimitive);
			this.Register(this.FloatPrimitive);
			this.Register(this.IntPrimitive);
			this.Register(this.LongPrimitive);
			this.Register(this.ShortPrimitive);
		}
		/// <summary>
		/// Reloads current class object.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>Current <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef ReloadClass(JClassObject? jClass)
		{
			if (jClass is null) return default;
			JClassLocalRef classRef = jClass.As<JClassLocalRef>();
			if (classRef.Value != default) return classRef;
			classRef = this.FindClass(jClass);
			jClass.SetValue(classRef);
			this.Register(jClass);
			return classRef;
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
					throw new ArgumentException("Invalid primitive type."),
				4 => className[0] switch
				{
					0x62 //b
						=> this.BooleanPrimitive,
					0x63 //c
						=> this.CharPrimitive,
					0x6C //l
						=> this.LongPrimitive,
					_ => throw new ArgumentException("Invalid primitive type."),
				},
				5 => className[0] switch
				{
					0x66 //f
						=> this.FloatPrimitive,
					0x73 //l
						=> this.ShortPrimitive,
					_ => throw new ArgumentException("Invalid primitive type."),
				},
				6 => className[0] == 0x64 /*d*/ ?
					this.DoublePrimitive :
					throw new ArgumentException("Invalid primitive type."),
				7 => className[0] == 0x62 /*b*/ ?
					this.BooleanPrimitive :
					throw new ArgumentException("Invalid primitive type."),
				_ => throw new ArgumentException("Invalid primitive type."),
			};
		/// <summary>
		/// Retrieves class instance for <paramref name="classRef"/>.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
		/// <returns>A <see cref="JClassObject"/> instance.</returns>
		private JClassObject GetClass(ReadOnlySpan<Byte> className, JClassLocalRef classRef)
		{
			CStringSequence classInformation = MetadataHelper.GetClassInformation(className);
			if (!this.TryGetClass(classInformation.ToString(), out JClassObject? jClass))
				jClass = new(this.ClassObject, new TypeInformation(classInformation), classRef);
			return jClass;
		}
		/// <summary>
		/// Attempts to get the value associated with the specified hash from the cache.
		/// </summary>
		/// <param name="hash">The hash class to get.</param>
		/// <param name="jClass"></param>
		/// <returns>
		/// <see langword="true"/> if the hash was found in the cache; otherwise, <see langword="false"/>.
		/// </returns>
		private Boolean TryGetClass(String hash, [NotNullWhen(true)] out JClassObject? jClass)
			=> this._classes.TryGetValue(hash, out jClass);
		/// <summary>
		/// Retrieves <see cref="JClassLocalRef"/> reference for given instance.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		private JClassLocalRef FindClass(JClassObject jClass)
			=> jClass.ClassSignature.Length != 1 ?
				jClass.Name.WithSafeFixed(this, EnvironmentCache.FindClass) :
				this.FindPrimitiveClass(jClass.ClassSignature[0]);

		/// <summary>
		/// Retrieves a <see cref="JClassLocalRef"/> using <paramref name="classNameCtx"/> as class name.
		/// </summary>
		/// <param name="classNameCtx">A <see cref="IReadOnlyFixedMemory"/> instance.</param>
		/// <param name="cache">Current <see cref="EnvironmentCache"/> instance.</param>
		/// <returns>A <see cref="JClassLocalRef"/> reference.</returns>
		public static JClassLocalRef FindClass(in IReadOnlyFixedMemory classNameCtx, EnvironmentCache cache)
		{
			FindClassDelegate findClass = cache.GetDelegate<FindClassDelegate>();
			JClassLocalRef result = findClass(cache.Reference, (ReadOnlyValPtr<Byte>)classNameCtx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
	}
}