using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace XmlPractice
{
    public partial class Test : System.Web.UI.Page
    {
        string filePathForXML, xmlString = "", xml;
        string Name, PhNo, Address;
        BLLManager bLL = new BLLManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            //filePathForXML = Server.MapPath("Person.xml");
            filePathForXML = Server.MapPath("~/Data/AddPerson_" + "11621" + ".xml");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Person aperson = new Person();

            /*Name = txtName.Text;
            PhNo = txtPhNo.Text;
            Address = txtAddress.Text;

            CreateAddXml(Name, PhNo, Address);*/

            aperson.Name = txtName.Text;
            aperson.PhNo = txtPhNo.Text;
            aperson.Address = txtAddress.Text;

            CreateAddXml(aperson);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePathForXML);

            XmlNode dSftTm = doc.SelectSingleNode("Person");
            string xmlString = dSftTm.InnerXml;

            xmlString = "<Person>" + xmlString + "</Person>";

            string message = bLL.PersonInfoInsert(xmlString);

            File.Delete(filePathForXML);
        }

        private void CreateAddXml(Person aperson)
        {
            XmlDocument doc = new XmlDocument();
            if(System.IO.File.Exists(filePathForXML))
            {
                doc.Load(filePathForXML);

                XmlNode rootNode = doc.SelectSingleNode("Person");
                XmlNode addItem = CreateItemNodeDocUpload(doc, aperson);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclarationNode = doc.CreateXmlDeclaration("1.0", "UTF-8", "");
                doc.AppendChild(xmldeclarationNode);

                XmlNode rootNode = doc.CreateElement("Person");
                XmlNode addItem = CreateItemNodeDocUpload(doc, aperson);
                rootNode.AppendChild(addItem);

                doc.AppendChild(rootNode);

            }

            doc.Save(filePathForXML);
            LoadGridwithXml();
        }
       /* private void CreateAddXml(string Name, string PhNo, string Address)
        {
            

                XmlDocument doc = new XmlDocument();

                if (System.IO.File.Exists(filePathForXML))

                {
                    doc.Load(filePathForXML);
                    XmlNode rootNode = doc.SelectSingleNode("Person");
                    XmlNode addItem = CreateItemNodeDocUpload(doc, Name, PhNo, Address);
                    rootNode.AppendChild(addItem);
                }
                else
                {
                    XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                    doc.AppendChild(xmldeclerationNode);
                    XmlNode rootNode = doc.CreateElement("Person");
                    XmlNode addItem = CreateItemNodeDocUpload(doc, Name, PhNo, Address);
                    rootNode.AppendChild(addItem);
                    doc.AppendChild(rootNode);
                }

                doc.Save(filePathForXML);
                LoadGridwithXml();

        }*/
        private void LoadGridwithXml()
        {
           /* try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathForXML);
                XmlNode xlnd = doc.SelectSingleNode("Person");
                xmlString = xlnd.InnerXml;
                xmlString = "<Person>" + xmlString + "</Person>";
                StringReader sr = new StringReader(xmlString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dgvPerson.DataSource = ds;
                }
                else
                {
                    dgvPerson.DataSource = "";
                }
                dgvPerson.DataBind();
            }

            catch
            {
                dgvPerson.DataSource = "";
                dgvPerson.DataBind();
            }
            */
            
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathForXML);

                XmlNode xlnd = doc.SelectSingleNode("Person");
                xmlString = xlnd.InnerXml;

                xmlString = "<Person>" + xmlString + "</Person>";
                StringReader sr = new StringReader(xmlString);

                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                if (ds.Tables[0].Rows.Count > 0)
                    dgvPerson.DataSource = ds;

                else
                    dgvPerson.DataSource = "";

                dgvPerson.DataBind();
            }
            catch
            {
                dgvPerson.DataSource = "";
                dgvPerson.DataBind();
            }
            
        }
        private XmlNode CreateItemNodeDocUpload(XmlDocument doc, Person aperson)
        {
           /* XmlNode node = doc.CreateElement("Persons");

            XmlAttribute name = doc.CreateAttribute("Name"); name.Value = Name;
            XmlAttribute phNo = doc.CreateAttribute("PhNo"); phNo.Value = PhNo;
            XmlAttribute address = doc.CreateAttribute("Address"); address.Value = Address;

            node.Attributes.Append(name);
            node.Attributes.Append(phNo);
            node.Attributes.Append(address);
            return node;*/
            
            XmlNode node = doc.CreateElement("ABC");
            
            XmlAttribute Name = doc.CreateAttribute("Name");
            Name.Value = aperson.Name;
            node.Attributes.Append(Name);

            XmlAttribute Phone = doc.CreateAttribute("PhNo");
            Phone.Value = aperson.PhNo;
            node.Attributes.Append(Phone);

            XmlAttribute Address = doc.CreateAttribute("Address");
            Address.Value = aperson.Address;
            node.Attributes.Append(Address);

            return node;
            

        }
        protected void dgvPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePathForXML);

                XmlNode dSftTm = doc.SelectSingleNode("Person");
                xmlString = dSftTm.InnerXml;
                xmlString = "<Person>" + xmlString + "</Person>";
                StringReader sr = new StringReader(xmlString);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                dgvPerson.DataSource = ds;

                DataSet dsGrid = (DataSet)dgvPerson.DataSource;
                dsGrid.Tables[0].Rows[dgvPerson.Rows[e.RowIndex].DataItemIndex].Delete();
                dsGrid.WriteXml(filePathForXML);
                DataSet dsGridAfterDelete = (DataSet)dgvPerson.DataSource;

                if (dsGridAfterDelete.Tables[0].Rows.Count <= 0)
                {
                    File.Delete(filePathForXML); dgvPerson.DataSource = ""; dgvPerson.DataBind();
                }
                else { LoadGridwithXml(); }
            }
            catch { }
        }











    }
}