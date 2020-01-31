using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XmlPractice.DALTableAdapters;

namespace XmlPractice
{
    public class BLLManager
    {
        string meg;
        public string PersonInfoInsert(string xml)
        {
            sprPersonInfoTableAdapter a = new sprPersonInfoTableAdapter();
            a.PersonInfoInsert(xml);
            return meg="successful";
        }
    }
}