using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public static int width = 10;
    public static int height = 24;
    public static Transform[,] grid = new Transform[width, height];

    private static int score = 0;

    public static Vector2 RoundVector2(Vector2 value)
    {
        return new Vector2(Mathf.Round(value.x), Mathf.Round(value.y));
    }

    public static bool InsideBorderCheck(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    public static void DeleteRow(int y)
    {
        for(int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void DropRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if(grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void DropRowsAbove(int y)
    {
        for(int i = y; i < height; i++)
        {
            DropRow(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        for(int x = 0; x < width; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    public static void DeleteFullRows()
    {
        for(int y = 0; y < height; y++)
        {
            if(IsRowFull(y))
            {
                DeleteRow(y);
                DropRowsAbove(y + 1);
                //forgetting to put in the next line was a fun bug...
                y--;
                FindObjectOfType<LightShow>().StartRave();
                score += 100;
                GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = score.ToString();
            }
        }
    }
}
