using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Log
{
    // All log colors. The value of each key is a RGB value, seperated by commas.
    private static Dictionary<string, string> logColors = new Dictionary<string, string>() {
        {"trace", "25, 25, 140"},
        {"debug", "3, 79, 132"},
        {"todo", "140, 110, 180"},
        {"info", "90, 155, 70"},
        {"error", "240, 25, 25"},
        {"critical", "200, 30, 30"},
        {"warn", "175, 96, 26"}
    };

    // Helper delegate that takes in params to support multiple string logging.
    // Had to be public because of accessibilty incombatibility.
    public delegate void Logger(params string[] s);

    // An optional path to write the log to. If specified, it'll write each log seperately on different lines.
    public static string outFile = "";
    // The log strings that'll be writen to the out file.
    private static List<string> logs = new List<string>();

    // Extra function that Debug.Logs multiple strings for conveniency's sake.
    public static void Print(params string[] str)
    {
        UnityEngine.Debug.Log(ConcatStrings(str));
    }

    // All the log functions.
    public static Logger Trace = m => LogWithColor("trace", m);
    public static Logger Debug = m => LogWithColor("debug", m);
    public static Logger Todo = m => LogWithColor("todo", m);
    public static Logger Info = m => LogWithColor("info", m);
    public static Logger Error = m => LogErrorWithColor("error", m);
    public static Logger Critical = m => LogErrorWithColor("critical", m);
    public static Logger Warn = m => LogWarningWithColor("warn", m);

    private static void LogWithColor(string name, params string[] strings)
    {
        Color color = ParseColorString(logColors[name.ToLower()]);
        string info = GetLogInfo(name.ToUpper());
        string fileName = GetCurrentFileName();
        string message = ConcatStrings(strings);

        logs.Add(String.Format(@"{0} {1}: {2}", info, fileName, message));

        // Where the magic happens. Each color value is converted into a hex code, and used as the log color. 
        // Then, the log info is logged using that color.
        // This is followed by logging the file name of the file that called this method,
        // and the original, concated string in plain black color.
        UnityEngine.Debug.Log(String.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color> {4}: {5}",
         (byte)(color.r * 255),
         (byte)(color.g * 255),
         (byte)(color.b * 255),
         info, fileName, message));

        if (!String.IsNullOrEmpty(outFile))
            File.WriteAllLines(outFile, logs.ToArray());
    }

    // Special case for the 'error' and 'critical' logs. Same as above, just Debug.LogErrors, and entirely in bold instead.
    private static void LogErrorWithColor(string name, params string[] strings)
    {
        Color color = ParseColorString(logColors[name.ToLower()]);
        string info = GetLogInfo(name.ToUpper());
        string fileName = GetCurrentFileName();
        string message = ConcatStrings(strings);

        logs.Add(String.Format(@"{0} {1}: {2}", info, fileName, message));
        UnityEngine.Debug.LogError(String.Format("<b><color=#{0:X2}{1:X2}{2:X2}>{3}</color> {4}: {5}</b>", (byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), info, fileName, message));

        if (!String.IsNullOrEmpty(outFile))
            File.WriteAllLines(outFile, logs.ToArray());
    }

    // Special case for the 'warning' log. Same as above, just Debug.LogErrors, and entirely in italics instead.
    private static void LogWarningWithColor(string name, params string[] strings)
    {
        Color color = ParseColorString(logColors[name.ToLower()]);
        string info = GetLogInfo(name.ToUpper());
        string fileName = GetCurrentFileName();
        string message = ConcatStrings(strings);

        logs.Add(String.Format(@"{0} {1}: {2}", info, fileName, message));
        UnityEngine.Debug.LogWarning(String.Format("<i><color=#{0:X2}{1:X2}{2:X2}>{3}</color> {4}: {5}</i>", (byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), info, fileName, message));

        if (!String.IsNullOrEmpty(outFile))
            File.WriteAllLines(outFile, logs.ToArray());
    }

    private static Color ParseColorString(string raw)
    {
        float[] colors = raw.Split(',').Select(s => Single.Parse(s)).ToArray();
        Color color = new Color(colors[0] / 255, colors[1] / 255, colors[2] / 255);
        return color;
    }

    private static string GetLogInfo(string logName)
    {
        return String.Format("[{0} {1}]", logName, DateTime.Now.ToString("h:mm:ss"));
    }

    private static string GetCurrentFileName()
    {
        return new System.Diagnostics.StackTrace(true).GetFrame(3).GetFileName();
    }

    private static string ConcatStrings(string[] strings)
    {
        string final = "";

        for (int i = 0; i < strings.Length; i++)
        {
            string addOnTo = "  ";
            if (i == strings.Length - 1) addOnTo = "";
            final += strings[i] + addOnTo;
        }

        return final;
    }
}