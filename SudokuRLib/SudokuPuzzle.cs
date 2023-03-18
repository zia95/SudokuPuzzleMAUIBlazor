using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuRLib;

public class SudokuPuzzle
{
    public const int CellsCount = 9 * 9; 
    public string Puzzle { get; set; }

    public string Solution { get; set; }


    public int GivenCount { get; set; }
    public int SingleCount { get; set; }
    public int HiddenSingleCount { get; set; }
    public int NakedPairCount { get; set; }
    public int HiddenPairCount { get; set; }
    public int PointingPairTripleCount { get; set; }
    public int BoxReductionCount { get; set; }
    public int GuessCount { get; set; }
    public int BacktrackCount { get; set; }
    public string DifficultyString { get; set; }




    public IEnumerable<int> GetSolutionBlock(int num) => GetBlock(Solution, num);
    public IEnumerable<int> GetPuzzleBlock(int num) => GetBlock(Puzzle, num);
    public static IEnumerable<int> GetBlock(string src, int num)
    {
        num--; //arrays starts at '0' index

        int row = num / 3;
        int row_idx = 9 * (3 * row);

        int col = num % 3;
        int col_idx = col * 3;

        int first = row_idx + col_idx;
        int second = first + 9;
        int third = second + 9;

        yield return first;
        yield return first + 1;
        yield return first + 2;

        yield return second;
        yield return second + 1;
        yield return second + 2;

        yield return third;
        yield return third + 1;
        yield return third + 2;
    }


    public IEnumerable<int> GetPuzzleFixedCells()
    {
        for (int i = 0; i < CellsCount; i++)
            if (Puzzle[i] is >= '1' and <= '9') yield return i;
    }


    public int GetSolutionIntValue(int index, int unknown = 0) => GetIntValue(Solution, index, unknown);
    public int GetPuzzleIntValue(int index, int unknown = 0) => GetIntValue(Puzzle, index, unknown);
    public static int GetIntValue(string src, int index, int unknown = 0) => (src[index] is >= '1' and <= '9') ? src[index] - '0' : unknown;

    public IEnumerable<int> SolutionToIntArray(int unknown = 0) => ToIntArray(Solution, unknown);
    public IEnumerable<int> PuzzleToIntArray(int unknown = 0) => ToIntArray(Puzzle, unknown);
    public static IEnumerable<int> ToIntArray(string src, int unknown = 0)
    {
        for (int i = 0; i < CellsCount; i++)
        {
            yield return GetIntValue(src, i, unknown);
        }
    }


    public bool CheckIfSolved(string sol) => sol == Solution;
    public bool CheckIfSolved(int[] sol)
    {
        for(int i = 0; i < CellsCount; i++)
        {
            if ((Solution[i] - '0') != sol[i]) return false;
        }
        return true;
    }

    public int this[int idx]
    {
        get
        {
            return GetPuzzleIntValue(idx);
        }
    }

    public override string ToString()
    {
        string str = "";
        if (!string.IsNullOrWhiteSpace(Puzzle))
        {
            for (int i = 0; i < 9 * 9; i++)
            {
                if (i % 3 == 0 && i % 9 != 0 && i != 0)
                {
                    str += ";";
                }
                if (i % 9 == 0 && i != 0)
                {
                    str += "#";
                }

                str += (Puzzle[i] is >= '1' and <= '9') ? Puzzle[i].ToString() : "0";

            }
        }
        return str;
    }

}
