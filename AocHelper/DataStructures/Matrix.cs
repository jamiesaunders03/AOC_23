using System.Collections;

namespace AocHelper.DataStructures;

public class Matrix : IReadOnlyCollection<double>
{
    private readonly double[,] _mat;

    /// <summary>
    /// The dimensions of the matrix
    /// </summary>
    public Tuple<int, int> Shape => new(_mat.GetLength(0), _mat.GetLength(1));

    /// <summary>
    /// The total number of elements in the matrix
    /// </summary>
    public int Count => _mat.Length;
    
    #region Constructors

    /// <summary>
    /// Create a new Matrix based on the 2d array passed in
    /// </summary>
    /// <param name="mat">The data to base the matrix off</param>
    public Matrix(double[,] mat)
    {
        _mat = mat.Clone() as double[,] ?? throw new ArgumentException("mat was null", nameof(mat));
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    public Matrix(Matrix m) : this(m._mat) { }

    /// <summary>
    /// Create a new array of the given size specified
    /// </summary>
    public Matrix(int dim1, int dim2)
    {
        _mat = new double[dim1, dim2];
    }
    
    #endregion End Constructors

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is Matrix m)
            return this == m;
        
        return false;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<double> GetEnumerator()
    {
        return _mat.Cast<double>().GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _mat.GetEnumerator();
    }

    #region Operations

    public static bool operator ==(Matrix m1, Matrix m2)
    {
        if (m1.Shape.Item1 != m2.Shape.Item1 || m1.Shape.Item2 != m2.Shape.Item2)
            return false;
        
        for (int i = 0; i < m1.Shape.Item1; ++i)
            for (int j = 0; j < m1.Shape.Item2; ++j)
                if (m1[i, j] != m2[i, j])
                    return false;

        return true;
    }
    
    public static bool operator !=(Matrix m1, Matrix m2)
    {
        return !(m1 == m2);
    }

    public double this[int k1, int k2]
    {
        get => _mat[k1, k2];
        set => _mat[k1, k2] = value;
    }

    #endregion End Operations

    /// <summary>
    /// Calculates the determinant of the matrix
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public double Determinant()
    {
        if (Shape.Item1 != Shape.Item2)
        {
            throw new InvalidOperationException("Cannot calculate determinant of non-square matrix");
        }

        var m = new Matrix(this);
        int size = m.Shape.Item1;
        double sign = 1;

        m[0, 0] = 1;
        for (int k = 0; k < size - 1; ++k)
        {
            if(m[k, k] == 0) {
                int t = 0;
                for(t = k + 1; t < size; ++t) {
                    if(m[t, k] != 0) {  
                        Utilities.Utilities.SwapRow(m._mat, t, k);
                        sign = -sign;
                        break;
                    }
                }

                //No entries != 0 found in column k -> det = 0
                if(t == size) {
                    return 0;
                }
            }
            
            for (int i = k + 1; i < size; ++i)
            {
                for (int j = k + 1; j < size; ++j)
                {
                    double div = k == 0 ? 1 : m[k - 1, k - 1];
                    m[i, j] = (m[i, j] * m[k, k] - m[i, k] * m[k, j]) / div;
                }
            }
        }

        return sign * m[size - 1, size - 1];
    }

    /// <summary>
    /// Multiplies the 2 matrices and returns the new matrix.
    /// Requires the inner dimension of the matrices to match.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Matrix MatMul(Matrix other)
    {
        if (Shape.Item2 != other.Shape.Item1)
        {
            throw new InvalidOperationException("Cannot multiply matrices, non-matching inner dimensions");
        }
        
        double[,] newMat = new double[Shape.Item1, other.Shape.Item2];

        for (int n = 0; n < Shape.Item1; ++n)
        {
            for (int p = 0; p < other.Shape.Item2; ++p)
            {
                double space = 0;
                for (int m = 0; m < Shape.Item2; m++)
                {
                    space += this[n, m] * other[m, p];
                }
                newMat[n, p] = space;
            }
        }

        return new Matrix(newMat);
    }
    
    // taken from https://stackoverflow.com/questions/5051528/how-to-calculate-matrix-determinant-nn-or-just-55
    private double[,] MatrixDecompose(out int[] perm, out int toggle)
    {
        int rows = Shape.Item1;
        int cols = Shape.Item2;

        var result = new Matrix(this); 

        perm = new int[rows]; // set up row permutation result
        for (int i = 0; i < rows; ++i) { perm[i] = i; } // i are rows counter

        toggle = 1; // toggle tracks row swaps. +1 -> even, -1 -> odd. used by MatrixDeterminant

        for (int j = 0; j < rows - 1; ++j) // each column, j is counter for coulmns
        {
            double colMax = Math.Abs(result[j, j]); // find largest value in col j
            int pRow = j;
            for (int i = j + 1; i < rows; ++i)
            {
                if (result[i, j] > colMax)
                {
                    colMax = result[i, j];
                    pRow = i;
                }
            }

            if (pRow != j) // if largest value not on pivot, swap rows
            {
                double[] rowPtr = new double[cols];
                for (int k = 0; k < cols; k++)
                {
                    rowPtr[k] = result[pRow, k];
                }

                for (int k = 0; k < cols; k++)
                {
                    result[pRow, k] = result[j, k];
                }

                for (int k = 0; k < cols; k++)
                {
                    result[j, k] = rowPtr[k];
                }

                (perm[pRow], perm[j]) = (perm[j], perm[pRow]);
                toggle = -toggle;
            }

            if (Math.Abs(result[j, j]) < 1.0E-20) // if diagonal after swap is zero ...
                throw new Exception("Could not decompose matrix");

            for (int i = j + 1; i < rows; ++i)
            {
                result[i, j] /= result[j, j];
                for (int k = j + 1; k < rows; ++k)
                {
                    result[i, k] -= result[i, j] * result[j, k];
                }
            }
        } 

        return result._mat;
    } 
}