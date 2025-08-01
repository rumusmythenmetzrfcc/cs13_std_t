/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers;

public static class AsyncTools
{
    public static async Task<string[]> SplitWordsAsync(string phrase)
    {
        // Use of null defaults to splitting on any whitespace character
        char[]? delimiters = null;

        var words = await Task.Factory.StartNew(() => 
            phrase.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));

        return words;
    }

    public static int CountWords()
    {
        var task = SplitWordsAsync("One Two\nThree");

        return task.Result.Length;
    }

    public static async Task<int> CountWordsAsync()
    {
        var words = await SplitWordsAsync("One Two\nThree");
        return words.Length;
    }

    [Test]
    public static void TestWordCount()
    {
        var busy = CountWords();
        Assert.That(busy, Is.EqualTo(3));
    }

    [Test]
    public static async Task TestWordCountAsync()
    {
        var busy = await CountWordsAsync();
        Assert.That(busy, Is.EqualTo(3));
    }
}
