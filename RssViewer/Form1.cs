using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RssViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Rss 文件抓取
        /// XDocument rssFeed = XDocument.Load("https://udn.com/rssfeed/news/2/6649?ch=news");
        /// 抓取網址內容
        /// Descendants("item") 子項目item
        /// where DateTime.Parse(item.Element("pubDate").Value)>DateTime.Now.AddHours(-1)
        /// 時間範圍抓取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            XDocument rssFeed = XDocument.Load("https://udn.com/rssfeed/news/2/6649?ch=news");
            var query =
                from item in rssFeed.Descendants("item")
                where DateTime.Parse(item.Element("pubDate").Value)>DateTime.Now.AddHours(-1)
                select new
                {
                    標題 = item.Element("title").Value,
                    內容=item.Element("description").Value,
                    連結=item.Element("link").Value,
                    發佈日期=DateTime.Parse(item.Element("pubDate").Value)
                };
            dataGridView1.DataSource = query.ToList();
                
        }


        /// <summary>
        /// 新聞連結開啟(dataGridView1.Rows[e.RowIndex].Cells[2].Value)
        /// webBrowser1.Url = new Uri(link);
        /// 從屬性判段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string link = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            webBrowser1.Url = new Uri(link);
        }
    }
}
