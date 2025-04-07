using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TailWindComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent _tailWindEvent;
    [SerializeField] private ParticleSystem _tailWindParticle;

    private float activeTime = 5f;
    private bool _tailWindActive = false;

    public void SetActive()
    {
        _tailWindActive = true;
        StartCoroutine(TurnOn());
    }

    private IEnumerator TurnOn()
    {
        _tailWindParticle.Play();
        yield return new WaitForSeconds(activeTime);
        _tailWindEvent?.Invoke();
        _tailWindActive = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_tailWindActive)
            {
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.AddForce(Vector2.up * 50f, ForceMode2D.Force);
            }
        }
    }
}
