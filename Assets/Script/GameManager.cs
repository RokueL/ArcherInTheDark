using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Room;
    private GameObject indexRoom;
    public GameObject wayRoom;
    public GameObject wallAll;
    public GameObject wallDoor;
    GameObject first;

    //public GameObject Player;

    public int minRoom = 10;
    public int maxRoom = 20;
    public int RandomRoom;
    public int RandomDirection;

    private int Distance = 10;
    private int RoomDistance = 20;
    private int i, j, k,l = 0;
    //길목 회전
    private Vector3 RoomRotation = new Vector3(0,0,0);

    //레이어체킹
    int layermask;

    Vector3 direction;
    Vector3 point;



    public GameObject Monster;
    public GameObject Boss;
    GameObject BossRoom;

    void StageLoading()
    {
        //방 생성 개수
        RandomRoom = Random.Range(minRoom, maxRoom);
        Debug.Log(RandomRoom);

        for (int j = 0; j < RandomRoom; j++)
        {
            MapCreate();
        }
        i = 1;
        BossRoom = indexRoom;
        indexRoom = GameObject.Find("FirstRoom");
        for (int j = 0; j< RandomRoom; j++)
        {
           WayCreate();
        }
        Debug.Log("CreateEnd");
        indexRoom = first;
        BossRoom.name = "BossRoom";
    }

    private void MapCreate()
    {

        // 4방향 시계방향 랜덤 생성
        RandomDirection = Random.Range(0, 4);

        if (RandomDirection == 0)
        {
            direction = new Vector3 (1,0,0);
        }
        else if (RandomDirection == 1)
        {
            direction = new Vector3(0, 0, -1);
        }
        else if (RandomDirection == 2)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (RandomDirection == 3)
        {
            direction = new Vector3(0, 0, 1);
        }
        point = direction * Distance;
        //맵이 있는 지 없는지 체킹용
        RaycastHit rayHit;
        //Debug.DrawRay(indexRoom.transform.position + point, direction * 10, new Color(0, 1, 0));
        if (Physics.Raycast(indexRoom.transform.position + point, direction, out rayHit, RoomDistance, layermask) == false)
        {
            indexRoom = Instantiate(Room, indexRoom.transform.position + (direction * RoomDistance), Quaternion.identity);
            i++;
            indexRoom.name = "Room" + i.ToString();
            indexRoom.layer = 11;
        }
    }

    private void WayCreate()
    {
        // 4방향 시계방향 랜덤 생성

        for (int rotate = 0; rotate < 4; rotate++)
        {
            if (rotate == 0)
            {
                direction = new Vector3(1, 0, 0);
                RoomRotation = new Vector3(0f, 0f, 0f);
            }
            else if (rotate == 1)
            {
                direction = new Vector3(0, 0, -1);
                RoomRotation = new Vector3(0f, 90f, 0f);
            }
            else if (rotate == 2)
            {
                direction = new Vector3(-1, 0, 0);
                RoomRotation = new Vector3(0f, 180f, 0f);
            }
            else if (rotate == 3)
            {
                direction = new Vector3(0, 0, 1);
                RoomRotation = new Vector3(0f, 270f, 0f);
            }
            point = direction * Distance;
            //맵이 있는 지 없는지 체킹용
            RaycastHit rayHit;
            //Debug.DrawRay(indexRoom.transform.position + point, direction * 10, new Color(0, 1, 0));
            if (Physics.Raycast(indexRoom.transform.position + point, direction, out rayHit, RoomDistance, layermask) == true)
            {
                GameObject wayroom = Instantiate(wayRoom, indexRoom.transform.position + (direction * 8), Quaternion.Euler(RoomRotation));
                GameObject wallall = Instantiate(wallDoor, indexRoom.transform.position, Quaternion.Euler(RoomRotation));
                
                k++;
                wayroom.name = "wayRoom" + k.ToString();
                wallall.name = "wallall" + k.ToString();
                wayroom.layer = 12;
            }
            else
            {

                GameObject walldoor = Instantiate(wallAll, indexRoom.transform.position, Quaternion.Euler(RoomRotation));
                walldoor.name = "wall" + l.ToString();
                l++;
            }
        }
        if (GameObject.Find("Room" + i) == null)
        {
            return;
        }
        else
        {
            indexRoom = GameObject.Find("Room" + i);
            i++;
        }
    }



    public void MonsterSpawn(Transform point1, Transform point2, Transform point3)
    {
        int randomMon = Random.Range(0, 3);
        if(randomMon == 1) 
        {
            Instantiate(Monster, point1.position, Quaternion.identity);
        }
        else if( randomMon== 2) 
        {
            Instantiate(Monster, point1.position, Quaternion.identity);
            Instantiate(Monster, point2.position, Quaternion.identity);
        }
        else if (randomMon== 3)
        {
            Instantiate(Monster, point1.position, Quaternion.identity);
            Instantiate(Monster, point2.position, Quaternion.identity);
            Instantiate(Monster, point3.position, Quaternion.identity);
        }
        else
        {

        }

    }


    //void playerSpawn()
    //{
    //    Instantiate(Player, Vector3.zero, Quaternion.identity);
    //}

    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("PlatForm", "CreatedRoom");
        indexRoom = GameObject.Find("GameManager");
        first = Instantiate(Room, this.transform.position, Quaternion.identity);
        first.name = "FirstRoom".ToString();
        StageLoading();
        //playerSpawn();

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
