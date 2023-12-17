namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IAccessProvider
	{
		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			AccessCache access = this.GetAccess(jLocal.Class);
			JFieldId fieldId = access.GetFieldId(definition, this.VirtualMachine.GetEnvironment(this.Reference));
			this.ReloadClass(jLocal as JClassObject);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					GetBooleanFieldDelegate getBooleanField = this.GetDelegate<GetBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getBooleanField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x66: //B
					GetByteFieldDelegate getByteField = this.GetDelegate<GetByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getByteField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x67: //C
					GetCharFieldDelegate getCharField = this.GetDelegate<GetCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getCharField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x68: //D
					GetDoubleFieldDelegate getDoubleField = this.GetDelegate<GetDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = getDoubleField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x70: //F
					GetFloatFieldDelegate getFloatField = this.GetDelegate<GetFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x73: //I
					GetIntFieldDelegate getIntField = this.GetDelegate<GetIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getIntField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x74: //J
					GetLongFieldDelegate getLongField = this.GetDelegate<GetLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getLongField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				case 0x83: //S
					GetShortFieldDelegate getShortField = this.GetDelegate<GetShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getShortField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		public void SetPrimitiveField(JLocalObject jLocal, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jLocal);
			AccessCache access = this.GetAccess(jLocal.Class);
			JFieldId fieldId = access.GetFieldId(definition, this.VirtualMachine.GetEnvironment(this.Reference));
			this.ReloadClass(jLocal as JClassObject);
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					SetBooleanFieldDelegate setBooleanField = this.GetDelegate<SetBooleanFieldDelegate>();
					setBooleanField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x66: //B
					SetByteFieldDelegate setByteField = this.GetDelegate<SetByteFieldDelegate>();
					setByteField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x67: //C
					SetCharFieldDelegate setCharField = this.GetDelegate<SetCharFieldDelegate>();
					setCharField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x68: //D
					SetDoubleFieldDelegate setDoubleField = this.GetDelegate<SetDoubleFieldDelegate>();
					setDoubleField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x70: //F
					SetFloatFieldDelegate setFloatField = this.GetDelegate<SetFloatFieldDelegate>();
					setFloatField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x73: //I
					SetIntFieldDelegate setIntField = this.GetDelegate<SetIntFieldDelegate>();
					setIntField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x74: //J
					SetLongFieldDelegate setLongField = this.GetDelegate<SetLongFieldDelegate>();
					setLongField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x83: //S
					SetShortFieldDelegate setShortField = this.GetDelegate<SetShortFieldDelegate>();
					setShortField(this.Reference, jLocal.As<JObjectLocalRef>(), fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this.VirtualMachine.GetEnvironment(this.Reference));
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					GetStaticBooleanFieldDelegate getStaticBooleanField = this.GetDelegate<GetStaticBooleanFieldDelegate>();
					MemoryMarshal.AsRef<Byte>(bytes) = getStaticBooleanField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x66: //B
					GetStaticByteFieldDelegate getStaticByteField = this.GetDelegate<GetStaticByteFieldDelegate>();
					MemoryMarshal.AsRef<SByte>(bytes) = getStaticByteField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x67: //C
					GetStaticCharFieldDelegate getStaticCharField = this.GetDelegate<GetStaticCharFieldDelegate>();
					MemoryMarshal.AsRef<Char>(bytes) = getStaticCharField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x68: //D
					GetStaticDoubleFieldDelegate getStaticDoubleField = this.GetDelegate<GetStaticDoubleFieldDelegate>();
					MemoryMarshal.AsRef<Double>(bytes) = getStaticDoubleField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x70: //F
					GetStaticFloatFieldDelegate getFloatField = this.GetDelegate<GetStaticFloatFieldDelegate>();
					MemoryMarshal.AsRef<Single>(bytes) = getFloatField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x73: //I
					GetStaticIntFieldDelegate getStaticIntField = this.GetDelegate<GetStaticIntFieldDelegate>();
					MemoryMarshal.AsRef<Int32>(bytes) = getStaticIntField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x74: //J
					GetStaticLongFieldDelegate getStaticLongField = this.GetDelegate<GetStaticLongFieldDelegate>();
					MemoryMarshal.AsRef<Int64>(bytes) = getStaticLongField(this.Reference, jClass.Reference, fieldId);
					break;
				case 0x83: //S
					GetStaticShortFieldDelegate getStaticShortField = this.GetDelegate<GetStaticShortFieldDelegate>();
					MemoryMarshal.AsRef<Int16>(bytes) = getStaticShortField(this.Reference, jClass.Reference, fieldId);
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		public void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			ValidationUtilities.ThrowIfDummy(jClass);
			AccessCache access = this.GetAccess(jClass);
			JFieldId fieldId = access.GetFieldId(definition, this.VirtualMachine.GetEnvironment(this.Reference));
			switch (definition.Information[1][^1])
			{
				case 0x90: //Z
					SetStaticBooleanFieldDelegate setStaticBooleanField = this.GetDelegate<SetStaticBooleanFieldDelegate>();
					setStaticBooleanField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Byte>(bytes));
					break;
				case 0x66: //B
					SetStaticByteFieldDelegate setStaticByteField = this.GetDelegate<SetStaticByteFieldDelegate>();
					setStaticByteField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<SByte>(bytes));
					break;
				case 0x67: //C
					SetStaticCharFieldDelegate setStaticCharField = this.GetDelegate<SetStaticCharFieldDelegate>();
					setStaticCharField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Char>(bytes));
					break;
				case 0x68: //D
					SetStaticDoubleFieldDelegate setStaticDoubleField = this.GetDelegate<SetStaticDoubleFieldDelegate>();
					setStaticDoubleField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Double>(bytes));
					break;
				case 0x70: //F
					SetStaticFloatFieldDelegate setStaticFloatField = this.GetDelegate<SetStaticFloatFieldDelegate>();
					setStaticFloatField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Single>(bytes));
					break;
				case 0x73: //I
					SetStaticIntFieldDelegate setStaticIntField = this.GetDelegate<SetStaticIntFieldDelegate>();
					setStaticIntField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int32>(bytes));
					break;
				case 0x74: //J
					SetStaticLongFieldDelegate setStaticLongField = this.GetDelegate<SetStaticLongFieldDelegate>();
					setStaticLongField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int64>(bytes));
					break;
				case 0x83: //S
					SetStaticShortFieldDelegate setStaticShortField = this.GetDelegate<SetStaticShortFieldDelegate>();
					setStaticShortField(this.Reference, jClass.Reference, fieldId, MemoryMarshal.AsRef<Int16>(bytes));
					break;
				default:
					throw new ArgumentException("Invalid primitive type.");
			}
		}
		
		public void CallPrimitiveStaticFunction(Span<Byte> bytes, JClassObject jClass, JFunctionDefinition definition,
			IObject?[] args)
		{
			throw new NotImplementedException();
		}
		public void CallPrimitiveFunction(Span<Byte> bytes, JLocalObject jLocal, JFunctionDefinition definition,
			IObject?[] args)
		{
			throw new NotImplementedException();
		}
		public void CallPrimitiveNonVirtualFunction(Span<Byte> bytes, JLocalObject jLocal, JClassObject jClass,
			JFunctionDefinition definition, IObject?[] args)
		{
			throw new NotImplementedException();
		}
		
		public TResult? GetField<TResult>(JLocalObject jLocal, JFieldDefinition definition)
			where TResult : IDataType<TResult>
			=> throw new NotImplementedException();
		public void SetField<TField>(JLocalObject jLocal, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>
		{
			throw new NotImplementedException();
		}
		public TField? GetStaticField<TField>(JClassObject jClass, JFieldDefinition definition)
			where TField : IDataType<TField>
			=> throw new NotImplementedException();
		public void SetStaticField<TField>(JClassObject jClass, JFieldDefinition definition, TField? value)
			where TField : IDataType<TField>
		{
			throw new NotImplementedException();
		}
		public TObject CallConstructor<TObject>(JClassObject jClass, JConstructorDefinition definition, IObject?[] args)
			where TObject : JLocalObject, IDataType<TObject>
			=> throw new NotImplementedException();
		public TResult? CallStaticFunction<TResult>(JClassObject jClass, JFunctionDefinition definition,
			IObject?[] args) where TResult : IDataType<TResult>
			=> throw new NotImplementedException();
		public void CallStaticMethod(JClassObject jClass, JMethodDefinition definition, IObject?[] args)
		{
			throw new NotImplementedException();
		}
		public TResult? CallFunction<TResult>(JLocalObject jLocal, JFunctionDefinition definition, IObject?[] args)
			where TResult : IDataType<TResult>
			=> throw new NotImplementedException();
		public TResult? CallNonVirtualFunction<TResult>(JLocalObject jLocal, JClassObject jClass,
			JFunctionDefinition definition, IObject?[] args) where TResult : IDataType<TResult>
			=> throw new NotImplementedException();
		public void CallMethod(JLocalObject jLocal, JMethodDefinition definition, IObject?[] args)
		{
			throw new NotImplementedException();
		}
		public void CallNonVirtualMethod(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition,
			IObject?[] args)
		{
			throw new NotImplementedException();
		}
	}
}