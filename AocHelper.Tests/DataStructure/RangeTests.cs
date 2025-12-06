using Range = AocHelper.DataStructures.Range;

namespace AocHelper.Tests.DataStructure;

public class RangeTests
{
    [Test]
    public void TestContains()
    {
        Range r1 = new(10, 5);
        Assert.Multiple(() =>
        {
            Assert.That(r1.Contains(9), Is.False);
            Assert.That(r1.Contains(10), Is.True);
            Assert.That(r1.Contains(14), Is.True);
            Assert.That(r1.Contains(15), Is.False);
        });
        
        Range r2 = Range.FromStartEnd(21, 74);
        Assert.Multiple(() =>
        {
            Assert.That(r2.Contains(20), Is.False);
            Assert.That(r2.Contains(21), Is.True);
            Assert.That(r2.Contains(74), Is.True);
            Assert.That(r2.Contains(75), Is.False);
        });
    }

    [Test]
    public void TestEqualsHash()
    {
        var r1 = new Range(10, 5);
        var r2 = new Range(10, 5);
        var r3 = new Range(10, 6);
        var r4 = new Range(11, 5);
        
        Assert.Multiple(() =>
        {
            Assert.That(r1, Is.EqualTo(r1));
            Assert.That(r1.GetHashCode(), Is.EqualTo(r1.GetHashCode()));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(r1, Is.EqualTo(r2));
            Assert.That(r1.GetHashCode(), Is.EqualTo(r2.GetHashCode()));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(r1, Is.Not.EqualTo(r3));
            Assert.That(r1.GetHashCode(), Is.Not.EqualTo(r3.GetHashCode()));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(r3, Is.Not.EqualTo(r4));
            Assert.That(r3.GetHashCode(), Is.Not.EqualTo(r4.GetHashCode()));
        });
    }

    [Test]
    public void TestOverlap()
    {
        var r1 = new Range(10, 5);
        var r2 = new Range(9, 7);
        var r3 = new Range(14, 12);
        var r4 = new Range(15, 12);
        var r5 = new Range(10, 7);
        var r6 = new Range(8, 2);
        
        Assert.Multiple(() =>
        {
            Assert.That(r1.OverlapsWith(r1));
            Assert.That(r1.OverlapsWith(r2));
            Assert.That(r2.OverlapsWith(r1));
            Assert.That(r1.OverlapsWith(r3));
            Assert.That(r3.OverlapsWith(r1));
            Assert.That(r4.OverlapsWith(r1), Is.False);
            Assert.That(r1.OverlapsWith(r4), Is.False);
            Assert.That(r1.OverlapsWith(r5));
            Assert.That(r5.OverlapsWith(r1));
            Assert.That(r1.OverlapsWith(r6), Is.False);
            Assert.That(r6.OverlapsWith(r1), Is.False);
        });
    }

    [Test]
    public void TestMerge()
    {
        var r1 = new Range(10, 5);
        var r2 = new Range(9, 7);
        var r3 = new Range(14, 12);
        var r4 = new Range(15, 12);
        var r5 = new Range(10, 7);
        var r6 = new Range(8, 2);
        
        Assert.Multiple(() =>
        {
            Assert.That(r1.MergedWith(r1), Is.EqualTo(r1));
            Assert.That(r1.MergedWith(r2), Is.EqualTo(r2));
            Assert.That(r1.MergedWith(r3), Is.EqualTo(Range.FromStartEnd(10, 25)));
            Assert.That(r4.MergedWith(r3), Is.EqualTo(Range.FromStartEnd(14, 26)));
            Assert.That(() => r5.MergedWith(r6), Throws.ArgumentException);
        });
    }
}