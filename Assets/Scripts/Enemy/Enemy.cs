using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;

    private void Update()
    {
        // Tambahkan logika enemy jika diperlukan
    }

    private void Start()
    {
        // Set dasar untuk enemy jika diperlukan
    }

    protected IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
    }
}
