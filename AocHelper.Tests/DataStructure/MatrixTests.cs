using AocHelper.DataStructures;

namespace AocHelper.Tests.DataStructure;

[TestClass]
public class MatrixTests
{
    [TestMethod]
    public void TestMatMulIdentity()
    {
        var m1 = new Matrix(new[,]
        {
            { 1.0, 0.0 }, 
            { 0.0, 1.0 },
        });
        Matrix res = m1.MatMul(m1);
        Assert.AreEqual(m1, res);
        
        m1 = new Matrix(new[,]
        {
            { 1.0, 0.0, 0.0 }, 
            { 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 1.0 },
        });
        var m2 = new Matrix(new[,]
        {
            { 1.0, 5.0, 2.5 }, 
            { 0.5, 1.0, 5.0 },
            { 3.0, 0.0, 0.0 },
        });
        res = m1.MatMul(m2);
        Assert.AreEqual(m2, res);
    }
    
    [TestMethod]
    public void TestMatMul()
    {
        var m1 = new Matrix(new[,]
        {
            { 1.0, 2.0 }, 
            { 3.0, 0.0 },
        });
        var m2 = new Matrix(new[,]
        {
            { 0.0, 1.0 }, 
            { 2.0, 4.0 },
        });
        var expected = new Matrix(new[,]
        {
            { 4.0, 9.0 }, 
            { 0.0, 3.0 },
        });
        
        Matrix res = m1.MatMul(m2);
        Assert.AreEqual(expected, res);
    }

    [TestMethod]
    public void TestDeterminant()
    {
        var m1 = new Matrix(new[,]
        {
            { 4.0, 1.0 }, 
            { 3.0, -2.0 },
        });
        Assert.AreEqual(-11, m1.Determinant(), 1e-5);
        
        m1 = new Matrix(new[,]
        {
            { -2.0, -7.0 }, 
            { 1.0, 4.0 },
        });
        Assert.AreEqual(-11, m1.Determinant(), 1e-5);
    }
}