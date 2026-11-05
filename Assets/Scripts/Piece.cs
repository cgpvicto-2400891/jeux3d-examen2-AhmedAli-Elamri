using System;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static event Action PieceCollecter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boule boule))
        {
            PieceCollecter?.Invoke();
            Destroy(gameObject);
        }
    }
}
