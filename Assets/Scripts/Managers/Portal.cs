using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : MonoBehaviour
{

    private Vector3 newPosition; 
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float rotateSpeed = 50f; 

    private void Start()
    {
        ChangePosition();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (PlayerHasWeapon())
        {
            EnablePortal(true);
        }
        else
        {
            EnablePortal(false);
        }
    }

    private void ChangePosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        newPosition = new Vector3(randomX, randomY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<LevelManager>().LoadScene("Main");
        }
    }

    private void EnablePortal(bool enabled)
    {
        GetComponent<SpriteRenderer>().enabled = enabled;
        GetComponent<Collider2D>().enabled = enabled;
    }

    private bool PlayerHasWeapon()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            return player.GetComponentInChildren<Weapon>() != null;
        }
        return false;
    }

}