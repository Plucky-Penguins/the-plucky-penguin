using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseHead : MonoBehaviour, EnemyInterface.IEnemy
{
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // jump to body position
        transform.position = body.transform.position;
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }

    public void stun(float duration = 2){}

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
    
    }

    public void yeet() {}

    
}
