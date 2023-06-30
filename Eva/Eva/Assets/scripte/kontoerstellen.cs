using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kontoerstellen : MonoBehaviour
{   
    public GameObject content;
   public Button button;
   public void buttonvergleich()
   {
    if(button.name=="NutzerbuttonDefault")
        Debug.Log("ok");
        Button newButton = Instantiate(button);
   }
}
