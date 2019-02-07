using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour {

    public Canvas canvas;

    public float rotationDamping;

    private Text text;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start () {
        this.text = canvas.GetComponentInChildren<Text> ();
        this.mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        Vector3 lookPos = mainCam.transform.position - canvas.transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation (lookPos);
        this.canvas.transform.rotation = Quaternion.Slerp (this.canvas.transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void SetText (string message) {
        this.text.text = message;
    }

    public void FadeInMessage (string message, float time = 5, float delay = 0) {
        this.SetText (message);
        StartCoroutine (FadeIn (this.canvas.gameObject, time, delay));
    }

    public void FadeOutMessage() {
        StartCoroutine(FadeOut(this.canvas.gameObject, 10));
    }

    public void FadeInOutMessage(string message) {
        this.SetText (message);
        StartCoroutine (FadeInOut (this.canvas.gameObject, 5, 6));
    }

    private IEnumerator FadeInOut(GameObject group, float time, float delay) {
        StartCoroutine(FadeIn(group, time));
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeOut(group, time));
    }

    private IEnumerator FadeIn (GameObject group, float time, float delay = 0) {

        yield return new WaitForSeconds(delay);
        group.SetActive (true);
        CanvasGroup canvasGroup = group.GetComponent<CanvasGroup> ();
        float elapsedTime = 0;
        canvasGroup.alpha = 0;

        while (elapsedTime < time) {
            canvasGroup.alpha = Mathf.Lerp (canvasGroup.alpha, 1, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut (GameObject group, float time) {

        CanvasGroup canvasGroup = group.GetComponent<CanvasGroup> ();
        float elapsedTime = 0;
        canvasGroup.alpha = 1;

        while (elapsedTime < time) {
            canvasGroup.alpha = Mathf.Lerp (canvasGroup.alpha, 0, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            if (elapsedTime > time) {
                group.SetActive (false);
            }
            yield return null;
        }
    }

}