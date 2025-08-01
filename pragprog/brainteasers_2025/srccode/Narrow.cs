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
public class Narrow : ConsoleRedirected
{
    public readonly struct Converter(char value)
    {
        public override string ToString() 
            => value.ToString();

        public static explicit operator Converter(char other)
            => new(other);
    }

    [Test]
    public void Double_to_char_Converter()
    {
        double money = 123.456;
        var converted = (Converter)money;

        Console.WriteLine($"Value of {nameof(converted)} is '{converted}'");

        /*
        Value of converted is '{'
        */

        var expected = "Value of converted is '{'";
        Assert.That(Output, Is.EqualTo(expected));
    }

    [Test]
    public void Int_to_double()
    {
        int original = 123;
        double converted = original;
        
        Assert.That(converted, Is.EqualTo(original));
    }

    [Test]
    public void Char_to_double()
    {
        char original = '{';
        double converted = original;
        
        Assert.That(converted, Is.EqualTo(123));
    }
    
    [Test]
    public void Explicit_char_cast()
    {
        /*
        char converted = 123.456;
         */

        char converted = (char)123.456;
        Console.WriteLine($"Value of {nameof(converted)} is '{converted}'");
        
        var expected = "Value of converted is '{'";
        Assert.That(Output, Is.EqualTo(expected));
    }

    [Test]
    public void Explicit_double_cast()
    {
        double money = 123.456;
        char intermediate = (char)money;
        var converted = (Converter)intermediate;

        Assert.That(converted.ToString(), Is.EqualTo("{"));
    }
}

public class ImplicitArgs
{
    public class Converter(char value)
    {
        public static explicit operator Converter(char other)
            => new(other);
    }
    public static void RegularMethod(char other)
    {
        // ...
    }

    [Test]
    public void _()
    {
        /*
        RegularMethod(123.456);
        Error CS1503 : Argument 1: cannot convert from 'double' to 'char'
        */
        RegularMethod((char)123.456);
    }

    [Test]
    public void Implicit_char_cast()
    {
        
    }
}
