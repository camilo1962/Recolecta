using System.Collections.Generic;
using UnityEngine;

// Sript se agrega a todos los elementos prefabricados que caen.
public class Elements : MonoBehaviour
{
    public int pointsElement;
    public int intexElements;
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            HudMenu.instance.SetSpriteElements(intexElements);
          
            HudMenu.instance.BonusSet(intexElements);

            // Tomar puntos y enviarlos a HUD
            HudMenu.instance.AddPoints(pointsElement);
            HudMenu.instance.SetElementFields();
            Destroy(gameObject, 0.03f);
        }
    }
}
