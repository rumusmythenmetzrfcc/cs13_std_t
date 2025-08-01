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
public class TestFloats
{
    StringBuilder output = new();
    StringWriter captured;
    
	[SetUp]
	public void ClearOutput()
	{
		output.Clear();
	}

    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);
        Console.SetOut(captured);
    }
    
    [Test]
    public void Double_key()
    {
        var scores = new Dictionary<double, int>()
        {
            { 48.483, 0 },
            { 23.811, 0 },
            { 69.707, 0 }
        };

        scores[48.483]++;

        Console.WriteLine(scores[48.483]);
        
        Assert.That(scores[48.483], Is.EqualTo(1));
        Assert.That(output.ToString().Trim(), Is.EqualTo("1"));
    }
        
    [Test]
    public void Double_equality()
    {
        var x = 1.1;
        var y = 2.2;
        
        Assert.That(() => {
        Assert.That(x + y, Is.EqualTo(3.3));
        }, Throws.TypeOf<AssertionException>());        
        /*
         Expected: 3.2999999999999998d
           But was:  3.3000000000000003d
         */        
    }

    [Test]
    public void Simulate_reading_file()
    {
        string[] inputData =
        {
            "48.483",
            "23.811",
            "48.483",
            "69.707"
        };

        var scores = inputData
            .Select(line => (double.Parse(line), 0))
            .Distinct()
            .ToDictionary();

        scores[48.483]++;
        Console.WriteLine(scores[48.483]);

        Assert.That(scores[48.483], Is.EqualTo(1));
        Assert.That(output.ToString().Trim(), Is.EqualTo("1"));
    }
}

