using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    public GameObject explode;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision!=null&&! collision.transform.GetComponent<PlayerController>())
        {
            if (collision.transform.CompareTag("Player")&& collision.transform.GetComponent<BIgGuy>())
            {
                collision.transform.GetComponent<BIgGuy>().GetHit(0.1f);
                Instantiate(explode, transform.position, transform.rotation);
                
                Destroy(gameObject);

            }
            if (collision.transform.CompareTag("Player") && collision.transform.GetComponent<Captain>())
            {
                collision.transform.GetComponent<Captain>().GetHit(0.1f);
                Instantiate(explode, transform.position, transform.rotation);
                Destroy(gameObject);

            }
            if (collision.transform.CompareTag("Player") && collision.transform.GetComponent<Cucumber>())
            {
                collision.transform.GetComponent<Cucumber>().GetHit(0.1f);
                Instantiate(explode, transform.position, transform.rotation);
                Destroy(gameObject);

            }
            if (collision.transform.CompareTag("Player") && collision.transform.GetComponent<Whale>())
            {
                collision.transform.GetComponent<Whale>().GetHit(0.1f);
                Instantiate(explode, transform.position, transform.rotation);
                Destroy(gameObject);

            }
            if (collision.transform.CompareTag("Player") && collision.transform.GetComponent<BaldPiate>())
            {
                collision.transform.GetComponent<BaldPiate>().GetHit(0.1f);
                Instantiate(explode, transform.position, transform.rotation);
                Destroy(gameObject);

            }
           else
            {
                Instantiate(explode, transform.position, transform.rotation);
                Destroy(gameObject);

            }
        }
        

    }
}
