using System.Collections;

public static class Recursion
{
    // Problem 1
    public static int SumSquaresRecursive(int n)
    {
        // if n is 0 or negative there's nothing to add
        if (n <= 0)
            return 0;

        // square n then add the sum of everything smaller
        return n * n + SumSquaresRecursive(n - 1);
    }

    // Problem 2
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // word is long enough so save it and stop
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // try every letter left as the next letter in the word
        for (int i = 0; i < letters.Length; i++)
        {
            // take the letter out so it doesn't get used twice
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + letters[i]);
        }
    }

    // Problem 3
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // make the dictionary the first time through
        remember ??= new Dictionary<int, decimal>();

        // if we already figured this one out just use it
        if (remember.TryGetValue(s, out decimal cached))
            return cached;

        // need to pass remember in here or it never saves anything and gets too slow
        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);

        // save it for next time
        remember[s] = ways;
        return ways;
    }

    // Problem 4
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // find the first * (-1 means there isn't one)
        int index = pattern.IndexOf('*');

        // no * left so it's a normal binary string, add it
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // split the string around the *
        string before = pattern[..index];
        string after = pattern[(index + 1)..];

        // swap the * for a 0 and then for a 1
        WildcardBinary(before + "0" + after, results);
        WildcardBinary(before + "1" + after, results);
    }

    // Problem 5
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // first call so make the list
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }

        // wall, outside the maze, or been here already so stop
        if (!maze.IsValidMove(currPath, x, y))
            return;

        // add this spot to the path
        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            // made it to the end so save the path
            results.Add(currPath.AsString());
        }
        else
        {
            // keep going right, left, down, up
            SolveMaze(results, maze, x + 1, y, currPath);
            SolveMaze(results, maze, x - 1, y, currPath);
            SolveMaze(results, maze, x, y + 1, currPath);
            SolveMaze(results, maze, x, y - 1, currPath);
        }

        // take this spot back off so other paths can use it
        currPath.RemoveAt(currPath.Count - 1);
    }
}