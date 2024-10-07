namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JConstructorDefinition
{
	/// <summary>
	/// This class stores a parameterless constructor definition.
	/// </summary>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	public sealed class Parameterless() : JConstructorDefinition(Parameterless.info, 0, [], 0)
	{
		/// <summary>
		/// Information for <c>ctor()</c>.
		/// </summary>
		private static readonly AccessibleInfoSequence info =
			new(JAccessibleObjectDefinition.ParameterlessConstructorHash, 6, 3);

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