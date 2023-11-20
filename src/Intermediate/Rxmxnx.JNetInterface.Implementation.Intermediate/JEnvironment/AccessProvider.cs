namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IAccessProvider
	{
		public void GetPrimitiveField(Span<Byte> bytes, JLocalObject jLocal, JFieldDefinition definition)
		{
			throw new NotImplementedException();
		}
		public void SetPrimitiveField(JLocalObject jLocal, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			throw new NotImplementedException();
		}
		public void GetPrimitiveStaticField(Span<Byte> bytes, JClassObject jClass, JFieldDefinition definition)
		{
			throw new NotImplementedException();
		}
		public void SetPrimitiveStaticField(JClassObject jClass, JFieldDefinition definition, ReadOnlySpan<Byte> bytes)
		{
			throw new NotImplementedException();
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