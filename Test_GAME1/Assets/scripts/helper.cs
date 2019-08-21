using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class helper : MonoBehaviour
{
    private bool _isAlive = true;
    private bool _inAlive = true;
    public NavMeshAgent agent;
    public Animator animator;
    private Transform player;
    public Text text;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Movement();
        StartCoroutine(detectPlayer());
        animator.GetComponent<Animation>();
    }
    private void Update()
    {
        if (_inAlive == false)
        {

            SceneManager.LoadScene("Menu");
        }
    }
    IEnumerator detectPlayer()
    {
        while (_isAlive)
        {

            if (player == null) break;
            if (Vector3.Distance(transform.position, player.position) < 1f)
            {
                animator.SetBool("attack", true);
                player.SendMessage("damage");
                break;
            }
            yield return new WaitForSeconds(.5f);

        }
    }

    IEnumerator findPath()
    {
        while (_isAlive)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);
                yield return new WaitForSeconds(2);
            }
            else break;
        }

    }
    public void Movement()
    {
        agent.SetDestination(player.position);
    }
    public void damage()
    {
        animator.SetTrigger("dead");
        Destroy(gameObject, 3f);
        enabled = false;
    }

    void attack()
    {
        animator.SetBool("attack", true);
        player.gameObject.BroadcastMessage("damage");
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {
            animator.Play("Deed");

            _isAlive = false;
            agent.SetDestination(transform.position);

            Destroy(gameObject, 1f);
        }
        if (other.CompareTag("Player"))
        {
            _inAlive = false;


        }
    }
}
