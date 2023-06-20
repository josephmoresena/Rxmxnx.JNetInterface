using System;

namespace Rxmxnx.JNetInterface.SourceGenerator
{
	internal sealed class NativeMethodGenerator
	{
		public String TypeName { get; set; }
		public String Documentation { get; set; }
		public String DefinitionPrefix { get; set; }
		public String DefinitionSufix { get; set; }
		public String BodyPrefix { get; set; }
		public String BodySufix { get; set; }
	}
}
