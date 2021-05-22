using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public static SpawnScript script;
    public GameObject[] egyptGround;
    public GameObject[] egyptMan;
    public int witchOne;
    GameObject tempGround;
    GameObject tempMan;

    private void Awake()
    {
        script = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Spawn();
        }
    }
    private void Start()
    {
        tempGround = Instantiate(egyptGround[4], new Vector2(0,6.27f), Quaternion.identity) as GameObject;
        tempMan = Instantiate(egyptMan[Random.Range(0,egyptMan.Length)], new Vector2(Random.Range(-2.1f, 2.1f), 7.05f), Quaternion.identity) as GameObject;
    }
    public void Spawn()
    {
        GameObject witchMan = null;
        GameObject witchGround = null;
        if (witchOne == 0)
        {
            witchMan = egyptMan[Random.Range(0, egyptMan.Length)] as GameObject;
            witchGround = egyptGround[Random.Range(0, egyptGround.Length)] as GameObject;
        }
        tempGround = Instantiate(witchGround, new Vector2(0, tempGround.transform.position.y + 3.75f), Quaternion.identity) as GameObject;
        tempMan = Instantiate(witchMan, new Vector2(Random.Range(-1.8f, 1.8f), tempMan.transform.position.y + 3.75f), Quaternion.identity) as GameObject;
    }
}
