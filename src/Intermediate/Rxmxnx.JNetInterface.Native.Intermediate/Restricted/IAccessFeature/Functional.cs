namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// Invokes a constructor for given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the constructor.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
	TObject CallConstructor<TObject, TArgs>(JClassObject jClass, JConstructorDefinition definition, in TArgs? args)
		where TObject : JLocalObject, IDataType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallConstructor<TObject>(jClass, definition, slot);
	}
	/// <summary>
	/// Invokes a reflected constructor on <paramref name="jConstructor"/>.
	/// </summary>
	/// <typeparam name="TObject"><see cref="IDataType"/> type of created instance.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jConstructor">A <see cref="JConstructorObject"/> instance.</param>
	/// <param name="definition"><see cref="JConstructorDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the constructor.</param>
	/// <returns>The new <typeparamref name="TObject"/> instance.</returns>
	/// <remarks>A default implementation is provided to avoid binary compatibility with older and proxy implementations.</remarks>
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	TObject CallConstructor<TObject, TArgs>(JConstructorObject jConstructor, JConstructorDefinition definition,
		in TArgs? args) where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallConstructor<TObject>(jConstructor, definition, slot);
	}
	/// <summary>
	/// Invokes a static function on <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the static call.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallStaticFunction<TResult, TArgs>(JClassObject jClass, JFunctionDefinition definition, in TArgs? args)
		where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallStaticFunction<TResult>(jClass, definition, slot);
	}
	/// <summary>
	/// Invokes a static function reflected on <paramref name="jMethod"/> instance.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the static call.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallStaticFunction<TResult, TArgs>(JMethodObject jMethod, JFunctionDefinition definition, in TArgs? args)
		where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallStaticFunction<TResult>(jMethod, definition, slot);
	}
	/// <summary>
	/// Invokes a static method on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the static call.</param>
	void CallStaticMethod<TArgs>(JClassObject jClass, JMethodDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallStaticMethod(jClass, definition, slot);
	}
	/// <summary>
	/// Invokes a static method reflected on <paramref name="jMethod"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="args">The arguments to be passed to the static call.</param>
	void CallStaticMethod<TArgs>(JMethodObject jMethod, JMethodDefinition definition, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallStaticMethod(jMethod, definition, slot);
	}
	/// <summary>
	/// Invokes a function on given <see cref="JLocalObject"/> instance and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The arguments to be passed to the instance call.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallFunction<TResult, TArgs>(JLocalObject jLocal, JClassObject jClass, JFunctionDefinition definition,
		Boolean nonVirtual, in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallFunction<TResult>(jLocal, jClass, definition, nonVirtual, slot);
	}
	/// <summary>
	/// Invokes a function reflected on <paramref name="jMethod"/> and returns its result.
	/// </summary>
	/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JFunctionDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The arguments to be passed to the instance call.</param>
	/// <returns><typeparamref name="TResult"/> function result.</returns>
	TResult? CallFunction<TResult, TArgs>(JMethodObject jMethod, JLocalObject jLocal, JFunctionDefinition definition,
		Boolean nonVirtual, in TArgs? args) where TResult : IDataType<TResult>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		return this.CallFunction<TResult>(jMethod, jLocal, definition, nonVirtual, slot);
	}
	/// <summary>
	/// Invokes a method on given <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass"><see cref="JClassObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> list with call arguments.</param>
	void CallMethod<TArgs>(JLocalObject jLocal, JClassObject jClass, JMethodDefinition definition, Boolean nonVirtual,
		in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallMethod(jLocal, jClass, definition, nonVirtual, slot);
	}
	/// <summary>
	/// Invokes a method reflected on <paramref name="jMethod"/>.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the static function.</typeparam>
	/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	/// <param name="definition"><see cref="JMethodDefinition"/> definition.</param>
	/// <param name="nonVirtual">Indicates whether the current call must be non-virtual.</param>
	/// <param name="args">The <see cref="IObject"/> list with call arguments.</param>
	void CallMethod<TArgs>(JMethodObject jMethod, JLocalObject jLocal, JMethodDefinition definition, Boolean nonVirtual,
		in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		DefaultSlot slot = new(definition.Count);
		args?.Configure(slot, definition);
		this.CallMethod(jMethod, jLocal, definition, nonVirtual, slot);
	}
}