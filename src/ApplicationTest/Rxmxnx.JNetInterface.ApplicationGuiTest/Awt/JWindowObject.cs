using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Types;
using Rxmxnx.JNetInterface.Types.Metadata;
using Rxmxnx.PInvoke;

namespace Rxmxnx.JNetInterface.Awt;

public class JWindowObject : JContainerObject, IClassType<JWindowObject>
{
	private static readonly IndeterminateCall setLocationRelativeToDef = IndeterminateCall.CreateMethodDefinition(
		"setLocationRelativeTo"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JComponentObject>(),]
#else
		JArgumentMetadata.Get<JComponentObject>()
#endif
	);
	private static readonly JMethodDefinition.Parameterless packDef = new("pack"u8);
	private static readonly IndeterminateCall setIconImageDef = IndeterminateCall.CreateMethodDefinition(
		"setIconImage"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JImageObject>(),]
#else
		JArgumentMetadata.Get<JImageObject>()
#endif
	);
	private static readonly IndeterminateCall setDockIconImageDef = IndeterminateCall.CreateMethodDefinition(
		"setDockIconImage"u8,
#if !NET9_0_OR_GREATER
		[JArgumentMetadata.Get<JImageObject>(),]
#else
		JArgumentMetadata.Get<JImageObject>()
#endif
	);

	private static readonly JClassTypeMetadata<JWindowObject> typeMetadata =
		TypeMetadataBuilder<JContainerObject>.Create<JWindowObject>("java/awt/Window"u8).Build();

	static JClassTypeMetadata<JWindowObject> IClassType<JWindowObject>.Metadata => JWindowObject.typeMetadata;

	protected JWindowObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
	protected JWindowObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

	public void SetRelativeTo(JComponentObject? comp = default)
	{
		IEnvironment env = this.Environment;
		if (env.Version < (Int32)JRuntimeVersion.SEd4) return;
		using JClassObject jClass = JClassObject.GetClass<JWindowObject>(env);
		JWindowObject.setLocationRelativeToDef.MethodCall(this, jClass, false,
#if !NET9_0_OR_GREATER
		                                                  [comp,]
#else
		                                                  comp
#endif
		);
	}
	public void SetIcon(JImageObject image)
	{
		IEnvironment env = this.Environment;
		using JClassObject jClass = this.Environment.Version >= (Int32)JRuntimeVersion.J6 ?
			JClassObject.GetClass<JWindowObject>(env) :
			this.Class;
		JWindowObject.setIconImageDef.MethodCall(this, jClass, false,
#if !NET9_0_OR_GREATER
		                                         [image,]
#else
		                                         image
#endif
		);
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

		if (env.Version < (Int32)JRuntimeVersion.J9)
		{
			if (!SystemInfo.IsMac)
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
			setIconDef.MethodCall(jLocal,
#if !NET9_0_OR_GREATER
			                      [image,]
#else
			                      image
#endif
			);
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