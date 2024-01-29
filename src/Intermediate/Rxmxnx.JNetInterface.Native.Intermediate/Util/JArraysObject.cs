namespace Rxmxnx.JNetInterface.Util;

/// <summary>
/// This class represents a local <c>java.util.Arrays</c> instance.
/// </summary>
public sealed class JArraysObject : JLocalObject, IClassType<JArraysObject>, IUninstantiableType<JArraysObject>
{
	private static readonly JClassTypeMetadata typeMetadata =
		JTypeMetadataBuilder<JArraysObject>.Create(UnicodeClassNames.ArraysObject()).Build();

	static JDataTypeMetadata IDataType.Metadata => JArraysObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	private JArraysObject(IEnvironment env) : base(env.ClassFeature.VoidObject, default) { }
}