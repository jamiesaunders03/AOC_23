using AocHelper;

namespace AOC_24.Challenges;

internal class Day09 : IAocChallenge
{
    public int Day => 9;

    private class FileRecord
    {
        public required int Id { get; init; }
        public required int Len { get; set; }
    }

    private readonly string _files;

    public Day09()
    {
        _files = new FetchData(Day, 2024).ReadInput().TrimEnd();
    }

    public string Challenge1()
    {
        List<int> files = GetFileSystem(_files);
        Fragment(files);

        long total = 0;
        for (int i = 0; i < files.Count; ++i)
        {
            total += i * (files[i] - 1);
        }
        
        return total.ToString();
    }

    public string Challenge2()
    {
        List<int> files = DeFragment(_files);
        
        long total = 0;
        for (int i = 0; i < files.Count; ++i)
        {
            int fileId = files[i];
            if (fileId != 0)  // 0 is null value (not a file)
                total += i * (fileId - 1);
        }
        
        return total.ToString();  // 6402809994419 too low, 9894694697800 too high
    }

    /// <summary>
    /// Return a list representing the file systems files with gaps
    /// 0 represents a gap, any other number represents the group the file is part of
    /// </summary>
    /// <param name="files">The compressed file representation</param>
    private static List<int> GetFileSystem(string files)
    {
        List<int> fragmented = [];
        int group = 1;
        bool isFile = true;

        foreach (char c in files)
        {
            int run = c - '0';
            int numChoice = isFile ? group : 0;

            fragmented.AddRange(Enumerable.Repeat(numChoice, run));

            if (isFile)
                ++group;
            
            isFile = !isFile;
        }

        return fragmented;
    }
    
    /// <summary>
    /// Given a list representing a file system, compresses but fragments the file system
    /// </summary>
    /// <param name="files">The files to compress</param>
    private static void Fragment(List<int> files)
    {
        int zeroPtr = files.IndexOf(0);  // Ptr to first zero

        while (zeroPtr < files.Count)
        {
            // If last number non-zero, move to first 0 and incr zero ptr
            if (files.Last() != 0)
            {
                files[zeroPtr] = files[^1];
                while (zeroPtr < files.Count && files[zeroPtr] != 0)
                {
                    ++zeroPtr;
                }
            }
            
            // Last item is '0', or has been moved
            files.RemoveAt(files.Count - 1);
        }
    }

    /// <summary>
    /// Given a list representing a file system, compresses the file system
    /// </summary>
    /// <param name="filesString">The files to compress</param>
    private static List<int> DeFragment(string filesString)
    {
        List<FileRecord> files = filesString
                            .Select((c, i) => new FileRecord { Len = c - '0', Id = ((i + 1) % 2) * (i / 2 + 1) })
                            .ToList();

        int blockIndex = files.Count - 1;
        while (blockIndex > 0)
        {
            if (files[blockIndex].Id == 0)
            {
                --blockIndex;
                continue;
            }

            FileRecord current = files[blockIndex];
            for (int i = 0; i < blockIndex; ++i)
            {
                FileRecord fr = files[i];
                if (fr.Id == 0 && fr.Len >= current.Len)
                {
                    // Reduce Gap, replace old file with gap and insert it to where found gap is
                    fr.Len -= current.Len;
                    files[blockIndex] = new FileRecord { Id = 0, Len = current.Len };
                    files.Insert(i, current);
                    
                    // Counter the new record 'changing' position of pointer forwards 1
                    ++blockIndex;
                    
                    break;
                }
            }
            
            --blockIndex;
        }

        return FileRecordsToFileData(files);
    }

    private static List<int> FileRecordsToFileData(ICollection<FileRecord> files)
    {
        var fileData = new List<int>();
        foreach (FileRecord fr in files)
        {
            fileData.AddRange(Enumerable.Repeat(fr.Id, fr.Len));
        }

        return fileData;
    }
}