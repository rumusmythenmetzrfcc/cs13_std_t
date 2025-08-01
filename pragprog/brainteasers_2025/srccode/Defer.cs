/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Defer
{
    [TestFixture]
    public class Defer : ConsoleRedirected
    {
        [Test]
        public void InvalidInput()
        {
            string[] inputs = [ "128", "256", "<error>", "512" ];
            
            IEnumerable<int> result;
            try
            {
                result = from number in inputs 
                         select Convert.ToInt32(number);
            }
            catch (Exception e)
            {
                result = [ 0 ];
            }
            try
            {
                result = inputs.Select(number => Convert.ToInt32(number));
            }
            catch (Exception e)
            {
                result = [ 0 ];
            }

            var expected = $"The input string '{inputs[2]}' was not in a correct format.";
                
            /*
            <System.FormatException>: The input string '<error>' was not 
            in a correct format.
            */
            
            Assert.That(() =>
            {
            var count = result.Count();
            
            Console.WriteLine($"There are {count} elements");
            TestContext.WriteLine(Output);
            }, Throws.TypeOf<System.FormatException>().With.Message.EqualTo(expected));
            
            /*
            
             */
        }

        public readonly record struct C(int Value) : IComparable<C>
        {
            public int CompareTo(C other)
            {
                Console.WriteLine($"{this} - {other}");
                return Value.CompareTo(other.Value);
            }
        }

        [Test]
        public void CountCalls()
        {
            /*
            IEnumerable<T> Where<T>(this IEnumerable<T> input, Func<T, bool> predicate)
            {
                foreach (var element in input)
                {
                    if(predicate(element))
                        yield return element;
                }
            }
            */
            
            C [] inputs = { new C(1), new C(2), new C(3) };
            var res = inputs.Where(i => i.CompareTo(new(2))==0).Any();
            
            TestContext.WriteLine(Output);
        }
        
        [Test]
        public static void WhereSequence()
        {
            int [] numbers = [ 20, 5, 2, 1 ];
            var result = numbers.Where(n => n == 20);
             
            if (result.Any())
            {
                // do something important with small numbers
            }
        }
    }
}
