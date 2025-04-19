namespace Rxmxnx.JNetInterface.Awt;

[Flags]
public enum EventMask : UInt64
{
	Component = 0x1,
	Container = 0x2,
	Focus = 0x4,
	Key = 0x8,
	Mouse = 0x10,
	MouseMotion = 0x20,
	Window = 0x40,
	Action = 0x80,
	Adjustment = 0x100,
	Text = 0x800,
	InputMethod = 0x1000,
	Paint = 0x2000,
	Invocation = 0x4000,
	Hierarchy = 0x8000,
	HierarchyBounds = 0x10000,
	MouseWheel = 0x20000,
	WindowState = 0x40000,
	WindowFocus = 0x80000,
}