/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Default.Positional
{
    public readonly record struct EmailAddress(string Name, string Address);
    
    [TestFixture]
    public class TestPositionalParams
    {
        private StringBuilder output = new();
        private StringWriter captured;

        [SetUp]
        public void RedirectOutput()
        {
            captured = new(output);
            Console.SetOut(captured);
        }
        
        [Test]
        public void PropertiesDefaultized()
        {
            var email = new EmailAddress("Ash", "ash@nostro.mo");
            
            Console.Write(email.Address.ToLower());
            
            Assert.That(output.ToString(), Is.EqualTo("ash@nostro.mo"));
        }
    }
}

namespace CsBrainTeasers.Default.Record
{
#pragma warning disable CS8907 // Parameter is unread. Did you forget to use it to initialize the property with that name?
    public sealed record EmailAddress(string Name, string Address)
    {
        public string Name { get; init; } = "";
        public string Address { get; init; } = "";
    }
#pragma warning restore CS8907 // Parameter is unread. Did you forget to use it to initialize the property with that name?


    public class TestRecordPositionalParams
    {
        [Test]
        public void PropertiesInit()
        {
            var email = new EmailAddress("Ash", "ash@nostro.mo");
            Assert.That(email.Address, Is.Empty);
        }
    }
}

namespace CsBrainTeasers.Default.Class
{
    public sealed class EmailAddress(string name, string address)
    {
        public string Name { get; init; } = name;
        public string Address { get; init; } = address;
    }


    public class TestRecordPositionalParams
    {
        [Test]
        public void PropertiesInit()
        {
            var email = new EmailAddress("Ash", "ash@nostro.mo");
            Assert.That(email.Address, Is.EqualTo("ash@nostro.mo"));
        }
    }
}

namespace CsBrainTeasers.Default.AutoProperties
{
    //   [CS8907] Parameter 'Name' is unread. Did you forget to use it to initialize the property with that name?
#pragma warning disable CS8907 // Parameter is unread. Did you forget to use it to initialize the property with that name?
    public readonly record struct EmailAddress(string Name, string Address)
    {
        public string Name { get; init; } = "";
        public string Address { get; init; } = "";
    }
    // Warning CS8907 : Parameter 'Name' is unread. Did you forget to use it to initialize the property with that name?

#pragma warning restore CS8907 // Parameter is unread. Did you forget to use it to initialize the property with that name?
    
    public class TestPositionalParams
    {
        private StringBuilder output = new();
        private StringWriter captured;

        [SetUp]
        public void RedirectOutput()
        {
            captured = new(output);
            Console.SetOut(captured);
        }
        
        [Test]
        public void PropertiesDefaultized()
        {
            var email = new EmailAddress(Name: "Ash",  Address: "Ash@Nostro.mo");
            
            Console.Write($"Email for '{email.Name}' is '{email.Address.ToLower()}'");
            
            /*
            Email for '' is ''
                */
            Assert.That(output.ToString(), Is.EqualTo("Email for '' is ''"));
        }

        [Test]
        public void DefaultInit()
        {
            var email = new EmailAddress();
            Assert.That(email.Address, Is.Null);
        }
    }

    namespace CsBrainTeasers.Property.PositionalProps
    {
        public readonly record struct EmailAddress(string Name, string Address)
        {
            public string Name { get; init; } = Name;
            public string Address { get; init; } = Address;
        }

        public class TestPositionalParams
        {
            [Test]
            public void PropertiesDefaultized()
            {
                var email = new EmailAddress("Ash", "ash@nostro.mo");

                Assert.That(email.Name, Is.EqualTo("Ash"));
                Assert.That(email.Address, Is.EqualTo("ash@nostro.mo"));
            }

            [Test]
            public void DefaultInit()
            {
                var email = new EmailAddress();
                Assert.That(email.Address, Is.Null);
            }

            [Test]
            public void ObjInit()
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                var email = new EmailAddress { Name = null, Address = null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                Assert.That(email.Address, Is.Null);
            }
        }
    }

    namespace CsBrainTeasers.Property.DefaultCtr
    {
        public readonly record struct EmailAddress(string Name, string Address)
        {
            public EmailAddress() : this("", "")
            {
            }

            public string Name { get; init; } = Name;
            public string Address { get; init; } = Address;
        }

        public class TestPositionalParams
        {
            [Test]
            public void PropertiesDefaultized()
            {
                var email = new EmailAddress("Ash", "ash@nostro.mo");
                
                Assert.That(email.Name, Is.EqualTo("Ash"));
                Assert.That(email.Address, Is.EqualTo("ash@nostro.mo"));
            }

            [Test]
            public void DefaultInit()
            {
                var email = new EmailAddress();
                
                Console.Write(email.Address.ToLower());

                Assert.That(email.Address, Is.Not.Null);
            }
        }
    }

    namespace CsBrainTeasers.Property.PositionalRecord
    {
        public readonly record struct EmailAddress(string Name, string Address);

        public class TestPositionalParams
        {
            [Test]
            public void PropertiesDefaultized()
            {
                var email = new EmailAddress("Ash", "ash@nostro.mo");
                
                Assert.That(email.Name, Is.EqualTo("Ash"));
                Assert.That(email.Address, Is.EqualTo("ash@nostro.mo"));
            }

            [Test]
            public void DefaultInit()
            {
                var email = new EmailAddress();
                Assert.That(email.Address, Is.Null);
            }
        }
    }
}
