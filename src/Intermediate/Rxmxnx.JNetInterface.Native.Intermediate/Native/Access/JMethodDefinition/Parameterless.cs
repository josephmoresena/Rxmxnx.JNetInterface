namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// This class stores a parameterless method definition.
	/// </summary>
	[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS3218,
	                 Justification = CommonConstants.NoMethodOverloadingJustification)]
	[ExcludeFromCodeCoverage]
	public sealed class Parameterless(ReadOnlySpan<Byte> methodName) : JMethodDefinition(methodName)
	{
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		public void Invoke(JLocalObject jLocal) => base.Invoke(jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		public void Invoke(JLocalObject jLocal, JClassObject jClass)
			=> base.Invoke(jLocal, jClass, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a method on <paramref name="jLocal"/> which matches with current definition but using the
		/// implementation declared on <paramref name="jClass"/>.
		/// </summary>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
		public void InvokeNonVirtual(JLocalObject jLocal, JClassObject jClass)
			=> base.InvokeNonVirtual(jLocal, jClass, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a static method on <paramref name="jClass"/> which matches with current definition.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		public void StaticInvoke(JClassObject jClass) => base.StaticInvoke(jClass, ReadOnlySpan<IObject?>.Empty);

		/// <summary>
		/// Invokes a reflected method which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public void InvokeReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeReflected(jMethod, jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a reflected method which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public void InvokeNonVirtualReflected(JMethodObject jMethod, JLocalObject jLocal)
			=> base.InvokeNonVirtualReflected(jMethod, jLocal, ReadOnlySpan<IObject?>.Empty);
		/// <summary>
		/// Invokes a reflected static method which matches with current definition.
		/// </summary>
		/// <param name="jMethod">A <see cref="JMethodObject"/> instance.</param>
		/// <returns>Function result.</returns>
		public void InvokeStaticReflected(JMethodObject jMethod)
			=> base.InvokeStaticReflected(jMethod, ReadOnlySpan<IObject?>.Empty);
	}
}