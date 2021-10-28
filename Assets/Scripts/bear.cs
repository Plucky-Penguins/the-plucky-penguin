using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear : MonoBehaviour
{
    [Header("References")]
    public GameObject player, bearObj;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Animator animator;

    [Header("Stats")]
    public float sight_range;
    public float speed;
    public int health;

    [Header("TEST")]
    public bool jumpNow;

    public bool player_close;
    private bool cannotMove = false;
    private int curStunDuration = 0; // This is only used for managing the stunMe coroutine
    private Directions walls;

    [HideInInspector]
    private float width;
    private float height;
    private enum Directions
    {
        Left,
        Right,
        None
    }

    // Start is called before the first frame update
    void Start()
    {
        player_close = false;
        width = Get_Width(bearObj);
        height = Get_Height(bearObj);

        // get horizontal walls
        if (Physics2D.OverlapBox(new Vector2(rb.position.x + width, rb.position.y + height), new Vector2(0.25f, 1.5f), 0, groundLayers)) // right side
        {
            walls = Directions.Right;
        }
        else if (Physics2D.OverlapBox(new Vector2(rb.position.x - width, rb.position.y + height), new Vector2(0.25f, 1.5f), 0, groundLayers)) // left side
        {
            walls = Directions.Left;
        }
        else
        {
            walls = Directions.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpNow)
        {
            StartCoroutine(Jump());
            jumpNow = false;
        }

        player_close = PlayerInRange(player, bearObj);
        if (player_close)
        {

        }

        // do normal walk cycle
        WalkAround(bearObj);
    }

    private bool PlayerInRange(GameObject player, GameObject bearObj)
    {
        if (Mathf.Abs(player.transform.position.x - bearObj.transform.position.x) < sight_range && Mathf.Abs(player.transform.position.y - bearObj.transform.position.y) < sight_range / 2)
        {
            player_close = true;
        }
        else
        {
            player_close = false;
        }
        return player_close;
    }

    private void WalkAround(GameObject bearObj)
    {
        if (!cannotMove)
        {
            // wall check
            if (walls == Directions.Left)
            {
                Debug.Log("Wall Left");
                StartCoroutine(Jump());
            }
            else if (walls == Directions.Right)
            {
                Debug.Log("Wall Right");
                StartCoroutine(Jump());
            }


            if (player.transform.position.x > bearObj.transform.position.x)
            {
                //move right
                rb.velocity = new Vector2(speed, rb.velocity.y);
                bearObj.transform.localScale = new Vector3(Mathf.Abs(bearObj.transform.localScale.x), bearObj.transform.localScale.y, 1);
                //facingRight = true;

            }
            else
            {
                //move left
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                bearObj.transform.localScale = new Vector3(Mathf.Abs(bearObj.transform.localScale.x) * -1, Mathf.Abs(bearObj.transform.localScale.y), 1);
                //facingRight = false;
            }
        }
    }

    public void takeDamage(int damage_delt, bool doesKnockback = true)
    {
        health -= damage_delt;
        GetComponent<Renderer>().material.color = Color.yellow;

        if (doesKnockback)
        {
            StartCoroutine(knockback(50));
        }

        if (health <= 0)
        {
            Destroy(bearObj);
        }
    }

    public void stun(float duration = 2f)
    {
        curStunDuration += 1;
        StartCoroutine(stunMe(duration));
    }

    public void yeet()
    {
        StartCoroutine(knockback(18));
    }

    private static Rect Get_Rect(GameObject gameObject)
    {
        if (gameObject != null)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                return rectTransform.rect;
            }
        }
        else
        {
            Debug.Log("Game object is null.");
        }

        return new Rect();
    }

    public static float Get_Width(Component component)
    {
        if (component != null)
        {
            return Get_Width(component.gameObject);
        }

        return 0;
    }
    public static float Get_Width(GameObject gameObject)
    {
        if (gameObject != null)
        {
            var rect = Get_Rect(gameObject);
            if (rect != null)
            {
                return rect.width;
            }
        }

        return 0;
    }


    public static float Get_Height(Component component)
    {
        if (component != null)
        {
            return Get_Height(component.gameObject);
        }

        return 0;
    }
    public static float Get_Height(GameObject gameObject)
    {
        if (gameObject != null)
        {
            var rect = Get_Rect(gameObject);
            if (rect != null)
            {
                return rect.height;
            }
        }

        return 0;
    }

    // stun status efect
    public IEnumerator stunMe(float duration)
    {
        cannotMove = true;
        GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(duration);

        // only release the stun if there are not more stuns waiting to happen
        if (curStunDuration <= 1)
        {
            GetComponent<Renderer>().material.color = Color.white;
            cannotMove = false;
        }
        curStunDuration -= 1;
    }

    // knockback on player slappp
    public IEnumerator knockback(float force)
    {
        cannotMove = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        cannotMove = false;
    }

    IEnumerator Jump()
    {
        // add jump force
        rb.AddForce(new Vector2(15f, 10f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
    }
}
