namespace Rxmxnx.JNetInterface.Native.Dummies;

public partial interface IDummyEnvironment
{
	Boolean IReferenceFeature.RealEnvironment => false;
	ObjectLifetime? IReferenceFeature.GetLifetime(JLocalObject jLocal, JObjectLocalRef localRef, JClassObject? jClass,
		Boolean overrideClass)
		=> default;
	JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		return metadata.NativeType switch
		{
			JNativeType.JBoolean => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JBoolean>(in primitive)),
			JNativeType.JByte => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JByte>(in primitive)),
			JNativeType.JChar => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JChar>(in primitive)),
			JNativeType.JDouble => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JDouble>(in primitive)),
			JNativeType.JFloat => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JFloat>(in primitive)),
			JNativeType.JInt => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JInt>(in primitive)),
			JNativeType.JLong => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JLong>(in primitive)),
			_ => this.CreateWrapper(NativeUtilities.Transform<TPrimitive, JShort>(in primitive)),
		};
	}
}