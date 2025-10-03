using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    // Pastikan ini terhubung ke objek PuzzleManager di Inspector
    public PuzzleManager puzzleManager; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan yang menginjak adalah pemain
        if (other.CompareTag("Player")) 
        {
            if (puzzleManager != null)
            {
                // **PENTING:** Kita HAPUS pengecekan IsCompleted di sini.
                // Biarkan PuzzleManager yang melakukan pengecekan di dalam fungsinya.
                
                // Langsung panggil fungsi reset
                puzzleManager.ResetPuzzle();
                
                // Opsional: Anda dapat menambahkan efek visual di sini
                // Contoh: Mengubah warna tombol sebentar saat diinjak
            }
        }
    }
    
    // ... (Fungsi OnTriggerExit2D tetap opsional) ...
}
