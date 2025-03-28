using System;
using UnityEngine;

    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private Collider2D pointChecker;
        [SerializeField] private LayerMask checkerLayer;

        private bool _isGrounded;
        public bool IsGrounded => _isGrounded;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (pointChecker.IsTouchingLayers(checkerLayer))
            {
                _isGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!pointChecker.IsTouchingLayers(checkerLayer))
            {
                _isGrounded = false;
            }
        }
    }
