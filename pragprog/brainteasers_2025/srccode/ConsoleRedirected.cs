/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers;

[TestFixture]
public abstract class ConsoleRedirected
{
    private StringBuilder buffer = new();
    StringWriter captured;
    private TextWriter defaultCon;

    public string Output => buffer.ToString().Trim();
    
    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(buffer);
        defaultCon = Console.Out;
        Console.SetOut(captured);
    }

    [OneTimeTearDown]
    public void ResetConsole()
    {
        Console.SetOut(defaultCon);
        captured.Dispose();
    }

    [TearDown]
    public void ClearBuffer()
    {
        buffer.Clear();
    }
}