/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Sentinel
{
    class TestBareUsing : ConsoleRedirected
    {
        public sealed class ResourceOwner : IDisposable
        {
            public ResourceOwner(string leftId, string rightId)
            {
                using (var temp = new ScarceResource(leftId))
                {
                    rightResource = new ScarceResource(rightId);
                    leftResource = temp;
                }
                /*
                using (var temp = new ScarceResource(leftId))
                {
                    rightResource = new ScarceResource(rightId);
                    leftResource = temp;
                    
                    temp = null;
                }
                */
            }

            public void Dispose()
            {
                leftResource.Dispose();
                rightResource.Dispose();
            }
            
            private readonly ScarceResource leftResource;
            private readonly ScarceResource rightResource;
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
        public void Test_withusing()
        {
            {
                using var owner = new ResourceOwner("left id", "right id");

                //... use owner
            }
            TestContext.WriteLine(Output);

            string[] expected = [ "Acquiring resource [left id]", "Acquiring resource [right id]", "Disposing resource", "Disposing resource", "Disposing resource" ];
            Assert.That(Output.ToString().Split(System.Environment.NewLine), Is.EqualTo(expected));

        }
    }

    namespace UsingReassigner
    {
        public sealed class Releasable<T> : IDisposable
            where T : class, IDisposable
        {
            public Releasable(T temp)
            {
                ArgumentNullException.ThrowIfNull(temp, nameof(temp));
                stored = temp;
            }

            public void Dispose() => stored?.Dispose();

            public T Release()
            {
                (stored, var result) = (null, stored);
                return result!;
            }

            private T? stored;
        }

        public sealed class Ref : IDisposable
        {
            public void Dispose()
            {
                if(disposed) throw new ObjectDisposedException("Disposed already");
                disposed = true;
            }
            public bool disposed = false;
        }
        
        public struct Val : IDisposable
        {
            public Val() { }
            public void Dispose()
            {
                val.Dispose();
            }
            public Ref val = new Ref();
        }

        public class TestDisposing
        {
            [Test]
            public void TempRef_disposes_once()
            {
                var item = new Ref();
                using(var temp = new Releasable<Ref>(item))
                {
                }
                Assert.That(item.disposed, Is.True);
            }

            [Test]
            public void Released_temp_not_disposed()
            {
                var item = new Ref();
                Ref? real = null;
                using(var temp = new Releasable<Ref>(item))
                {
                    real = temp.Release();
                }
                Assert.That(item.disposed, Is.False);
                Assert.That(ReferenceEquals(real, item), Is.True);
            }
        }

        public class ScarceResource(string x) : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public class ResourceOwner : IDisposable
        {
            public ResourceOwner(string leftId, string rightId)
            {
                using var temp = new Releasable<ScarceResource>(
                    new ScarceResource(leftId));

                rightResource = new ScarceResource(rightId);
                leftResource = temp.Release();
            }

            /*
            using(var temp = <object to manage conditional disposal>)
            {
                leftFile = File.Open(...);
                temp.Acquire(leftFile);

                rightFile = File.Open(...);

                temp.Release(); 

            } // temp disposes leftFile if not released
            */
            
            public void Dispose()
            {
                leftResource.Dispose();
                rightResource.Dispose();
            }
            
            private readonly ScarceResource leftResource;
            private readonly ScarceResource rightResource;
        }
    }
}
