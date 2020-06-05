using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningOptions {
    int [][] TopLeft = new int[3][]
    {
        new int[] { 1, 2 },
        new int[] { 4, 8 },
        new int[] { 3, 6 }
    };
    int [][] TopMiddle = new int[2][]
    {
        new int[] { 0, 2 },
        new int[] { 4, 7 }
    };
    int [][] TopRight = new int[3][]
    {
        new int[] { 0, 1 },
        new int[] { 4, 6 },
        new int[] { 5, 8 }
    };

    int [][] Left = new int[2][]
    {
        new int[] { 0, 6 },
        new int[] { 4, 5 }
    };
    int [][] Middle = new int[4][]
    {
        new int[] { 0, 8 },
        new int[] { 2, 6 },
        new int[] { 3, 5 },
        new int[] { 1, 7 }
    };
    int [][] Right = new int[2][]
    {
        new int[] { 2, 8 },
        new int[] { 3, 4 }
    };

    int [][] BottomLeft = new int[3][]
    {
        new int[] { 0, 3 },
        new int[] { 4, 2 },
        new int[] { 7, 8 }
    };
    int [][] BottomMiddle = new int[2][]
    {
        new int[] { 1, 4 },
        new int[] { 6, 8 }
    };
    int [][] BottomRight = new int[3][]
    {
        new int[] { 0, 4 },
        new int[] { 2, 5 },
        new int[] { 6, 7 }
    };

    public int[][] GetByPosition(int position) {
        switch (position)
        {
            case 0:
                return TopLeft;
            case 1:
                return TopMiddle; 
            case 2:
                return TopRight;
            case 3:
                return Left;
            case 4:
                return Middle;
            case 5:
                return Right;
            case 6:
                return BottomLeft;
            case 7:
                return BottomMiddle;
            case 8:
                return BottomRight;
            default:
                Debug.LogError("Invalid value provided to GetByPosition WinningOption.");
                return new int[0][];
        }
        
    }
}
