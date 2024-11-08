using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] public Weapon weaponHolder;

    private Weapon weapon;

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void Awake()
    {
        weapon = Instantiate(weaponHolder, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if(playerWeapon != null)
            {
                playerWeapon.transform.SetParent(transform, false);
                playerWeapon.transform.localPosition = Vector3.zero;
                TurnVisual(false, playerWeapon);
            }

            weapon.parentTransform = other.transform;
            weapon.transform.SetParent(Player.Instance.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localScale = Vector3.one; 

            TurnVisual(true, weapon);
        }
        else {
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

}