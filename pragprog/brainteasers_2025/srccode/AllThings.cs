/***
 * Excerpted from "C# Brain Teasers",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit https://pragprog.com/titles/csharpbt for more book information.
***/
﻿namespace CsBrainTeasers
{
    [TestFixture]
    public class TestForeach
    {
        StringBuilder output = new();
        StringWriter captured;
        
        [OneTimeSetUp]
        public void RedirectConsole()
        {
            captured = new(output);
            Console.SetOut(captured);
        }
        
        [Test]
        public void ForeachLoop()
        {   
            string[] filesToProcess = [ "Program.cs", "Shared.cs", "Tests.cs" ];
            
            List<Task> tasks = [];
            
            foreach (var file in filesToProcess)
            {
                var task = Task.Factory.StartNew(() => Console.WriteLine(file));
                tasks.Add(task);
            }
            
            Task.WaitAll(tasks.ToArray());
            
            var actual = output.ToString().Trim().ReplaceLineEndings(" ").Split().ToHashSet();

            Assert.That(actual, Is.EquivalentTo(filesToProcess));
        }
        
        [Test]
        public async Task AsyncForeachLoop()
        {   
            string[] filesToProcess = [ "Program.cs", "Shared.cs", "Tests.cs" ];
            
            var tasks = filesToProcess.Select(
                async name => await Console.Out.WriteLineAsync(name));
            
            await Task.WhenAll(tasks);
            
            var actual = output.ToString().Trim().ReplaceLineEndings(" ").Split().ToHashSet();

            Assert.That(actual, Is.EquivalentTo(filesToProcess));
        }
    }
}
