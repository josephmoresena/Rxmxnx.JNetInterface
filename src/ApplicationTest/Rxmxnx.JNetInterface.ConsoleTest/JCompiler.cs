using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Rxmxnx.JNetInterface.ApplicationTest;

/// <summary>
/// Java compiler.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed partial class JCompiler
{
	/// <summary>
	/// Sdk path.
	/// </summary>
	public String JdkPath { get; set; } = default!;
	/// <summary>
	/// JAVAC path.
	/// </summary>
	public String CompilerPath { get; set; } = default!;
	/// <summary>
	/// JVM library path.
	/// </summary>
	public String LibraryPath { get; set; } = default!;

	/// <summary>
	/// Compiles HelloJni java class.
	/// </summary>
	/// <returns>Java class bytecode.</returns>
	public async Task<Byte[]> CompileHelloJniClassAsync()
	{
		String javaFilePath = Path.Combine(Path.GetTempPath(), "HelloDotnet.java");
		String classFilePath = Path.Combine(Path.GetTempPath(), "HelloDotnet.class");
		try
		{
			File.Delete(javaFilePath);
			File.Delete(classFilePath);
			await File.WriteAllTextAsync(javaFilePath, JCompiler.JavaCode);
			ProcessStartInfo info = new(Path.Combine(this.JdkPath, this.CompilerPath))
			{
				ArgumentList = { javaFilePath, },
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
	{
		String libraryPath = Path.Combine(this.JdkPath, this.LibraryPath);
		return JVirtualMachineLibrary.LoadLibrary(libraryPath) ??
			throw new InvalidOperationException($"Invalid JVM library. {libraryPath}");
	}
}