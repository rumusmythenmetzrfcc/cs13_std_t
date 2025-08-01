/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿using NUnit.Framework;

namespace CsBrainTeasers
{
    [TestFixture]
    public class TestInParams
    {
        public class Timer(Func<DateTime> getNow)
        {
            public TimeSpan Activate(in DateTime on)
            {
                return getNow() - on;
            }
        }
        
        [Test]
        public void _()
        {
            var time = new DateTime(2025, 12, 31, 0, 0, 0);

            var timer = new Timer(() => time + TimeSpan.FromHours(1));

            var result = timer.Activate(time);
            Console.WriteLine(result);
        }
    }
}
