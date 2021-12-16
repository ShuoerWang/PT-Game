using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/PatientChart/chartComponent")]
public class ChartComponent: ScriptableObject
{
    public string patientName;
    public string symptoms;
    public string cause;
    public string history;
    public string goal;
    public string socialHis;
}