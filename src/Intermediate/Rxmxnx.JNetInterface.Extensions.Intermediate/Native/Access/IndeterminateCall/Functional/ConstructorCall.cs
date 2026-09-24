namespace Rxmxnx.JNetInterface.Native.Access;

// ReSharper disable once ClassCannotBeInstantiated
public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public JLocalObject NewCall<TArgs>(JClassObject jClass, in TArgs? args)
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		return IndeterminateCall.NewCall<JLocalObject, TArgs>(definition, jClass, in args);
	}

	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <typeparam name="TArgs">The <see cref="ICallArgument"/> type of the arguments to pass to the constructor.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="args">Method arguments.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
	public TObject NewCall<TObject, TArgs>(IEnvironment env, in TArgs? args)
		where TObject : JLocalObject, IClassType<TObject>
#if !NET9_0_OR_GREATER
		where TArgs : ICallArgument
#else
		where TArgs : ICallArgument, allows ref struct
#endif
	{
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		JClassObject jClass = JClassObject.GetClass<TObject>(env);
		return IndeterminateCall.NewCall<TObject, TArgs>(definition, jClass, in args);
	}
}