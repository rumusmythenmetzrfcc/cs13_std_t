/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿using System.Net.Mail;

namespace CsBrainTeasers.Which
{
    namespace PrimitiveConstructors
    {
        public class Which : ConsoleRedirected
        {
            [Test]
            public void DateTimeCtr()
            {
                var vacation = new DateOnly(10, 10, 10);
                Console.WriteLine(vacation.ToString("O"));   // ISO 8601 format

                Assert.That(Output, Is.EqualTo("0010-10-10"));
                Assert.That(vacation, Is.EqualTo(DateOnly.ParseExact("10-10-0010", "dd-MM-yyyy")));
            }

            [Test]
            public void MailConstructor()
            {
                Assert.That(() =>
                {
                    var msg = new MailMessage("", "", "", "");
                }, Throws.TypeOf<ArgumentException>());
            }
        }

        public class SimpleTypes : ConsoleRedirected
        {
            public readonly record struct Year(int Value);

            public enum Month
            {
                Jan = 1, Feb, Mar, Apr, May, Jun, 
                Jul, Aug, Sep, Oct, Nov, Dec
            };

            public static class DateBuilder
            {
                public static DateOnly ToDateOnly(Year y, Month m, int d)
                    => new(y.Value, (int)m, d);

                public static DateOnly ToDateOnly(Month m, int d, Year y)
                    => ToDateOnly(y, m, d);

                public static DateOnly ToDateOnly(int d, Month m, Year y)
                    => ToDateOnly(y, m, d);
            }

            [Test]
            public void YMD()
            {
                /*
                var vacation = DateBuilder.ToDateOnly(new Year(-2010), Month.Nov, 12);
                
                Error CS1503 : Argument 1: cannot convert from 'uint' to 'int'
                */
                
                var vacation = DateBuilder.ToDateOnly(new Year(2010), Month.Oct, 10);

                Console.WriteLine(vacation.ToString("O"));
                
                Assert.That(Output, Is.EqualTo("2010-10-10"));
                
                Assert.That(() =>
                {
                    new DateOnly(0, 11, 11);
                }, Throws.TypeOf<ArgumentOutOfRangeException>());
            }

            [Test]
            public void DMY()
            {
                var vacation = DateBuilder.ToDateOnly(10, Month.Oct, new Year(2010));
                Console.WriteLine(vacation.ToLongDateString());
            }
        }

        class Validation
        {
            public readonly record struct Year
            {
                public int Value { get; }

                public Year(int value)
                {
                    ArgumentOutOfRangeException.ThrowIfZero(value, nameof(value));
                    ArgumentOutOfRangeException.ThrowIfGreaterThan
                        (value, 9999, nameof(value));
                 
                    Value = value;
                }
            }

            [Test]
            public void Validate_zero_year()
            {
                var msg = $"value ('0') must be a non-zero value. (Parameter 'value'){Environment.NewLine}Actual value was 0.";

                Assert.That(() =>
                    {
                        var zero = new Year(0);
                    },
                    Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                        .EqualTo(msg.Trim()));
            }
            
            [Test]
            public void Validate_year_too_large()
            {
                var msg = $"value ('10000') must be less than or equal to '9999'. (Parameter 'value'){Environment.NewLine}Actual value was 10000.";

                Assert.That(() =>
                    {
                        var tooLarge = new Year(10000);
                    },
                    Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                        .EqualTo(msg));
            }
        }
    }
}
