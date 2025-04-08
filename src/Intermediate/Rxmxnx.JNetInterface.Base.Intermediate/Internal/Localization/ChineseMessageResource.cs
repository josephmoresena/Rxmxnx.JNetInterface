namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Chinese message resource.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
internal sealed class ChineseMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly ChineseMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => ChineseMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private ChineseMessageResource() { }

	String IMessageResource.InvalidType => "请求的值不能包含在 JValue 中。";
	String IMessageResource.VoidArgument => "void 类型不能用作实例参数。";
	String IMessageResource.VoidInstantiation => "void 类型不能被实例化。";
	String IMessageResource.VoidArray => "void 类型不能用作数组元素类型。";
	String IMessageResource.VoidEquality => "Void 实例不能相等。";
	String IMessageResource.SignatureNotAllowed => "签名不被允许。";
	String IMessageResource.CriticalExceptionMessage => "执行处于关键状态。存在挂起的异常，但无法通过本机接口进行调用。";
	String IMessageResource.UnknownExceptionMessage => "存在挂起的异常，但无法创建 ThrowableException 实例。";
	String IMessageResource.InvalidSignatureMessage => "无效的签名。";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "定义不是原始类型。";
	String IMessageResource.InvalidPrimitiveTypeMessage => "无效的原始类型。";
	String IMessageResource.ProxyOnNonProxyProcess => "代理对象无法处理非代理元数据。";
	String IMessageResource.NonProxyOnProxyProcess => "非代理对象无法处理代理元数据。";
	String IMessageResource.NotConstructorDefinition => "当前调用定义不是构造函数。";
	String IMessageResource.NotPrimitiveObject => "对象不是原始类型。";
	String IMessageResource.InvalidProxyObject => "Java 对象必须是代理才能执行此操作。";
	String IMessageResource.InvalidGlobalObject => "无效的全局对象。";
	String IMessageResource.OnlyLocalReferencesAllowed => "JNI 调用仅允许本地引用。";
	String IMessageResource.ClassRedefinition => "不支持重新定义类。";
	String IMessageResource.StackTraceFixed => "当前堆栈帧是不可变的。";
	String IMessageResource.InvalidClass => "无效的类对象。";
	String IMessageResource.UnloadedClass => "类已被卸载。";
	String IMessageResource.NotClassObject => "对象不是类。";
	String IMessageResource.SingleReferenceTransaction => "此事务只能包含一个引用。";
	String IMessageResource.InvalidReferenceObject => "无效的 JReferenceObject。";
	String IMessageResource.InvalidObjectMetadata => "无效的对象元数据。";
	String IMessageResource.DisposedObject => "JReferenceObject 已被释放。";
	String IMessageResource.InvalidArrayLength => "数组长度必须为零或正数。";
	String IMessageResource.DeadVirtualMachine => "当前 JVM 未运行。";
	String IMessageResource.NotAttachedThread => "当前线程未附加到 JVM。";
	String IMessageResource.IncompatibleLibrary => "不兼容的 JVM 库。";
	String IMessageResource.UnmanagedMemoryContext => "内存块未受管理。";

	String IMessageResource.InvalidInstantiation(String className) => $"{className} 不是可实例化的类型。";
	String IMessageResource.InvalidCastTo(Type type) => $"无法转换为 {type}。";
	String IMessageResource.InvalidCastTo(String className) => $"当前实例无法转换为类型 {className}。";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} 必须是非空字符串。";
	String IMessageResource.InvalidMetadata(String className, Type typeOfT) => $"{className} 的数据类型元数据与 {typeOfT} 不匹配。";
	String IMessageResource.AbstractClass(String className) => $"{className} 是抽象类型。";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName) => $"{interfaceName} 不能扩展自身的超接口。";
	String IMessageResource.SameInterfaceExtension(String interfaceName) => $"{interfaceName} 和其超接口不能相同。";
	String IMessageResource.SameClassExtension(String className) => $"{className} 和其超类不能相同。";
	String IMessageResource.AnnotationType(String interfaceName, String annotationName)
		=> $"无法扩展 {interfaceName}。{annotationName} 是一个注解。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className)
		=> $"{className} 没有实现 {interfaceName}。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className, String missingSuperInterface)
		=> $"{className} 没有实现 {interfaceName}，缺少: {missingSuperInterface}。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{className} 没有实现 {interfaceName}，缺少: {String.Join(", ", missingSuperInterfaces)}。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName)
		=> $"{interfaceName} 不能扩展 {superInterfaceName}。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		String missingSuperInterface)
		=> $"{interfaceName} 不能扩展 {superInterfaceName}，缺少: {missingSuperInterface}。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{interfaceName} 不能扩展 {superInterfaceName}，缺少: {String.Join(", ", missingSuperInterfaces)}。";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> ChineseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal,
		IReadOnlySet<Int32> missing)
		=> $"{ChineseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)}缺少值: {String.Join(", ", missing)}。";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} 必须使用 {expectedBuilder} 进行构建。";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} 不是有效的引用类型。";
	String IMessageResource.NotTypeObject(String objectClassName, String className)
		=> $"{objectClassName} 不是 {className} 的类型对象。";
	String IMessageResource.MainClassGlobalError(String mainClassName) => $"创建 {mainClassName} 类的 JNI 全局引用时出错。";
	String IMessageResource.MainClassUnavailable(String mainClassName) => $"主类 {mainClassName} 不可用。";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName) => $"原始类 {primitiveClassName} 不可用。";
	String IMessageResource.InvalidOrdinal(String enumTypeName) => $"{enumTypeName} 的序数必须为零或正数。";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} 已经有一个序数为 {ordinal} 的字段。";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"{enumTypeName} 的名称必须为非空字符串。";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} 已经有一个名为 '{valueName}' 的字段。";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity) => $"事务容量溢出: {transactionCapacity}。";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"定义不匹配: {definition} 与 {otherDefinition}。";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"JNI 环境 ({envRef}) 分配给另一个线程。尝试在不同线程 {threadId} 上操作。当前线程: {Environment.CurrentManagedThreadId}。";
	String IMessageResource.CallOnUnsafe(String functionName) => $"当前 JNI 环境处于无效状态，无法安全调用 {functionName}。";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} 需要版本 0x{requiredVersion:x8}，但当前版本为 0x{currentVersion:x8}。";
	String IMessageResource.InvalidArrayClass(String className) => $"{className} 不是数组类。";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"{enumTypeName} 的枚举字段列表无效。数量: {count}, 最大序数: {maxOrdinal}。";
}