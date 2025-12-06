using AocHelper.DataStructures;

namespace AocHelper.Tests.DataStructure;

public class MatrixTests
{
    [Test]
    public void TestMatMulIdentity()
    {
        var m1 = new Matrix(new[,]
        {
            { 1.0, 0.0 }, 
            { 0.0, 1.0 },
        });
        Matrix res = m1.MatMul(m1);
        Assert.That(res, Is.EqualTo(m1));
        
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
        Assert.That(res, Is.EqualTo(m2));
    }
    
    [Test]
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
        Assert.That(res, Is.EqualTo(expected));
    }

    [Test]
    public void TestDeterminant()
    {
        var m1 = new Matrix(new[,]
        {
            { 4.0, 1.0 }, 
            { 3.0, -2.0 },
        });
        Assert.That(m1.Determinant(), Is.EqualTo(-11).Within(1e-5));
        
        m1 = new Matrix(new[,]
        {
            { -2.0, -7.0 }, 
            { 1.0, 4.0 },
        });
        Assert.That(m1.Determinant(), Is.EqualTo(-1).Within(1e-5));
        
        m1 = new Matrix(new double[,]
        {
            { 3, 1, 0 }, 
            { 5, 7, 5 },
            { 2, 5, 0 },
        });
        Assert.That(m1.Determinant(), Is.EqualTo(-65).Within(1e-5));
    }

    [Test]
    public void TestInvert()
    {
        var m1 = new Matrix(new[,]
        {
            { 2.0, 4.0 }, 
            { -1.0, 3.0 },
        });
        var exp = new Matrix(new[,]
        {
            { 0.3, -0.4 }, 
            { 0.1, 0.2 },
        });
        Matrix inverse = m1.Inverse();
        Assert.That(inverse, Is.EqualTo(exp).Within(1e-5));


        Matrix identity = m1.MatMul(inverse);
        Assert.That(identity, Is.EqualTo(Matrix.Identity(2)).Within(1e-5));
    }
}