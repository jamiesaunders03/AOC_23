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

    /// <summary>
    /// Returns a new instance of the identity matrix of the given size
    /// </summary>
    /// <param name="size">The n x n size of the identity matrix to create</param>
    /// <returns></returns>
    public static Matrix Identity(int size)
    {
        var m = new Matrix(size, size);
        for (int i = 0; i < size; ++i)
        {
            m[i, i] = 1;
        }

        return m;
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
        private set => _mat[k1, k2] = value;
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

        double[,] mat = (double[,])_mat.Clone();
        int n = mat.GetLength(0);
        int sign = 1;
        for(int i = 0; i < n - 1; i++) {
            //Pivot - row swap needed
            if(this[i,i] == 0) {
                int m = 0;
                for(m = i + 1; m < n; m++) {
                    if(this[m,i] != 0) {
                        Utilities.Utilities.SwapRow(mat, m, i);
                        sign = -sign;
                        break;
                    }
                }

                //No entries != 0 found in column k -> det = 0
                if(m == n) {
                    return 0;
                }
            }
            for(int j = i + 1; j < n; j++) {
                double ratio = mat[j,i] / mat[i,i];
                for(int k = 0; k < n; k++) {
                    mat[j,k] -= ratio * mat[i,k];
                }
            }
        }

        double solution = 1;
        for(int i = 0; i < n; i++) {
            solution *= mat[i,i];
        }

        return sign * solution;
    }

    /// <summary>
    /// Returns the inverse of the current matrix, this can only happen when the current matrix has a
    /// non-zero determinant.
    /// If the current matrix's determinant is zero, a <exception cref="InvalidOperationException" /> is thrown.
    /// </summary>
    /// <returns></returns>
    public Matrix Inverse()
    {
        double det = Determinant();
        if (Math.Abs(det) < 1e-5)
            throw new InvalidOperationException("Cannot invert a matrix with a determinant of 0");

        int n = Shape.Item1;
        double[,] augmented = new double[n, n * 2];

        // Initialize augmented matrix with the input matrix and the identity matrix
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                augmented[i, j] = this[i, j];
                augmented[i, j + n] = (i == j) ? 1 : 0;
            }
        }

        // Apply Gaussian elimination
        for (int i = 0; i < n; i++)
        {
            int pivotRow = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Math.Abs(augmented[j, i]) > Math.Abs(augmented[pivotRow, i]))
                {
                    pivotRow = j;
                }
            }

            if (pivotRow != i)
            {
                for (int k = 0; k < n * 2; k++)
                {
                    (augmented[i, k], augmented[pivotRow, k]) = (augmented[pivotRow, k], augmented[i, k]);
                }
            }

            if (Math.Abs(augmented[i, i]) < 1e-10)
            {
                return null;
            }

            double pivot = augmented[i, i];
            for (int j = 0; j < 2 * n; j++)
            {
                augmented[i, j] /= pivot;
            }

            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    double factor = augmented[j, i];
                    for (int k = 0; k < 2 * n; k++)
                    {
                        augmented[j, k] -= factor * augmented[i, k];
                    }
                }
            }
        }

        double[,] result = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = augmented[i, j + n];
            }
        }

        return new Matrix(result);
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
}