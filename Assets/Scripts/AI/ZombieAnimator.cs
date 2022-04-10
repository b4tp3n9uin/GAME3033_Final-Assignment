using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieAnimator : MonoBehaviour
{
    private Animator animator;
    private ZombieAIScript zombieScript;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        zombieScript = transform.parent.GetComponent<ZombieAIScript>();
        player = zombieScript.target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombieScript.isAlive)
        {
            if (Vector3.Distance(player.position, transform.position) < 2.1f)
            {
                // face the player.
                Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(targetPos);

                animator.SetInteger("AnimationState", 2); // attack when close.
            }
            else
            {
                animator.SetInteger("AnimationState", 1); // move when further.
            }
        }
        else
        {
            animator.SetInteger("AnimationState", 3); // Death animation
        }
    }

    // This Script can find the Capsule Collider.
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (zombieScript.isAlive)
                zombieScript.DamageZombie(25);
        }
    }
}
