using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Util.Concurrent;

public class JCountDownLatchObject : JLocalObject, IClassType<JCountDownLatchObject>
{
	private static readonly IndeterminateCall constructorDef =
		IndeterminateCall.CreateConstructorDefinition([JArgumentMetadata.Get<JInt>(),]);
	private static readonly JMethodDefinition.Parameterless countDownDef = new("countDown"u8);
	private static readonly JMethodDefinition.Parameterless awaitDef = new("await"u8);
	private static readonly JClassTypeMetadata<JCountDownLatchObject> typeMetadata =
		TypeMetadataBuilder<JCountDownLatchObject>.Create("java/util/concurrent/CountDownLatch"u8).Build();
	static JClassTypeMetadata<JCountDownLatchObject> IClassType<JCountDownLatchObject>.Metadata
		=> JCountDownLatchObject.typeMetadata;

	protected JCountDownLatchObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JCountDownLatchObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JCountDownLatchObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void CountDown()
	{
		using JClassObject jClass = JClassObject.GetClass<JCountDownLatchObject>(this.Environment);
		JCountDownLatchObject.countDownDef.Invoke(this, jClass);
	}
	public void Await()
	{
		using JClassObject jClass = JClassObject.GetClass<JCountDownLatchObject>(this.Environment);
		JCountDownLatchObject.awaitDef.Invoke(this, jClass);
	}

	public static JCountDownLatchObject Create(IEnvironment env, Int32 value)
	{
		using JClassObject jClass = JClassObject.GetClass<JCountDownLatchObject>(env);
		return JCountDownLatchObject.constructorDef.NewCall<JCountDownLatchObject>(env, [(JInt)value,]);
	}

	static JCountDownLatchObject IClassType<JCountDownLatchObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JCountDownLatchObject IClassType<JCountDownLatchObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JCountDownLatchObject IClassType<JCountDownLatchObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}