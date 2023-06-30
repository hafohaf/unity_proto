using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Gamelogik : MonoBehaviour
{
    
    public Text Text;
    public Image endext;
    public Image farbe;
    public Text rundeText;
    public Text fehlertext;
    public GameObject panel;
    public GameObject rundepanel;
    public Transform KorbPlacement;
    string rightChoice = "Untagged";
    string targetColor;
    string[] colors = { "rot", "grün", "gelb" };
    string[] colors2 = { "rot", "grün", "gelb","lila","orange" };
    public Transform[] placementPoints; // Array mit den Platzierungspunkten
    public int currentRound = 1;
    private string scencename;
    public Button[] flashButton;
    [SerializeField]
    internal sencemanger sencemanger;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    public AudioClip clip8;
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
               else if(Text.text.Equals("Lila"))
               {
                flashButton.tag = "lila";
               }
                else if(Text.text.Equals("Orange"))
               {
                flashButton.tag = "orange";
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
        { audioSource.clip=clip8;
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
        rundepanel.gameObject.SetActive(true);
        fehlertext.gameObject.SetActive(false);
        endext.gameObject.SetActive(false);
        
        PlaceObjectsRandomly();
        if(sencemanger.getcurrentscencename(scencename)=="game1_easy")
        {
            targetColor = colors[Random.Range(0, colors.Length)];
        }
        else if(sencemanger.getcurrentscencename(scencename)=="game1_hard")
        {
            targetColor = colors2[Random.Range(0, colors2.Length)];
        }
        string nummer=currentRound.ToString();
        rundeText.text=nummer;
        foreach (Button flashButton in flashButton)
        {
            flashButton.onClick.AddListener(() => CheckTag(flashButton));
        }
        
        if (targetColor == "rot")
        {
            farbe.color = Color.red;
            Text.color = Color.red;
            Text.text = "ROTEN";
            audioSource.clip=clip1;
        }
        else if (targetColor == "grün")
        {
          farbe.color = Color.green;
            Text.color = Color.green;
            Text.text = "GRÜNEN";
            audioSource.clip=clip3;
        }
        
        else if (targetColor == "gelb")
        {
           farbe.color= Color.yellow;
            Text.color = Color.yellow;
            Text.text = "GELBEN";
            audioSource.clip=clip2;
        }
        else if (targetColor == "lila")
        {
           farbe.color= new Color(0.6f, 0.4f, 0.8f); 
            Text.color = new Color(0.6f, 0.4f, 0.8f); 
            Text.text = "Lila";
            audioSource.clip=clip2;
        }
        else if (targetColor == "orange")
        {
           farbe.color= new Color(1f, 0.647f, 0f);
            Text.color = new Color(1f, 0.647f, 0f);
            Text.text = "Orange";
            audioSource.clip=clip2;
        }
        task=audioSource.clip;
        audioSource.Play();
    }

    public IEnumerator waitup(float delay)
    {
        yield return new WaitForSeconds(audioSource.clip.length + delay);
        Debug.Log("wait");
        
        sencemanger.senceindex();
        
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
            fehlertext.text = "Das war falsh";
            audioSource.clip=clip4;
            audioSource.Play();
            fehlertext.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        GameObject targetObjects = GameObject.FindWithTag(targetColor);

        if (targetObjects==null && currentRound !=3)
        {
            settagback();
            currentRound++;
            StartGame();
        }
        else if (targetObjects == null && currentRound == 3)
        {
            fehlertext.gameObject.SetActive(false);
            endext.gameObject.SetActive(true);
            rundepanel.gameObject.SetActive(false);
            panel.gameObject.SetActive(false);
            farbe.gameObject.SetActive(false);
            playendsouds();

            foreach (Button flashButton in flashButton)
            {flashButton.gameObject.SetActive(false);}            
        }
    }
}