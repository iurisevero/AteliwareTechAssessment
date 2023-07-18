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
    public static Vector2 ChessboardToMatriz(string coordinate) {
        Vector2 matrizCoordinate = new Vector2(-1, -1);
        coordinate = coordinate.ToUpper();

        if(CheckChessboardCoordinate(coordinate)) {
            matrizCoordinate.x = chessboardLetterToInt[coordinate[0]];
            matrizCoordinate.y = chessboardSize - (coordinate[1] - '0');
        }
        return matrizCoordinate;
    }

    public static Vector2 GetDirectionFromNodes(string from, string to) {
        Vector2 fromValue = ChessboardToMatriz(from);
        Vector2 toValue = ChessboardToMatriz(to);
        return toValue - fromValue;
    }
}
