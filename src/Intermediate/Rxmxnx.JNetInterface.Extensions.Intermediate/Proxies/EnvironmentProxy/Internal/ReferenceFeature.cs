namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	ObjectLifetime IReferenceFeature.GetLifetime(JLocalObject jLocal, InternalClassInitializer initializer)
		=> this.GetSourceInstance(initializer.LocalReference)?.Lifetime ?? new(this, jLocal, initializer.LocalReference)
		{
			Class = initializer.Class, IsRealClass = initializer.Class is not null && initializer.Class.IsFinal,
		};
	JLocalObject IReferenceFeature.CreateWrapper<TPrimitive>(TPrimitive primitive)
	{
		JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
		return metadata.NativeType switch
		{
			JNativeType.JBoolean => this.CreateWrapper((Byte)primitive == JBoolean.TrueValue),
			JNativeType.JByte => this.CreateWrapper((SByte)primitive),
			JNativeType.JChar => this.CreateWrapper((JChar)(Char)primitive),
			JNativeType.JDouble => this.CreateWrapper((Double)primitive),
			JNativeType.JFloat => this.CreateWrapper((JFloat)(Single)primitive),
			JNativeType.JInt => this.CreateWrapper((Int32)primitive),
			JNativeType.JLong => this.CreateWrapper((Int64)primitive),
			_ => this.CreateWrapper((Int16)primitive),
		};
	}
}