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
public class Inequality : ConsoleRedirected
{
    [Test]
    public void ViralNan()
    {
        double[] inputs = [ 1.01, double.NaN, 0.99 ];
            
        var mean = inputs
            .Where(n => n != double.NaN)
            .Average();
            
        Console.WriteLine($"The average value is {mean}");
        
        Assert.That(mean, Is.NaN);
        TestContext.Write(Output);
    }
    
    [Test]
    public void IsNaNCheck()
    {
        double[] inputs = [ 1.01, double.NaN, 0.99 ];

        {
            var n = 0;
            double.IsNaN(n);
        }
        
        var mean = inputs
            .Where(n => !double.IsNaN(n))
            .Average();
        
        Console.WriteLine($"The average value is {mean}");
        
        Assert.That(mean, Is.EqualTo(1.0));
        TestContext.Write(Output);
    }
    
    [Test]
    public void ConstantPatternCheck()
    {
        double[] inputs = [ 1.01, double.NaN, 0.99 ];

        var mean = inputs
            .Where(n => n is not double.NaN)
            .Average();
        
        Console.WriteLine($"The average value is {mean}");
        
        Assert.That(mean, Is.EqualTo(1.0));
        TestContext.Write(Output);
    }
    
    [Test]
    public void DoubleEquals()
    {
        double[] inputs = [ 1.01, double.NaN, 0.99 ];

        var mean = inputs
            .Where(n => !n.Equals(double.NaN))
            .Average();
        
        Console.WriteLine($"The average value is {mean}");
        
        Assert.That(mean, Is.EqualTo(1.0));
        TestContext.Write(Output);
    }
    
    [Test]
    public void Infinity()
    {
        double[] inputs = [ 1.01, double.NegativeInfinity, 0.99 ];
            
        var mean = inputs
            .Where(n => !double.IsNaN(n))
            .Average();
        
        Assert.That(mean, Is.EqualTo(double.NegativeInfinity));
    }

    [Test]
    public void CompareTo()
    {
        var x = double.NaN;
        var y = double.NaN;
        Assert.That(x.CompareTo(y), Is.Zero);
    }

    [Test]
    public void Decimal_0over0()
    {
        decimal zero = 0m;

        Assert.That(() =>
        {
        var nan = zero / 0m;
        }, Throws.TypeOf<DivideByZeroException>());
    }
}
