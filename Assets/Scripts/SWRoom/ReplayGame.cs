using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayGame : MonoBehaviour
{
    public Treatment treatment;
    public SWRoomManager SW;
    public Results results;
    public Inventory inventory;
    public TreatTarget target;

    public GameObject Gel;
    public GameObject Wand;
    public GameObject marker;
    public GameObject Gloves;

    public PatientChart patient;

    public void restart()
    {
        instatiateTools();
        treatment.resetToPreviousGame();
        SW.ResetGame();
        inventory.ResetGame();
        destroyClone();
        results.closeResult();
        patient.resetChart();
        target.resetWand();
    }

    public void newGame()
    {
        instatiateTools();
        treatment.resetToNewGame();
        SW.ResetGame();
        inventory.ResetGame();
        destroyClone();
        results.closeResult();
        patient.resetChart();
        target.resetWand();
        target.generateTarget();
    }

    private void destroyClone()
    {
        var clones = GameObject.FindGameObjectsWithTag("markerArea");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        clones = GameObject.FindGameObjectsWithTag("gelBlob");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    private void instatiateTools()
    {
        if (PlayerPrefs.GetInt("gel") == 1)
        {
            Instantiate(Gel, new Vector3(1.9f, -18.2f, 1.015983f), Quaternion.identity);
        }
        if (PlayerPrefs.GetInt("wand") == 1)
        {
            Debug.Log("???");
            Instantiate(Wand, new Vector3(7.98f, -22.58f, 1.015983f), Quaternion.identity);
        }
        if (PlayerPrefs.GetInt("marker") == 1)
        {
            Debug.Log("???");
            Instantiate(marker, new Vector3(-1.9f, -9.96f, 1.015983f), Quaternion.identity);
        }
        if (PlayerPrefs.GetInt("glove") == 1)
        {
            Debug.Log("???");
            Instantiate(Gloves, new Vector3(1.44f, -9.97f, 1.015983f), Quaternion.identity);
        }

    }
}
