namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Japanese message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class JapaneseMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly JapaneseMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => JapaneseMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private JapaneseMessageResource() { }

	String IMessageResource.InvalidType => "要求された値は JValue に含めることができません。";
	String IMessageResource.VoidArgument => "void 型はインスタンス引数として使用できません。";
	String IMessageResource.VoidInstantiation => "void 型はインスタンス化できません。";
	String IMessageResource.VoidArray => "void 型は配列の要素型として使用できません。";
	String IMessageResource.VoidEquality => "Void インスタンスは比較可能ではありません。";
	String IMessageResource.SignatureNotAllowed => "シグネチャは許可されていません。";
	String IMessageResource.CriticalExceptionMessage => "実行は重大な状態です。保留中の例外がありますが、ネイティブインターフェースを通じて呼び出しを行うことはできません。";
	String IMessageResource.UnknownExceptionMessage => "保留中の例外がありますが、ThrowableException インスタンスを作成できませんでした。";
	String IMessageResource.InvalidSignatureMessage => "無効なシグネチャです。";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "定義はプリミティブ型ではありません。";
	String IMessageResource.InvalidPrimitiveTypeMessage => "無効なプリミティブ型です。";
	String IMessageResource.ProxyOnNonProxyProcess => "プロキシオブジェクトは非プロキシメタデータを処理できません。";
	String IMessageResource.NonProxyOnProxyProcess => "非プロキシオブジェクトはプロキシメタデータを処理できません。";
	String IMessageResource.NotConstructorDefinition => "現在の呼び出し定義はコンストラクタではありません。";
	String IMessageResource.NotPrimitiveObject => "オブジェクトはプリミティブ型ではありません。";
	String IMessageResource.InvalidProxyObject => "この操作を実行するには、Java オブジェクトがプロキシである必要があります。";
	String IMessageResource.InvalidGlobalObject => "無効なグローバルオブジェクトです。";
	String IMessageResource.OnlyLocalReferencesAllowed => "JNI 呼び出しはローカル参照のみ許可されます。";
	String IMessageResource.ClassRedefinition => "クラスの再定義はサポートされていません。";
	String IMessageResource.StackTraceFixed => "現在のスタック フレームは不変です。";
	String IMessageResource.InvalidClass => "無効なクラスオブジェクトです。";
	String IMessageResource.UnloadedClass => "クラスはアンロードされました。";
	String IMessageResource.NotClassObject => "オブジェクトはクラスではありません。";
	String IMessageResource.SingleReferenceTransaction => "このトランザクションには 1 つの参照のみを保持できます。";
	String IMessageResource.InvalidReferenceObject => "無効な JReferenceObject です。";
	String IMessageResource.InvalidObjectMetadata => "無効な ObjectMetadata です。";
	String IMessageResource.DisposedObject => "JReferenceObject は破棄されました。";
	String IMessageResource.InvalidArrayLength => "配列の長さは 0 以上である必要があります。";
	String IMessageResource.DeadVirtualMachine => "現在の JVM は実行されていません。";
	String IMessageResource.NotAttachedThread => "現在のスレッドは JVM にアタッチされていません。";
	String IMessageResource.IncompatibleLibrary => "互換性のない JVM ライブラリです。";
	String IMessageResource.UnmanagedMemoryContext => "メモリブロックはアンマネージドです。";

	String IMessageResource.InvalidInstantiation(String className) => $"{className} はインスタンス化できない型です。";
	String IMessageResource.InvalidCastTo(Type type) => $"{type} へのキャストが無効です。";
	String IMessageResource.InvalidCastTo(String className) => $"現在のインスタンスは {className} 型にキャストできません。";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} は空でない文字列である必要があります。";
	String IMessageResource.InvalidMetadata(String className, Type typeOfT)
		=> $"{className} のデータ型メタデータが {typeOfT} と一致しません。";
	String IMessageResource.AbstractClass(String className) => $"{className} は抽象型です。";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName) => $"{interfaceName} は拡張できません。";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} とそのスーパ・インターフェースは同じであってはなりません。";
	String IMessageResource.SameClassExtension(String className) => $"{className} とそのスーパークラスは同じであってはなりません。";
	String IMessageResource.AnnotationType(String interfaceName, String annotationName)
		=> $"{interfaceName} を拡張できません。{annotationName} はアノテーションです。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className)
		=> $"{className} は {interfaceName} を実装していません。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className, String missingSuperInterface)
		=> $"{className} は {interfaceName} を実装していません。欠落: {missingSuperInterface}。";
	String IMessageResource.InvalidImplementation(String interfaceName, String className,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{className} は {interfaceName} を実装していません。欠落: {String.Join(", ", missingSuperInterfaces)}。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName)
		=> $"{interfaceName} は {superInterfaceName} を拡張できません。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		String missingSuperInterface)
		=> $"{interfaceName} は {superInterfaceName} を拡張できません。欠落: {missingSuperInterface}。";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{interfaceName} は {superInterfaceName} を拡張できません。欠落: {String.Join(", ", missingSuperInterfaces)}。";
	String IMessageResource.InvalidOrdinal(String enumTypeName) => $"{enumTypeName} の序数は 0 以上である必要があります。";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} にはすでに序数 {ordinal} のフィールドがあります。";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"{enumTypeName} の名前は空でない文字列である必要があります。";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} にはすでに '{valueName}' という名前のフィールドがあります。";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"{JapaneseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)}。";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal,
		IReadOnlySet<Int32> missing)
		=> $"{JapaneseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)}, 欠落: {String.Join(", ", missing)}。";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} は {expectedBuilder} を使用して構築する必要があります。";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} は無効な参照型です。";
	String IMessageResource.NotTypeObject(String objectClassName, String className)
		=> $"{objectClassName} は {className} の型オブジェクトではありません。";
	String IMessageResource.MainClassGlobalError(String mainClassName) => $"クラス {mainClassName} の JNI グローバル参照を作成できません。";
	String IMessageResource.MainClassUnavailable(String mainClassName) => $"メインクラス {mainClassName} は利用できません。";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"プリミティブクラス {primitiveClassName} は利用できません。";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"トランザクション容量オーバーフロー: {transactionCapacity}。";

	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"定義が一致しません: {definition} vs {otherDefinition}。";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"JNI 環境 ({envRef}) は別のスレッドに割り当てられています。別のスレッド: {threadId}、現在のスレッド: {Environment.CurrentManagedThreadId}。";
	String IMessageResource.CallOnUnsafe(String functionName) => $"JNI 環境は現在、{functionName} を安全に呼び出すには無効な状態です。";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} はバージョン 0x{requiredVersion:x8} を必要としますが、現在のバージョンは 0x{currentVersion:x8} です。";
	String IMessageResource.InvalidArrayClass(String className) => $"{className} は配列クラスではありません。";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"{enumTypeName} の列挙フィールドリストが無効です。数: {count}, 最大序数: {maxOrdinal}";
}