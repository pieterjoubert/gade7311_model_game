using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject redWall;
    public GameObject blueWall;
    public GameObject redCube;
    public GameObject blueCube;
    public GameObject peg;
    public GameObject emptyWall;

    public int height;
    public int width;

    // Start is called before the first frame update
    void Start()
    {
        /*for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {*/
                Instantiate(peg, new Vector3((float) x, 0f, (float)z), Quaternion.identity);
         /*   }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
