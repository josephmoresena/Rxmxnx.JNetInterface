using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;

namespace Rxmxnx.JNetInterface.Awt;

public class JWindowObject : JContainerObject, IClassType<JWindowObject>
{
	private static readonly IndeterminateCall setLocationRelativeToDef =
		IndeterminateCall.CreateMethodDefinition("setLocationRelativeTo"u8,
		                                         [JArgumentMetadata.Get<JComponentObject>(),]);
	private static readonly JMethodDefinition.Parameterless packDef = new("pack"u8);
	private static readonly IndeterminateCall setIconImageDef =
		IndeterminateCall.CreateMethodDefinition("setIconImage"u8, [JArgumentMetadata.Get<JImageObject>(),]);
	private static readonly IndeterminateCall setDockIconImageDef =
		IndeterminateCall.CreateMethodDefinition("setDockIconImage"u8, [JArgumentMetadata.Get<JImageObject>(),]);

	private static readonly JClassTypeMetadata<JWindowObject> typeMetadata =
		TypeMetadataBuilder<JContainerObject>.Create<JWindowObject>("java/awt/Window"u8).Build();

	static JClassTypeMetadata<JWindowObject> IClassType<JWindowObject>.Metadata => JWindowObject.typeMetadata;

	protected JWindowObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetRelativeTo(JComponentObject? comp = default)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JWindowObject>(env);
		JWindowObject.setLocationRelativeToDef.MethodCall(this, jClass, false, [comp,]);
	}
	public void SetIcon(JImageObject image)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JWindowObject>(env);
		JWindowObject.setIconImageDef.MethodCall(this, jClass, false, [image,]);
	}
	public void Pack()
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = JClassObject.GetClass<JWindowObject>(env);
		JWindowObject.packDef.Invoke(this, jClass);
	}

	public static void SetApplicationIcon(JImageObject image)
	{
		IEnvironment env = image.Environment;
		ReadOnlySpan<Byte> className;
		ReadOnlySpan<Byte> getInstanceFunctionName;
		IndeterminateCall setIconDef;

		if (env.Version < 0x00090000)
		{
			if (!OperatingSystem.IsMacOS() && !OperatingSystem.IsIOS() && !OperatingSystem.IsTvOS())
				return;
			className = "com/apple/eawt/Application"u8;
			getInstanceFunctionName = "getApplication"u8;
			setIconDef = JWindowObject.setDockIconImageDef;
		}
		else
		{
			className = "java/awt/Taskbar"u8;
			getInstanceFunctionName = "getTaskbar"u8;
			setIconDef = JWindowObject.setIconImageDef;
		}

		try
		{
			using JClassObject jClass = JClassObject.GetClass(env, className);
			using JLocalObject jLocal =
				new JNonTypedFunctionDefinition(getInstanceFunctionName, jClass.ClassSignature).StaticInvoke(jClass)!;
			setIconDef.MethodCall(jLocal, [image,]);
		}
		catch (ThrowableException)
		{
			env.PendingException = default;
		}
	}

	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JWindowObject IClassType<JWindowObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}