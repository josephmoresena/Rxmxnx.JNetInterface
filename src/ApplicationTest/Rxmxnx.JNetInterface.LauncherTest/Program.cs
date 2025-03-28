﻿using Rxmxnx.JNetInterface.ApplicationTest;

if (args.Length == 0)
	throw new ArgumentException("Please set project directory.");

DirectoryInfo projectDirectory = new(args[0]);
DirectoryInfo outputDirectory = args.Length >= 2 ?
	new(args[1]) :
	new DirectoryInfo(Environment.CurrentDirectory).CreateSubdirectory("Output");
Boolean compile = args.Length < 3 || "compile".AsSpan().SequenceEqual(args[2].ToLowerInvariant());
Boolean run = args.Length < 3 || "run".AsSpan().SequenceEqual(args[2].ToLowerInvariant());

if (compile)
	await TestCompiler.RunManagedTest(projectDirectory);

Launcher launcher = await Launcher.Create(outputDirectory);

Jdk minJdk = launcher.GetMinJdk();
_ = Boolean.TryParse(Environment.GetEnvironmentVariable("JNETINTERFACE_ONLY_NATIVE_TEST"), out Boolean onlyNativeAot);

if (compile)
{
	await TestCompiler.CompileClass(minJdk, launcher.OutputDirectory);
	await TestCompiler.CompileNet(projectDirectory, launcher.RuntimeIdentifierPrefix, outputDirectory.FullName,
	                              onlyNativeAot);
}

if (run)
	await launcher.Execute();