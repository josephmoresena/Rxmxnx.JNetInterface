namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JNumberObject>;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.NumberHash, 16);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = JLocalObject.CreateBuiltInMetadata<JNumberObject>(
		JNumberObject.typeInfo, JTypeModifier.Final, InterfaceSet.SerializableSet);

	static TypeMetadata IClassType<JNumberObject>.Metadata => JNumberObject.typeMetadata;

	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}