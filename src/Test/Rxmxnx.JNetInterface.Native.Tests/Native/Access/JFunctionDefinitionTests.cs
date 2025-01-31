namespace Rxmxnx.JNetInterface.Tests.Native.Access;

[ExcludeFromCodeCoverage]
public sealed class JFunctionDefinitionTests
{
	private static readonly IFixture fixture = new Fixture().RegisterReferences();
	private static readonly JArgumentMetadata[] args =
		[JArgumentMetadata.Create<JClassObject>(), JArgumentMetadata.Create<JStringObject>(),];

	[Fact]
	internal void ObjectTest() => JFunctionDefinitionTests.ObjectFunctionTest<JLocalObject>();
	[Fact]
	internal void ClassTest() => JFunctionDefinitionTests.ObjectFunctionTest<JClassObject>();
	[Fact]
	internal void StringTest() => JFunctionDefinitionTests.ObjectFunctionTest<JStringObject>();
	[Fact]
	internal void ThrowableTest() => JFunctionDefinitionTests.ObjectFunctionTest<JThrowableObject>();
	[Fact]
	internal void EnumTest() => JFunctionDefinitionTests.ObjectFunctionTest<JEnumObject>();
	[Fact]
	internal void NumberTest() => JFunctionDefinitionTests.ObjectFunctionTest<JNumberObject>();
	[Fact]
	internal void AccessibleObjectTest() => JFunctionDefinitionTests.ObjectFunctionTest<JAccessibleObject>();
	[Fact]
	internal void ExecutableTest() => JFunctionDefinitionTests.ObjectFunctionTest<JExecutableObject>();
	[Fact]
	internal void ModifierTest() => JFunctionDefinitionTests.ObjectFunctionTest<JModifierObject>();
	[Fact]
	internal void ProxyTest() => JFunctionDefinitionTests.ObjectFunctionTest<JProxyObject>();

	[Fact]
	internal void ErrorTest() => JFunctionDefinitionTests.ObjectFunctionTest<JErrorObject>();
	[Fact]
	internal void ExceptionTest() => JFunctionDefinitionTests.ObjectFunctionTest<JExceptionObject>();
	[Fact]
	internal void RuntimeExceptionTest() => JFunctionDefinitionTests.ObjectFunctionTest<JRuntimeExceptionObject>();
	[Fact]
	internal void BufferTest() => JFunctionDefinitionTests.ObjectFunctionTest<JBufferObject>();

	[Fact]
	internal void FieldTest() => JFunctionDefinitionTests.ObjectFunctionTest<JFieldObject>();
	[Fact]
	internal void MethodTest() => JFunctionDefinitionTests.ObjectFunctionTest<JMethodObject>();
	[Fact]
	internal void ConstructorTest() => JFunctionDefinitionTests.ObjectFunctionTest<JConstructorObject>();
	[Fact]
	internal void StackTraceElementTest() => JFunctionDefinitionTests.ObjectFunctionTest<JStackTraceElementObject>();

	[Fact]
	internal void DirectBufferTest() => JFunctionDefinitionTests.ObjectFunctionTest<JDirectBufferObject>();
	[Fact]
	internal void CharSequenceTest() => JFunctionDefinitionTests.ObjectFunctionTest<JCharSequenceObject>();
	[Fact]
	internal void CloneableTest() => JFunctionDefinitionTests.ObjectFunctionTest<JCloneableObject>();
	[Fact]
	internal void ComparableTest() => JFunctionDefinitionTests.ObjectFunctionTest<JComparableObject>();
	[Fact]
	internal void AnnotatedElementTest() => JFunctionDefinitionTests.ObjectFunctionTest<JAnnotatedElementObject>();
	[Fact]
	internal void GenericDeclarationTest() => JFunctionDefinitionTests.ObjectFunctionTest<JGenericDeclarationObject>();
	[Fact]
	internal void MemberTest() => JFunctionDefinitionTests.ObjectFunctionTest<JMemberObject>();
	[Fact]
	internal void TypeTest() => JFunctionDefinitionTests.ObjectFunctionTest<JTypeObject>();
	[Fact]
	internal void AnnotationTest() => JFunctionDefinitionTests.ObjectFunctionTest<JAnnotationObject>();
	[Fact]
	internal void SerializableTest() => JFunctionDefinitionTests.ObjectFunctionTest<JSerializableObject>();

	private static void ObjectFunctionTest<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		JFunctionDefinitionTests.SimpleTest<TDataType>(true);
		JFunctionDefinitionTests.SimpleTest<TDataType>(false);
		JFunctionDefinitionTests.SimpleTest<JArrayObject<TDataType>>(true);
		JFunctionDefinitionTests.SimpleTest<JArrayObject<TDataType>>(false);

		JFunctionDefinitionTests.Test<TDataType>();
		JFunctionDefinitionTests.NonTypedTest<TDataType>();
	}
	private static void SimpleTest<TDataType>(Boolean nonVirtual)
		where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TDataType>();
		String functionName = JFunctionDefinitionTests.fixture.Create<String>();
		String parameterlessDescriptor = $"(){TDataType.Argument.Signature}";
		CStringSequence seq = new(functionName, parameterlessDescriptor);
		JFunctionDefinition<TDataType>.Parameterless functionDefinition = new((CString)functionName);
		JObjectLocalRef localRef0 = JFunctionDefinitionTests.fixture.Create<JObjectLocalRef>();
		JObjectLocalRef localRef1 = JFunctionDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef0 = JFunctionDefinitionTests.fixture.Create<JClassLocalRef>();
		JClassLocalRef classRef1 = JFunctionDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClassClass = new(env);
		using JClassObject jClass = new(jClassClass, typeMetadata, classRef0);
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(), classRef1);
		using TDataType? instance =
			typeMetadata.Signature[0] != CommonNames.ArraySignaturePrefixChar &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual(CommonNames.ClassObject) &&
			!typeMetadata.ClassName.AsSpan().SequenceEqual("java/lang/String"u8) ?
				Assert.IsType<TDataType>(
					typeMetadata.ParseInstance(typeMetadata.CreateInstance(jClass, localRef0, true))) :
				default;
		using JMethodObject jMethod = new(jMethodClass, localRef1, functionDefinition, jClassClass);

		env.AccessFeature.CallFunction<TDataType>((JLocalObject)jMethod, jMethodClass, functionDefinition, nonVirtual,
		                                          Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature.CallFunction<TDataType>((JLocalObject)jMethod, jClassClass, functionDefinition, nonVirtual,
		                                          Arg.Any<IObject?[]>()).Returns(instance);
		env.AccessFeature.CallStaticFunction<TDataType>(jClass, functionDefinition, Arg.Any<IObject?[]>())
		   .Returns(instance);
		env.AccessFeature.GetReflectedFunction(functionDefinition, jClassClass, Arg.Any<Boolean>()).Returns(jMethod);

		Assert.Equal(seq.ToString(), functionDefinition.Information.ToString());
		Assert.Equal(functionDefinition.Information.GetHashCode(), functionDefinition.GetHashCode());

		Assert.False(functionDefinition.Equals(default));
		Assert.True(functionDefinition.Equals((Object)functionDefinition));
		Assert.True(functionDefinition.Equals((Object)new JFunctionDefinition<TDataType>(functionDefinition)));
		Assert.True(functionDefinition.Equals(
			            (Object)new JNonTypedFunctionDefinition((CString)functionName, TDataType.Argument.Signature)));
		Assert.Equal(0, functionDefinition.Count);
		Assert.Equal(0, functionDefinition.ReferenceCount);
		Assert.Equal(functionDefinition.Sizes.Sum(), functionDefinition.Size);
		Assert.Empty(functionDefinition.Sizes);
		Assert.Equal(jMethod, functionDefinition.GetReflected(jClassClass));
		Assert.Equal(jMethod, functionDefinition.GetStaticReflected(jClassClass));

		Assert.Equal($"{{ Method: {functionName} Descriptor: {parameterlessDescriptor} }}",
		             functionDefinition.ToString());

		Assert.Equal(instance, JFunctionDefinition.Invoke(functionDefinition, jMethod, nonVirtual: nonVirtual));
		Assert.Equal(instance, JFunctionDefinition.Invoke(functionDefinition, jMethod, jClassClass, nonVirtual));
		Assert.Equal(instance, JFunctionDefinition.StaticInvoke(functionDefinition, jClass));

		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jMethod.Class, functionDefinition,
		                                                      nonVirtual, Arg.Is<IObject?[]>(i => i.Length == 0));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jClassClass, functionDefinition,
		                                                      nonVirtual, Arg.Is<IObject?[]>(i => i.Length == 0));
		env.AccessFeature.Received(1)
		   .CallStaticFunction<TDataType>(jClass, functionDefinition, Arg.Is<IObject?[]>(i => i.Length == 0));

		env.AccessFeature.Received(1).GetReflectedFunction(functionDefinition, jClassClass, true);
		env.AccessFeature.Received(1).GetReflectedFunction(functionDefinition, jClassClass, false);
	}
	private static void Test<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		String functionName = JFunctionDefinitionTests.fixture.Create<String>();
		String functionDescriptor =
			$"({String.Concat(JFunctionDefinitionTests.args.Select(a => a.Signature))}){TDataType.Argument.Signature}";
		CStringSequence seq = new(functionName, functionDescriptor);
		JFakeFunctionDefinition<TDataType> functionDefinition =
			new((CString)functionName, JFunctionDefinitionTests.args);
		JObjectLocalRef localRef = JFunctionDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JFunctionDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(), classRef);
		using JMethodObject jMethod = new(jMethodClass, localRef, functionDefinition, jClass);
		IObject?[] parameters = [jClass, jMethodClass, jMethod,];

		Assert.Equal(seq.ToString(), functionDefinition.Information.ToString());
		Assert.Equal(functionDefinition.Information.GetHashCode(), functionDefinition.GetHashCode());

		Assert.False(functionDefinition.Equals(default));
		Assert.True(functionDefinition.Equals((Object)functionDefinition));
		Assert.True(functionDefinition.Equals((Object)new JFunctionDefinition<TDataType>(functionDefinition)));
		Assert.True(functionDefinition.Equals(
			            (Object)new JNonTypedFunctionDefinition((CString)functionName, TDataType.Argument.Signature,
			                                                    JFunctionDefinitionTests.args)));
		Assert.Equal(JFunctionDefinitionTests.args.Length, functionDefinition.Count);
		Assert.Equal(functionDefinition.Sizes.Sum(), functionDefinition.Size);
		Assert.Equal(JFunctionDefinitionTests.args.Count(a => a.Signature.Length > 1),
		             functionDefinition.ReferenceCount);
		Assert.Equal(JFunctionDefinitionTests.args.Select(a => a.Size), functionDefinition.Sizes);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jMethod.Class, functionDefinition,
		                                                      false,
		                                                      Arg.Is<IObject?[]>(
			                                                      a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jClass, functionDefinition, false,
		                                                      Arg.Is<IObject?[]>(
			                                                      a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jMethod.Class, functionDefinition,
		                                                      false,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, jClass, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jClass, functionDefinition, false,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtual(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jClass, functionDefinition, true,
		                                                      Arg.Is<IObject?[]>(
			                                                      a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtual(jMethod, jClass, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>((JLocalObject)jMethod, jClass, functionDefinition, true,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.StaticInvoke(jMethodClass));
		env.AccessFeature.Received(1).CallStaticFunction<TDataType>(jMethodClass, functionDefinition,
		                                                            Arg.Is<IObject?[]>(
			                                                            a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.StaticInvoke(jClass, parameters));
		env.AccessFeature.Received(1)
		   .CallStaticFunction<TDataType>(jClass, functionDefinition,
		                                  Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeReflected(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, (JLocalObject)jClass, functionDefinition, false,
		                                                      Arg.Is<IObject?[]>(
			                                                      a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeReflected(jMethod, jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jMethod, functionDefinition, false,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtualReflected(jMethod, jMethodClass));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, (JLocalObject)jMethodClass, functionDefinition,
		                                                      true,
		                                                      Arg.Is<IObject?[]>(
			                                                      a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtualReflected(jMethod, jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<TDataType>(jMethod, jMethod, functionDefinition, true,
		                                                      Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeStaticReflected(jMethod));
		env.AccessFeature.Received(1).CallStaticFunction<TDataType>(jMethod, functionDefinition,
		                                                            Arg.Is<IObject?[]>(
			                                                            a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeStaticReflected(jMethod, parameters));
		env.AccessFeature.Received(1)
		   .CallStaticFunction<TDataType>(jMethod, functionDefinition,
		                                  Arg.Is<IObject[]>(a => a.SequenceEqual(parameters)));
	}
	private static void NonTypedTest<TDataType>() where TDataType : JReferenceObject, IReferenceType<TDataType>
	{
		String functionName = JFunctionDefinitionTests.fixture.Create<String>();
		String functionDescriptor =
			$"({String.Concat(JFunctionDefinitionTests.args.Select(a => a.Signature))}){TDataType.Argument.Signature}";
		CStringSequence seq = new(functionName, functionDescriptor);
		JNonTypedFunctionDefinition functionDefinition =
			new((CString)functionName, TDataType.Argument.Signature, JFunctionDefinitionTests.args);
		JObjectLocalRef localRef = JFunctionDefinitionTests.fixture.Create<JObjectLocalRef>();
		JClassLocalRef classRef = JFunctionDefinitionTests.fixture.Create<JClassLocalRef>();
		EnvironmentProxy env = EnvironmentProxy.CreateEnvironment();
		using JClassObject jClass = new(env);
		using JClassObject jMethodClass = new(jClass, IDataType.GetMetadata<JMethodObject>(), classRef);
		using JMethodObject jMethod = new(jMethodClass, localRef, functionDefinition, jClass);
		IObject?[] parameters = [jClass, jMethod,];

		Assert.Equal(seq.ToString(), functionDefinition.Information.ToString());
		Assert.Equal(functionDefinition.Information.GetHashCode(), functionDefinition.GetHashCode());

		Assert.False(functionDefinition.Equals(default));
		Assert.True(functionDefinition.Equals((Object)functionDefinition));
		Assert.True(functionDefinition.Equals((Object)new JFunctionDefinition<TDataType>(functionDefinition)));
		Assert.True(functionDefinition.Equals(
			            (Object)new JNonTypedFunctionDefinition((CString)functionName, TDataType.Argument.Signature,
			                                                    JFunctionDefinitionTests.args)));
		Assert.Equal(JFunctionDefinitionTests.args.Length, functionDefinition.Count);
		Assert.Equal(functionDefinition.Sizes.Sum(), functionDefinition.Size);
		Assert.Equal(JFunctionDefinitionTests.args.Count(a => a.Signature.Length > 1),
		             functionDefinition.ReferenceCount);
		Assert.Equal(JFunctionDefinitionTests.args.Select(a => a.Size), functionDefinition.Sizes);

		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jMethod.Class,
		                                                         functionDefinition, false,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jClass, functionDefinition,
		                                                         false,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jMethod.Class,
		                                                         functionDefinition, false,
		                                                         Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.Invoke(jMethod, jClass, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jClass, functionDefinition,
		                                                         false,
		                                                         Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtual(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jClass, functionDefinition,
		                                                         true,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtual(jMethod, jClass, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>((JLocalObject)jMethod, jClass, functionDefinition,
		                                                         true,
		                                                         Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.StaticInvoke(jMethodClass));
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jMethodClass, functionDefinition,
		                                                               Arg.Is<IObject?[]>(
			                                                               a => JFunctionDefinitionTests
				                                                               .IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.StaticInvoke(jClass, parameters));
		env.AccessFeature.Received(1)
		   .CallStaticFunction<JLocalObject>(jClass, functionDefinition,
		                                     Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeReflected(jMethod, jClass));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, (JLocalObject)jClass, functionDefinition,
		                                                         false,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeReflected(jMethod, jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jMethod, functionDefinition, false,
		                                                         Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtualReflected(jMethod, jMethodClass));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, (JLocalObject)jMethodClass,
		                                                         functionDefinition, true,
		                                                         Arg.Is<IObject?[]>(
			                                                         a => JFunctionDefinitionTests.IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeNonVirtualReflected(jMethod, jMethod, parameters));
		env.AccessFeature.Received(1).CallFunction<JLocalObject>(jMethod, jMethod, functionDefinition, true,
		                                                         Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeStaticReflected(jMethod));
		env.AccessFeature.Received(1).CallStaticFunction<JLocalObject>(jMethod, functionDefinition,
		                                                               Arg.Is<IObject?[]>(
			                                                               a => JFunctionDefinitionTests
				                                                               .IsEmptyArgs(a)));
		env.AccessFeature.ClearReceivedCalls();
		Assert.Null(functionDefinition.InvokeStaticReflected(jMethod, parameters));
		env.AccessFeature.Received(1)
		   .CallStaticFunction<JLocalObject>(jMethod, functionDefinition,
		                                     Arg.Is<IObject?[]>(i => i.SequenceEqual(parameters)));
	}
#pragma warning disable CA1859
	private static Boolean IsEmptyArgs(IReadOnlyCollection<IObject?> cArgs)
		=> (cArgs.Count == JFunctionDefinitionTests.args.Length || cArgs.Count == 0) && cArgs.All(o => o is null);
#pragma warning restore CA1859

	private class JFakeFunctionDefinition<TResult>(ReadOnlySpan<Byte> functionName, params JArgumentMetadata[] metadata)
		: JFunctionDefinition<TResult>(functionName, metadata) where TResult : JReferenceObject, IReferenceType<TResult>
	{
		public new TResult? Invoke(JLocalObject jLocal) => base.Invoke(jLocal);
		public new TResult? Invoke(JLocalObject jLocal, JClassObject jClass) => base.Invoke(jLocal, jClass);
		public TResult? Invoke(JLocalObject jLocal, IObject?[] args) => base.Invoke(jLocal, args);
		public TResult? Invoke(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
			=> base.Invoke(jLocal, jClass, args);
		public new TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass);
		public TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass, IObject?[] args)
			=> base.InvokeNonVirtual(jLocal, jClass, args);
		public new TResult? StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass);
		public TResult? StaticInvoke(JClassObject jClass, IObject?[] args) => base.StaticInvoke(jClass, args);
		public new TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeReflected(jMethod, jLocal);
		public TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
			=> base.InvokeReflected(jMethod, jLocal, args);
		public new TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal);
		public TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal, IObject?[] args)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal, args);
		public new TResult? InvokeStaticReflected(JMethodObject jMethod) => base.InvokeStaticReflected(jMethod);
		public TResult? InvokeStaticReflected(JMethodObject jMethod, IObject?[] args)
			=> base.InvokeStaticReflected(jMethod, args);
	}
}