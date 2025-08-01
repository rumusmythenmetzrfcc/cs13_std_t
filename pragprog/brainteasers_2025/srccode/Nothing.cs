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

[TestFixture]
public class Nothing : ConsoleRedirected
{
    string[] ParseParts(string? input)
    {
        return input switch
        {
            null          => throw new ArgumentNullException(nameof(input)),
            /*
            string.Empty  => throw new ArgumentException(nameof(input)),
            */
            ""            => throw new ArgumentException(nameof(input)),
            _             => input.Split(';')
        };
    }
    
    [Test]
    public void Empty_string_use()
    {
        Assert.That(() =>
        {
        Console.WriteLine(ParseParts(""));
        }, Throws.TypeOf<ArgumentException>());
    }

    [Test]
    public void NullStringException()
    {
        string? s = null;
        Assert.That(() =>
            {
                var upper = s!.ToLower();
            }
            , Throws.TypeOf<NullReferenceException>());
    }
    [Test]
    public void Uses_of_string_empty()
    {
        const char x = 'x';
        
        var empty = string.Empty;
        var literal = "";

        Type type = typeof(string);
        
        Assert.That(empty == literal, Is.True);

        Debug.Assert(empty == literal);
        if(empty == "") { /*...*/ }
        if(empty == string.Empty) { /*...*/ }
        
        // tag::const_literal]]
        const string constant = "";
        // end::const_literal]]
        /*
        // tag::const_empty]]
        const string constEmpty = string.Empty;
        // tag::const_empty]]
        */
        Assert.That(literal, Is.EqualTo(empty));
    }

    [Test]
    public void Constant_patterns()
    {
        var seconds = 3819;
        var isOver1Hour = seconds switch
        {
            > 60 * 60 => true,
            _ => false
        };
        
        Assert.That(isOver1Hour, Is.True);
    }

    [Test]
    public void Constant_declaration()
    {
        const int totalSecondsPerHour = 60 * 60;
        
        var seconds = 3819;
        var isOver1Hour = seconds switch
        {
            > totalSecondsPerHour => true,
            _ => false
        };

        Assert.That(isOver1Hour, Is.True);
    }
}

public class NullOrEmpty
{
    string[] ParseParts(string? input)
    {
        ArgumentException.ThrowIfNullOrEmpty(input, nameof(input));
        return input.Split(';');
    }

    [Test]
    public void Empty_string_exception()
    {
        var input = "";
        Assert.That(() => ParseParts(input), Throws.TypeOf<ArgumentException>());
    }
}
