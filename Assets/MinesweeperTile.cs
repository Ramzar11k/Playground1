using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinesweeperTile : MonoBehaviour
{
    public int value;
    public bool isBomb = false;
    public bool marked = false;
    public List<GameObject> neighbours = new List<GameObject>();
    public Text tValue;
    public GameObject spear;
    private void Awake()
    {
        tValue = transform.GetChild(2).GetChild(0).GetComponent<Text>();
    }
    private void Start()
    {
        if (isBomb)
            tValue.text = "#"; 
        else
            tValue.text = value.ToString();
    }

    void CheckTile(GameObject tile)
    {
        MinesweeperTile msTile = tile.GetComponent<MinesweeperTile>();
        if (msTile.value == 0)
        {
            msTile.tValue.gameObject.SetActive(true);
            foreach(GameObject neig in msTile.neighbours)
            {
                if (neig.GetComponent<MinesweeperTile>().value == 0 && neig.GetComponent<MinesweeperTile>().tValue.gameObject.activeInHierarchy == false)
                {
                    CheckTile(neig);
                }
                neig.GetComponent<MinesweeperTile>().tValue.gameObject.SetActive(true);
            }
        }
        else
        {
            tValue.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (transform.GetChild(2).GetChild(1).gameObject.activeInHierarchy)
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                marked = false;
            }
            GetComponent<Collider>().enabled = false;
            if (isBomb)
            {
                CheckTile(gameObject);
                StartCoroutine(WrongPick());
            }
            else
            {
                CheckTile(gameObject);
            }
            Destroy(other.gameObject);
            transform.parent.GetComponent<Minesweeper>().CheckIfWon();
        }
        if (other.CompareTag("Bullet2"))
        {
            if (!transform.GetChild(2).GetChild(1).gameObject.activeInHierarchy)
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                marked = true;
            }
            else
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
                marked = false;
            }
            transform.parent.GetComponent<Minesweeper>().CheckIfWon();
        }
    }

    IEnumerator WrongPick()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(spear, player.position + new Vector3(Random.Range(-10.0f, 10.0f), 15.0f, Random.Range(-10.0f, 10.0f)), Quaternion.identity, null);
            yield return new WaitForSeconds(2.0f);
        }
        GetComponent<MinesweeperTile>().tValue.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = true;
    }
}
