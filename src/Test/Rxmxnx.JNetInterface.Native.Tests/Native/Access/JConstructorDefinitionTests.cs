namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JConstructorDefinitionTests
{
	private const String MethodName = "<init>";
	private const String VoidParameterlessDescriptor = "()V";

	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly JArgumentMetadata[] args =
		[JArgumentMetadata.Create<JClassObject>(), JArgumentMetadata.Create<JStringObject>(),];
	private static readonly String methodDescriptor =
		$"({String.Concat(JConstructorDefinitionTests.args.Select(a => a.Signature))})V";

	[Fact]
	internal void ObjectTest() => JConstructorDefinitionTests.ConstructorClassTest<JLocalObject>();
	[Fact]
	internal void ClassTest() => JConstructorDefinitionTests.ConstructorClassTest<JClassObject>();
	[Fact]
	internal void StringTest() => JConstructorDefinitionTests.ConstructorClassTest<JStringObject>();
	[Fact]
	internal void ThrowableTest() => JConstructorDefinitionTests.ConstructorClassTest<JThrowableObject>();
	[Fact]
	internal void EnumTest() => JConstructorDefinitionTests.ConstructorClassTest<JEnumObject>();
	[Fact]
	internal void NumberTest() => JConstructorDefinitionTests.ConstructorClassTest<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => JConstructorDefinitionTests.ConstructorClassTest<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => JConstructorDefinitionTests.ConstructorClassTest<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => JConstructorDefinitionTests.ConstructorClassTest<JModifierObject>();
	[Fact]
	internal void ProxyTest() => JConstructorDefinitionTests.ConstructorClassTest<JProxyObject>();

	[Fact]
	internal void ErrorTest() => JConstructorDefinitionTests.ConstructorClassTest<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => JConstructorDefinitionTests.ConstructorClassTest<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => JConstructorDefinitionTests.ConstructorClassTest<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => JConstructorDefinitionTests.ConstructorClassTest<JBufferObject>();

	[Fact]
	internal void FieldTest() => JConstructorDefinitionTests.ConstructorClassTest<JFieldObject>();
	[Fact]
	internal void MethodTest() => JConstructorDefinitionTests.ConstructorClassTest<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => JConstructorDefinitionTests.ConstructorClassTest<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest()
		=> JConstructorDefinitionTests.ConstructorClassTest<JStackTraceElementObject>();

	private static void ConstructorClassTest<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		JConstructorDefinitionTests.SimpleTest<TDataType>();
		JConstructorDefinitionTests.Test<TDataType>();
	}

	private static void SimpleTest<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TDataType>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		CStringSequence seq = new(JConstructorDefinitionTests.MethodName,
		                          JConstructorDefinitionTests.VoidParameterlessDescriptor);
		JConstructorDefinition.Parameterless constructorDefinition = new();
		JObjectLocalRef localRef0 = JConstructorDefinitionTests.fixture.Create<JObjectLocalRef>();
		JObjectLocalRef localRef1 = JConstructorDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JConstructorDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JConstructorDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef0);
		using JClassObject jConstructorClass = new(jClass, IDataType.GetMetadata<JConstructorObject>(), classRef1);
		using TDataType? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef0, true))) :
				default;
		using JConstructorObject jConstructor = new(jConstructorClass, localRef1, constructorDefinition, jClassClass);

		env.AccessFeature.CallConstructor<TDataType>(jClass, constructorDefinition, Arg.Any<IObject[]>())
		   .Returns(instance);
		env.AccessFeature.GetReflectedConstructor(constructorDefinition, jClass).Returns(jConstructor);

		Assert.Equal(seq.ToString(), constructorDefinition.Information.ToString());
		Assert.Equal(constructorDefinition.Information.GetHashCode(), constructorDefinition.GetHashCode());

		Assert.False(constructorDefinition.Equals(default));
		Assert.True(constructorDefinition.Equals((Object)constructorDefinition));
		Assert.True(constructorDefinition.Equals((Object)new JConstructorDefinition.Parameterless()));
		Assert.True(constructorDefinition.Equals(
			            (Object)new JMethodDefinition.Parameterless((CString)JConstructorDefinitionTests.MethodName)));
		Assert.Equal(0, constructorDefinition.Count);
		Assert.Equal(0, constructorDefinition.ReferenceCount);
		Assert.Equal(0, constructorDefinition.Size);
		Assert.Empty(constructorDefinition.Sizes);
		Assert.Equal(jConstructor, constructorDefinition.GetReflected(jClass));

		Assert.Equal("{ Method: <init> Descriptor: ()V }", constructorDefinition.ToString());

		if (!isAbstract)
			Assert.Equal(instance, JConstructorDefinition.New<TDataType>(constructorDefinition, jClass));
		else
			Assert.Equal($"{typeMetadata.ClassName} is an abstract type.",
			             Assert.Throws<InvalidOperationException>(
				             () => JConstructorDefinition.New<TDataType>(constructorDefinition, jClass)).Message);

		env.AccessFeature.Received(!isAbstract ? 1 : 0)
		   .CallConstructor<TDataType>(jClass, constructorDefinition, Arg.Is<IObject?[]>(i => i.Length == 0));
		env.AccessFeature.Received(1).GetReflectedConstructor(constructorDefinition, jClass);
	}
	private static void Test<TDataType>() where TDataType : JLocalObject, IClassType<TDataType>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TDataType>();
		Boolean isAbstract = typeMetadata.Modifier == JTypeModifier.Abstract;
		CStringSequence seq = new(JConstructorDefinitionTests.MethodName, JConstructorDefinitionTests.methodDescriptor);
		JFakeConstructor constructorDefinition = new(JConstructorDefinitionTests.args);
		JObjectLocalRef localRef0 = JConstructorDefinitionTests.fixture.Create<JObjectLocalRef>();
		JObjectLocalRef localRef1 = JConstructorDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JConstructorDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JConstructorDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef0);
		using JClassObject jConstructorClass = new(jClass, IDataType.GetMetadata<JConstructorObject>(), classRef1);
		using TDataType? instance =
			!isAbstract && !typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef0, true))) :
				default;
		using JConstructorObject jConstructor = new(jConstructorClass, localRef1, constructorDefinition, jClassClass);
		IObject?[] parameters = [jClass, jConstructorClass, jConstructor,];

		Assert.Equal(seq.ToString(), constructorDefinition.Information.ToString());
		Assert.Equal(constructorDefinition.Information.GetHashCode(), constructorDefinition.GetHashCode());

		Assert.False(constructorDefinition.Equals(default));
		Assert.True(constructorDefinition.Equals((Object)constructorDefinition));
		Assert.True(
			constructorDefinition.Equals((Object)JConstructorDefinition.Create(JConstructorDefinitionTests.args)));
		Assert.True(constructorDefinition.Equals(
			            (Object)JMethodDefinition.Create((CString)JConstructorDefinitionTests.MethodName,
			                                             JConstructorDefinitionTests.args)));
		Assert.Equal(JConstructorDefinitionTests.args.Select(a => a.Size).Sum(), constructorDefinition.Size);
		Assert.Equal(JConstructorDefinitionTests.args.Length, constructorDefinition.Count);
		Assert.Equal(JConstructorDefinitionTests.args.Count(a => a.Signature.Length > 1),
		             constructorDefinition.ReferenceCount);
		Assert.Equal(JConstructorDefinitionTests.args.Select(a => a.Size), constructorDefinition.Sizes);

		env.ClassFeature.GetClass<TDataType>().Returns(jClass);
		env.AccessFeature.CallConstructor<JLocalObject>(jClass, constructorDefinition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallConstructor<TDataType>(jClass, constructorDefinition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallConstructor<JLocalObject>(jConstructor, constructorDefinition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.CallConstructor<TDataType>(jConstructor, constructorDefinition, Arg.Any<IObject?[]>())
		   .Returns(instance);

		if (isAbstract) return;

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.New(jClass));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jClass, constructorDefinition,
		                                                            Arg.Is<IObject?[]>(
			                                                            a => JConstructorDefinitionTests
				                                                            .IsEmptyArgs(a)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.New<TDataType>(env));
		env.ClassFeature.Received(1).GetClass<TDataType>();
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jClass, constructorDefinition,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JConstructorDefinitionTests.IsEmptyArgs(a)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.New(jClass, parameters));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jClass, constructorDefinition, parameters);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.New<TDataType>(env, parameters));
		env.ClassFeature.Received(1).GetClass<TDataType>();
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jClass, constructorDefinition, parameters);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.NewReflected(jConstructor));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jConstructor, constructorDefinition,
		                                                            Arg.Is<IObject?[]>(
			                                                            a => JConstructorDefinitionTests
				                                                            .IsEmptyArgs(a)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.NewReflected<TDataType>(jConstructor));
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jConstructor, constructorDefinition,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JConstructorDefinitionTests.IsEmptyArgs(a)));

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.NewReflected(jConstructor, parameters));
		env.AccessFeature.Received(1).CallConstructor<JLocalObject>(jConstructor, constructorDefinition, parameters);

		env.ClassFeature.ClearReceivedCalls();
		env.AccessFeature.ClearReceivedCalls();
		Assert.Equal(instance, constructorDefinition.NewReflected<TDataType>(jConstructor, parameters));
		env.AccessFeature.Received(1).CallConstructor<TDataType>(jConstructor, constructorDefinition, parameters);
	}
	private static Boolean IsEmptyArgs(IReadOnlyCollection<IObject?> cArgs)
		=> cArgs.Count == JConstructorDefinitionTests.args.Length && cArgs.All(o => o is null);

	private class JFakeConstructor(params JArgumentMetadata[] metadata) : JConstructorDefinition(metadata)
	{
		public new JLocalObject New(JClassObject jClass) => base.New(jClass);
		public new TObject New<TObject>(IEnvironment env) where TObject : JLocalObject, IClassType<TObject>
			=> base.New<TObject>(env);
		public new JLocalObject New(JClassObject jClass, IObject?[] args) => base.New(jClass, args);
		public new TObject New<TObject>(IEnvironment env, IObject?[] args)
			where TObject : JLocalObject, IClassType<TObject>
			=> base.New<TObject>(env, args);
		public new JLocalObject NewReflected(JConstructorObject jConstructorObject)
			=> base.NewReflected(jConstructorObject);
		public new JLocalObject NewReflected(JConstructorObject jConstructorObject, IObject?[] args)
			=> base.NewReflected(jConstructorObject, args);
		public new TObject NewReflected<TObject>(JConstructorObject jConstructorObject)
			where TObject : JLocalObject, IClassType<TObject>
			=> base.NewReflected<TObject>(jConstructorObject);
		public new TObject NewReflected<TObject>(JConstructorObject jConstructorObject, IObject?[] args)
			where TObject : JLocalObject, IClassType<TObject>
			=> base.NewReflected<TObject>(jConstructorObject, args);
	}
}