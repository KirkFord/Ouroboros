using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    [SerializeField] private GameObject current1;
    [SerializeField] private GameObject current2;
    [SerializeField] private GameObject current3;
    [SerializeField] private GameObject current4;

    [SerializeField] private GameObject need1;
    [SerializeField] private GameObject need2;
    [SerializeField] private GameObject need3;
    [SerializeField] private GameObject need4;

    [SerializeField] private Animator chestAnim;

    private GameObject[] cubes = new GameObject[8];
    private int[] colors = new int[8];

    public bool solved = false;
    // Red = 1, Blue = 2, Green = 3, Yellow = 4

    // Start is called before the first frame update
    void Start()
    {
        InitialColors();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateColors();
    }


    void InitialColors()
    {
        cubes[0] = current1;
        cubes[1] = current2;
        cubes[2] = current3;
        cubes[3] = current4;
        cubes[4] = need1;
        cubes[5] = need2;
        cubes[6] = need3;
        cubes[7] = need4;

        colors[0] = 1;
        colors[1] = 1;
        colors[2] = 1;
        colors[3] = 1;
        RandomizeColors();
    }

    void RandomizeColors()
    {
        while (true)
        {
            colors[4] = Random.Range(1, 4);
            colors[5] = Random.Range(1, 4);
            colors[6] = Random.Range(1, 4);
            colors[7] = Random.Range(1, 4);
            if (colors[4] != 1 & colors[5] != 1 & colors[6] != 1 & colors[7] != 1)
            {
                break;
            }
        }
    }

    void UpdateColors()
    {
        for (int i = 0; i < 8; i++)
        {
            switch(colors[i])
            {
                case 1:
                    cubes[i].GetComponent<Renderer>().material.color = Color.red;
                    break;

                case 2:
                    cubes[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;

                case 3:
                    cubes[i].GetComponent<Renderer>().material.color = Color.green;
                    break;

                case 4:
                    cubes[i].GetComponent<Renderer>().material.color = Color.yellow;
                    break;
            }
        }
    }

    public void ActivateLever(int leverNum)
    {
        // Proper logic

        switch(leverNum)
        {
            case 1:
                colors[0] += 1;
                colors[1] += 1;
                colors[2] += 1;
                colors[3] += 1;
                break;

            case 2:
                colors[1] += 1;
                colors[2] += 1;
                break;

            case 3:
                colors[1] += 1;
                colors[2] += 1;
                colors[3] += 1;
                break;

            case 4:
                colors[1] += 1;
                break;
        }
        RotateColors();

        if(CompareColors())
        {
            if(!solved)
            {
                solved = true;
            }
            
        }
    }

    void RotateColors()
    {
        for (int i = 0; i < 4; i++)
        {
            if (colors[i] >= 5)
            {
                colors[i] = 1;
            }
        }
    }

    bool CompareColors()
    {
        for (int i = 0; i < 4; i++)
        {
            if (colors[i] != colors[i + 4])
            {
                return false;
            }
        }
        return true;
    }

    public void GiveReward()
    {
        chestAnim.Play("Chest Open");
    }

    public void SpawnRewardCoins()
    {
        GameObject[] coins;
        coins = GameObject.FindGameObjectsWithTag("Loot");
        foreach (GameObject coin in coins)
        {
            var lootScript = coin.GetComponent<SpawnLoot>();
            lootScript.DropCoin();
        }
    } 
}

