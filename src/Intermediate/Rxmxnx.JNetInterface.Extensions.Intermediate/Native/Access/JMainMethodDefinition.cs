namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// Definition for a <c>static void main(String[])</c> method.
/// </summary>
public sealed record JMainMethodDefinition : JMethodDefinition
{
	/// <summary>
	/// Instance.
	/// </summary>
	public static readonly JMainMethodDefinition Instance = new();

	/// <summary>
	/// Constructor.
	/// </summary>
	private JMainMethodDefinition() : base(UnicodeMethodNames.Main,
	                                       JArgumentMetadata.Create<JArrayObject<JStringObject>>()) { }

	/// <summary>
	/// Invokes method defined in <paramref name="mainClass"/>.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	/// <param name="nullArgs">
	/// Optional. Indicates whether <c>args</c> parameter should be passed as <c>null</c> to main method.
	/// </param>
	public void Invoke(JClassObject mainClass, Boolean nullArgs = false)
	{
		IEnvironment env = mainClass.Environment;
		JArrayObject<JStringObject>? args = !nullArgs ? JArrayObject<JStringObject>.Create(env, 0) : default;
		this.Invoke(mainClass, args);
	}
	/// <summary>
	/// Invokes method defined in <paramref name="mainClass"/>.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	/// <param name="args">Arguments.</param>
	public void Invoke(JClassObject mainClass, params String?[] args)
	{
		IEnvironment env = mainClass.Environment;
		JArrayObject<JStringObject> jArgs = JArrayObject<JStringObject>.Create(env, args.Length);
		for (Int32 i = 0; i < args.Length; i++)
		{
			if (args[i] is not null)
				using (JStringObject jString = JStringObject.Create(env, args[i])!)
					jArgs[i] = jString;
		}
		this.Invoke(mainClass, jArgs);
	}

	/// <summary>
	/// Invokes current definition as static method in <paramref name="mainClass"/>.
	/// </summary>
	/// <param name="mainClass">A Java main class.</param>
	/// <param name="args">Java arguments array.</param>
	private void Invoke(JClassObject mainClass, JArrayObject<JStringObject>? args)
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
}