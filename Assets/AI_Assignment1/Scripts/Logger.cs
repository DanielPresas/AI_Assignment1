using System.Diagnostics;

class Logger {

    private static string getCallerName() {
        var callingMethod = new StackTrace().GetFrame(2).GetMethod();
        return callingMethod.ReflectedType.Name + ":" + callingMethod.Name;
    }

    // ---------------------------------------------------------------
    // NORMAL LOG FUNCTIONS
    // ---------------------------------------------------------------

    public static void Log(object log) {
        UnityEngine.Debug.Log($"[{getCallerName()}] {log}");
    }

    public static void Log(string fmt, params object[] args) {
        UnityEngine.Debug.LogFormat($"[{getCallerName()}] {fmt}", args);
    }

    public static void Warning(object log) {
        UnityEngine.Debug.LogWarning($"[{getCallerName()}] {log}");
    }

    public static void Warning(string fmt, params object[] args) {
        UnityEngine.Debug.LogWarningFormat($"[{getCallerName()}] {fmt}", args);
    }

    public static void Error(object log) {
        UnityEngine.Debug.LogError($"[{getCallerName()}] {log}");
    }

    public static void Error(string fmt, params object[] args) {
        UnityEngine.Debug.LogErrorFormat($"[{getCallerName()}] {fmt}", args);
    }

    public static void Assert(bool condition, object log) {
        UnityEngine.Debug.Assert(condition, $"[{getCallerName()}] {log}");
    }

    public static void Assert(bool condition, string fmt, params object[] args) {
        UnityEngine.Debug.AssertFormat(condition, $"[{getCallerName()}] {fmt}", args);
    }

    // ---------------------------------------------------------------
    // GENERAL EXCEPTION LOG FUNCTIONS
    // ---------------------------------------------------------------

    public static void ExceptionWarning(System.Exception exception) {
        UnityEngine.Debug.LogWarning($"[{exception.TargetSite}::{getCallerName()}] {exception.GetType()}: {exception.Message}");
    }

    public static void ExceptionWarning(System.Exception exception, object log) {
        UnityEngine.Debug.LogWarning($"[{exception.TargetSite}::{getCallerName()}] {log}\n{exception.GetType()}: {exception.Message}");
    }

    public static void ExceptionError(System.Exception exception) {
        UnityEngine.Debug.LogError($"[{exception.TargetSite}::{getCallerName()}] {exception.GetType()}: {exception.Message}");
    }

    public static void ExceptionError(System.Exception exception, object log) {
        UnityEngine.Debug.LogError($"[{exception.TargetSite}::{getCallerName()}] {log}\n{exception.GetType()}: {exception.Message}");
    }
}
