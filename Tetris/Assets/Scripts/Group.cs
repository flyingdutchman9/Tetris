using UnityEngine;

public class Group : MonoBehaviour
{
    private float fallTickerTime;

    private void Start()
    {
        fallTickerTime = Time.time;
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        //Left/Right
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        //Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0, 0, -90);
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }

        //Fall down
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fallTickerTime >= 0.5 || Input.GetKeyDown(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
            if (IsValidGridPosition())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                Grid.DeleteFullRows();

                if(!CheckForGameOver())
                {
                    FindObjectOfType<Spawner>().SpawnNext();
                    this.enabled = false;
                }
                else
                {
                    Debug.Log("GAME OVER");
                    FindObjectOfType<Spawner>().StopSpawning();
                    Destroy(gameObject);
                }
                
            }

            fallTickerTime = Time.time;
        }
    }

    private bool CheckForGameOver()
    {
        foreach (Transform child in transform)
        {
            Vector2 singleBlockPosition = Grid.RoundVector2(child.position);

            if(singleBlockPosition.y > 20)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            Vector2 singleBlockPosition = Grid.RoundVector2(child.position);

            if (!Grid.InsideBorderCheck(singleBlockPosition))
            {
                return false;
            }

            //Here is the counter of how many times I made a typo in this check
            //3
            if (Grid.grid[(int)singleBlockPosition.x, (int)singleBlockPosition.y] != null &&
                Grid.grid[(int)singleBlockPosition.x, (int)singleBlockPosition.y].parent != transform)
            {
                return false;
            }
        }

        return true;
    }

    private void UpdateGrid()
    {
        //This is ugly but I am tired
        for (int y = 0; y < Grid.height; y++)
        {
            for (int x = 0; x < Grid.width; x++)
            {
                if (Grid.grid[x, y] != null)
                {
                    if (Grid.grid[x, y].parent == transform)
                    {
                        Grid.grid[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVector2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
