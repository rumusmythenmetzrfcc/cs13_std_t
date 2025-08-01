/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers.Chicken;

public class Chicken : ConsoleRedirected
{
    public class StaticFields
    {
        public class Speed
        {
            public static readonly double LightSpeed = C;
            
            private static readonly double C = 299_792_458;
        }

        [Test]
        public void Static_member_init()
        {
            Console.WriteLine($"The speed of light is {Speed.LightSpeed}");

            Assert.That(Speed.LightSpeed, Is.Zero);
            var speed = new Speed();
        }
    }
    
    public class AutoProperty
    {
        public class Speed
        {
            public static double LightSpeed { get; } = C;
            
            private static readonly double C = 299_792_458;
        }

        [Test]
        public void Static_member_init()
        {
            Console.WriteLine($"The speed of light is {Speed.LightSpeed}");

            Assert.That(Speed.LightSpeed, Is.Zero);
        }
    }
    
    public class ComputedProperty
    {
        public class Speed
        {
            public static double LightSpeed => C;
            
            private static readonly double C = 299_792_458;
        }

        [Test]
        public void Static_member_init()
        {
            Console.WriteLine($"The speed of light is {Speed.LightSpeed}");

            Assert.That(Speed.LightSpeed, Is.GreaterThan(0));
        }
    }
    
    public class StaticCtr
    {
        public class Speed
        {
            static Speed()
            {
                C = 299_792_458;
                LightSpeed = C;
            }
            
            public static readonly double LightSpeed;
            
            private static readonly double C;
        }

        [Test]
        public void Static_member_init()
        {
            Console.WriteLine($"The speed of light is {Speed.LightSpeed}");

            Assert.That(Speed.LightSpeed, Is.GreaterThan(0));
        }
    }
    
    public class ConstFields
    {
        public class Speed
        {
            public double SpeedOfLight => C;

            public static readonly double LightSpeed = C;
            
            private const double C = 299_792_458;
        }

        [Test]
        public void Static_member_init()
        {
            Console.WriteLine($"The speed of light is {Speed.LightSpeed}");

            Assert.That(Speed.LightSpeed, Is.GreaterThan(0));
        }
    }

    public class ComputedMutateProp
    {
        public static TimeSpan TimeUntilTomorrow
            => DateTime.Today.AddDays(1) - DateTime.Now;

        [Test]
        public void TimeTilTomorrow()
        {
            var timeUntilTomorrow = ComputedMutateProp.TimeUntilTomorrow;
            Assert.That(timeUntilTomorrow, Is.GreaterThan(TimeSpan.MinValue));

            Assert.That(timeUntilTomorrow, Is.GreaterThan(ComputedMutateProp.TimeUntilTomorrow));
        }
    }

    public class AutoMutateProp
    {
        public static TimeSpan TimeUntilTomorrow { get; } 
            = DateTime.Today.AddDays(1) - DateTime.Now;
    }
    [Test]
    public void TimeTilTomorrow()
    {
        var timeUntilTomorrow = AutoMutateProp.TimeUntilTomorrow;
        Assert.That(timeUntilTomorrow, Is.GreaterThan(TimeSpan.MinValue));

        Assert.That(timeUntilTomorrow, Is.EqualTo(AutoMutateProp.TimeUntilTomorrow));
    }
}
