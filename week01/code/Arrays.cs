public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // my plan:
        // - I need an array that is the same size as "length" since that's how many multiples I need to return
        // - then I'll use a for loop starting at 0 and going until length
        // - the tricky part is figuring out what the multiple actually is at each index
        //   since arrays start at 0 but the first multiple should be number * 1, not number * 0
        //   so I think I need to do number * (i + 1) instead of just number * i
        // - whatever I calculate just gets put into the array at that same index i
        // - once the loop is done I just return the array

        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // my plan:
        // - going with the list slicing method from the instructions instead of modulo, felt easier to think through
        // - basically rotating right just means taking some numbers off the end and putting them at the front
        // - so if amount is 3, I need to grab the last 3 numbers and move them to the beginning
        //   and the rest of the numbers just get pushed after them, keeping their same order
        // - to grab "the last amount numbers" I can use GetRange starting at (data.Count - amount) for "amount" items
        // - to grab "everything else" I use GetRange starting at 0 for (data.Count - amount) items
        // - one edge case: if amount is equal to data.Count, that means a full rotation which should
        //   just give back the same list, so I used amount % data.Count to handle that case
        // - then I clear the original list and add the "last part" back first, followed by the "first part"

        amount = amount % data.Count;

        if (amount == 0)
        {
            return; // no rotation needed
        }

        List<int> tail = data.GetRange(data.Count - amount, amount);
        List<int> head = data.GetRange(0, data.Count - amount);

        data.Clear();
        data.AddRange(tail);
        data.AddRange(head);
    }
}