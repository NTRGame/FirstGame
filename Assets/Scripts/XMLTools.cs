using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
public class XMLTools
{


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



     static XMLTools instance;
     private static string path;
     

     private void Awake() {
         
         
     }



    public static XMLTools Instance (){
       
            if (instance == null) {
                instance = new XMLTools ();
                  path = Application.dataPath+"/data.xml";
            }
            return instance;
        

    }

    public void savedatatoxml(object data,Type type){

    
        FileInfo fileinfo = new FileInfo(path);
        
        StreamWriter sw; 
        
        if (!fileinfo.Exists) 
        {
            
            sw = fileinfo.CreateText();
        }
        else
        {
            
            fileinfo.Delete();
            sw = fileinfo.CreateText();
        }


       
        XmlSerializer ser = new XmlSerializer(type);
        
        ser.Serialize(sw,data);

        sw.Close();  

        Debug.Log("存储成功");


    }
    public object loaddataftomxml(Type type){


     
        FileStream fstream = new FileStream(path,FileMode.Open);
        XmlSerializer xmlSer = new XmlSerializer(type);
        
       
        
        return xmlSer.Deserialize(fstream);

    }



     public static Soldier GetSoldierByType(SoldierType soldierType,PlayerType player)
     {

        
      List<solider_data> solider_Datas=(List<solider_data>)XMLTools.Instance().loaddataftomxml(typeof(List<solider_data>));
      //float healthy, float speed, float attackDistance, SoldierType soldierType,float force
    　foreach (solider_data element in solider_Datas)  
　　　　{ 
　　　　　　if(element.solidertype==(int)soldierType){
              return new Soldier(element.healthy, element.speed, element.attackDistance, soldierType, element.force) { playerType = player };  
           }
　　　　} 
      

         return new Soldier(10f, 3f, 3, soldierType, 3f) { playerType = player };
         
     }
}