using System.Numerics;

namespace RandomShit.LeetCode;

public class Solution {
    public void SolveSudoku(char[][] board)
    {
        var newBoard = new char[9, 9];
        for (int x = 0; x < 9; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                newBoard[x, y] = board[x][y];
            }
        }
        new SudokuSolver(newBoard).Solve();
    }
}

public class SudokuSolver
{
    private readonly char[,] _board;
    private readonly List<int>?[,] _possible;

    public SudokuSolver(char[,] board)
    {
        _board = board;
        _possible = new List<int>[9,9];
    }
    
    public char[,] Solve()
    {
        int reps = 0;
        while (!BoardSolved())
        {
            CalculatePossibilities();
            LookForSolved();
            TrySolveAreas();
            reps++;
            if (reps > 9 * 9 * 9) throw new OverflowException("Solving took to many repetitions");
        }

        return _board;
    }

    private void TrySolveAreas()
    {
        for (int i = 0; i < 9; i++)
        {
            TrySolveRow(i);
        }

        for (int i = 0; i < 9; i++)
        {
            
            TrySolveCol(i);
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                TrySolveQuad(col,row);
            }
        }
    }

    private void TrySolveRow(int row)
    {
        var missing = GetMissingHorizontal(row);
        
        foreach (int num in missing)
        {
            int index = -1;
            for (int i = 0; i < 9; i++)
            {
                if (_possible[row, i] is null 
                    || !_possible[row, i].Contains(num)) continue;
                if (index == -1) index = i;
                else
                {
                    index = -1;
                    break;
                }
            }

            if (index == -1) continue;
            _board[row, index] = Convert.ToChar(num.ToString());
        }
    }

    private void TrySolveCol(int col)
    {
        var missing = GetMissingVertical(col);
        
        foreach (int num in missing)
        {
            int index = -1;
            for (int i = 0; i < 9; i++)
            {
                if (_possible[col, i] is null 
                    || !_possible[col, i].Contains(num)) continue;
                if (index == -1) index = i;
                else
                {
                    index = -1;
                    break;
                }
            }

            if (index == -1) continue;
            _board[col, index] = Convert.ToChar(num.ToString());
        }
    }

    private void TrySolveQuad(int x, int y)
    {
        var missing = GetMissingQuad(x, y);

        var negativeOne = Vector2.One * -1;
        
        foreach (int num in missing)
        {
            var pos = negativeOne;
            
            int offSetX = x * 3;
            int offSetY = y * 3;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    var curX = offSetX + col;
                    var curY = offSetY + row;
                    
                    if (_possible[curX, curY] is null 
                        || !_possible[curX, curY].Contains(num)) continue;
                    if (pos == negativeOne) pos = new Vector2(curX, curY);
                    else
                    {
                        pos = negativeOne;
                        goto exitLoop;
                    }
                }
            }

            exitLoop:

            if (pos == negativeOne) continue;
            _board[(int) pos.X, (int) pos.Y] = Convert.ToChar(num.ToString());
        }
    }

    private void LookForSolved()
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                var possibilities = _possible[col, row];
                if (possibilities == null) continue;
                if (possibilities.Count.Equals(1))
                {
                    _board[col, row] = Convert.ToChar(possibilities.FirstOrDefault().ToString());
                }
            }
        }
    }
    
    private void CalculatePossibilities()
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 9; row++)
            {
                if (_board[col, row] != '.') continue;
                CalculatePossibilities(col, row);
            }
        }
    }

    private void CalculatePossibilities(int col, int row)
    {
        int squarePosX = (int) Math.Floor(col / 3F);
        int squarePosY = (int) Math.Floor(row / 3F);

        var horizontal = GetMissingHorizontal(row);
        var vertical = GetMissingVertical(col);
        var square = GetMissingQuad(squarePosX, squarePosY);

        _possible[col, row] = GetCommonMissing(horizontal, vertical, square);
    }

    private List<int> GetMissingHorizontal(int row)
    {
        var present = new List<int>();
        
        for (int i = 0; i < 9; i++)
        {
            if (_board[i, row] == '.') continue;
            present.Add(Convert.ToInt32(_board[i, row].ToString()));
        }

        return GetMissing(present);
    }

    private List<int> GetMissingVertical(int col)
    {
        var present = new List<int>();
        
        for (int i = 0; i < 9; i++)
        {
            if (_board[col, i] == '.') continue;
            present.Add(Convert.ToInt32(_board[col, i].ToString()));
        }

        return GetMissing(present);
    }

    private List<int> GetMissingQuad(int x, int y)
    {
        int offSetX = x * 3;
        int offSetY = y * 3;

        var present = new List<int>();

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (_board[col + offSetX, row + offSetY] == '.') continue;
                present.Add(Convert.ToInt32(_board[col + offSetX, row + offSetY].ToString()));
            }
        }

        return GetMissing(present);
    }

    private static List<int> GetMissing(ICollection<int> present)
    {
        var missing = new List<int>();

        for (int i = 1; i < 10; i++)
        {
            if (present.Contains(i)) continue;
            missing.Add(i);
        }

        return missing;
    }

    private static List<int> GetCommonMissing(ICollection<int> horizontal, ICollection<int> vertical, ICollection<int> square)
    {
        var commonMissing = new List<int>();
        
        for (int i = 1; i < 10; i++)
        {
            bool missing = horizontal.Contains(i)
                           && vertical.Contains(i)
                           && square.Contains(i);

            if (missing) commonMissing.Add(i);
        }

        return commonMissing;
    }

    private bool BoardSolved()
    {
        return _board.Cast<char>().All(c => c != '.');
    }
}

//perf ideen
// hashset statt list
// missing buffer für rows cols und squares
// board solved kein linq benutzen