using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue A (1), B (5), C (3). Dequeue once.
    // Expected Result: B (the highest priority) is returned.
    // Defect(s) Found: None. This passed before the fix because B was inside the range
    // the buggy loop checked.
    public void TestPriorityQueue_HighestInMiddle()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (1), B (2), C (9) so the highest priority is the LAST item. Dequeue once.
    // Expected Result: C is returned.
    // Defect(s) Found: The loop condition was index < Count - 1, so the last item was never
    // examined. B was returned instead of C. Fixed the loop to index < _queue.Count.
    public void TestPriorityQueue_HighestAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (5), B (1), C (3). Dequeue three times.
    // Expected Result: A, C, B (each item is removed after it is returned).
    // Defect(s) Found: Dequeue returned the value but never removed the item from the list,
    // so A was returned again on the second call instead of C. Fixed by calling
    // _queue.RemoveAt(highPriorityIndex) before returning.
    public void TestPriorityQueue_DequeueRemovesItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (5), B (5), C (2), D (5). Dequeue four times.
    // Expected Result: A, B, D, C (ties on the highest priority are served FIFO).
    // Defect(s) Found: The comparison used >=, so a later item with equal priority replaced
    // the earlier one. B was returned instead of A. Fixed by changing >= to >.
    public void TestPriorityQueue_TiesAreFifo()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 5);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue X (1), A (3), B (3). Dequeue once.
    // Expected Result: A (the first of the tied highest-priority items).
    // Defect(s) Found: This passed before any fix only by accident, because the loop skipped
    // the last item (B). After fixing the loop bound alone it would have returned B because
    // of the >= comparison. Fixed by changing >= to >.
    public void TestPriorityQueue_TieAtEndFifo()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("X", 1);
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 3);

        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Call Dequeue on an empty queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. The exception and message already matched the requirements.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    [TestMethod]
    // Scenario: Enqueue A (1), dequeue it, then Dequeue again on the now-empty queue.
    // Expected Result: The second Dequeue throws InvalidOperationException "The queue is empty."
    // Defect(s) Found: Failed before the fix because the item was never removed, so the queue
    // never became empty. Fixed by removing the item in Dequeue.
    public void TestPriorityQueue_EmptyAfterDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());

        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }
}