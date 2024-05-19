namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		/// <summary>
		/// Uses <paramref name="jObject"/> into <paramref name="jniTransaction"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference</returns>
		private JObjectLocalRef UseObject(INativeTransaction jniTransaction, JReferenceObject? jObject)
			=> jObject is JLocalObject jLocal ? this.UseObject(jniTransaction, jLocal) : jniTransaction.Add(jObject);
		/// <summary>
		/// Uses <paramref name="jLocal"/> into <paramref name="jniTransaction"/>.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <see cref="JObjectLocalRef"/> reference</returns>
		private JObjectLocalRef UseObject(INativeTransaction jniTransaction, JLocalObject? jLocal)
			=> jLocal is JClassObject jLocalClass ?
				jniTransaction.Add(this.ReloadClass(jLocalClass).Value) :
				jniTransaction.Add(jLocal);
		/// <summary>
		/// Indicates whether current call must use <see langword="stackalloc"/> or <see langword="new"/> to
		/// hold JNI call parameter.
		/// </summary>
		/// <param name="jCall">A <see cref="JCallDefinition"/> instance.</param>
		/// <param name="requiredBytes">Output. Number of bytes to allocate.</param>
		/// <returns>
		/// <see langword="true"/> if current call must use <see langword="stackalloc"/>; otherwise,
		/// <see langword="false"/>.
		/// </returns>
		private Boolean UseStackAlloc(JCallDefinition jCall, out Int32 requiredBytes)
		{
			requiredBytes = jCall.Count * JValue.Size;
			return this.UseStackAlloc(requiredBytes);
		}
		/// <summary>
		/// Retrieves <paramref name="argSpan"/> as a <see cref="JValue"/> span containing <paramref name="args"/> information.
		/// </summary>
		/// <param name="jniTransaction">A <see cref="INativeTransaction"/> transaction.</param>
		/// <param name="args">A <see cref="IObject"/> array.</param>
		/// <param name="argSpan">Destination span.</param>
		/// <exception cref="InvalidOperationException">Invalid object.</exception>
		private void CopyAsJValue(INativeTransaction jniTransaction, IReadOnlyList<IObject?> args, Span<Byte> argSpan)
		{
			Span<JValue> result = argSpan.AsValues<Byte, JValue>();
			for (Int32 i = 0; i < args.Count; i++)
			{
				switch (args[i])
				{
					case null:
						result[i] = JValue.Empty;
						continue;
					case IPrimitiveType:
						args[i]!.CopyTo(result, i);
						break;
					default:
						JReferenceObject referenceObject = (args[i] as JReferenceObject)!;
						ImplementationValidationUtilities.ThrowIfProxy(referenceObject);
						this.ReloadClass(referenceObject as JClassObject);
						ImplementationValidationUtilities.ThrowIfDefault(referenceObject, $"Invalid object at {i}.");
						jniTransaction.Add(referenceObject);
						args[i]!.CopyTo(result, i);
						break;
				}
			}
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for class transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="methodId">Call method id.</param>
		/// <param name="execution">Indicates whether transaction is for call execution..</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetClassTransaction(JClassObject jClass, JCallDefinition definition,
			out JMethodId methodId, Boolean execution = true)
		{
			INativeTransaction jniTransaction =
				this.VirtualMachine.CreateTransaction(1 + (execution ? definition.ReferenceCount : 0));
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetStaticMethodId(definition, this._env);
			return jniTransaction;
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for instance transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="methodId">Output. Call method id.</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetInstanceTransaction(JClassObject jClass, JCallDefinition definition,
			out JMethodId methodId)
		{
			INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(1);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetMethodId(definition, this._env);
			return jniTransaction;
		}
		/// <summary>
		/// Creates <see cref="INativeTransaction"/> for instance transaction.
		/// </summary>
		/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <param name="definition">Transaction call definition.</param>
		/// <param name="localRef">Output. Used object reference.</param>
		/// <param name="methodId">Output. Call method id.</param>
		/// <returns>A <see cref="INativeTransaction"/> instance.</returns>
		private INativeTransaction GetInstanceTransaction(JClassObject jClass, JLocalObject jLocal,
			JCallDefinition definition, out JObjectLocalRef localRef, out JMethodId methodId)
		{
			INativeTransaction jniTransaction = this.VirtualMachine.CreateTransaction(2 + definition.ReferenceCount);
			AccessCache access = this.GetAccess(jniTransaction, jClass);
			methodId = access.GetMethodId(definition, this._env);
			localRef = this.UseObject(jniTransaction, jLocal);
			return jniTransaction;
		}
	}
}