/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Hanging
{
    class DanglingTests
    {
        public sealed class ResourceOwner(string leftId, string rightId) : IDisposable
        {
            public void Dispose()
            {
                leftResource.Dispose();
                rightResource.Dispose();
            }
            
            private readonly ScarceResource leftResource = new(leftId);
            private readonly ScarceResource rightResource = new(rightId);
        }

        public class ScarceResource : IDisposable
        {
            public ScarceResource(string id)
            {
                Console.WriteLine($"Acquiring resource [{id}]");
                ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));
            }
            
            public void Dispose()
            {
                Console.WriteLine("Disposing resource");
            }
        }
        
        [Test]
        public void TestDangling()
        {
            Assert.That(() =>
            { 
            using var owner = new ResourceOwner("valid", "");

            // ... use owner
            }, Throws.TypeOf<ArgumentException>());
            
            /*
            Acquiring resource [valid]
            Acquiring resource []
            Unhandled exception. System.ArgumentException: The value cannot be an empty
                string or composed entirely of whitespace. (Parameter 'id')
             */
        }
    }

    class TestNestExceptions
    {
        public class ScarceResource : IDisposable
        {
            public ScarceResource(string id)
            {
                Console.WriteLine($"Acquiring resource [{id}]");
                ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));
            }
            
            public void Dispose()
            {
                Console.WriteLine($"Disposing resource");
            }
        }
        
        public sealed class ResourceOwner : IDisposable
        {
            public ResourceOwner(string leftId, string rightId)
            {
                leftResource = new ScarceResource(leftId);
                try
                {
                    rightResource = new ScarceResource(rightId);
                }
                catch // any exception
                {
                    leftResource.Dispose();
                    throw;
                }
            }

            public void Dispose()
            {
                leftResource.Dispose();
                rightResource.Dispose();
            }
            
            private readonly ScarceResource leftResource;
            private readonly ScarceResource rightResource;
        }
        
        [Test]
        public void TestNestedTry()
        {
            Assert.That(() =>
            { 
                using var owner = new ResourceOwner("valid", "");
            }, Throws.TypeOf<ArgumentException>());
            
            /*
            Acquiring resource [valid]
            Acquiring resource []
            Disposing resource
            Unhandled exception. System.ArgumentException: The value cannot be an empty 
                string or composed entirely of whitespace. (Parameter 'id')
             */
        }
    }
}
