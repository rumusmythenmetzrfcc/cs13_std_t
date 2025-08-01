/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Optional;

[TestFixture]
public class OptionalParams
{
    public enum Level
    {
        Debug,
        Info,
        Warn,
        Error,
        Critical
    }

    public readonly record struct LogEntry(Level Level=Level.Info, 
                                           string Message="Event");

    StringBuilder output = new();
    StringWriter captured;
        
    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);
        Console.SetOut(captured);
    }

    [Test]
    public void _()
    {
        var entry = new LogEntry();
        
        Console.WriteLine($"{entry.Level} - {entry.Message}");
        Assert.That(entry.Message, Is.Null);
        
        TestContext.WriteLine(output);
    }
}

public class OptionalMethod
{
    public static class Version
    {
        public static int Check()
        {
            return 1;
        }

        public static int Check(bool usePatch=false)
        {
            return 2;
        }
    }

    [Test]
    public void _()
    {
        Assert.That(Version.Check(), Is.EqualTo(1));
    }
}

public static class TestMultiDefault
{
    public static class Version
    {
        public static int Check(bool useMajor=true, bool useMinor=true)
        {
            return 2;
        }

        public static int Check(bool useMajor, bool useMinor, bool usePatch=true)
        {
            return 3;
        }
    }

    [Test]
    public static void CallWithTwoArgs()
    {
        Assert.That(() =>
        { return
        Version.Check(false, false);
        }, Is.EqualTo(2));
    }

    [Test]
    public static void NamingOptionalArgs()
    {
        Assert.That(Version.Check(useMajor: false, useMinor: true), Is.EqualTo(2));
    }
}

public class ParameterlessCtr
{
    public enum Level
    {
        Debug,
        Info,
        Warn,
        Error,
        Critical
    }

    public readonly record struct LogEntry(Level Level, string Message)
    {
        public LogEntry() : this(Level.Info, "Event")
        {
        }
    }
    
    StringBuilder output = new();
    StringWriter captured;
        
    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);
        Console.SetOut(captured);
    }

    [Test]
    public void _()
    {
        var entry = new LogEntry();
        
        Console.WriteLine($"{entry.Level} - {entry.Message}");

        Assert.That(entry.Message, Is.EqualTo("Event"));
        
        TestContext.WriteLine(output);
    }
}
