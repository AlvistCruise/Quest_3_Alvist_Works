using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    // Hubungkan ini di Inspector ke objek PuzzleManager Anda
    public PuzzleManager puzzleManager;
    
    // Status penempatan
    private bool isOccupied = false;
    public bool IsOccupied { get { return isOccupied; } }

    // Dipanggil saat ada objek masuk/keluar dari area trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        // Pastikan balok yang menabrak memiliki Collider2D
        Collider2D blockCollider = other.GetComponent<Collider2D>();
        PushableBlock block = other.GetComponent<PushableBlock>();
        
        if (block != null && blockCollider != null)
        {
            Bounds blockBounds = blockCollider.bounds;
            Bounds targetBounds = GetComponent<Collider2D>().bounds;
            
            // --- LOGIKA PENGHITUNGAN OVERLAP 50% ---

            // 1. Tentukan batas perpotongan (intersection)
            float minX = Mathf.Max(blockBounds.min.x, targetBounds.min.x);
            float maxX = Mathf.Min(blockBounds.max.x, targetBounds.max.x);
            float minY = Mathf.Max(blockBounds.min.y, targetBounds.min.y);
            float maxY = Mathf.Min(blockBounds.max.y, targetBounds.max.y);

            // 2. Hitung luas area perpotongan
            float overlapX = maxX - minX;
            float overlapY = maxY - minY;
            
            // Cek apakah ada perpotongan yang valid (overlap > 0)
            if (overlapX > 0 && overlapY > 0)
            {
                float overlapArea = overlapX * overlapY;
                float targetArea = targetBounds.size.x * targetBounds.size.y;
                
                // 3. Periksa syarat overlap 50%
                if (overlapArea >= (targetArea * 0.5f))
                {
                    if (!isOccupied)
                    {
                        isOccupied = true;
                        // Beri tahu balok bahwa ia telah berada di tempat yang benar
                        block.SetOnTarget(true); 
                        // Periksa penyelesaian teka-teki
                        puzzleManager.CheckCompletion(); 
                    }
                }
                else
                {
                    // Jika overlap di bawah 50% saat berada di trigger
                    if (isOccupied)
                    {
                        // Ini penting jika balok terdorong sedikit, tapi belum keluar dari trigger
                        isOccupied = false;
                        block.SetOnTarget(false);
                    }
                }
            }
        }
    }
    
    // Gunakan OnTriggerExit2D untuk memastikan status reset jika balok benar-benar meninggalkan area trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        PushableBlock block = other.GetComponent<PushableBlock>();

        if (block != null && isOccupied)
        {
            // Reset status hanya jika balok yang keluar adalah balok yang menempati
            isOccupied = false;
            block.SetOnTarget(false);
            
            // Catatan: Anda mungkin perlu menambahkan logika di PuzzleManager untuk memeriksa
            // ulang penyelesaian teka-teki (CheckCompletion) jika balok keluar.
        }
    }
}
