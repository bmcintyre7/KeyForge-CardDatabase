using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace KeyforgeCardPoster {
    [Serializable]
    public class CardExpansion {
        public String expansion { get; set; }
        public int number { get; set; }

        public CardExpansion(String _e, int _n) {
            expansion = _e;
            number = _n;
        }
    }

    [Serializable]
    public class DTOCard {
        public String name { get; set; }
        public String type { get; set; }
        public String text { get; set; }
        public int? aember { get; set; }
        public int? power { get; set; }
        public int? armor { get; set; }
        public String rarity { get; set; }
        public String artist { get; set; }
        public List<String> expansions { get; set; }
        public List<String> houses { get; set; }
        public List<String> keywords { get; set; }
        public List<String> traits { get; set; }

        public DTOCard() {
            name = String.Empty;
            type = String.Empty;
            text = String.Empty;
            aember = null;
            power = null;
            armor = null;
            rarity = String.Empty;
            artist = String.Empty;
            expansions = new List<String>();
            houses = new List<String>();
            keywords = new List<String>();
            traits = new List<String>();
        }
    }

    public class Card {
        public String name { get; set; }
        public String type { get; set; }
        public String text { get; set; }
        public String aember { get; set; }
        public String power { get; set; }
        public String armor { get; set; }
        public String rarity { get; set; }
        public String artist { get; set; }
        public String setAndNumber { get; set; }
        public String house { get; set; }
        public String keyword { get; set; }
        public String traits { get; set; }

        public Card() {
            name = String.Empty;
            type = String.Empty;
            text = String.Empty;
            aember = String.Empty;
            power = String.Empty;
            armor = String.Empty;
            rarity = String.Empty;
            artist = String.Empty;
            setAndNumber = String.Empty;
            house = String.Empty;
            keyword = String.Empty;
            traits = String.Empty;
        }
    }

    [Serializable]
    public class DTOCardList {
        public List<DTOCard> cards { get; set; }

        public DTOCardList() {
            cards = new List<DTOCard>();
        }
    }

    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            String[] setFiles = Directory.GetFiles(Environment.CurrentDirectory, "*.json");

            foreach (String s in setFiles) {
                int start = s.LastIndexOf('\\') + 1;
                int end = s.LastIndexOf('.') - start;
                listBox1.Items.Add(s.Substring(start, end));
            }
        }

        private void SendSet(ref DTOCardList _set) {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://localhost:7230/cards");
            wr.Method = "POST";
            wr.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(wr.GetRequestStream())) {
                string json = new JavaScriptSerializer().Serialize(_set);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try {
                HttpWebResponse resp = (HttpWebResponse)wr.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                String response = sr.ReadToEnd();
                MessageBox.Show(response);
                int dmg = 0;
            }
            catch (Exception _e) {
                MessageBox.Show(_e.Message);
            }
        }


        private void SendSet(DTOCard card) {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://localhost:7230/cards");
            wr.Method = "POST";
            wr.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(wr.GetRequestStream())) {
                string json = new JavaScriptSerializer().Serialize(card);
                //MessageBox.Show("SENDING: " + json);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try {
                HttpWebResponse resp = (HttpWebResponse)wr.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                String response = sr.ReadToEnd();
                //MessageBox.Show(response);
                int dmg = 0;
            }
            catch (Exception _e) {
                MessageBox.Show(_e.Message);
            }


        }

        private DTOCard CardToDTOCard(Card _c) {
            DTOCard ret = new DTOCard();
            if (null != _c.aember)
                ret.aember = int.Parse(_c.aember.Substring(_c.aember.IndexOf(' ')));
            if (null != _c.power)
                ret.power = int.Parse(_c.power.Substring(_c.power.IndexOf(' ')));
            if (null != _c.armor)
                ret.armor = int.Parse(_c.armor.Substring(_c.armor.IndexOf(' ')));
            if (null != _c.artist) {
                if (_c.artist.Contains("Art by "))
                    ret.artist = _c.artist.Substring(_c.artist.IndexOf("Art By ") + "Art By ".Length + 1);
            }
            if (null != _c.house) {
                String[] houses = _c.house.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in houses)
                    ret.houses.Add(s);
            }
            if (null != _c.keyword) {
                String[] keywords = _c.keyword.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in keywords)
                    ret.keywords.Add(s);
            }
            if (null != _c.traits) {
                String[] traits = _c.traits.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (String s in traits)
                    ret.traits.Add(s);
            }
            if (null != _c.setAndNumber) {
                String[] setAndNumber = _c.setAndNumber.Split(" #".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                ret.expansions.Add(_c.setAndNumber);//new CardExpansion(setAndNumber[0], int.Parse(setAndNumber[1])));
            }
            if (null != _c.name)
               ret.name = System.Web.HttpUtility.HtmlDecode(Uri.UnescapeDataString(_c.name));
            if (null != _c.text)
                ret.text = System.Web.HttpUtility.HtmlDecode(Uri.UnescapeDataString(_c.text));
            if (null != _c.type)
                ret.type = _c.type;
            if (null != _c.rarity)
                ret.rarity = _c.rarity;

            return ret;
        }

        private void button1_Click(object sender, EventArgs e) {
            foreach (Object o in listBox1.SelectedItems) {
                String fileName = o.ToString() + ".json";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                String json = File.ReadAllText(fileName);

                List<Card> cards = jss.Deserialize<List<Card>>(json);

                DTOCardList dtoCards = new DTOCardList();
                for (int i = 0; i < cards.Count; ++i) {// (Card c in cards) {
                    SendSet(CardToDTOCard(cards[i]));
                }
                //dtoCards.cards.Add(CardToDTOCard(cards[0]));
                //dtoCards.cards.Add(CardToDTOCard(cards[1]));

                SendSet(ref dtoCards);
            }
        }
    }
}
