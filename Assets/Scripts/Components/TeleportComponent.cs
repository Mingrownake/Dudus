using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportComponent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _currentFadeTime;
    [SerializeField] private float _fadetTime;

    [SerializeField] private float _currentTeleportTime;
    [SerializeField] private float _teleportTime;

    public void Teleport(Player player)
    {
        StartCoroutine(TeleportCoroutine(player));
    }

    private IEnumerator TeleportCoroutine(Player player)
    {
        yield return FadeInOutCoroutine(player, 0);
        yield return TransformCoroutine(player);
        yield return FadeInOutCoroutine(player, 1);
    }

    private IEnumerator FadeInOutCoroutine(Player player, float directionAlpha)
    {
        SpriteRenderer playerRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerRigidbody.bodyType = RigidbodyType2D.Static;
        playerCollider.enabled = false;
        playerInput.enabled = false;
        while (_currentFadeTime <= _fadetTime)
        {
            _currentFadeTime += Time.deltaTime;
            float currentAlpha = playerRenderer.color.a;
            float nextAlpha = Mathf.Lerp(currentAlpha, directionAlpha, _currentFadeTime / _fadetTime);
            playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, nextAlpha);
            yield return null;
        }
        playerRigidbody.bodyType = RigidbodyType2D.Dynamic;
        playerCollider.enabled = true;
        playerInput.enabled = true;
        _currentFadeTime = 0;
    }

    private IEnumerator TransformCoroutine(Player player)
    {
        while (_currentTeleportTime <= _teleportTime)
        {
            _currentTeleportTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(player.transform.position, target.position, _currentTeleportTime / _teleportTime);
            yield return null;
        }

        _currentTeleportTime = 0;
    }
}
