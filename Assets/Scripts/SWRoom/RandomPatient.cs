using UnityEngine;

public class RandomPatient : MonoBehaviour
{
    //0: no problem
    //1: have problem but did not say
    //2: have problem
    public DialogueNode[] greetings = new DialogueNode[3];
    //0: feel great
    //1: pain but ok
    //2: pain but not ok
    public DialogueNode[] treatments = new DialogueNode[3];
    
    private int greeting;
    private int treatment;

    private int state;

    // Use this for initialization
    void Start()
    {
        greeting = Random.Range(0, 3);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("treated") == 1)
        {
            
        }
    }

}
