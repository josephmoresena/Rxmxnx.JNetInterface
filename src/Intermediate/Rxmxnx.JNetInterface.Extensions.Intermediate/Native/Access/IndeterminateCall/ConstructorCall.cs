namespace Rxmxnx.JNetInterface.Native.Access;

public abstract partial class IndeterminateCall
{
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jClass">Target class.</param>
	/// <param name="args">Method arguments.</param>
	public JLocalObject NewCall(JClassObject jClass, ReadOnlySpan<IObject?> args)
	{
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		return IndeterminateCall.NewCall<JLocalObject>(definition, jClass, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
	public JLocalObject NewCall(JConstructorObject jConstructor, ReadOnlySpan<IObject?> args)
	{
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		return IndeterminateCall.NewCall<JLocalObject>(definition, jConstructor, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="args">Method arguments.</param>
	public TObject NewCall<TObject>(IEnvironment env, ReadOnlySpan<IObject?> args)
		where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		JClassObject jClass = JClassObject.GetClass<TObject>(env);
		return IndeterminateCall.NewCall<TObject>(definition, jClass, args);
	}
	/// <summary>
	/// Invokes a constructor on given <see cref="JClassObject"/> instance.
	/// </summary>
	/// <typeparam name="TObject">A <see cref="IClassType{TObject}"/> type.</typeparam>
	/// <param name="jConstructor">Reflected constructor instance.</param>
	/// <param name="args">Method arguments.</param>
	public TObject NewCall<TObject>(JConstructorObject jConstructor, ReadOnlySpan<IObject?> args)
		where TObject : JLocalObject, IClassType<TObject>
	{
		NativeValidationUtilities.ThrowIfAbstractClass(IClassType.GetMetadata<TObject>());
		JConstructorDefinition definition = NativeValidationUtilities.ThrowIfNotConstructor(this.Definition);
		return IndeterminateCall.NewCall<TObject>(definition, jConstructor, args);
	}
}