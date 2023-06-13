using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamelogik : MonoBehaviour
{
    public Text Text;
    public Text endext;
    public Text fehlertext;
    public GameObject panel;
    private bool taskDone = false;

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
    public string targetText;
    private AudioSource audioSource;
    private bool endsouds=true;
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

    string[] Task = new string[]
    {
        "ROTE",
        "GRÜNE",
        "GELBE"
    };
    public void playendsouds()
    {
       if (!audioSource.isPlaying&&endsouds==true)
        { audioSource.clip=clip6;
            endsouds=false;
            audioSource.Play();
           StartCoroutine(waitup(1f));
        
        }
    }
    public IEnumerator waitup(float delay)
    {
        yield return new WaitForSeconds(audioSource.clip.length + delay);
        Debug.Log("wait");
        sencemanger.Nextscnec();
    }
    private void Start()
    {
        fehlertext.gameObject.SetActive(false);
        endext.gameObject.SetActive(false);
        if (!taskDone)
        {
            int randomIndex = Random.Range(0, Task.Length);
            string word = Task[randomIndex];
            Text.text = word;

            if (word == "GRÜNE") // Modify this condition as needed
            {
                Text.color = Color.green; // Change color to green if the word is "GRÜNE"
                audioSource.clip=clip3;
            }
            else if (word == "ROTE")
            {
                Text.color = Color.red; // Change color to red if the word is "ROTE"
                audioSource.clip=clip1;
            }
            
            else
            {
                Text.color = Color.yellow; // Change color to yellow for other words
               audioSource.clip=clip2;
            }
            
        }
        task=audioSource.clip;
        audioSource.Play();
    }

        

            
        
    

    private void Update()
    {
        bool matchFound = false;

        foreach (Button flashButton in flashButton)
        {
            if (flashButton.gameObject.activeSelf && flashButton.image.color == Text.color)
            {
                matchFound = true;
                break;
            }
        }

        if (!matchFound)
        {
            foreach (Button flashButton in flashButton)
            {
                flashButton.gameObject.SetActive(false);
            }

            endext.gameObject.SetActive(true);
            
            panel.gameObject.SetActive(false);
            playendsouds();
            
        }
        
    }

    public void checkfabe(Button flashButton)
    {
        if (flashButton.image.color != Text.color)
        {
            string colorName = GetColorName(flashButton.image.color);
            fehlertext.text = "Das ist " + colorName;
            fehlertext.gameObject.SetActive(true);
        }
        else
        {
            flashButton.gameObject.SetActive(false);
        }
    }

    private string GetColorName(Color color)
    {
        if (color == Color.green)
        {
            return "Grün";
        }
        else if (color == Color.red)
        {
            return "Rot";
        }
        else if (color == Color.yellow)
        {
            return "Gelb";
        }
        else
        {
            return "Unbekannt";
        }
    }
}
