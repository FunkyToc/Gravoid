using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpeedDecay : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            if (col.TryGetComponent<ParticleMove>(out ParticleMove pm))
            {
                pm.setCtimeReset(Time.time);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            if (col.TryGetComponent<ParticleMove>(out ParticleMove pm))
            {
                pm.setCtimeReset(Time.time);
            }
        }
    }
}
