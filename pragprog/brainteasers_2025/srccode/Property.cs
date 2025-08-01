/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Property
{
    [TestFixture]
    public class TestProperties
    {
        StringBuilder output = new();
        StringWriter captured;
        
        [OneTimeSetUp]
        public void RedirectConsole()
        {
            captured = new(output);
            Console.SetOut(captured);
        }

        public record struct Product(string Name, decimal Price)
        {
            public void SetDiscount(decimal value)
                => Price *= 1 - value;
        }

        public class Purchase
        {
            public Product Item { get; init; }
            public int Count { get; init; }
        }
        
        [Test]
        public void Method_mutates_copy()
        {   
            var buy = new Purchase
            {
                Item = new Product("Ballpoint pen", 1.0m),
                Count = 100
            };

            buy.Item.SetDiscount(0.2m);

            Console.WriteLine($"Total: ${buy.Count * buy.Item.Price:0.00}");


            Assert.That(output.ToString().Trim(), Is.EqualTo("Total: $100.00"));

            //error CS1612: Cannot modify the return value of 'Purchase.Item' because it is not a variable 
            /*
            START:prop_equivalent
            private Product _backing_Item;

            public Product get_Item()
            {
                return _backing_Item;
            }
            END:prop_equivalent
            */
        }
    }
}
