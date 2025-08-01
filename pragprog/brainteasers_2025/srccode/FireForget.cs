/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers;

public static class Operations
{
    public static async void TidyCache(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        
        await Task.Run(() => 
            { 
                // ...do some hard, time consuming work
            });
        // ...
    }
}

public class EventArgs {} 
public class TextBox { public string Text { get; } }
public class Forms
{
    private TextBox cacheKey;

    public static async Task TidyCache(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        
        await Task.Run(() => 
            { 
                // ...do some hard, time consuming work
            });
        // ...
    }

    private async void OnButtonClick(object sender, EventArgs ev)
    {
        try
        {
            await TidyCache(cacheKey.Text);
        }
        catch // anything
        {
            // log the error
            // or otherwise report to the user
        }
    }
}


[TestFixture]
public class FireForget
{
    StringBuilder output = new();
    StringWriter captured;
        
    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);
        Console.SetOut(captured);
    }
        
    [Test, Ignore("Crashes intentionall")]
    public void Void_async_throws()
    {
        Assert.That(() => Operations.TidyCache(""), Throws.Nothing);
    }
    
    [Test, Ignore("Crashes intentionall")]
    public void Fire_and_forget()
    {
        try
        {
            Operations.TidyCache("");
            Console.WriteLine("Completed Successfully");
        }
        catch(ArgumentException error)
        {
            Console.Error.WriteLine($"Critical error: {error.Message}");
        }
        
        Assert.That(output.ToString().Trim(), Is.EqualTo("Completed Successfully"));
    }
}
