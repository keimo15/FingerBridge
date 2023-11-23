using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static int rows = 5;
    public static int columns = 10;
    public static bool[,] areBlack = new bool[rows, columns];
    

    void Start()
    {
        ResetBlockManager();
    }

    void Update()
    {
        
    }

    void ResetBlockManager()
    {
        for (int i=0; i<rows; i++) {
            for (int j=0; j<columns; j++) {
                areBlack[i, j] = true;
            }
        }
    }
}
