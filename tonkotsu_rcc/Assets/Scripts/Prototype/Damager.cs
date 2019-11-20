using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(Damager source, ContactPoint contact);
}

public class Damager : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        var idmg = collision.gameObject.GetComponent<IDamagable>();
        if (idmg == null)
        {
            return;
        }

        idmg.TakeDamage(this, collision.GetContact(0));
    }

}
