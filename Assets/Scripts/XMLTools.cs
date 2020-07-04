using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using System.Xml;

public class XMLTools
{
    public static Soldier GetSoldierByType(SoldierType soldierType)
    {
        ////float healthy, float speed, float attackDistance, SoldierType soldierType,float force
        //foreach (Soldier element in Soldiers)
        //{
        //    if (element.soldierType == soldierType)
        //    {
        //        return new Soldier(element.healthy, element.speed, element.attackDistance, soldierType, element.force) { playerType = player };
        //    }
        //}
        var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/data.xml");
        request.SendWebRequest();
        while (!request.isDone) ;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(request.downloadHandler.text);
        foreach (XmlNode node in xmlDocument.SelectSingleNode("SoliderDataList").ChildNodes)
        {
            if ((SoldierType)Enum.Parse(typeof(SoldierType), node.ChildNodes.Item(0).InnerText) == soldierType)
            {
                return new Soldier(float.Parse(node.ChildNodes.Item(1).InnerText), float.Parse(node.ChildNodes.Item(2).InnerText), float.Parse(node.ChildNodes.Item(3).InnerText), soldierType, float.Parse(node.ChildNodes.Item(4).InnerText), float.Parse(node.ChildNodes.Item(5).InnerText),(BulletType)Enum.Parse(typeof(BulletType), node.ChildNodes.Item(6).InnerText));
            }
           
        }

        return new Soldier(10f, 3f, 3f,SoldierType.FlagZombie, 3f,2f,BulletType.ProjectileCabbage);

    }
}