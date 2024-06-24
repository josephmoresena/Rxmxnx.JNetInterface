namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// This class stores a parameterless method definition.
	/// </summary>
	public sealed class Parameterless(ReadOnlySpan<Byte> methodName) : JMethodDefinition(methodName)
	{
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public new void Invoke(JLocalObject jLocal) => base.Invoke(jLocal);
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		public new void Invoke(JLocalObject jLocal, JClassObject jClass) => base.Invoke(jLocal, jClass);
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		public new void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass);
		/// <summary>
		/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		public new void StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass);
	}
}