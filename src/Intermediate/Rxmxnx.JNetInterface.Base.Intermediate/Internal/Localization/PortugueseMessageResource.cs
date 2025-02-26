namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Portuguese message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class PortugueseMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly PortugueseMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => PortugueseMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private PortugueseMessageResource() { }

	String IMessageResource.InvalidType => "O valor solicitado não pode ser contido em um JValue.";
	String IMessageResource.VoidArgument => "O tipo void não pode ser usado como argumento de instância.";
	String IMessageResource.VoidInstantiation => "O tipo void não pode ser instanciado.";
	String IMessageResource.VoidArray => "O tipo void não pode ser usado como tipo de elemento de array.";
	String IMessageResource.VoidEquality => "Uma instância de Void não pode ser comparável.";
	String IMessageResource.SignatureNotAllowed => "Assinatura não permitida.";
	String IMessageResource.CriticalExceptionMessage
		=> "A execução está em um estado crítico. Há uma exceção pendente, mas nenhuma chamada pode ser feita através da interface nativa.";
	String IMessageResource.UnknownExceptionMessage
		=> "Há uma exceção pendente, mas a instância de ThrowableException não pôde ser criada.";
	String IMessageResource.InvalidSignatureMessage => "Assinatura inválida.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "A definição não é um tipo primitivo.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Tipo primitivo inválido.";
	String IMessageResource.ProxyOnNonProxyProcess => "Um objeto proxy não pode processar metadados não proxy.";
	String IMessageResource.NonProxyOnProxyProcess => "Um objeto não proxy não pode processar metadados proxy.";
	String IMessageResource.NotConstructorDefinition => "A definição da chamada atual não é um construtor.";
	String IMessageResource.NotPrimitiveObject => "O objeto não é primitivo.";
	String IMessageResource.InvalidProxyObject => "O objeto Java deve ser um proxy para executar esta operação.";
	String IMessageResource.InvalidGlobalObject => "Objeto global inválido.";
	String IMessageResource.OnlyLocalReferencesAllowed => "Chamadas JNI permitem apenas referências locais.";
	String IMessageResource.ClassRedefinition => "Redefinir uma classe não é suportado.";
	String IMessageResource.StackTraceFixed => "O frame atual da pilha é imutável.";
	String IMessageResource.InvalidClass => "Classe inválida.";
	String IMessageResource.UnloadedClass => "A classe foi descarregada.";
	String IMessageResource.NotClassObject => "O objeto não é uma classe.";
	String IMessageResource.SingleReferenceTransaction => "Esta transação pode conter apenas uma referência.";
	String IMessageResource.InvalidReferenceObject => "JReferenceObject inválido.";
	String IMessageResource.InvalidObjectMetadata => "Metadados do objeto inválidos.";
	String IMessageResource.DisposedObject => "JReferenceObject foi descartado.";
	String IMessageResource.InvalidArrayLength => "O comprimento do array deve ser zero ou positivo.";
	String IMessageResource.DeadVirtualMachine => "A JVM atual não está em execução.";
	String IMessageResource.NotAttachedThread => "A thread atual não está anexada à JVM.";
	String IMessageResource.IncompatibleLibrary => "Biblioteca JVM incompatível.";
	String IMessageResource.UnmanagedMemoryContext => "O bloco de memória é não gerenciado.";

	String IMessageResource.InvalidInstantiation(String className) => $"{className} não é um tipo instanciável.";
	String IMessageResource.InvalidCastTo(Type type) => $"Conversão inválida para {type}.";
	String IMessageResource.InvalidCastTo(String className)
		=> $"A instância atual não pode ser convertida para o tipo {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} deve ser uma string não vazia.";
	String IMessageResource.InvalidMetadata(String className, Type typeOfT)
		=> $"Os metadados do tipo {className} não correspondem ao tipo {typeOfT}.";
	String IMessageResource.AbstractClass(String className) => $"{className} é um tipo abstrato.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} não pode estender uma interface que já a estende.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} e sua superinterface não podem ser iguais.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} e sua superclasse não podem ser iguais.";
	String IMessageResource.AnnotationType(String interfaceName, String annotationName)
		=> $"Não é possível estender {interfaceName}. {annotationName} é uma anotação.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className)
		=> $"{className} não implementa {interfaceName}.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className, String missingSuperInterface)
		=> $"{className} não implementa {interfaceName}. Faltando: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(String interfaceName, String className,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{className} não implementa {interfaceName}. Faltando: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName)
		=> $"{interfaceName} não pode estender {superInterfaceName}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		String missingSuperInterface)
		=> $"{interfaceName} não pode estender {superInterfaceName}. Faltando: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(String superInterfaceName, String interfaceName,
		IReadOnlySet<String> missingSuperInterfaces)
		=> $"{interfaceName} não pode estender {superInterfaceName}. Faltando: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"O ordinal para {enumTypeName} deve ser zero ou positivo.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} já possui um campo com ordinal {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"O nome para {enumTypeName} deve ser não vazio.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} já possui um campo chamado '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> PortugueseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{PortugueseMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Valores ausentes: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} deve ser construído usando {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} não é um tipo de referência válido.";
	String IMessageResource.NotTypeObject(String objectClassName, String className)
		=> $"{objectClassName} não é um objeto de tipo para {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Erro ao criar uma referência global JNI para a classe {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName)
		=> $"Classe principal {mainClassName} indisponível.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Classe primitiva {primitiveClassName} indisponível.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Capacidade de transação excedida: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"As definições não correspondem: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"O ambiente JNI ({envRef}) está atribuído a outra thread. Operação tentada em thread diferente: {threadId}. Thread atual: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"O ambiente JNI atual está em um estado inválido para chamar com segurança {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} requer a versão 0x{requiredVersion:x8}, mas a versão atual é 0x{currentVersion:x8}.";
	String IMessageResource.InvalidArrayClass(String className) => $"{className} não é uma classe de array.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"A lista de valores do enum {enumTypeName} é inválida. Quantidade: {count}, Ordinal máximo: {maxOrdinal}.";
}