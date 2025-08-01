/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CsBrainTeasers.Unforgettable
{
    public class RequiredProps
    {
        [method:SetsRequiredMembers]
        public struct Address(string street, string city, string postcode)
        {
            public required string Street { get; init; } = street;
            public required string Town { get; init; }
            public required string City { get; init; } = city;
            public required string PostCode { get; init; } = postcode;
        }
        
        [Test]
        public void _()
        {
            var own = new Address("12 The Rise", "London", "E14 1ZZ");
        }
    }
}
