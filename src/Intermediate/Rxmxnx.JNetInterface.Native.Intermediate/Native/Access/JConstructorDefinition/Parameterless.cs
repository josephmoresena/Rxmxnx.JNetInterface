namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// This class stores a parameterless constructor definition.
	/// </summary>
	public sealed class Parameterless : JConstructorDefinition
	{
		/// <summary>
		/// Creates a new <see cref="JLocalObject"/> instance using a constructor on <paramref name="jClass"/>
		/// which matches with current definition.
		/// </summary>
		/// <param name="jClass">An <see cref="JClassObject"/> instance.</param>
		/// <returns>A new <see cref="JLocalObject"/> instance.</returns>
		public new JLocalObject New(JClassObject jClass) => base.New(jClass);
		/// <summary>
		/// Creates a new <typeparamref name="TObject"/> instance using a constructor which matches with
		/// current definition.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IClassType{TClass}"/> type.</typeparam>
		/// <param name="env"><see cref="IEnvironment"/> instance.</param>
		/// <returns>A new <typeparamref name="TObject"/> instance.</returns>
		public new TObject New<TObject>(IEnvironment env) where TObject : JLocalObject, IClassType<TObject>
			=> base.New<TObject>(env);
	}
}