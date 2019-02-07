using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject GameOverCanvas;
    public GameObject VictoryCanvas;
    public GameObject IngameCanvas;

    public GameObject FindSurfaceCanvas;
    public GameObject PlaceObjectCanvas;

    public void ShowIngameCanvas() {
        IngameCanvas.SetActive(true);
        PlaceObjectCanvas.SetActive(false);
        FindSurfaceCanvas.SetActive(false);
    }

    public void ShowGameOver() {
        GameOverCanvas.SetActive(true);
        IngameCanvas.SetActive(false);
    }
    public void ShowVictory() {
        VictoryCanvas.SetActive(true);
        IngameCanvas.SetActive(false);
    }

    public void ShowFindSurface() {
        FindSurfaceCanvas.SetActive(true);
        PlaceObjectCanvas.SetActive(false);
    }
    public void ShowPlaceObject() {
        FindSurfaceCanvas.SetActive(false);
        PlaceObjectCanvas.SetActive(true);
    }

}
