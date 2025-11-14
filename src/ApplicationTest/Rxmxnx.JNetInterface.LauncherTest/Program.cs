using Rxmxnx.JNetInterface.ApplicationTest;

if (args.Length == 0)
	throw new ArgumentException("Please set project directory.");

DirectoryInfo projectDirectory = new(args[0]);
DirectoryInfo outputDirectory = args.Length >= 2 ?
	new(args[1]) :
	new DirectoryInfo(Environment.CurrentDirectory).CreateSubdirectory("Output");
Boolean compile = args.Length < 3 || "compile".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean run = args.Length < 3 || "run".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean net = args.Length < 3 || "net".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean r2R = args.Length < 3 || "r2r".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean nAot = args.Length < 3 || "naot".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean rfm = args.Length < 3 || "rfm".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean jar = args.Length < 3 || "jar".AsSpan().SequenceEqual(args[2].ToLowerInvariant());

Launcher launcher = await Launcher.Create(outputDirectory);

if (compile)
	await TestCompiler.RunManagedTest(projectDirectory, launcher.NetVersions);

Jdk minJdk = launcher.GetMinJdk();
_ = Boolean.TryParse(Environment.GetEnvironmentVariable("JNETINTERFACE_ONLY_NATIVE_TEST"), out Boolean onlyNativeAot);

if (compile)
{
	await TestCompiler.CompileClass(minJdk, launcher.OutputDirectory);
	await TestCompiler.CompileNet(projectDirectory, launcher.RuntimeIdentifierPrefix, outputDirectory.FullName,
	                              launcher.NetVersions, onlyNativeAot);
}

if (run || jar)
	await launcher.ExecuteJar(launcher.NetVersions);
if (run || nAot)
	await launcher.Execute(".NAOT");
if (run || rfm)
	await launcher.Execute(".RFM");
if (run || r2R)
	await launcher.Execute(".R2R");
if (run || net)
	await launcher.Execute();