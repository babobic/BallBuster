using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    #region Singleton
    // Using a singleton to ensure only one instance of a manager exists
    private static BrickManager _instance;

    public static BrickManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }
    #endregion

    private int maxRows = 17;
    private int maxColumns = 12;

    public Sprite[] Sprites;
    public List<int[,]> LevelData { get; set; }

    private void Start()
    {
        this.LevelData = this.LoadLevelData();
    }

    private List<int[,]> LoadLevelData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;

        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        List<int[,]> levelData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxColumns];

        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if(line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                //end of curr level
                currentRow = 0;
                levelData.Add(currentLevel);
                currentLevel = new int[maxRows, maxColumns];
            }
        }
        return levelData;
    }
}
