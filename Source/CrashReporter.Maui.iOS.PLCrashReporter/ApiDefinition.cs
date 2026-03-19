using System;
using Foundation;
using ObjCRuntime;

namespace CrashReporter.Maui.iOS.PLCrashReporter;

[BaseType(typeof(NSObject))]
interface PLCrashReporterConfig
{
    [Export("initWithSignalHandlerType:symbolicationStrategy:shouldRegisterUncaughtExceptionHandler:")]
    NativeHandle Constructor(
        PLCrashReporterSignalHandlerType signalHandlerType,
        PLCrashReporterSymbolicationStrategy strategy,
        bool shouldRegisterUncaughtExceptionHandler);
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
    void PurgePendingCrashReport();

    [Export("enableCrashReporterAndReturnError:")]
    bool Enable([NullAllowed] out NSError error);

    [Export("generateLiveReportWithException:error:")]
    [return: NullAllowed]
    NSData GenerateLiveReport(NSException exception, [NullAllowed] out NSError error);
}

[BaseType(typeof(NSObject))]
interface PLCrashReport
{
    [Export("initWithData:error:")]
    NativeHandle Constructor(NSData data, [NullAllowed] out NSError error);

    [Export("hasExceptionInfo")]
    bool HasExceptionInfo { get; }
}

[BaseType(typeof(NSObject))]
[DisableDefaultCtor]
interface PLCrashReportTextFormatter
{
    [Static]
    [Export("stringValueForCrashReport:withTextFormat:")]
    [return: NullAllowed]
    string StringValue(PLCrashReport report, PLCrashReportTextFormat format);
}