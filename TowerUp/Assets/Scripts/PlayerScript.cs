using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript script;
    public float speed;
    public float force;
    public int score;
    public float windForce = 0;
    [HideInInspector] public GameObject man;
    [HideInInspector] public bool canCameraMove = false;
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool canJump = true;
    [HideInInspector] public bool isLose = false;

    float[] borders = {-2.1f,2.1f};
    bool moveLeft = false;
    bool moveRight = true;

    Transform tempManTransform;
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
        script = this;
    }
    void Update()
    {
        if (tempManTransform != null)
        {
            if (tempManTransform.position.x < borders[0])
            {
                moveLeft = false;
                moveRight = true;
            }
            else if (tempManTransform.position.x > borders[1])
            {
                moveLeft = true;
                moveRight = false;
            }
            if (canMove == true && !isLose)
            {
                Move();
            }
        }
    }
    void Move()
    {
        if (moveRight)
        {
            tempManTransform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (moveLeft)
        {
            tempManTransform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    public void PushTheRock()
    {
        if (man.transform.childCount > 1 && canJump && !isLose)
        {
            canJump = false;
            man.transform.GetChild(2).parent = null;
            RockScript.script.Jump();
        }
    }
    public void ChangeMan(GameObject temp)
    {
        man = null;
        man = temp;
        tempManTransform = man.transform;
    }
    public void re()
    {
        Application.LoadLevel(0);
    }
}

