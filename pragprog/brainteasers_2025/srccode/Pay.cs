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
public class Pay
{
    public IEnumerable<int> Read(int n)
    {
        Console.WriteLine(n);
        return [1, 2, 3, 4, 5];
    }

    [Test]
    public void _()
    {
        var results = Enumerable.Range(0, 3)
            .SelectMany(Read);

        if (results.Any())
        {
            Console.WriteLine(results.Count());
        }
    }

    public void Process(IEnumerable<int> values)
    {
        foreach (var value in values)
        {
            // do something with value
        }
    }

    [Test]
    public void Paged()
    {
        var results = Enumerable.Range(0, 3)
            .SelectMany(Read);

        var next = results;
        while (next.Any())
        {
            Process(next.Take(5));
            next = next.Skip(5);
        }
    }
}
