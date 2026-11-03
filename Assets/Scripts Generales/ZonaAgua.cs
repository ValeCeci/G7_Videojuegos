using UnityEngine;

// Mata al jugador al tocar el agua. Va en el Tilemap del agua junto a un TilemapCollider2D con Is Trigger activado.
public class ZonaAgua : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var jugador = collision.GetComponentInParent<PlayerController>();
        if (jugador != null)
            jugador.MorirPorAgua();
    }
}
