using System;

namespace CrashReporter.Maui.iOS.PLCrashReporter;

public enum PLCrashReporterSignalHandlerType : long
{
    BSD = 0,
    Mach = 1
}

[Flags]
public enum PLCrashReporterSymbolicationStrategy : long
{
    None = 0,
    SymbolTable = 1 << 0,
    ObjC = 1 << 1,
    All = SymbolTable | ObjC  // = 3
}

public enum PLCrashReportTextFormat : uint
{
    iOS = 0
}