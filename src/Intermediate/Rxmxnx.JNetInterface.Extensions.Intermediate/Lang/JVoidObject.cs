namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.Void</c> instance.
/// </summary>
public sealed class JVoidObject : JLocalObject, IPrimitiveEquatable, IPrimitiveWrapperType<JVoidObject>
{
	static JPrimitiveTypeMetadata IPrimitiveWrapperType.PrimitiveMetadata => JPrimitiveTypeMetadata.VoidMetadata;
	static JDataTypeMetadata IDataType.Metadata => new JPrimitiveWrapperTypeMetadata<JVoidObject>();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	private JVoidObject(IEnvironment env) : base(env.ClassFeature.VoidObject, default) { }

	Boolean IEquatable<JPrimitiveObject>.Equals(JPrimitiveObject? other) => throw new NotImplementedException();
	Boolean IEquatable<IPrimitiveType>.Equals(IPrimitiveType? other) => throw new NotImplementedException();

	static JVoidObject IReferenceType<JVoidObject>.Create(IReferenceType.ClassInitializer initializer)
		=> throw new NotImplementedException();
	static JVoidObject IReferenceType<JVoidObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> throw new NotImplementedException();
	static JVoidObject IReferenceType<JVoidObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> throw new NotImplementedException();
}