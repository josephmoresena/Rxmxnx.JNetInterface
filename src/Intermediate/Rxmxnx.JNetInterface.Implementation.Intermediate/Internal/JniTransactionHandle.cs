namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Represents a JNI call transaction.
/// </summary>
internal readonly partial struct JniTransactionHandle : IDisposable
{
	/// <summary>
	/// Dictionary of transactions.
	/// </summary>
	private readonly IDictionary<Guid, INativeTransaction>? _transactions;
	/// <summary>
	/// Transaction id.
	/// </summary>
	private readonly Guid _id;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="transactions">Dictionary of transactions.</param>
	private JniTransactionHandle(IDictionary<Guid, INativeTransaction> transactions)
	{
		this._id = Guid.NewGuid();
		this._transactions = transactions;
	}

	/// <inheritdoc/>
	public void Dispose() => this._transactions?.Remove(this._id);

	/// <summary>
	/// Creates an empty <see cref="INativeTransaction"/> instance.
	/// </summary>
	/// <param name="capacity">Transaction capacity.</param>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new <see cref="INativeTransaction"/> instance.</returns>
	public static INativeTransaction CreateTransaction(Int32 capacity,
		IDictionary<Guid, INativeTransaction> transactions)
		=> capacity switch
		{
			< 2 => JniTransactionHandle.Create<UnaryTransaction>(transactions),
			2 => JniTransactionHandle.Create<BinaryTransaction>(transactions),
			3 => JniTransactionHandle.Create<TernaryTransaction>(transactions),
			_ => JniTransactionHandle.Create<SetTransaction>(transactions),
		};
	/// <summary>
	/// Creates a synchronizer instance for <paramref name="jObject"/>.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jObject"><see cref="JLocalObject"/> instance.</param>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new synchronizer instance for <paramref name="jObject"/>.</returns>
	public static IDisposable CreateSynchronizer(IEnvironment env, JReferenceObject jObject,
		IDictionary<Guid, INativeTransaction> transactions)
	{
		Synchronizer result = JniTransactionHandle.Initialize<Synchronizer>(new(env, jObject), transactions);
		result.Activate();
		return result;
	}
	/// <summary>
	/// Creates a native memory adapter instance for <paramref name="jString"/>.
	/// </summary>
	/// <param name="jString"><see cref="JStringObject"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new native memory adapter instance for <paramref name="jString"/>.</returns>
	public static INativeMemoryAdapter CreateMemoryAdapter(JStringObject jString, JMemoryReferenceKind referenceKind,
		Boolean? critical, IDictionary<Guid, INativeTransaction> transactions)
	{
		NativeStringMemoryAdapter result =
			JniTransactionHandle.Initialize<NativeStringMemoryAdapter>(new(jString, referenceKind, critical),
			                                                           transactions);
		return JniTransactionHandle.ActivateMemoryAdapter(result, jString.Environment);
	}
	/// <summary>
	/// Creates a native memory adapter instance for <paramref name="jArray"/>.
	/// </summary>
	/// <typeparam name="TPrimitive">Type of <typeref name="TPrimitive"/> element.</typeparam>
	/// <param name="jArray"><see cref="JArrayObject{TPrimitive}"/> instance.</param>
	/// <param name="referenceKind">Reference memory kind.</param>
	/// <param name="critical">Indicates this adapter is for a critical sequence.</param>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new native memory adapter instance for <paramref name="jArray"/>.</returns>
	public static INativeMemoryAdapter CreateMemoryAdapter<TPrimitive>(JArrayObject<TPrimitive> jArray,
		JMemoryReferenceKind referenceKind, Boolean critical, IDictionary<Guid, INativeTransaction> transactions)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		NativeArrayMemoryAdapter<TPrimitive> result =
			JniTransactionHandle.Initialize<NativeArrayMemoryAdapter<TPrimitive>>(new(jArray, referenceKind, critical),
				transactions);
		return JniTransactionHandle.ActivateMemoryAdapter(result, jArray.Environment);
	}

	/// <summary>
	/// Creates an empty <typeparamref name="TTransaction"/> instance.
	/// </summary>
	/// <typeparam name="TTransaction">A <see cref="INativeTransaction"/> instance.</typeparam>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>A new <typeparamref name="TTransaction"/> instance.</returns>
	private static TTransaction Create<TTransaction>(IDictionary<Guid, INativeTransaction> transactions)
		where TTransaction : INativeTransaction, new()
	{
		TTransaction result = new();
		return JniTransactionHandle.Initialize(result, transactions);
	}
	/// <summary>
	/// Initialize <typeparamref name="TTransaction"/> instance.
	/// </summary>
	/// <typeparam name="TTransaction">A <see cref="INativeTransaction"/> instance.</typeparam>
	/// <param name="transaction">Transaction to initialize.</param>
	/// <param name="transactions">Dictionary of transactions.</param>
	/// <returns>Initialized <paramref name="transaction"/>.</returns>
	private static TTransaction Initialize<TTransaction>(TTransaction transaction,
		IDictionary<Guid, INativeTransaction> transactions) where TTransaction : INativeTransaction
	{
		do
			transaction.Reference = new(transactions);
		while (!transactions.TryAdd(transaction.Reference._id, transaction));
		return transaction;
	}
	/// <summary>
	/// Activates <paramref name="adapter"/> using <paramref name="env"/>.
	/// </summary>
	/// <param name="adapter">A <see cref="NativeMemoryAdapter"/> instance.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns><paramref name="adapter"/> activated.</returns>
	private static NativeMemoryAdapter ActivateMemoryAdapter(NativeMemoryAdapter adapter, IEnvironment env)
	{
		adapter.Activate(env);
		return adapter;
	}
}