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
public class TestFor
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
    public void PlainForLoop()
    {   
        string[] filesToProcess = [ "Program.cs", "Shared.cs", "Tests.cs" ];
            
        List<Action> actions = [];
            
        for (var i = 0; i != filesToProcess.Length; ++i)
        {
            var task = () => Console.WriteLine(filesToProcess[i]);
            actions.Add(task);
        }
            
        Assert.That(() => {
        actions.ForEach(action => action());
        }, Throws.TypeOf<IndexOutOfRangeException>());
    }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Closure
    {
        public int i;
        public string[] filesToProcess;
        
        public void Func()
        {
            Console.WriteLine(filesToProcess[i]);
        }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    [Test]
    public void For_loop_captured_control_var()
    {
        List<Action> actions = [];
        
        var closure = new Closure();
        closure.filesToProcess = new[] { "Program.cs", "Shared.cs", "Tests.cs" };

        for (closure.i = 0; closure.i != closure.filesToProcess.Length; ++closure.i)
        {
            actions.Add(closure.Func);
        }
        
        
        Assert.That(() => {
        actions.ForEach(action => action());
        }, Throws.TypeOf<IndexOutOfRangeException>());
    }
        
    [Test]
    public void ForLoopWithLocal()
    {   
        string[] filesToProcess = [ "Program.cs", "Shared.cs", "Tests.cs" ];
            
        List<Action> actions = [];
            
        for (var i = 0; i != filesToProcess.Length; ++i)
        {
            var local = i;
            
            var task = () => Console.WriteLine(filesToProcess[local]);
            actions.Add(task);
        }
        
        Assert.That(() => {
            actions.ForEach(action => action());
        }, Throws.Nothing);
    }

    [Test]
    public void Declared_actions()
    {   
        string[] filesToProcess = [ "Program.cs", "Shared.cs", "Tests.cs" ];

        var actions = filesToProcess.Select(name => new Action(() => Console.WriteLine(name))).ToList();
            

        Assert.That(() => {
            actions.ForEach(action => action());
        }, Throws.Nothing);
            
        var actual = output.ToString().Trim().ReplaceLineEndings(" ").Split().ToHashSet();

        Assert.That(actual, Is.EquivalentTo(filesToProcess));
    }
}

