namespace Rxmxnx.JNetInterface;

/// <summary>
/// This interface exposes a JNI instance.
/// </summary>
public interface IEnvironment : IWrapper<JEnvironmentRef>
{
	/// <summary>
	/// JNI reference to the interface.
	/// </summary>
	JEnvironmentRef Reference { get; }
	/// <summary>
	/// Virtual machine that owns the current JNI context.
	/// </summary>
	IVirtualMachine VirtualMachine { get; }
	/// <summary>
	/// JNI version.
	/// </summary>
	Int32 Version { get; }
	/// <summary>
	/// The current ensured capacity for local references.
	/// </summary>
	Int32? LocalCapacity { get; set; }
	/// <summary>
	/// JNI pending exception.
	/// </summary>
	ThrowableException? PendingException { get; set; }

	/// <summary>
	/// Actual number of bytes used by JNI calls from stack.
	/// </summary>
	Int32 UsedStackBytes { get; }
	/// <summary>
	/// Actual number of bytes usable by JNI calls from stack.
	/// </summary>
	Int32 UsableStackBytes { get; set; }

	/// <summary>
	/// Accessing feature.
	/// </summary>
	internal IAccessFeature AccessFeature { get; }
	/// <summary>
	/// Classing feature.
	/// </summary>
	internal IClassFeature ClassFeature { get; }
	/// <summary>
	/// Referencing feature.
	/// </summary>
	internal IReferenceFeature ReferenceFeature { get; }
	/// <summary>
	/// String feature.
	/// </summary>
	internal IStringFeature StringFeature { get; }
	/// <summary>
	/// Array feature.
	/// </summary>
	internal IArrayFeature ArrayFeature { get; }
	/// <summary>
	/// Native I/O feature.
	/// </summary>
	internal INioFeature NioFeature { get; }
	/// <summary>
	/// Function cache.
	/// </summary>
	internal NativeFunctionSet FunctionSet { get; }
	/// <summary>
	/// Indicates whether the current instance is not a proxy.
	/// </summary>
	internal Boolean NoProxy { get; }

	JEnvironmentRef IWrapper<JEnvironmentRef>.Value => this.Reference;

	/// <summary>
	/// Indicates whether validation of <paramref name="jGlobal"/> can be avoided.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jGlobal"/> validation can be avoided;
	/// otherwise, <see langword="false"/>;
	/// </returns>
	internal Boolean IsValidationAvoidable(JGlobalBase jGlobal);
	/// <summary>
	/// Retrieves the JNI type reference of <paramref name="jObject"/>.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <returns>
	/// <see cref="JReferenceType"/> value indicating the reference <paramref name="jObject"/>
	/// reference type.
	/// </returns>
	JReferenceType GetReferenceType(JObject jObject);
	/// <summary>
	/// Indicates whether the both instance <paramref name="jObject"/> and <paramref name="jOther"/>
	/// refer to the same object.
	/// </summary>
	/// <param name="jObject">A <see cref="JObject"/> instance.</param>
	/// <param name="jOther">Another <see cref="JObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if both <paramref name="jObject"/> and <paramref name="jOther"/> refer
	/// to the same object; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean IsSameObject(JObject jObject, JObject? jOther);
	/// <summary>
	/// Indicates whether JNI execution is secure.
	/// </summary>
	/// <returns>
	/// <see langword="true"/> if is secure execute JNI calls; otherwise, <see langword="false"/>.
	/// </returns>
	Boolean JniSecure();

	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="action">An action to invoke inside created new local reference.</param>
	void WithFrame(Int32 capacity, Action action);
	/// <summary>
	/// Creates a new local reference frame and invokes <paramref name="action"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="action">An action to invoke inside created new local reference.</param>
	void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action)
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
		;
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="func">A function to execute inside created new local reference.</param>
	TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func);
	/// <summary>
	/// Creates a new local reference frame and executes <paramref name="func"/> inside of it.
	/// </summary>
	/// <param name="capacity">New local reference frame capacity.</param>
	/// <param name="state">A state object.</param>
	/// <param name="func">A function to execute inside created new local reference.</param>
	TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func)
#if NET9_0_OR_GREATER
	where TState : allows ref struct
#endif
		;

	/// <summary>
	/// JNI pending exception describe.
	/// </summary>
	void DescribeException();

	/// <summary>
	/// Indicates whether <paramref name="jThread"/> is virtual.
	/// </summary>
	/// <param name="jThread"></param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jThread"/> is virtual; otherwise, <seealso langword="false"/>.
	/// </returns>
	Boolean? IsVirtual(JThreadObject jThread);
}