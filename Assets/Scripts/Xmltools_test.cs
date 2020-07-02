using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Xml.Serialization;

public class Xmltools_test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


[Serializable]
[XmlRoot("root")]
   public class testthing{

    public int a;
  
   

    }

[Serializable]
[XmlRoot("root")]
    public class solider_data{

     public   int solidertype;
      public  float healthy;
     public   float speed;
     public   float attackDistance;
        
    public    float force;
      public solider_data( int solidertype,float healthy,float speed,float attackDistance,float force){
        this.solidertype=solidertype;
        this.healthy=healthy;
        this.speed=speed;
        this.attackDistance=attackDistance;
        this.force=force;

      }
      public solider_data(){
        

      }

    }



 public void savebutton(){
    //  testthing a=new testthing();
    //  a.a=2;

     
     //Assets/Scripts

    List<solider_data> solider_Datas=new List<solider_data>();
    for(int i=0;i<20;i++){
      
        solider_Datas.Add(new solider_data(1,2,3,4,5));
    }


    XMLTools.Instance().savedatatoxml(solider_Datas,typeof(List<solider_data>));

 }

 public void loadbutton(){

    //  testthing a=(testthing) XMLTools.Instance().loaddataftomxml(typeof(testthing));
 
    //    Debug.Log(a.a);

    List<solider_data> solider_Datas=(List<solider_data>)XMLTools.Instance().loaddataftomxml(typeof(List<solider_data>));
    Debug.Log(solider_Datas.Count);
 }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
