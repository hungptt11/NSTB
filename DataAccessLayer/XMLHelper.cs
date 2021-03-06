﻿using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Utility;


namespace DataAccessLayer
{
    public class XMLHelper
    {
        public static string path = ".\\Settings.ini";

        // Read Format file XML
        public static string ReadFormatXML(string keyName, string session)
        {
            try
            {
                var ini = new IniFile(path);
                string xmlfile = ini.Read(keyName, session).ToString();
                return xmlfile;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.ReadFormatXML(string keyName, string session)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        // Add values into XML
        public static void AddNodeValueXML(ref string xmlname, params object[] nodevalues)
        {
            try
            {
                xmlname = String.Format(xmlname, nodevalues);
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.AddNodeValueXML(ref string xmlname, params object[] nodevalues)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
        }

        // Edit value of node
        public static void EditNodeXML(ref string xmlname, string nodename, string nodevalue)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlname);
                XmlNode xmlnode = xml.GetElementsByTagName(nodename).Item(0);
                if (xmlnode != null || xmlnode.InnerText != nodevalue)
                {
                    xmlnode.InnerText = nodevalue;
                    xmlname = xml.InnerXml;
                }
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.EditNodeXML(ref string xmlname, string nodename, string nodevalue)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
        }

        public static string BuildXml(Dictionary<string, string> lstItem)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Document>" + "</Document>");
            XmlText text = null;
            try
            {
                foreach (var item in lstItem)
                {
                    XmlElement elem = doc.CreateElement(item.Key);                   
                    text = doc.CreateTextNode(item.Value);
                    doc.DocumentElement.AppendChild(elem);
                    doc.DocumentElement.LastChild.AppendChild(text);
                }
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.BuildXml(Dictionary<string, string> lstItem)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
            return doc.InnerXml;
        }
        /*public static string BuildXmlForUpdateTills(Dictionary<CurrencyForUpdateTill_Object,Dictionary<string, string>> denominations)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><xml>" + "</xml>");
            try
            {
                foreach (var item in denominations)
                {
                    XmlElement elem = doc.CreateElement("currency");
                    elem.SetAttribute("code", item.Key.currencyCode.ToString());
                    elem.SetAttribute("totalAmount", ((long)item.Key.TotalAmount).ToString());
                    elem.SetAttribute("totalCount", item.Key.totalCount.ToString());                    
                    foreach (var deno in item.Value)
                    {
                         XmlElement denochild = doc.CreateElement("denomination");
                         XmlElement value = doc.CreateElement("value");
                         //value.InnerText = (long.Parse( deno.Key.ToString())).ToString();
                         value.InnerText = ((long)decimal.Parse(deno.Key)).ToString();
                         denochild.AppendChild(value);
                         XmlElement count = doc.CreateElement("count");
                         count.InnerText = deno.Value;
                         denochild.AppendChild(count);
                         elem.AppendChild(denochild);
                    }                    
                    doc.DocumentElement.AppendChild(elem);
                }
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.BuildXmlForUpdateTills(Dictionary<CurrencyForUpdateTill_Object,Dictionary<string, string>> denominations)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
            return doc.InnerXml;
        }*/
        public static string GetElementByNodeName(string xmlDocument)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlDocument);
            try
            {
                XmlNode xmlnode = doc.GetElementsByTagName("currency").Item(0);
                if (xmlnode != null)
                {
                    return xmlnode.InnerXml;
                }
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.GetElementByNodeName(string xmlDocument)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
            return null;
        }
        /*public static string AppendNode(string xmlDocument, string xmlNode)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlDocument);
            XmlNode node = new XmlNode();
            node.InnerXml(xmlNode);
            try
            {
                doc.AppendChild(xmlNode);
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.CommonLib.XMLHelper.AppendNode(string xmlDocument, string xmlNode)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
            }
            return null;
        }*/


       /* public static bool AddingDatabaseConfig(string keyinput, string dbName = null, string tbName = null, string moduleName = null, string transId = null , string typeData= null, string data = null)
        {
            try
            {
                // Allowing application access to the App_Layouts folder
                ProtectData.SetFolderAccessRule(DiagConstants.DataFolder);

                // Creation the BackupData.data file if it does not exist
                if (!File.Exists(DiagConstants.DataFileName))
                    File.WriteAllText(DiagConstants.DataFileName, CreateBackupDataXmlHeader());

                // Applying data changes
                CreateBackupDataXmlContent(keyinput, dbName, tbName, moduleName, transId, typeData, data);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static  string CreateBackupDataXmlHeader()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement xmlElmntRoot;
                xmlElmntRoot = xmlDoc.CreateElement("BackupData");

                xmlDoc.RemoveAll();
                xmlDoc.AppendChild(xmlDoc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\""));
                xmlDoc.AppendChild(xmlElmntRoot);

                return xmlDoc.InnerXml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateBackupDataXmlContent(string keyinput, string dbName, string tbName, string moduleName, string transId,string typeData ,string data)
        {

            try
            {
                if (keyinput == null)
                {
                    Guid key;
                    key = Guid.NewGuid();
                    // Reading the BackupData.data file
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(DiagConstants.DataFileName);

                    //// Remove old node
                    //XmlNodeList xmlNodes = xmlDoc.SelectNodes("/BackupData/add[@name='" + dbName + "']");
                    //for (int i = xmlNodes.Count - 1; i >= 0; i--)
                    //    xmlNodes[i].ParentNode.RemoveChild(xmlNodes[i]);

                    // Creation new node
                    XmlNode xmlNodeRoot = xmlDoc.SelectSingleNode("/BackupData");
                    XmlElement xmlElmntConn = default(XmlElement);
                    xmlElmntConn = xmlDoc.CreateElement("add");
                    xmlElmntConn.SetAttribute("Key", key.ToString());
                    xmlElmntConn.SetAttribute("prsStatus", "0");
                    xmlElmntConn.SetAttribute("dbName", ProtectData.EncryptData(dbName));
                    xmlElmntConn.SetAttribute("tbName", ProtectData.EncryptData(tbName));
                    xmlElmntConn.SetAttribute("moduleName", ProtectData.EncryptData(moduleName));
                    xmlElmntConn.SetAttribute("transId", transId);
                    xmlElmntConn.SetAttribute("userName", DataMem.UserLogin);
                    xmlElmntConn.SetAttribute("dateTime", DateTime.Now.ToString());
                    xmlElmntConn.SetAttribute("typeData", ProtectData.EncryptData(typeData));
                    xmlElmntConn.SetAttribute("data", ProtectData.EncryptData(data));
                    xmlNodeRoot.AppendChild(xmlElmntConn);

                    // Saving changes
                    xmlDoc.Save(DiagConstants.DataFileName);
                }
                else
                {
                    // Reading the BackupData.data file
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(DiagConstants.DataFileName);

                    // Remove old node
                    XmlNodeList xmlNodes = xmlDoc.SelectNodes("/BackupData/add[@Key='" + keyinput + "']");
                    for (int i = xmlNodes.Count - 1; i >= 0; i--)
                        xmlNodes[i].ParentNode.RemoveChild(xmlNodes[i]);
                    // Saving changes
                    xmlDoc.Save(DiagConstants.DataFileName);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public static Dictionary<int, Backupdata_Object> getBackupData()
        {
            
            Dictionary<int, Backupdata_Object> Dic = new Dictionary<int, Backupdata_Object>();
            try
            {

                XDocument xmlDoc = GetXDocumentData("BAK", null);
                IEnumerable<XElement> xmlElmnts = from s in xmlDoc.Elements("BackupData").Elements("add") select s;
                int i = 0;
                foreach (XElement node in xmlElmnts)
                {
                    var objBk = new Backupdata_Object();
                    objBk.getkey = Guid.Parse(DCC.ToString(node.Attribute("Key")));
                    objBk.getprsStatus = DCC.ToString(node.Attribute("prsStatus"));
                    objBk.getdbName = ProtectData.DecryptData(DCC.ToString(node.Attribute("dbName")));
                    objBk.gettbName = ProtectData.DecryptData(DCC.ToString(node.Attribute("tbName")));
                    objBk.getmoduleName = ProtectData.DecryptData(DCC.ToString(node.Attribute("moduleName")));
                    objBk.gettransId = DCC.ToString(node.Attribute("transId"));
                    objBk.getuserName = DCC.ToString(node.Attribute("userName"));
                    objBk.getdateTime = DCC.ToString(node.Attribute("dateTime")).ToDateTime();
                    objBk.gettypeData = ProtectData.DecryptData(DCC.ToString(node.Attribute("typeData")));
                    objBk.getdata = ProtectData.DecryptData(DCC.ToString(node.Attribute("data")));
                    
                    Dic.Add(i, objBk);
                    i++;
                    
                }
                return Dic;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.Controller.CacheController.WriteConnectionToMemory()";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static XDocument GetXDocumentData(string fileName, string folPath = null)
        {
            try
            {
                string resourceFile = fileName;

                string resourceFolder = folPath ?? ".\\App_Layouts\\";

                    resourceFile += ".DAT";

                return XDocument.Load(resourceFolder + resourceFile);
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.DataMemory.ApplicationCore.GetXDocumentData(string fileName, string folPath = null)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }*/
    }
}
