using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public static RockScript script;

    public GameObject firstMan;
    public ParticleSystem windPartical;

    public bool canWindChangeMyPosition = false;
    public bool canAddPartical = false;

    Rigidbody2D rock;
    Transform rockTransform;
    GameObject temp;
    GameObject tempGround;

    private void Awake()
    {
        script = this;
        temp = firstMan;
        rockTransform = this.gameObject.transform;
    }

    private void Start()
    {
        rock = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (canWindChangeMyPosition)
        {
            rockTransform.Translate(Vector2.right * PlayerScript.script.windForce * Time.deltaTime, Space.World);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             temp = other.gameObject;
             StartCoroutine("TrunOnHeadCollider");
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            tempGround = other.gameObject;
            StartCoroutine("TrunOnGroundCollider");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            HideWind();
            transform.parent = temp.transform;
            PlayerScript.script.ChangeMan(temp.gameObject);
            UIScript.script.ChangeScore(PlayerScript.script.score += 2);
            transform.GetChild(0).gameObject.SetActive(true);
            PlayerScript.script.canMove = true;
            PlayerScript.script.canJump = true;
            SetWind();
            SpawnScript.script.Spawn();
            PlayerScript.script.canCameraMove = true;
        }
        else if(other.gameObject.CompareTag("FirstRun"))
        {
            transform.parent = temp.transform;
            PlayerScript.script.ChangeMan(temp.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            PlayerScript.script.canMove = true;
            PlayerScript.script.canJump = true;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            UIScript.script.Lose();
            canWindChangeMyPosition = false;
            canAddPartical = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript.script.canMove = false;
            if (PlayerScript.script.score > 10 && PlayerScript.script.score % 2 == 0)
            {
                PlayerScript.script.speed += 0.035f;
                CameraScript.script.cameraSpeed += 0.025f;
            }
            if (PlayerScript.script.score > 10 && PlayerScript.script.score % 4 == 0 && transform.localScale.x > 0.76f && transform.localScale.y > 0.15f)
            {
                transform.localScale = new Vector2(transform.localScale.x - 0.05f, transform.localScale.y - 0.025f);
            }
        }
    }
    public void Jump()
    {
        rock.AddForce(Vector2.up * PlayerScript.script.force);
        rock.gravityScale = 1f;
        canWindChangeMyPosition = canAddPartical;
    }
    void ShowWind()
    {
        if (canAddPartical) { 
            windPartical.gameObject.SetActive(true);
            float tempParticalSpeed = PlayerScript.script.windForce * 6;
            var forceLifeTime = windPartical.forceOverLifetime;
            forceLifeTime.x = tempParticalSpeed;

            if (PlayerScript.script.windForce > 0)
            {
                windPartical.transform.position = new Vector2(-3f, windPartical.transform.position.y+1f);
            }
            else
            {
                windPartical.transform.position = new Vector2(3f, windPartical.transform.position.y + 1f);
            }
        }
        else
        {
            windPartical.gameObject.SetActive(false);
        }
    }
    void SetWind()
    {
        if (PlayerScript.script.score > 8)
        {
            canAddPartical = RandomBool();
            if (canAddPartical)
            {
                PlayerScript.script.windForce = Random.Range(-0.6f, 0.61f);
                ShowWind();
            }
        }
    }
    void HideWind()
    {
        PlayerScript.script.windForce = 0;
        canAddPartical = false;
        canWindChangeMyPosition = false;
        windPartical.gameObject.SetActive(false);
    }
    bool RandomBool()
    {
        return (Random.value > 0.5f);
    }

    IEnumerator TrunOnHeadCollider()
    {
        yield return new WaitForSecondsRealtime(0.45f);
        temp.transform.GetChild(1).gameObject.SetActive(true);

    }
    IEnumerator TrunOnGroundCollider()
    {
        yield return new WaitForSecondsRealtime(0.45f);
        tempGround.transform.GetChild(0).gameObject.SetActive(true);
        
    }
}
