using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sencemanger : MonoBehaviour
{
   public void Nextscnec()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }

   public void hard()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+2);
   }

   public void back()
   {
    
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
   
   }
   public void mag()
   {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-3);
   
   }
   public void magnicht()
   {

     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-4);
   }
   public void gosetting()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+6);
   }
   public void backvonsetting()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-6);
   }

}
