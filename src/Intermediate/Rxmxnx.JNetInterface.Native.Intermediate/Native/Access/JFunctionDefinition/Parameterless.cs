namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// This class stores a parameterless function definition.
	/// </summary>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	[ExcludeFromCodeCoverage]
	public sealed class Parameterless : JFunctionDefinition<TResult>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="functionName">Function name.</param>
		public Parameterless(ReadOnlySpan<Byte> functionName) : base(functionName) { }
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="info">Function information.</param>
		internal Parameterless(AccessibleInfoSequence info) : base(info, 0, [], 0) { }

		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public TResult? Invoke(JLocalObject jLocal) => base.Invoke(jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		/// <returns>Function result.</returns>
		public TResult? Invoke(JLocalObject jLocal, JClassObject jClass)
			=> base.Invoke(jLocal, jClass, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		/// <returns>Function result.</returns>
		public TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a static function on <paramref name="jClass"/> which matches with current definition.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public TResult? StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass, ReadOnlySpan<IObject?>.Empty);

		/// <summary>
		/// Invokes a reflected function which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public TResult? InvokeReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeReflected(jMethod, jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a reflected function which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public TResult? InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a reflected static function which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public TResult? InvokeStaticReflected(JMethodObject jMethod)
			=> base.InvokeStaticReflected(jMethod, ReadOnlySpan<IObject?>.Empty);
	}
}