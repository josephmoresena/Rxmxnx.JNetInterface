using Rxmxnx.JNetInterface.ApplicationTest;

if (args.Length == 0)
	throw new ArgumentException("Please set project directory.");

DirectoryInfo projectDirectory = new(args[0]);
DirectoryInfo outputDirectory = args.Length >= 2 ?
	new(args[1]) :
	new DirectoryInfo(Environment.CurrentDirectory).CreateSubdirectory("Output");
Launcher launcher = await Launcher.Create(outputDirectory);

Jdk minJdk = launcher.GetMinJdk();
await TestCompiler.CompileClass(minJdk, launcher.OutputDirectory);
await TestCompiler.CompileNet(projectDirectory, launcher.RuntimeIdentifierPrefix, outputDirectory.FullName);

await launcher.Execute();