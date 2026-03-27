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

public enum PLCrashReportOperatingSystem : long
{
    MacOSX = 0,
    iPhoneOS = 1,
    iPhoneSimulator = 2,
    Unknown = 3,
    AppleTVOS = 4
}

public enum PLCrashReportArchitecture : long
{
    X86_32 = 0,
    X86_64 = 1,
    ARMv6 = 2,
    PPC = 3,
    PPC64 = 4,
    ARMv7 = 5,
    Unknown = 6
}

public enum PLCrashReportProcessorTypeEncoding : long
{
    Unknown = 0,
    Mach = 1
}