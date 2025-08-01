/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers;

[TestFixture]
public class InitOrder
{
    public abstract class Base
    {
        public Base()
        {
            Console.WriteLine("BaseCtr");
            Name = Init();
        }

        public virtual string Init() => id;
        public string Name { get; } = "blank";
        
        private readonly string id = "Base";
    }

    public class Child : Base
    {
        public override string Init() => $"[{Name}]:{id}";

        private readonly string id = "Child";
    }

    [Test]
    public void _()
    {
        var c = new Child();
        Console.WriteLine(c.Name);
    }
}
