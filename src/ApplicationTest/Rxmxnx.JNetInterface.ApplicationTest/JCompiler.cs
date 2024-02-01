using System.Diagnostics;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// Java compiler.
/// </summary>
public sealed partial record JCompiler
{
	/// <summary>
	/// Sdk path.
	/// </summary>
	public String JdkPath { get; init; } = default!;
	/// <summary>
	/// JAVAC path.
	/// </summary>
	public String CompilerPath { get; init; } = default!;
	/// <summary>
	/// JVM library path.
	/// </summary>
	public String LibraryPath { get; init; } = default!;

	/// <summary>
	/// Compiles HelloJni java class.
	/// </summary>
	/// <returns>Java class bytecode.</returns>
	public async Task<Byte[]> CompileHelloJniClassAsync()
	{
		String javaFilePath = Path.Combine(Environment.CurrentDirectory, "HelloDotnet.java");
		String classFilePath = Path.Combine(Environment.CurrentDirectory, "HelloDotnet.class");
		try
		{
			File.Delete(javaFilePath);
			File.Delete(classFilePath);
			await File.WriteAllTextAsync(javaFilePath, await JCompiler.GetHelloJniJavaAsync());
			ProcessStartInfo info = new(Path.Combine(this.JdkPath, this.CompilerPath))
			{
				ArgumentList = { javaFilePath, }, WindowStyle = ProcessWindowStyle.Hidden,
			};
			Process javac = Process.Start(info)!;
			await javac.WaitForExitAsync();
			return await File.ReadAllBytesAsync(classFilePath);
		}
		finally
		{
			File.Delete(javaFilePath);
			File.Delete(classFilePath);
		}
	}
	/// <summary>
	/// Retrieves a <see cref="JVirtualMachineLibrary"/> instance
	/// from current JDK.
	/// </summary>
	/// <returns>A <see cref="JVirtualMachineLibrary"/> instance.</returns>
	public JVirtualMachineLibrary GetLibrary()
		=> JVirtualMachineLibrary.LoadLibrary(Path.Combine(this.JdkPath, this.LibraryPath)) ??
			throw new InvalidOperationException("Invalid JVM library.");
}