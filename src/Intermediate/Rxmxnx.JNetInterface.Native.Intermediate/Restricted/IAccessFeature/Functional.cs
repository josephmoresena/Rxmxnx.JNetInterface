namespace Rxmxnx.JNetInterface.Restricted;

internal partial interface IAccessFeature
{
	/// <summary>
	/// Invokes a constructor method for given <see cref="JClassObject"/> instance.
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
	/// Invokes a reflected constructor method on <paramref name="jConstructor"/>.
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
}