using System;
using System.Xml;
using UnityEngine;

public class XmlLoader
{
    private const string PlayerPowerUpsResourceName = "PowerUps";
    private const string PlayerPowerUpsResourceTag = "PowerUp";

    public static XmlNodeList LoadPlayerPowerUps() {
        return LoadXmlResource(
            PlayerPowerUpsResourceName,
            PlayerPowerUpsResourceTag
        );
    }


    public static XmlNodeList LoadXmlResource(string resourceName, string resourceTag)
    {
        TextAsset xmlAsset = LoadAsset(resourceName);
        XmlDocument xmlDocument = new XmlDocument();
        
        try
        {
            xmlDocument.LoadXml(xmlAsset.text);
        }
        catch (XmlException)
        {
            throw new ArgumentException($"Failed to load XML from the resource {resourceName}");
        }

        return xmlDocument.GetElementsByTagName(resourceTag);
    }

    private static TextAsset LoadAsset(string resourceName) {
        TextAsset xmlAsset = Resources.Load<TextAsset>(resourceName);

        if(xmlAsset == null)
            throw new ArgumentException($"Resource {resourceName} not found");

        return xmlAsset;
    }
}