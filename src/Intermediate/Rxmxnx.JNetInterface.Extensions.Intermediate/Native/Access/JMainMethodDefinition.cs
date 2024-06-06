namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// Definition for a <c>static void main(String[])</c> method.
/// </summary>
public sealed class JMainMethodDefinition : JMethodDefinition
{
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly JMainMethodDefinition Instance = new();

	/// <summary>
	/// Constructor.
	/// </summary>
	private JMainMethodDefinition() : base("main"u8, JArgumentMetadata.Get<JArrayObject<JStringObject>>()) { }

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
			IObject?[] invokeArgs = this.CreateArgumentsArray();
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