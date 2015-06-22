using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ghostSCSIM.XML
{
    
    public class WarehouseStock
    {
        [XmlElement("article")]
        public List<Article> article = new List<Article>();

        public int getIndexOfArticleById(int id)
        {
            int index = 0;
            foreach (Article a in this.article)
            {
                if (a.id.Equals(id))
                {
                    index = article.IndexOf(a);
                }
            }
            return index;
        }
    }

    public class Article
    {
        [XmlAttribute]
        public int id { get ; set; }

        [XmlAttribute]
        public int amount { get; set; }

        [XmlAttribute]
        public int startamount { get; set; }
        
        [XmlIgnore]
        public decimal pct { get; set; }

        [XmlAttribute("pct")]
        public string pctFormatted
        {
            get { return pct.ToString(CultureInfo.GetCultureInfo("de-DE").NumberFormat); }
            set { pct = decimal.Parse(value, CultureInfo.GetCultureInfo("de-DE").NumberFormat); }
        }

        [XmlIgnore]
        public decimal price { get; set; }

        [XmlAttribute("price")]
            public string priceFormatted {
                get { return price.ToString(CultureInfo.GetCultureInfo("de-DE").NumberFormat); }
                set { price = decimal.Parse(value, CultureInfo.GetCultureInfo("de-DE").NumberFormat); } 
            }

        [XmlIgnore]
        public decimal stockvalue { get; set; }

        [XmlAttribute("stockvalue")]
        public string stockvalueFormatted
        {
            get { return stockvalue.ToString(CultureInfo.GetCultureInfo("de-DE").NumberFormat); }
            set { stockvalue = decimal.Parse(value, CultureInfo.GetCultureInfo("de-DE").NumberFormat); }
        }

        
    }
}
