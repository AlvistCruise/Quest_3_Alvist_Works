using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    private Vector3 initialPosition; 
    private bool isOnTarget = false; // Status baru
    
    void Start()
    {
        initialPosition = transform.position; 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 0;
        }
    }

    // Fungsi baru yang dipanggil dari TargetArea.cs
    public void SetOnTarget(bool status)
    {
        isOnTarget = status;
        if (status)
        {
            // Opsional: Kunci Rigidbody agar balok tidak bisa didorong lagi
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; 
            Debug.Log(gameObject.name + " berhasil ditempatkan!");
        }
        else
        {
            // Buka kunci Rigidbody jika keluar dari target
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
    // FUNGSI RESET: Dipanggil oleh PuzzleManager
    public void ResetPosition()
    {
        // Atur ulang status balok
        SetOnTarget(false); 
        
        transform.position = initialPosition;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        Debug.Log(gameObject.name + " direset."); 
    }
    
    // Tambahkan fungsi untuk dicek oleh skrip pergerakan pemain Anda
    public bool IsMovable()
    {
        // Balok tidak bisa digerakkan jika sudah berada di target
        return !isOnTarget;
    }
}
