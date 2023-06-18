using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamelogik : MonoBehaviour
{
    public Text Text;
    public Image endext;
    public Text fehlertext;
    public GameObject panel;
    public Transform KorbPlacement;
    string rightChoice = "Untagged";
    string targetColor;
    string[] colors = { "rot", "grün", "gelb" };
    public Transform[] placementPoints; // Array mit den Platzierungspunkten
    private int currentRound = 0;

    public Button[] flashButton;
    [SerializeField]
    internal sencemanger sencemanger;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    private AudioClip task;
    private AudioSource audioSource;
    private bool endsouds=true;

    public void settagback()
    {
        foreach(Button flashButton in flashButton)
        {
            if(flashButton.CompareTag(rightChoice))
            {
               if(Text.text.Equals("ROTEN"))
               {
                flashButton.tag = "rot";
               }
               else if(Text.text.Equals("GRÜNEN"))
               {
                flashButton.tag = "grün";
               }
                else if(Text.text.Equals("GELBEN"))
               {
                flashButton.tag = "gelb";
               }
            }
        }
    }


    public void PlaceObjectsRandomly()
    {
        int objectCount = flashButton.Length;
        int pointCount = placementPoints.Length;

        if (objectCount != pointCount)
        {
            Debug.LogError("Die Anzahl der zu platzierenden Objekte stimmt nicht mit der Anzahl der Platzierungspunkte überein.");
            return;
        }

        // Mische die Platzierungspunkte zufällig
        for (int i = 0; i < pointCount - 1; i++)
        {
            int randomIndex = Random.Range(i, pointCount);
            Transform temp = placementPoints[randomIndex];
            placementPoints[randomIndex] = placementPoints[i];
            placementPoints[i] = temp;
        }

        // Platziere die Gameobjects an den zufällig sortierten Platzierungspunkten
        for (int i = 0; i < objectCount; i++)
        {
            flashButton[i].transform.position = placementPoints[i].position;
        }
    }

    public void read()
    {
         if (!audioSource.isPlaying)
         { audioSource.clip=task;
            audioSource.Play();}
    }
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playendsouds()
    {
       if (!audioSource.isPlaying&&endsouds==true)
        { audioSource.clip=clip6;
            endsouds=false;
            audioSource.Play();
           StartCoroutine(waitup(1f));
        
        }
    }
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        currentRound++;

        fehlertext.gameObject.SetActive(false);
        endext.gameObject.SetActive(false);
        PlaceObjectsRandomly();

        targetColor = colors[Random.Range(0, colors.Length)];

        foreach (Button flashButton in flashButton)
        {
            flashButton.onClick.AddListener(() => CheckTag(flashButton));
        }
        
        if (targetColor == "rot")
        {
            Text.color = Color.red;
            Text.text = "ROTEN";
            audioSource.clip=clip1;
        }
        else if (targetColor == "grün")
        {
            Text.color = Color.green;
            Text.text = "GRÜNEN";
            audioSource.clip=clip3;
        }
        
        else if (targetColor == "gelb")
        {
            Text.color = Color.yellow;
            Text.text = "GELBEN";
            audioSource.clip=clip2;
        }
        
        task=audioSource.clip;
        audioSource.Play();
    }

    public IEnumerator waitup(float delay)
    {
        yield return new WaitForSeconds(audioSource.clip.length + delay);
        Debug.Log("wait");
        sencemanger.Nextscnec();
    }
    
    private void CheckTag(Button flashButton)
    {
        if (flashButton.CompareTag(targetColor))
        {
            flashButton.transform.position = KorbPlacement.position;
            flashButton.tag = rightChoice;
        }
        else if (!flashButton.CompareTag(rightChoice))
        {
            string colorName = flashButton.tag;
            fehlertext.text = "Das ist " + colorName;
            fehlertext.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        GameObject targetObjects = GameObject.FindWithTag(targetColor);

        if (targetObjects==null && currentRound < 3)
        {
            settagback();
            StartGame();
        }
        else if (targetObjects == null && currentRound == 3)
        {
            endext.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);

            playendsouds();

            foreach (Button flashButton in flashButton)
            {flashButton.gameObject.SetActive(false);}            
        }
    }
}