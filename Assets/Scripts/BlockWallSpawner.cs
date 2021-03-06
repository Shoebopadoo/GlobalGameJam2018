﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWallSpawner : MonoBehaviour {
    public GameObject Block;
    public int height;
    public int length;
    public GameObject[] walloBlock;
    public GameObject spawningGrid;
    public Vector3 BottomLeft;

	// Use this for initialization
	void Start () {
        BottomLeft = spawningGrid.transform.position;
        walloBlock = new GameObject[length * height];
        int l = 0;
        for(int i = 0; i < length; i++)
        {
            for(int q = 0; q < height; q++)
            {
                walloBlock[l] = Instantiate(Block, new Vector3(BottomLeft.x + Block.transform.localScale.x * i, BottomLeft.y + Block.transform.localScale.y * q, spawningGrid.transform.position.z), Quaternion.identity);
                l++;
            }
        }
    }
}
