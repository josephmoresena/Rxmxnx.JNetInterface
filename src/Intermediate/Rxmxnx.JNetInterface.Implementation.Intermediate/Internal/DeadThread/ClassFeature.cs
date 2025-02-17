namespace Rxmxnx.JNetInterface.Internal;

internal partial class DeadThread : IClassFeature
{
	JClassObject IClassFeature.VoidPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.BooleanPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.BytePrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.CharPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.DoublePrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.FloatPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.IntPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.LongPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.ShortPrimitive => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.ClassObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.VoidObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.BooleanObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.ByteObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.CharacterObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.DoubleObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.FloatObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.IntegerObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.LongObject => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.ShortObject => this.ThrowInvalidResult<JClassObject>();

	JClassObject IClassFeature.AsClassObject(JClassLocalRef classRef) => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.AsClassObject(JReferenceObject jObject) => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.GetClass<TDataType>() => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.GetObjectClass(ObjectMetadata objectMetadata) => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.GetClass(ITypeInformation typeInformation) => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.GetObjectClass(JLocalObject jLocal) => this.ThrowInvalidResult<JClassObject>();
	JClassObject? IClassFeature.GetSuperClass(JClassObject jClass) => this.ThrowInvalidResult<JClassObject?>();
	Boolean IClassFeature.IsAssignableFrom(JClassObject jClass, JClassObject otherClass)
		=> this.ThrowInvalidResult<Boolean>();
	Boolean IClassFeature.IsInstanceOf(JReferenceObject jObject, JClassObject jClass)
	{
		this.IsInstanceOfTrace(jObject, jClass.Name);
		return jObject.ObjectClassName.AsSpan().SequenceEqual(jClass.Name);
	}
	Boolean IClassFeature.IsInstanceOf<TDataType>(JReferenceObject jObject)
	{
		this.IsInstanceOfTrace(jObject, IDataType.GetMetadata<TDataType>().ClassName);
		return jObject is TDataType || jObject.ObjectClassName.AsSpan()
		                                      .SequenceEqual(IDataType.GetMetadata<TDataType>().ClassName);
	}
	JReferenceTypeMetadata? IClassFeature.GetTypeMetadata(JClassObject? jClass)
		=> this.ThrowInvalidResult<JReferenceTypeMetadata?>();
	JModuleObject? IClassFeature.GetModule(JClassObject jClass) => this.ThrowInvalidResult<JModuleObject?>();
	void IClassFeature.ThrowNew<TThrowable>(CString? message, Boolean throwException)
		=> this.ThrowInvalidResult<Byte>();
	void IClassFeature.ThrowNew<TThrowable>(String? message, Boolean throwException) => this.ThrowInvalidResult<Byte>();
	JClassObject IClassFeature.GetClass(ReadOnlySpan<Byte> className) => this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.LoadClass(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> rawClassBytes,
		JClassLoaderObject? jClassLoader)
		=> this.ThrowInvalidResult<JClassObject>();
	JClassObject IClassFeature.
		LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
			ReadOnlySpan<Byte> rawClassBytes, JClassLoaderObject? jClassLoader)
		=> this.ThrowInvalidResult<JClassObject>();
	void IClassFeature.GetClassInfo(JClassObject jClass, out CString name, out CString signature, out String hash)
	{
		Unsafe.SkipInit(out name);
		Unsafe.SkipInit(out signature);
		Unsafe.SkipInit(out hash);
		this.ThrowInvalidResult<Byte>();
	}
}