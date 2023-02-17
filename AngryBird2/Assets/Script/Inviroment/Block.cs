using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float _heath;

    private void OnCollisionEnter2D(Collision2D other)
    {
        float dam = gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude;
        if (dam > 0.2f)
        {
            _heath = _heath - dam * 10;
        }
    }
    private void Update()
    {
        if (_heath <= 0)
        {
            Destroy(gameObject);
        }
    }
}
