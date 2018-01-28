using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWallSpawner : MonoBehaviour {
    public GameObject Block;
    public int height;
    public int length;
    public GameObject[] walloBlock;

	// Use this for initialization
	void Start () {
        walloBlock = new GameObject[length * height];
        int l = 0;
        for(int i = 0; i < length; i++)
        {
            for(int q = 0; q < height; q++)
            {
                walloBlock[l] = Instantiate(Block, new Vector3(Block.transform.localScale.x * i, Block.transform.localScale.y * q, 0), Quaternion.identity);
                l++;
            }
        }
    }
}
