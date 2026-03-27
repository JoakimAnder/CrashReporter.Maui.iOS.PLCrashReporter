using System;
using Foundation;
using ObjCRuntime;

namespace CrashReporter.Maui.iOS.PLCrashReporter;

// MARK: - Configuration & Reporter

[BaseType(typeof(NSObject))]
interface PLCrashReporterConfig
{
    [Static]
    [Export("defaultConfiguration")]
    PLCrashReporterConfig DefaultConfiguration { get; }

    [Export("initWithBasePath:")]
    NativeHandle Constructor(string basePath);

    [Export("initWithSignalHandlerType:symbolicationStrategy:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy symbolicationStrategy);

    [Export("initWithSignalHandlerType:symbolicationStrategy:basePath:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy symbolicationStrategy,
        string basePath);

    [Export("initWithSignalHandlerType:symbolicationStrategy:shouldRegisterUncaughtExceptionHandler:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy symbolicationStrategy,
        bool shouldRegisterUncaughtExceptionHandler);

    [Export("initWithSignalHandlerType:symbolicationStrategy:shouldRegisterUncaughtExceptionHandler:basePath:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy symbolicationStrategy,
        bool shouldRegisterUncaughtExceptionHandler,
        string basePath);

    [Export("initWithSignalHandlerType:symbolicationStrategy:shouldRegisterUncaughtExceptionHandler:basePath:maxReportBytes:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy symbolicationStrategy,
        bool shouldRegisterUncaughtExceptionHandler,
        string basePath,
        nuint maxReportBytes);

    [Export("basePath")]
    string BasePath { get; }

    [Export("signalHandlerType")]
    PLCrashReporterSignalHandlerType SignalHandlerType { get; }

    [Export("symbolicationStrategy")]
    PLCrashReporterSymbolicationStrategy SymbolicationStrategy { get; }

    [Export("shouldRegisterUncaughtExceptionHandler")]
    bool ShouldRegisterUncaughtExceptionHandler { get; }

    [Export("maxReportBytes")]
    nuint MaxReportBytes { get; }
}

[BaseType(typeof(NSObject))]
interface PLCrashReporter
{
    [Export("initWithConfiguration:")]
    NativeHandle Constructor(PLCrashReporterConfig config);

    [Export("hasPendingCrashReport")]
    bool HasPendingCrashReport { get; }

    [Export("loadPendingCrashReportDataAndReturnError:")]
    [return: NullAllowed]
    NSData LoadPendingCrashReportData([NullAllowed] out NSError error);

    [Export("purgePendingCrashReport")]
    bool PurgePendingCrashReport();

    [Export("purgePendingCrashReportAndReturnError:")]
    bool PurgePendingCrashReportAndReturnError([NullAllowed] out NSError error);

    [Export("enableCrashReporterAndReturnError:")]
    bool Enable([NullAllowed] out NSError error);

    [Export("generateLiveReportAndReturnError:")]
    [return: NullAllowed]
    NSData GenerateLiveReport([NullAllowed] out NSError error);

    [Export("generateLiveReportWithException:error:")]
    [return: NullAllowed]
    NSData GenerateLiveReport(NSException exception, [NullAllowed] out NSError error);

    [Export("crashReportPath")]
    string CrashReportPath { get; }

    [NullAllowed]
    [Export("customData", ArgumentSemantic.Strong)]
    NSData CustomData { get; set; }
}

// MARK: - Crash Report (root object)

[BaseType(typeof(NSObject))]
interface PLCrashReport
{
    [Export("initWithData:error:")]
    NativeHandle Constructor(NSData data, [NullAllowed] out NSError error);

    [Export("systemInfo")]
    PLCrashReportSystemInfo SystemInfo { get; }

    [Export("hasMachineInfo")]
    bool HasMachineInfo { get; }

    [NullAllowed]
    [Export("machineInfo")]
    PLCrashReportMachineInfo MachineInfo { get; }

    [Export("applicationInfo")]
    PLCrashReportApplicationInfo ApplicationInfo { get; }

    [Export("hasProcessInfo")]
    bool HasProcessInfo { get; }

    [NullAllowed]
    [Export("processInfo")]
    PLCrashReportProcessInfo ProcessInfo { get; }

    [Export("signalInfo")]
    PLCrashReportSignalInfo SignalInfo { get; }

    [NullAllowed]
    [Export("machExceptionInfo")]
    PLCrashReportMachExceptionInfo MachExceptionInfo { get; }

    [Export("threads")]
    PLCrashReportThreadInfo[] Threads { get; }

    [Export("images")]
    PLCrashReportBinaryImageInfo[] Images { get; }

    [Export("hasExceptionInfo")]
    bool HasExceptionInfo { get; }

    [NullAllowed]
    [Export("exceptionInfo")]
    PLCrashReportExceptionInfo ExceptionInfo { get; }

    [NullAllowed]
    [Export("customData")]
    NSData CustomData { get; }

    [Export("uuidRef")]
    IntPtr UuidRef { get; }

    [Export("imageForAddress:")]
    [return: NullAllowed]
    PLCrashReportBinaryImageInfo ImageForAddress(ulong address);
}

// MARK: - Text Formatter

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportTextFormatter
{
    [Static]
    [Export("stringValueForCrashReport:withTextFormat:")]
    [return: NullAllowed]
    string StringValue(PLCrashReport report, PLCrashReportTextFormat format);
}

// MARK: - Crash Report Model Classes

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportProcessorInfo
{
    [Export("initWithTypeEncoding:type:subtype:")]
    NativeHandle Constructor(PLCrashReportProcessorTypeEncoding typeEncoding, ulong type, ulong subtype);

    [Export("typeEncoding")]
    PLCrashReportProcessorTypeEncoding TypeEncoding { get; }

    [Export("type")]
    ulong Type { get; }

    [Export("subtype")]
    ulong Subtype { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportSystemInfo
{
    [Export("initWithOperatingSystem:operatingSystemVersion:operatingSystemBuild:architecture:processorInfo:timestamp:")]
    NativeHandle Constructor(
        PLCrashReportOperatingSystem operatingSystem,
        string operatingSystemVersion,
        [NullAllowed] string operatingSystemBuild,
        PLCrashReportArchitecture architecture,
        PLCrashReportProcessorInfo processorInfo,
        [NullAllowed] NSDate timestamp);

    [Export("operatingSystem")]
    PLCrashReportOperatingSystem OperatingSystem { get; }

    [Export("operatingSystemVersion")]
    string OperatingSystemVersion { get; }

    [NullAllowed]
    [Export("operatingSystemBuild")]
    string OperatingSystemBuild { get; }

    [Export("architecture")]
    PLCrashReportArchitecture Architecture { get; }

    [NullAllowed]
    [Export("timestamp")]
    NSDate Timestamp { get; }

    [Export("processorInfo")]
    PLCrashReportProcessorInfo ProcessorInfo { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportMachineInfo
{
    [Export("initWithModelName:processorInfo:processorCount:logicalProcessorCount:")]
    NativeHandle Constructor(
        [NullAllowed] string modelName,
        PLCrashReportProcessorInfo processorInfo,
        nuint processorCount,
        nuint logicalProcessorCount);

    [NullAllowed]
    [Export("modelName")]
    string ModelName { get; }

    [Export("processorInfo")]
    PLCrashReportProcessorInfo ProcessorInfo { get; }

    [Export("processorCount")]
    nuint ProcessorCount { get; }

    [Export("logicalProcessorCount")]
    nuint LogicalProcessorCount { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportApplicationInfo
{
    [Export("initWithApplicationIdentifier:applicationVersion:applicationMarketingVersion:")]
    NativeHandle Constructor(
        string applicationIdentifier,
        string applicationVersion,
        [NullAllowed] string applicationMarketingVersion);

    [Export("applicationIdentifier")]
    string ApplicationIdentifier { get; }

    [Export("applicationVersion")]
    string ApplicationVersion { get; }

    [NullAllowed]
    [Export("applicationMarketingVersion")]
    string ApplicationMarketingVersion { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportProcessInfo
{
    [Export("initWithProcessName:processID:processPath:processStartTime:parentProcessName:parentProcessID:native:")]
    NativeHandle Constructor(
        [NullAllowed] string processName,
        nuint processId,
        [NullAllowed] string processPath,
        [NullAllowed] NSDate processStartTime,
        [NullAllowed] string parentProcessName,
        nuint parentProcessId,
        bool native);

    [NullAllowed]
    [Export("processName")]
    string ProcessName { get; }

    [Export("processID")]
    nuint ProcessId { get; }

    [NullAllowed]
    [Export("processPath")]
    string ProcessPath { get; }

    [NullAllowed]
    [Export("processStartTime")]
    NSDate ProcessStartTime { get; }

    [NullAllowed]
    [Export("parentProcessName")]
    string ParentProcessName { get; }

    [Export("parentProcessID")]
    nuint ParentProcessId { get; }

    [Export("native")]
    bool Native { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportSignalInfo
{
    [Export("initWithSignalName:code:address:")]
    NativeHandle Constructor(string name, string code, ulong address);

    [Export("name")]
    string Name { get; }

    [Export("code")]
    string Code { get; }

    [Export("address")]
    ulong Address { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportMachExceptionInfo
{
    [Export("initWithType:codes:")]
    NativeHandle Constructor(ulong type, NSNumber[] codes);

    [Export("type")]
    ulong MachExceptionType { get; }

    [Export("codes")]
    NSNumber[] Codes { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportSymbolInfo
{
    [Export("initWithSymbolName:startAddress:endAddress:")]
    NativeHandle Constructor(string symbolName, ulong startAddress, ulong endAddress);

    [Export("symbolName")]
    string SymbolName { get; }

    [Export("startAddress")]
    ulong StartAddress { get; }

    [Export("endAddress")]
    ulong EndAddress { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportStackFrameInfo
{
    [Export("initWithInstructionPointer:symbolInfo:")]
    NativeHandle Constructor(ulong instructionPointer, [NullAllowed] PLCrashReportSymbolInfo symbolInfo);

    [Export("instructionPointer")]
    ulong InstructionPointer { get; }

    [NullAllowed]
    [Export("symbolInfo")]
    PLCrashReportSymbolInfo SymbolInfo { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportRegisterInfo
{
    [Export("initWithRegisterName:registerValue:")]
    NativeHandle Constructor(string registerName, ulong registerValue);

    [Export("registerName")]
    string RegisterName { get; }

    [Export("registerValue")]
    ulong RegisterValue { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportThreadInfo
{
    [Export("initWithThreadNumber:stackFrames:crashed:registers:")]
    NativeHandle Constructor(nint threadNumber, PLCrashReportStackFrameInfo[] stackFrames,
        bool crashed, PLCrashReportRegisterInfo[] registers);

    [Export("threadNumber")]
    nint ThreadNumber { get; }

    [Export("stackFrames")]
    PLCrashReportStackFrameInfo[] StackFrames { get; }

    [Export("crashed")]
    bool Crashed { get; }

    [Export("registers")]
    PLCrashReportRegisterInfo[] Registers { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportBinaryImageInfo
{
    [Export("initWithCodeType:baseAddress:size:name:uuid:")]
    NativeHandle Constructor(
        [NullAllowed] PLCrashReportProcessorInfo codeType,
        ulong baseAddress,
        ulong size,
        string name,
        [NullAllowed] NSData uuid);

    [NullAllowed]
    [Export("codeType")]
    PLCrashReportProcessorInfo CodeType { get; }

    [Export("imageBaseAddress")]
    ulong ImageBaseAddress { get; }

    [Export("imageSize")]
    ulong ImageSize { get; }

    [Export("imageName")]
    string ImageName { get; }

    [Export("hasImageUUID")]
    bool HasImageUUID { get; }

    [NullAllowed]
    [Export("imageUUID")]
    string ImageUUID { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportExceptionInfo
{
    [Export("initWithExceptionName:reason:")]
    NativeHandle Constructor(string name, string reason);

    [Export("initWithExceptionName:reason:stackFrames:")]
    NativeHandle Constructor(string name, string reason, [NullAllowed] PLCrashReportStackFrameInfo[] stackFrames);

    [Export("exceptionName")]
    string ExceptionName { get; }

    [Export("exceptionReason")]
    string ExceptionReason { get; }

    [NullAllowed]
    [Export("stackFrames")]
    PLCrashReportStackFrameInfo[] StackFrames { get; }
}
