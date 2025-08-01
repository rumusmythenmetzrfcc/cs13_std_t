using System.Diagnostics;

async Task<string[]> SplitWordsAsync(string phrase)
{
#nullable enable
    // Use of null defaults to splitting on any whitespace character
    char[]? delimiters = null;

    var words = await Task.Factory.StartNew(() =>
        phrase.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
    
    return words;
}

int CountWords()
{
    var task = SplitWordsAsync("One Two\nThree");
    return task.Result.Length;
}

Print(CountWords());