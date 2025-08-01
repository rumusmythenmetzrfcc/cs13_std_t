/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
namespace CsBrainTeasers.NewTricks
{
    namespace ClassInitializers
    {

        public readonly struct EmailAddress
        {
            public EmailAddress() { }
            public string Name { get; init; } = "";
            public string Address { get; init; } = "";

            public override string ToString()
                => $"Name: {Name}, Address: {Address.ToLower()}";
        }

        public class Test_initialized_properties
        {
            private StringBuilder output = new();
            private StringWriter captured;

            [SetUp]
            public void RedirectConsole()
            {
                captured = new(output);
                Console.SetOut(captured);
            }

            [Test]
            public void Properties_take_init_values()
            {
                var address = new EmailAddress();
                
                Console.Write(address.ToString());

                Assert.That(address.Name, Is.Not.Null);
                Assert.That(output.ToString(), Is.EqualTo("Name: , Address: "));
            }
        }

        /*
        error CS8983: A 'struct' with field initializers must include an explicitly declared constructor.
        */
    }

    namespace StructInitializer
    {
        public readonly struct EmailAddress
        {
            public EmailAddress() { }

            public string Name { get; init; } = "";
            public string Address { get; init; } = "";

            public override string ToString()
                => $"Name: {Name}, Address: {Address.ToLower()}";
        }

        public class Test_initialized_properties
        {
            private StringBuilder output = new();
            private StringWriter captured;

            [SetUp]
            public void RedirectConsole()
            {
                captured = new(output);
                Console.SetOut(captured);
            }

            [Test]
            public void New_instance_uses_init_values()
            {
                var address = new EmailAddress();
                
                Console.Write(address.ToString());

                Assert.That(address.Name, Is.Not.Null);
                Assert.That(output.ToString(), Is.EqualTo("Name: , Address: "));
            }

            [Test]
            public void Default_instance_fields_are_null()
            {
                EmailAddress address = default;


                Assert.That(address.Name, Is.Null);
                
                Assert.That(() => {
                Console.Write(address.ToString());
                }, Throws.TypeOf<NullReferenceException>());

                Assert.That(address.Name, Is.Null);
            }
        }
    }

    public class DefenseAgainstNull
    {
        public class EmailAddress
        {
            public string Name { get; init; } = "";
            public string Address { get; init; } = "";

            public override string ToString()
                => $"Name: {Name}, Address: {Address?.ToLower()}";

            public static string Concatenate(string left, string right)
            {
                ArgumentNullException.ThrowIfNull(left);
                ArgumentNullException.ThrowIfNull(right);

                // ...
                return "";
            }
        }

        public class NullChecks
        {
            [Test]
            public void Check_null_arguments()
            {
                Assert.That(() => EmailAddress.Concatenate(null, ""),
                    Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => EmailAddress.Concatenate("", null),
                    Throws.TypeOf<ArgumentNullException>());
            }
        }
    }
}
