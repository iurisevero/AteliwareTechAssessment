using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    static string regexChessboard = @"[A-H][1-8]";
    public static Boolean CheckChessboardCoordinate(string coordinate) {
        return System.Text.RegularExpressions.Regex.IsMatch(coordinate, regexChessboard);
    }

    static int chessboardSize = 8;
    static Dictionary<char, int> chessboardLetterToInt = new Dictionary<char, int>()
    {
        {'A', 0},
        {'B', 1},
        {'C', 2},
        {'D', 3},
        {'E', 4},
        {'F', 5},
        {'G', 6},
        {'H', 7}
    };
    public static (int, int) ChessboardToMatriz(string coordinate) {
        (int, int) matrizCoordinate = (-1, -1);
        coordinate = coordinate.ToUpper();

        if(CheckChessboardCoordinate(coordinate)) {
            matrizCoordinate.Item1 = chessboardLetterToInt[coordinate[0]];
            matrizCoordinate.Item2 = chessboardSize - (coordinate[1] - '0');
        }
        return matrizCoordinate;
    }
}
