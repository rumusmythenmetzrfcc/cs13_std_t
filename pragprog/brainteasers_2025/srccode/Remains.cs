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
public class Remains
{
    private StringBuilder buffer = new();
    private StringWriter output;

    [SetUp]
    public void Redirect()
    {
        output = new(buffer);
        Console.SetOut(output);
    }

    [TearDown]
    public void ClearOutput()
    {
        buffer.Clear();
    }
    
    [Test]
    public void Modulus_with_negative_dividend()
    {
        var a = -9;
        var b = 4;

        var remainder = a % b;
    
        Console.WriteLine($"{a} % {b} is {remainder}");

        var quot = a / b;
        Assert.That(quot, Is.EqualTo(-2));
        Assert.That(remainder, Is.EqualTo(-1));
        
        Assert.That(buffer.ToString().Trim(), Is.EqualTo("-9 % 4 is -1"));
    }

    [Test]
    public void Identity()
    {
        var a = 1.0;
        var n = 3.0;

        var q = a / n;
        
        Assert.That((a / n) * n, Is.EqualTo(a));
    }

    [Test]
    public void Reverse_engineered()
    {
        double dividend = -9.0;
        double divisor = 4.0;
        
        var quotient = Math.Truncate(dividend / divisor);   // -2
        var remainder = dividend - quotient * divisor;      // -1
        
        Assert.That(quotient, Is.EqualTo(-2));
        Assert.That(remainder, Is.EqualTo(-1));

        var equal = dividend - divisor * quotient == remainder;
        
        Assert.That(equal, Is.True);
    }

    [Test]
    public void Python_modulo()
    {
        double dividend = -9.0;
        double divisor = 4.0;
        
        var quotient = Math.Floor(dividend / divisor);       // -3
        var remainder = dividend - quotient * divisor;       // 3
        
        Assert.That(quotient, Is.EqualTo(-3.0));
        Assert.That(remainder, Is.EqualTo(3.0));

        var equal = dividend - divisor * quotient == remainder;
        
        Assert.That(equal, Is.True);
    }

    [Test]
    public void Floor_vs_Ceiling_vs_trunc()
    {
        var a = -9.0;
        var n = 4.0;

        int q = (int)(a / n);
        
        Assert.That(a - n * q, Is.EqualTo(-1));
    }

    [Test]
    public void Floored_mod_with_neg()
    {
        const double a = -9.0;
        const double n = 4.0;
        
        double q = a / n;

        Assert.That(q, Is.Negative);
        
        var f = Math.Floor(q);
        var r = a - n * f;
        
        Assert.That(r, Is.EqualTo(3));
    }

    [Test]
    public void Reverse_polarity()
    {
        const double a = 9;
        const double n = -4;

        double q = a / n;

        Assert.That(q, Is.Negative);
        
        var f = Math.Floor(q);
        var r = a - n * f;
        
        Assert.That(r, Is.EqualTo(-3));
    }

    [Test]
    public void dow_math_arithmetic()
    {
        const double dividend = -1;
        const double divisor = 7;

        var quotient = Math.Floor(dividend / divisor);  // -1
        var remainder = dividend - quotient * divisor;  // 6
        
        Assert.That(remainder, Is.EqualTo(6));
    }

    [Test]
    public void Clock_arithmetic()
    {
        Assert.That((-1 + 12) % 12, Is.EqualTo(11));
    }

    [Test]
    public void dow_arithmetic()
    {
        const double dividend = -1;
        const double divisor = 7;

        var remainder = dividend % divisor;  // 6
        
        Assert.That(remainder, Is.EqualTo(-1));
    }

    public enum DayName { Sun, Mon, Tue, Wed, Thu, Fri, Sat }

    public readonly record struct DayOfWeek
    {
        private DayOfWeek(int day) => dayNum = (7 + day) % 7;

        public DayName Day => (DayName)dayNum;

        public static DayOfWeek From(DayName day) => new((int)day);
        
        public static DayOfWeek operator +(DayOfWeek day, int val) => new(day.dayNum + val);
        public static DayOfWeek operator -(DayOfWeek day, int val) => new(day.dayNum - val);

        public static implicit operator DayOfWeek(DayName d) => new((int)d);

        private readonly int dayNum;
    }

    [Test]
    public void Enum_arithmetic()
    {
        var mon = DayName.Mon;
        var sat = mon - 2;

        Console.WriteLine($"{sat} is {mon} - 2");
        
        Assert.That(buffer.ToString().Trim(), Is.EqualTo("-1 is Mon - 2"));
    }

    [Test]
    public void Enum_modulus()
    {
        var mon = DayName.Mon;
        var sat = (DayName)((int)(mon - 2) % 7);

        Console.WriteLine($"{sat} is {mon} - 2");
        Assert.That(buffer.ToString().Trim(), Is.EqualTo("-1 is Mon - 2"));
    }

    [Test]
    public void Enum_wrap_modulus()
    {
        var mon = DayName.Mon;
        var sat = (DayName)((int)(7 + mon - 2) % 7);

        Console.WriteLine($"{sat} is {mon} - 2");
        Assert.That(buffer.ToString().Trim(), Is.EqualTo("Sat is Mon - 2"));
    }
    
    [Test]
    public void Day_of_week()
    {
        var sat = DayOfWeek.From(DayName.Mon - 2);
        var sun = sat + 8;
        
        Assert.That(sat.Day, Is.EqualTo(DayName.Sat));
        Assert.That(sun.Day, Is.EqualTo(DayName.Sun));
    }
}
