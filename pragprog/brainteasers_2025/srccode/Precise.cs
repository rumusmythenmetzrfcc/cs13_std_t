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
public class Precise
{
    StringBuilder output = new();
    StringWriter captured;
    private TextWriter def;
    
    [SetUp]
    public void ClearConsole()
    {
        output.Clear();
    }

    [OneTimeSetUp]
    public void RedirectConsole()
    {
        captured = new(output);

        def = Console.Out;
        Console.SetOut(captured);
    }

    [TearDown]
    public void Reset()
    {
        Console.SetOut(def);
    }

    [Test]
    public void Double_calc()
    {
        var x = 1.1;
        var y = 2.2;
        
        Console.WriteLine($"{x} + {y} == {x + y}");
        /*
        1.1 + 2.2 == 3.3000000000000003
        */
        TestContext.WriteLine(output);
    }

    [Test]
    public void Double_roundtrip()
    {
        var x = 3.3;
        Assert.That(double.Parse("3.3000000000000003"), Is.EqualTo(1.1 + 2.2));
        Assert.That(double.Parse("3.2999999999999998"), Is.EqualTo(x));
        Assert.That(double.Parse("3.3"), Is.EqualTo(x));

        Assert.That(() =>
        {
            return
        double.Parse(x.ToString()).Equals(x);
        }, Is.True);
        
        Console.WriteLine($"{1.1:G32}");
        /*
        1.1000000000000000888178419700125
        */
        TestContext.WriteLine(output);
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
        START:double_representation
         Expected: 3.2999999999999998d
           But was:  3.3000000000000003d
        END:double_representation
         */        
        
        Assert.That(x + y, Is.EqualTo(3.3).Within(0.001));
    }
}
