namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceProvider
	{
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive)
			where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			return metadata.Signature[0] switch
			{
				0x90 => //Z
					this.Register(JBooleanObject.Create(env, Unsafe.As<TPrimitive, JBoolean>(ref primitive))),
				0x66 => //B
					this.Register(JByteObject.Create(env, Unsafe.As<TPrimitive, JByte>(ref primitive))),
				0x67 => //C
					this.Register(JCharacterObject.Create(env, Unsafe.As<TPrimitive, JChar>(ref primitive))),
				0x68 => //D
					this.Register(JDoubleObject.Create(env, Unsafe.As<TPrimitive, JDouble>(ref primitive))),
				0x70 => //F
					this.Register(JFloatObject.Create(env, Unsafe.As<TPrimitive, JFloat>(ref primitive))),
				0x73 => //I
					this.Register(JIntegerObject.Create(env, Unsafe.As<TPrimitive, JInt>(ref primitive))),
				0x74 => //J
					this.Register(JLongObject.Create(env, Unsafe.As<TPrimitive, JLong>(ref primitive))),
				0x83 => //S
					this.Register(JShortObject.Create(env, Unsafe.As<TPrimitive, JShort>(ref primitive))),
				_ => throw new InvalidOperationException("Object is not primitive."),
			};
		}
		public Boolean ReloadObject(JLocalObject jLocal)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);

			JClassObject? jClass = jLocal as JClassObject;
			JObjectLocalRef localRef = jLocal.InternalReference;
			JObjectLocalRef globalRef = jLocal.As<JObjectLocalRef>();

			if (jClass?.Hash is not null) return true;
			if (!this.JniSecure() || localRef != default || globalRef == default) return false;

			NewLocalRefDelegate newLocalRef = this.GetDelegate<NewLocalRefDelegate>();
			localRef = newLocalRef(this.Reference, globalRef);
			this.CheckJniError();
			jLocal.SetValue(localRef);

			return true;
		}
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
			=> throw new NotImplementedException();
		public Boolean Unload(JLocalObject jLocal)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			Boolean isClass = jLocal is JClassObject;
			JObjectLocalRef localRef = jLocal.InternalReference;
			try
			{
				if (localRef != default && this.JniSecure())
				{
					DeleteLocalRefDelegate deleteLocalRef = this.GetDelegate<DeleteLocalRefDelegate>();
					deleteLocalRef(this.Reference, localRef);
				}
			}
			finally
			{
				//jLocal.ClearValue();
				this._objects.Remove(localRef);
			}
			return !isClass;
		}
		public Boolean Unload(JGlobalBase jGlobal) => throw new NotImplementedException();
		public Boolean IsParameter(JLocalObject jLocal) => throw new NotImplementedException();

		/// <summary>
		/// Registers a <typeparamref name="TObject"/> in current <see cref="IEnvironment"/> instance.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jObject">A <see cref="IDataType{TObject}"/> instance.</param>
		/// <returns>Registered <see cref="IDataType{TObject}"/> instance.</returns>
		[return: NotNullIfNotNull(nameof(jObject))]
		public TObject? Register<TObject>(TObject? jObject) where TObject : IDataType<TObject>
		{
			if (jObject is JClassObject jClass) this._classes[jClass.Hash] = jClass;
			JLocalObject? jLocal = jObject as JLocalObject;
			ValidationUtilities.ThrowIfDummy(jLocal);
			if (!JObject.IsNullOrDefault(jLocal)) this._objects[jLocal.As<JObjectLocalRef>()] = new(jLocal);
			return jObject;
		}

		/// <summary>
		/// Applies cast from <see cref="JLocalObject"/> to <typeparamref name="TObject"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <typeparamref name="TObject"/> instance.</returns>
		private TObject? Cast<TObject>(JLocalObject? jLocal) where TObject : IDataType<TObject>
		{
			if (jLocal is null || jLocal.IsDefault) return default;
			if (typeof(TObject) == typeof(JLocalObject))
				return this.Register((TObject)(Object)jLocal);
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TObject>();
			TObject result = (TObject)(Object)metadata.ParseInstance(jLocal);
			jLocal.Dispose();
			return this.Register(result);
		}
	}
}