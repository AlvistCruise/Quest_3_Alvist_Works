using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    private PushableBlock[] allBlocks; 
    public TargetArea[] allTargets; // Array baru untuk placeholder
    
    private bool isPuzzleCompleted = false; 
    public bool IsCompleted { get { return isPuzzleCompleted; } }

    void Start()
    {
        allBlocks = FindObjectsOfType<PushableBlock>();
        
        // Pastikan jumlah balok dan placeholder sama
        if (allBlocks.Length != allTargets.Length)
        {
            Debug.LogError("ERROR: Jumlah balok (" + allBlocks.Length + ") dan Target Area (" + allTargets.Length + ") TIDAK SAMA!");
        }
    }
    
    // Fungsi baru: Mengecek apakah semua placeholder sudah terisi
    public void CheckCompletion()
    {
        // LINQ: Cek apakah SEMUA TargetArea memiliki status IsOccupied = true
        bool allFilled = allTargets.All(target => target.IsOccupied);
        
        if (allFilled && !isPuzzleCompleted)
        {
            CompletePuzzle();
        }
    }
    
    public void CompletePuzzle()
    {
        isPuzzleCompleted = true;
        Debug.Log("Selamat! Teka-teki berhasil diselesaikan. Tombol Reset dinonaktifkan secara permanen.");
        // Anda bisa menambahkan logika membuka pintu/cutscene di sini
    }

    // Fungsi Reset: Kunci reset ada di sini
    public void ResetPuzzle()
    {
        // Kunci: Jika sudah selesai, JANGAN lakukan reset.
        if (isPuzzleCompleted)
        {
            Debug.Log("Reset gagal. Teka-teki sudah selesai.");
            return; // Menghentikan fungsi reset
        }

        if (allBlocks == null || allBlocks.Length == 0) return;

        // Lakukan reset pada semua balok
        foreach (PushableBlock block in allBlocks)
        {
            if (block != null)
            {
                block.ResetPosition(); 
            }
        }
        Debug.Log("Teka-teki balok berhasil direset.");
    }
}
