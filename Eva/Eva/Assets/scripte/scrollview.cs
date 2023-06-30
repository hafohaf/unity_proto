using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class scrollview : MonoBehaviour
{
    
    private float[] rateArr;
    public RectTransform contentTransform;
    public RectTransform itemTransform;
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void stelltkont()
    {
        //NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        //{
           // if (path != null)
           // {
                Transform temp= Instantiate(itemTransform).transform;
                temp.SetParent(contentTransform);
                temp.localPosition=Vector3.zero;
                temp.localRotation= Quaternion.identity;
                temp.localScale=Vector3.one;
          //  }
     
      //  },"Select a photo", "image/*");
    }
    

    
}
