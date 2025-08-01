/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿using System.Diagnostics;

namespace CsBrainTeasers;

public class TestAsyncWait
{
    StringBuilder output = new();
    StringWriter captured;
        
    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);
        Console.SetOut(captured);
    }
    
    public TimeSpan Time(Action action)
    {
        var start = Stopwatch.StartNew();
        action();
        
        return start.Elapsed;
    }

    [Test]
    public void Time_async()
    {
        var elapsed = Time(async () => await Task.Delay(1000));

        // Round up or down to nearest second
        var seconds = Math.Round(elapsed.TotalSeconds);
        
        Console.WriteLine($"{seconds} second(s)");
        
        Assert.That(elapsed.TotalSeconds, Is.GreaterThan(0).And.LessThan(0.01));
        
        TestContext.Write(output.ToString());
    }
}
