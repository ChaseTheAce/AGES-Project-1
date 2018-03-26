using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour {

    public LayerMask playerMask;
    //public ParticleSystem explosionParticles;
    //public AudioSource explosionAudio;
    //public float maxDamage = 100f;
    public float maxplosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;


    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "PlayArea")
        {



            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, playerMask);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();

                if (!targetRigidBody)
                {
                    continue;
                }

                targetRigidBody.AddExplosionForce(maxplosionForce, transform.position, explosionRadius);

            }

            //explosionParticles.transform.parent = null;

            //explosionParticles.Play();

            //explosionAudio.Play();

            //Destroy(explosionParticles.gameObject, explosionParticles.duration);
            Destroy(gameObject);
        }

    }
}
