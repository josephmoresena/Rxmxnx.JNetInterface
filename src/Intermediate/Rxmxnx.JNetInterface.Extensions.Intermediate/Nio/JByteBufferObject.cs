namespace Rxmxnx.JNetInterface.Nio;

/// <summary>
/// This class represents a local <c>java.nio.ByteBuffer</c> instance.
/// </summary>
public class JByteBufferObject : JBufferObject<JByte>, IClassType<JByteBufferObject>
{
	/// <summary>
	/// Type metadata.
	/// </summary>
	private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JBufferObject>
	                                                      .Create<JByteBufferObject>(
		                                                      UnicodeClassNames.ByteBufferObject(),
		                                                      JTypeModifier.Abstract).Implements<JComparableObject>()
	                                                      .Build();

	static JDataTypeMetadata IDataType.Metadata => JByteBufferObject.metadata;
	/// <inheritdoc/>
	internal JByteBufferObject(JClassObject jClass, JObjectLocalRef localRef) : base(jClass, localRef) { }

	/// <inheritdoc/>
	protected JByteBufferObject(IEnvironment env, JGlobalBase jGlobal) : base(env, jGlobal) { }
	/// <inheritdoc/>
	protected JByteBufferObject(JLocalObject jLocal, JClassObject? jClass = default) : base(jLocal, jClass) { }

	/// <summary>
	/// Creates a direct <see cref="JByteBufferObject"/> instance.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="memory">A <see cref="Memory{TMemory}"/> instance.</param>
	/// <returns>A direct <see cref="JBufferObject"/> instance.</returns>
	public static JByteBufferObject CreateDirectBuffer<TMemory>(IEnvironment env, Memory<TMemory> memory)
		where TMemory : unmanaged
	{
		JBufferObject buffer = env.NioFeature.NewDirectByteBuffer(memory.GetFixedContext());
		return (JByteBufferObject)buffer;
	}
	/// <summary>
	/// Creates an ephemeral direct <see cref="JByteBufferObject"/> instance and executes <paramref name="action"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="action">Action to execute.</param>
	public static void WithDirectBuffer(IEnvironment env, Int32 capacity, Action<JByteBufferObject> action)
		=> env.NioFeature.WithDirectByteBuffer(capacity, action);
	/// <summary>
	/// Creates an ephemeral direct <see cref="JByteBufferObject"/> instance and executes <paramref name="action"/>.
	/// </summary>
	/// <typeparam name="TState">The type of the state object used by the action.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="state">The state object of type <typeparamref name="TState"/>.</param>
	/// <param name="action">Action to execute.</param>
	public static void WithDirectBuffer<TState>(IEnvironment env, Int32 capacity, TState state,
		Action<JByteBufferObject, TState> action)
		=> env.NioFeature.WithDirectByteBuffer(capacity, state, action);
	/// <summary>
	/// Creates an ephemeral direct <see cref="JByteBufferObject"/> instance and executes <paramref name="func"/>.
	/// </summary>
	/// <typeparam name="TResult">The type of the return value of the function.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="func">Function to execute.</param>
	/// <returns>The result of <paramref name="func"/> execution.</returns>
	public static void WithDirectBuffer<TResult>(IEnvironment env, Int32 capacity,
		Func<JByteBufferObject, TResult> func)
		=> env.NioFeature.WithDirectByteBuffer(capacity, func);
	/// <summary>
	/// Creates an ephemeral direct <see cref="JByteBufferObject"/> instance and executes <paramref name="func"/>.
	/// </summary>
	/// <typeparam name="TState">The type of the state object used by the function.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the function.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <param name="state">The state object of type <typeparamref name="TState"/>.</param>
	/// <param name="func">Function to execute.</param>
	/// <returns>The result of <paramref name="func"/> execution.</returns>
	public static void WithDirectBuffer<TState, TResult>(IEnvironment env, Int32 capacity, TState state,
		Func<JByteBufferObject, TState, TResult> func)
		=> env.NioFeature.WithDirectByteBuffer(capacity, state, func);

	static JByteBufferObject? IReferenceType<JByteBufferObject>.Create(JLocalObject? jLocal)
		=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JByteBufferObject>(jLocal)) : default;
	static JByteBufferObject? IReferenceType<JByteBufferObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
		=> !JObject.IsNullOrDefault(jGlobal) ?
			new(env, JLocalObject.Validate<JByteBufferObject>(jGlobal, env)) :
			default;
}