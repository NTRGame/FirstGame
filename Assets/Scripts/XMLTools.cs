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
    public static Soldier GetSoldierByType(SoldierType soldierType, PlayerType player)
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
        Debug.Log(xmlDocument.SelectSingleNode("SoliderDataList").InnerText);
        foreach (XmlNode node in xmlDocument.SelectSingleNode("SoliderDataList").ChildNodes)
        {
            if (node.ChildNodes.Item(0).Equals(soldierType))
            {
                Debug.Log(node.ChildNodes.Item(0).InnerText);
                return new Soldier(float.Parse(node.ChildNodes.Item(1).InnerText), float.Parse(node.ChildNodes.Item(2).InnerText), float.Parse(node.ChildNodes.Item(3).InnerText), soldierType, float.Parse(node.ChildNodes.Item(4).InnerText)) { playerType = player };
            }
           
        }

        return new Soldier(10f, 3f, 3, soldierType, 3f) { playerType = player };

    }
}