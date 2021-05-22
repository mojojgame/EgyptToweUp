using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManScript : MonoBehaviour
{
    public string type;
    public bool selfMove;
    bool moveLeft = false;
    bool moveRight = false;
    float[] borders = { -2.1f, 2.1f };
    Transform tempManTransform;
    private void Start()
    {
        if (PlayerScript.script.score > 1)
        {
            RandomSelfMove();
        }
        tempManTransform = this.gameObject.transform;
    }
    void Update()
    {
        if (selfMove == true && !PlayerScript.script.isLose)
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

            Move();
        }
    }
    void Move()
    {
        if (moveRight) tempManTransform.Translate(Vector2.right * PlayerScript.script.speed * Time.deltaTime);
        if (moveLeft) tempManTransform.Translate(Vector2.left * PlayerScript.script.speed * Time.deltaTime);
    }
    void RandomSelfMove()
    {
        selfMove = RandomBool();
        int randomSide = Random.Range(0, 2);
        if(randomSide == 0)
        {
            moveLeft = false;
            moveRight = true;
        }
        else if (randomSide == 1)
        {
            moveLeft = true;
            moveRight = false;
        }
    }
    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            selfMove = false;
        }
    }
}
