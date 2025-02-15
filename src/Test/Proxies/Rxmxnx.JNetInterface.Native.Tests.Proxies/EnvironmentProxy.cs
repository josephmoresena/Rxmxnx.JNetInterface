namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public abstract partial class EnvironmentProxy : IEnvironment
{
	public abstract VirtualMachineProxy VirtualMachine { get; }
	public abstract AccessFeatureProxy AccessFeature { get; }
	public abstract ClassFeatureProxy ClassFeature { get; }
	public abstract ReferenceFeatureProxy ReferenceFeature { get; }
	public abstract StringFeatureProxy StringFeature { get; }
	public abstract ArrayFeatureProxy ArrayFeature { get; }
	public abstract NioFeatureProxy NioFeature { get; }

	public abstract NativeFunctionSetProxy FunctionSet { get; }

	public abstract Int32 UsedStackBytes { get; }
	public abstract Int32 UsableStackBytes { get; set; }
	public abstract JEnvironmentRef Reference { get; }
	public abstract Int32 Version { get; }
	public abstract ThrowableException? PendingException { get; set; }
	public abstract Int32? LocalCapacity { get; set; }
	public abstract Boolean NoProxy { get; }

	public abstract Boolean IsValidationAvoidable(JGlobalBase jGlobal);
	public abstract JReferenceType GetReferenceType(JObject jObject);
	public abstract Boolean IsSameObject(JObject jObject, JObject? jOther);
	public abstract Boolean JniSecure();
	public abstract void WithFrame(Int32 capacity, Action action);
	public abstract void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action);
	public abstract TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func);
	public abstract TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func);
	public abstract void DescribeException();
	public abstract Boolean? IsVirtual(JThreadObject jThread);

	NativeFunctionSet IEnvironment.FunctionSet => this.FunctionSet;

	public static EnvironmentProxy CreateEnvironment(Boolean isProxy = false, VirtualMachineProxy? vm = default)
	{
		EnvironmentProxy env = Substitute.For<EnvironmentProxy>();
		env.Version.Returns(IVirtualMachine.MinimalVersion);
		env.VirtualMachine.Returns(vm ?? Substitute.For<VirtualMachineProxy>());
		env.NoProxy.Returns(!isProxy);
		env.AccessFeature.Returns(Substitute.For<AccessFeatureProxy>());
		env.ClassFeature.Returns(Substitute.For<ClassFeatureProxy>());
		env.ReferenceFeature.Returns(Substitute.For<ReferenceFeatureProxy>());
		env.StringFeature.Returns(Substitute.For<StringFeatureProxy>());
		env.ArrayFeature.Returns(Substitute.For<ArrayFeatureProxy>());
		env.NioFeature.Returns(Substitute.For<NioFeatureProxy>());
		env.FunctionSet.Returns(Substitute.For<NativeFunctionSetProxy>());

		env.ReferenceFeature.GetLifetime(Arg.Any<JLocalObject>(), Arg.Any<JObjectLocalRef>(), Arg.Any<JClassObject>(),
		                                 Arg.Any<Boolean>()).ReturnsForAnyArgs(l =>
		{
			JLocalObject jLocal = (JLocalObject)l[0];
			JObjectLocalRef localRef = (JObjectLocalRef)l[1];
			JClassObject jClass = (JClassObject)l[2];
			return new()
			{
				Value = new(env, jLocal, localRef)
				{
					Class = jClass, IsRealClass = jClass is not null && (jClass.IsFinal || (Boolean)l[3]),
				},
			};
		});
		return env;
	}
	public static Type? GetFamilyType<TDataType>() where TDataType : IDataType => TDataType.FamilyType;
	public static JTypeKind? GetKind<TDataType>() where TDataType : IDataType => TDataType.Kind;
}