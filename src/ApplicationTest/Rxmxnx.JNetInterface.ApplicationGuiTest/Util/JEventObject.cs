using Rxmxnx.JNetInterface.Io;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Util;

public class JEventObject : JLocalObject, IClassType<JEventObject>, IInterfaceObject<JSerializableObject>
{
	private static readonly JFunctionDefinition<JLocalObject>.Parameterless getSourceDef = new("getSource"u8);
	private static readonly JClassTypeMetadata<JEventObject> typeMetadata = TypeMetadataBuilder<JEventObject>
	                                                                        .Create("java/util/EventObject"u8)
	                                                                        .Implements<JSerializableObject>().Build();

	static JClassTypeMetadata<JEventObject> IClassType<JEventObject>.Metadata => JEventObject.typeMetadata;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd1;

	protected JEventObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JEventObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JEventObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public JLocalObject? GetSource()
	{
		using JClassObject jClass = JClassObject.GetClass<JEventObject>(this.Environment);
		return JEventObject.getSourceDef.Invoke(this, jClass);
	}
	static JEventObject IClassType<JEventObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JEventObject IClassType<JEventObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JEventObject IClassType<JEventObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}