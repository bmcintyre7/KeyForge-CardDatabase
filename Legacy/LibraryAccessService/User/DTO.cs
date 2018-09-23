using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LibraryAccessService
{
    [Serializable]
    public class Card {
        public int? id                       { get; set; }
        public String name                  { get; set; }
        public String type                  { get; set; }
        public String text                  { get; set; }
        public int? aember                   { get; set; }
        public int? power                    { get; set; }
        public int? armor                    { get; set; }
        public String rarity                { get; set; }
        public String artist                { get; set; }
        public List<String> expansions      { get; set; }
        public List<String> houses          { get; set; }
        public List<String> keywords        { get; set; }
        public List<String> traits          { get; set; }

        public Card() {
            id = null;
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

    [Serializable]
    public class CardList {
        public List<Card> cards                    { get; set; }

        public CardList() {
            cards = new List<Card>();
        }
    }
/*
    [Serializable]
    [XmlRoot("Book")]
    public class Book
    {
        public int id                       { get; set; }
        public String series                { get; set; }
        public String imageURL              { get; set; }
        public char cover                   { get; set; }
        public Decimal issueNumber          { get; set; }
        public Decimal grade                { get; set; }
        public Decimal value                { get; set; }
        public String grader                { get; set; }
        public String gradeCode             { get; set; }
        public String isbn                  { get; set; }
        public String publisher             { get; set; }
        public int pageCount                { get; set; }

        [XmlArray("Signatures")]
        public List<String> signatures      { get; set; }
        [XmlArray("Writers")]
        public List<String> writers         { get; set; }
        [XmlArray("Artists")]
        public List<String> artists         { get; set; }
        [XmlArray("CoverArtists")]
        public List<String> coverArtists    { get; set; }

        public Book()
        {
            signatures      = new List<String>();
            writers         = new List<String>();
            artists         = new List<String>();
            coverArtists    = new List<String>();
            grader = "Unassigned";
            cover = 'A';
            value = -1;
            grade = -1;
            isbn = "";
            publisher = "";
            pageCount = -1;
        }
    }

    [Serializable]
    [XmlRoot("HttpBook")]
    public class HttpBook : Book
    {
        public String sender { get; set; }

        public HttpBook()
        {
            signatures = new List<String>();
            writers = new List<String>();
            artists = new List<String>();
            coverArtists = new List<String>();
            grader = "Unassigned";
            cover = 'A';
            value = -1;
            grade = -1;
            isbn = "";
            publisher = "";
            pageCount = -1;
        }

        public Book GetBook()
        {
            Book b = new Book();
            b.artists = this.artists;
            b.cover = this.cover;
            b.coverArtists = this.coverArtists;
            b.grade = this.grade;
            b.gradeCode = this.gradeCode;
            b.grader = this.grader;
            b.isbn = this.isbn;
            b.publisher = this.publisher;
            b.pageCount = this.pageCount;
            b.id = this.id;
            b.imageURL = this.imageURL;
            b.issueNumber = this.issueNumber;
            b.series = this.series;
            b.signatures = this.signatures;
            b.value = this.value;
            b.writers = this.writers;
            return b;
        }
    }

    [Serializable]
    [XmlRoot("BulkAdd")]
    public class BulkAdd
    {
        public String sender;
        public String series;
        public String issues;

        public BulkAdd()
        {
            sender = "";
            series = "";
            issues = "";
        }

        public List<Decimal> IssueList
        {
            get
            {
                List<Decimal> ret = new List<Decimal>();
                String[] nums = issues.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    foreach (String c in nums)
                        ret.Add(Convert.ToDecimal(c.Trim()));
                }
                catch (Exception e)
                {
                    throw new IssueListException("Issue List Format Error: Be sure issue numbers are comma separated (ex. 1, 2, 3, 4, etc. [spaces not necessary])");
                }
                return ret;
            }
        }
    }

    public class IssueListException : Exception
    {
        public IssueListException(String _message) : base(_message)
        {
        }
    }

    [Serializable]
    [XmlRoot("User")]
    public class User
    {
        public int id               { get; set; }
        public String firstName     { get; set; }
        public String lastName      { get; set; }
        public String email         { get; set; }
        public DateTime signupDate  { get; set; }

        private List<Book> books;

        [XmlElement("Books")]
        public List<Book> Books
        {
            get { return books; }
            set { books = value; }
        }

        public User()
        {
            books = new List<Book>();
        }
    }
    */
}
