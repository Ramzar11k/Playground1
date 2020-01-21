using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minesweeper : MonoBehaviour
{
    public int width;
    public int height;
    public int nrOfBombs;
    public GameObject tile;
    List<GameObject> tiles = new List<GameObject>();
    public List<int> bombPosition = new List<int>();
    void Start()
    {
        for (int i = 0; i < nrOfBombs; i++)
        {
            int bombPos = Random.Range(0, width * height);
            if (bombPosition.Contains(bombPos))
                i--;
            else
                bombPosition.Add(bombPos);
        }
        float posX = -0.8f * (width - 1);
        float posZ = -0.8f * (height - 1);
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject bomb = Instantiate(tile, transform.localPosition + new Vector3(posX, 0, posZ), transform.rotation, transform);
                tiles.Add(bomb);
                if (bombPosition.Contains(i * width + j))
                {
                    bomb.GetComponent<MinesweeperTile>().isBomb = true;
                }
                posX += 1.6f;
            }
            posZ += 1.6f;
            posX = -0.8f * (width - 1);
        }

        foreach (GameObject tile in tiles)
        {
            int pos = tiles.IndexOf(tile);
            tile.name = pos.ToString();
            if (tile.GetComponent<MinesweeperTile>().isBomb)
                continue;
            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    if (pos <= width || pos % width == 0)
                        continue;
                    if (tiles[pos - width -1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos - width - 1]);
                }
                else if (i == 1)
                {
                    if (pos <= width)
                        continue;
                    if (tiles[pos - width].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos - width]);
                }
                else if (i == 2)
                {
                    if (pos <= width || pos % width == width-1)
                        continue;
                    if (tiles[pos - width +1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos - width + 1]);
                }
                else if (i == 3)
                {
                    if (pos % width == 0)
                        continue;
                    if (tiles[pos - 1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos - 1]);
                }
                else if (i == 4)
                {
                    if (pos % width == width - 1)
                        continue;
                    if (tiles[pos + 1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos + 1]);
                }
                else if (i == 5)
                {
                    if (pos >= tiles.Count - width || pos % width == 0)
                        continue;
                    if (tiles[pos + width - 1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos + width - 1]);
                }
                else if (i == 6)
                {
                    if (pos >= tiles.Count - width)
                        continue;
                    if (tiles[pos + width].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos + width]);
                }
                else if (i == 7)
                {
                    if (pos >= tiles.Count - width || pos % width == width - 1)
                        continue;
                    if (tiles[pos + width + 1].GetComponent<MinesweeperTile>().isBomb)
                    {
                        tiles[pos].GetComponent<MinesweeperTile>().value++;
                    }
                    tiles[pos].GetComponent<MinesweeperTile>().neighbours.Add(tiles[pos + width + 1]);
                }
            }
        }
    }

    public void CheckIfWon()
    {
        bool won = true;
        foreach (int index in bombPosition)
        {
            if (!tiles[index].GetComponent<MinesweeperTile>().marked)
                won = false;
        }
        if (won)
            print("WWOAGJO");
    }
}
