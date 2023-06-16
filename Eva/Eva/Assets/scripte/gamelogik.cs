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

    public Transform KorbPlacement;
    public string rightChoice;
    string targetColor;
    string[] colors = { "rot", "grün", "blau", "gelb" };

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
        else if (targetColor == "blau")
        {
            Text.color = Color.blue;
            Text.text = "BLAUEN";
        }
        else if (targetColor == "gelb")
        {
            Text.color = Color.yellow;
            Text.text = "GELBEN";
        }
        
        task=audioSource.clip;
        audioSource.Play();
    }
    
     private void CheckTag(Button flashButton)
    {
        if (flashButton.CompareTag(targetColor))
        {
            flashButton.transform.position = KorbPlacement.position;
            flashButton.tag = rightChoice;
        }
        else
        {
            string colorName = flashButton.tag;
            fehlertext.text = "Das ist " + colorName;
            fehlertext.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        GameObject targetObjects = GameObject.FindWithTag(targetColor);
        if (targetObjects == null)
        {
            endext.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);
            playendsouds();

            foreach (Button flashButton in flashButton)
            {flashButton.gameObject.SetActive(false);}            
        }
    }
}