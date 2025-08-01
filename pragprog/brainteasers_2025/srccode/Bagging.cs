/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Bagging;

[TestFixture]
public class Bagging : ConsoleRedirected
{
    string SuitIcon(Suit suit)
        => suit switch
        {
            Suit.Hearts => "♥",     // \u2665
            Suit.Clubs => "♣",      // \u2663
            Suit.Diamonds => "♦",   // \u2666
            Suit.Spades => "♠"      // \u2660
        };

    enum Suit { Hearts, Clubs, Diamonds, Spades }

    [Test]
    public void _()
    {
        Assert.That(() =>
        {
            var card = SuitIcon(Suit.Diamonds + 2);
            Console.WriteLine($"Chosen suit is {card}");
        }, Throws.TypeOf<System.Runtime.CompilerServices.SwitchExpressionException>()
            .With.Message.EndsWith("4."));

        /*
        Unhandled exception. System.Runtime.CompilerServices.SwitchExpressionException:
            Non-exhaustive switch expression failed to match its input.
            Unmatched value was 4
        */

        /*
        Warning CS8524 : The switch expression does not handle some values 
        of its input type (it is not exhaustive) involving an unnamed enum 
        value. For example, the pattern '(Suit)4' is not covered.
         */
    }

    [Test]
    public void SwitchStatement()
    {
        string SuitIcon(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts: return "♥";   // \u2665
                case Suit.Clubs: return "♣";    // \u2663
                case Suit.Diamonds: return "♦"; // \u2666
                case Suit.Spades: return "♠";   // \u2660

                default: throw new ArgumentOutOfRangeException( /*...*/);
            }

            return null;
        }
    }

    [Test]
    public void SwitchStatementNoDefault()
    {
        string SuitIcon(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts: return "♥";   // \u2665
                case Suit.Clubs: return "♣";    // \u2663
                case Suit.Diamonds: return "♦"; // \u2666
                case Suit.Spades: return "♠";   // \u2660
            }

            return "";
        }

        Assert.That(SuitIcon(Suit.Spades), Is.EqualTo("♠"));
        var card = Suit.Diamonds + 2;
        Assert.That(() => SuitIcon(card), Is.Empty);

        /*
        Error CS0161 : 'SuitIcon(Suit)': not all code paths return a value
        */

        TestContext.WriteLine(Output);
    }

    [Test]
    public void With_catch_all()
    {
        string SuitIcon(Suit suit) 
            => suit switch
            {
                Suit.Hearts => "♥",     // \u2665
                Suit.Clubs => "♣",      // \u2663
                Suit.Diamonds => "♦",   // \u2666
                Suit.Spades => "♠",     // \u2660
                
                _ => throw new ArgumentOutOfRangeException( /*...*/ )
            };
        var card = Suit.Diamonds + 2;
        
        Assert.That(() => SuitIcon(card), Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}

public class EnumTypes
{
    enum Suit : byte { Hearts, Clubs, Diamonds, Spades }
}
