using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// .NET representation of <c>com.rxmxnx.dotnet.test.HelloDotnet</c> java class.
/// </summary>
internal sealed partial class JHelloDotnetObject : JLocalObject, IClassType<JHelloDotnetObject>
{
	public static JDataTypeMetadata Metadata { get; } = JTypeMetadataBuilder<JHelloDotnetObject>
	                                                    .Create("com/rxmxnx/dotnet/test/HelloDotnet"u8).Build();

	private JHelloDotnetObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	private JHelloDotnetObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	private JHelloDotnetObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public static event Func<String> GetStringEvent = default!;
	public static event Func<Int32> GetIntegerEvent = default!;
	public static event Action<String> PassStringEvent = default!;

	static JHelloDotnetObject IReferenceType<JHelloDotnetObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IReferenceType<JHelloDotnetObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JHelloDotnetObject IReferenceType<JHelloDotnetObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}