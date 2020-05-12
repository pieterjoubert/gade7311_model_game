using Assets.Scripts;
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

    //height and width is the number of pegs
    public int height;
    public int width;

    private Board board;
    // Start is called before the first frame update

    private void Awake()
    {
        EventManager.Instance.AddListener(EVENT_TYPE.WALL_SELECTED, OnWallSelected);
    }
    void Start()
    {
        board = new Board(height, width);

        board.Place(Owner.RED, 4, 4);
        board.Place(Owner.BLUE, 4, 3);
        Debug.Log(board.Get(4, 4));
        Debug.Log(board.Get(4, 3));
        Debug.Log(board.Get(5, 5));

        ReDraw();

        for (int px = 0; px < height; px++)
        {
            for (int pz = 0;pz < width; pz++)
            {
                Instantiate(peg, new Vector3((float)px * 1.1f, 0f, (float)pz * 1.1f), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWallSelected(EVENT_TYPE Event_Type, Component sender, object param = null)
    {
        Wall w = sender.GetComponent<Wall>();
        if (board.Place(GameManager.Instance.CurrentPlayer, w.X, w.Z))
        {
            if(board.ClosedBox(GameManager.Instance.CurrentPlayer, w.X, w.Z))
            {
                Debug.Log("CLOSED AFTER PLAYER: ");
            }
            else
            {
                EventManager.Instance.PostNotification(EVENT_TYPE.TURN_CHANGED, this);
            }
            ReDraw();
        }
        Debug.Log(w.X + "," + w.Z);
    }

    void ReDraw()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject w in walls)
            Destroy(w);

        bool horizontal = true;
        int x = 0;
        int z = 0;
        int tempZ = 0;
        float rotation = 90f;
        float hOffset = 0.55f;
        float vOffset = 0.0f;
        foreach(Owner o in board.Walls)
        {
           switch (o)
            {
                case Owner.EMPTY:
                    { 
                        GameObject wall; //This is repeated to prevent the creation of actual game objects in game
                        wall = Instantiate(emptyWall,
                            new Vector3(((float)x * 1.1f) + hOffset, 0f, ((float)z * 1.1f) + vOffset),
                            transform.rotation * Quaternion.Euler(0f, rotation, 0f));
                        wall.GetComponent<Wall>().X = x;
                        wall.GetComponent<Wall>().Z = tempZ;
                        break;
                    }
                case Owner.RED:
                    { 
                        GameObject wall;
                        wall = Instantiate(redWall,
                            new Vector3(((float)x * 1.1f) + hOffset, 0f, ((float)z * 1.1f) + vOffset),
                            transform.rotation * Quaternion.Euler(0f, rotation, 0f));
                        wall.GetComponent<Wall>().X = x;
                        wall.GetComponent<Wall>().Z = tempZ;
                        break;
                    }
                case Owner.BLUE:
                    { 
                        GameObject wall;
                        wall = Instantiate(blueWall,
                            new Vector3(((float)x * 1.1f) + hOffset, 0f, ((float)z * 1.1f) + vOffset),
                            transform.rotation * Quaternion.Euler(0f, rotation, 0f));
                        wall.GetComponent<Wall>().X = x;
                        wall.GetComponent<Wall>().Z = tempZ;
                        break;
                    }
                default: Debug.Log("Missing OwnedType"); break;
            }

            x++;
            if(horizontal == true && x == board.Width - 1)
            {
                x = 0;
                tempZ++;
                rotation = 0f;
                horizontal = false;
                hOffset = 0f;
                vOffset = 0.55f;
            }
            else if (horizontal == false && x == board.Width)
            {
                x = 0;
                z++;
                tempZ++;
                rotation = 90f;
                horizontal = true;
                hOffset = 0.55f;
                vOffset = 0f;
            }
        }

        hOffset = 0.55f;
        vOffset = 0.55f;

        for(int bx = 0; bx < board.Width - 1; bx++)
        {
            for(int bz = 0; bz < board.Height - 1; bz++)
            {
                switch (board.Boxes[bx,bz])
                {
                    case Owner.EMPTY: break;
                    case Owner.RED:
                        Instantiate(redCube,
                                new Vector3(((float)bx * 1.1f) + hOffset, 0f, ((float)bz * 1.1f) + vOffset),
                                transform.rotation * Quaternion.Euler(0f, rotation, 0f));
                            break;
                    case Owner.BLUE:
                        Instantiate(blueCube,
                                new Vector3(((float)bx * 1.1f) + hOffset, 0f, ((float)bz * 1.1f) + vOffset),
                                transform.rotation * Quaternion.Euler(0f, rotation, 0f));
                            break;
                    default: Debug.Log("Missing OwnedType"); break;
                }

            }
        }
    }
}
