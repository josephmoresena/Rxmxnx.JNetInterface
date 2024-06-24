namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JFunctionDefinition<TResult>
{
	/// <summary>
	/// This class stores a parameterless function definition.
	/// </summary>
	public sealed class Parameterless(ReadOnlySpan<Byte> functionName) : JFunctionDefinition<TResult>(functionName)
	{
		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public new TResult? Invoke(JLocalObject jLocal) => base.Invoke(jLocal);
		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		/// <returns>Function result.</returns>
		public new TResult? Invoke(JLocalObject jLocal, JClassObject jClass) => base.Invoke(jLocal, jClass);
		/// <summary>
		/// Invokes a function on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		/// <returns>Function result.</returns>
		public new TResult? InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass);
		/// <summary>
		/// Invokes a static function on <paramref name="jClass"/> which matches with current definition.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public new TResult? StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass);
	}
}