/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Exceptional
{
    namespace ExceptionSafety
    {
        public class ScarceResource : IDisposable
        {
            public ScarceResource(string id)
            {
                Console.WriteLine("Acquiring a scarce resource");
                
                if(string.IsNullOrWhiteSpace(id))
                    throw new ArgumentException();
            }
            
            public void Dispose()
            {
                Console.WriteLine("Disposing a scarce resource");
            }
        }
        
        [TestFixture]
        public class TestLeaking
        {
            [Test]
            public void Test_using_exception()
            {
                var resourceId = "";
                Assert.That(() => {
                using(var resource = new ScarceResource(""))
                {
                    // use the resource
                }
                }, Throws.TypeOf<ArgumentException>());
            }

            public void UseResource(string resourceId)
            {
                using var resource = new ScarceResource(resourceId);
                
                // use the resource
            }
            

            public void Equivalent_using()
            {
                var resource = new ScarceResource("");
                using(resource)
                {
                    // use the resource
                }
            }
            
            public void Equivalent_using_finally()
            {
                var resource = new ScarceResource("");
                try
                {
                    // use the resource
                }
                finally
                {
                    resource?.Dispose();
                }
            }
        }
    }
}
