/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers
{

    namespace Truth
    {
        record Channel(bool Active)
        {
            public static bool operator true(Channel ch) => ch.Active;
            public static bool operator false(Channel ch) => ch.Active == false;
            
            public static Channel operator &(Channel l, Channel r) => r;
            public static Channel operator |(Channel l, Channel r) => r;
        }

        record DebugChannel(string Name, bool Active)
        {
            private static bool Report(DebugChannel ch, bool test)
            {
                Console.WriteLine($"{ch.Name}.{ch.Active} is {test}?");
                return ch.Active == test;
            }

            private static DebugChannel Report(DebugChannel l, DebugChannel r, string test)
            {
                Console.WriteLine($"{l.Name}.{l.Active} {test} {r.Name}.{r.Active}");
                return r;
            }
            public static bool operator true(DebugChannel ch) => Report(ch, true);
            public static bool operator false(DebugChannel ch) => Report(ch, false);
            
            public static DebugChannel operator &(DebugChannel l, DebugChannel r) => Report(l, r, "&");
            public static DebugChannel operator |(DebugChannel l, DebugChannel r) => Report(l, r, "|");
        }

        [TestFixture]
        public class Truth
        {
            private StringBuilder buffer = new();
            private StringWriter output;

            [SetUp]
            public void Redirect()
            {
                buffer.Clear();
                output = new(buffer);
                Console.SetOut(output);
            }

            [Test]
            public void TrueOr()
            {
                Uri primaryAddress = new("local://localhost/1");
                Uri secondaryAddress = new("local://localhost/2");
                
                var primary = new Channel(false);
                var backup = new Channel(true);
                
                if (primary || backup)
                {
                    Console.WriteLine("Connection established");
                }
                
                TestContext.WriteLine(buffer.ToString().Trim());
            }

            [Test]
            public void TrueAnd()
            {
                Uri primaryAddress = new("local://localhost/1");
                Uri secondaryAddress = new("local://localhost/2");
                
                var primary = new Channel(false);
                var backup = new Channel(true);
                
                if (backup && primary)
                {
                    Console.WriteLine("Connection established");
                }
                else
                {
                    Console.WriteLine("Connection failed");
                }
                
                TestContext.WriteLine(buffer.ToString().Trim());
            }

            [Test]
            public void DebugTrueOr()
            {
                var primary = new DebugChannel("A", false);
                var backup = new DebugChannel("B", true);
                
                if (primary || backup)
                {
                    Console.WriteLine("Connection established");
                }
                
                TestContext.WriteLine(buffer.ToString().Trim());
            }

            [Test]
            public void DebugTrueAnd()
            {
                var primary = new DebugChannel("A", false);
                var backup = new DebugChannel("B", true);
                
                if (backup && primary)
                {
                    Console.WriteLine("Connection established");
                }
                
                TestContext.WriteLine(buffer.ToString().Trim());
            }
        }
    }
}
