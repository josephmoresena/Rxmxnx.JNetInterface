namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// Definition for a <c>static void main(String[])</c> method.
/// </summary>
public sealed class JMainMethodDefinition : JMethodDefinition
{
	/// <summary>
	/// Information for <c>main(java.lang.String[])</c>.
	/// </summary>
	private static readonly AccessibleInfoSequence info = new(JAccessibleObjectDefinition.MainMethodHash, 4, 22);
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly JMainMethodDefinition Instance = new();

	/// <summary>
	/// Constructor.
	/// </summary>
	private JMainMethodDefinition() : base(JMainMethodDefinition.info, IntPtr.Size, [IntPtr.Size,], 1) { }

	/// <summary>
	/// Invokes method defined in <paramref name="mainClass"/> with null args.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	public void InvokeWithNullArgs(JClassObject mainClass) => this.InvokeMain(mainClass);
	/// <summary>
	/// Invokes method defined in <paramref name="mainClass"/>.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	/// <param name="args">Arguments.</param>
	public void Invoke(JClassObject mainClass, params String?[] args)
	{
		IEnvironment env = mainClass.Environment;
		using JArrayObject<JStringObject> jArgs = JMainMethodDefinition.CreateArgsArray(env, args);
		this.InvokeMain(mainClass, jArgs);
	}

	/// <summary>
	/// Invokes current definition as static method in <paramref name="mainClass"/>.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	/// <param name="args">Java arguments array.</param>
	private void InvokeMain(JClassObject mainClass, JArrayObject<JStringObject>? args = default)
	{
		try
		{
			NativeFunctionSetImpl.SingleObjectBuffer buffer = new();
			Span<IObject?> invokeArgs = NativeFunctionSetImpl.SingleObjectBuffer.GetSpan(ref buffer);
			invokeArgs[0] = args;
			this.StaticInvoke(mainClass, invokeArgs);
		}
		finally
		{
			args?.Dispose();
		}
	}

	/// <summary>
	/// Creates a <see cref="JArrayObject{JStringObject}"/> in order to invoke PSVM method.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="args">A <see cref="String"/> array.</param>
	/// <returns>
	/// A <see cref="JArrayObject{JStringObject}"/> instance containing each value from <paramref name="args"/>.
	/// </returns>
	[return: NotNullIfNotNull(nameof(args))]
	private static JArrayObject<JStringObject>? CreateArgsArray(IEnvironment env, String?[]? args = default)
	{
		if (args is null) return default;
		JArrayObject<JStringObject> jArgs = JArrayObject<JStringObject>.Create(env, args.Length);
		for (Int32 i = 0; i < args.Length; i++)
		{
			if (args[i] is null) continue;
			using JStringObject jString = JStringObject.Create(env, args[i])!;
			jArgs[i] = jString;
		}
		return jArgs;
	}
}