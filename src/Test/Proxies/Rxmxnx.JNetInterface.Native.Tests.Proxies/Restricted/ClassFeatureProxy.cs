namespace Rxmxnx.JNetInterface.Tests.Restricted;

[ExcludeFromCodeCoverage]
public abstract partial class ClassFeatureProxy : IClassFeature
{
	public Boolean UseNonGeneric { get; set; }

	public abstract JClassObject VoidPrimitive { get; }
	public abstract JClassObject BooleanPrimitive { get; }
	public abstract JClassObject BytePrimitive { get; }
	public abstract JClassObject CharPrimitive { get; }
	public abstract JClassObject DoublePrimitive { get; }
	public abstract JClassObject FloatPrimitive { get; }
	public abstract JClassObject IntPrimitive { get; }
	public abstract JClassObject LongPrimitive { get; }
	public abstract JClassObject ShortPrimitive { get; }
	public abstract JClassObject VoidObject { get; }
	public abstract JClassObject BooleanObject { get; }
	public abstract JClassObject ByteObject { get; }
	public abstract JClassObject CharacterObject { get; }
	public abstract JClassObject DoubleObject { get; }
	public abstract JClassObject FloatObject { get; }
	public abstract JClassObject IntegerObject { get; }
	public abstract JClassObject LongObject { get; }
	public abstract JClassObject ShortObject { get; }

	public abstract JClassObject AsClassObject(JClassLocalRef classRef);
	public abstract JClassObject AsClassObject(JReferenceObject jObject);
	public abstract JClassObject GetObjectClass(ObjectMetadata objectMetadata);
	public abstract JClassObject GetClass(ITypeInformation typeInformation);
	public abstract JClassObject GetObjectClass(JLocalObject jLocal);
	public abstract JClassObject? GetSuperClass(JClassObject jClass);
	public abstract Boolean IsAssignableFrom(JClassObject jClass, JClassObject otherClass);
	public abstract Boolean IsInstanceOf(JReferenceObject jObject, JClassObject jClass);
	public abstract Boolean IsInstanceOf<TDataType>(JReferenceObject jObject)
		where TDataType : JReferenceObject, IDataType<TDataType>;
	public abstract JReferenceTypeMetadata GetTypeMetadata(JClassObject? jClass);
	public abstract JModuleObject? GetModule(JClassObject jClass);
	public abstract void ThrowNew(JClassObject jClass, String? message, Boolean throwException);
	public abstract void ThrowNew(JClassObject jClass, CString? message, Boolean throwException);
	public abstract void ThrowNew<TThrowable>(CString? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
	public abstract void ThrowNew<TThrowable>(String? message, Boolean throwException)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>;
	public abstract JClassObject GetClass<TDataType>() where TDataType : IDataType<TDataType>;
	public abstract TypeInformationProxy GetClassInfo(JClassObject jClass);
	public abstract JClassObject GetNonGenericClass(Type classHash);
	public abstract JClassObject GetClass(CString className);
	public abstract JClassObject LoadClass(CString className, Byte[] rawClassBytes,
		JClassLoaderObject? jClassLoader = default);
	public abstract JClassObject LoadClass<TDataType>(Byte[] rawClassBytes, JClassLoaderObject? jClassLoader = default)
		where TDataType : JReferenceObject, IReferenceType<TDataType>;
}