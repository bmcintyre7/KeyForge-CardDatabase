using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace LibraryAccessService
{
    class Program
    {
        //private static String Serialize(Book emp)
        //{
        //    try
        //    {
        //        String XmlizedString = null;
        //        XmlSerializer xs = new XmlSerializer(typeof(Book));
        //        //create an instance of the MemoryStream class since we intend to keep the XML string 
        //        //in memory instead of saving it to a file.
        //        MemoryStream memoryStream = new MemoryStream();
        //        //XmlTextWriter - fast, non-cached, forward-only way of generating streams or files 
        //        //containing XML data
        //        XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        //        //Serialize emp in the xmlTextWriter
        //        xs.Serialize(xmlTextWriter, emp);
        //        //Get the BaseStream of the xmlTextWriter in the Memory Stream
        //        memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        //        //Convert to array
        //        XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
        //        return XmlizedString;
        //    }
        //    catch (Exception ex)
        //    {
        //        String s = ex.Message;
        //        //WriteResponse("Error in SERIALIZE");
        //        //errHandler.ErrorMessage = ex.Message.ToString();
        //        int dbg = 0;
        //        return s;
        //    }
        //
        //}

        private static  String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        static bool detailedView = false;
        static void Main(string[] args)
        {
           String connString = @"Server=50.62.209.16;Database=keyforgedb;Uid=remm;password=Pass1;";
           DAL d = new DAL(connString);
           
           //
           // User u = d.GetUser("riek.account@gmail.com");
           // d.GetUsersBooks(u);
           // List<Book> booksOnly = d.GetUsersBooksList(u);

#if !detailedView // Show Book List
            //for(int i = 0; i < booksOnly.Count; ++i)
            //{
            //    Console.Write(booksOnly[i].series + "\n");
            //    String s = Serialize(booksOnly[i]);
            //    Console.WriteLine(s);
            //}
#endif // Show Book List

#if detailedView // Show Detailed Books
            Console.Write("id: " + u.id + '\n');
            Console.Write("firstName: " + u.firstName + '\n');
            Console.Write("lastName: " + u.lastName + '\n');
            Console.Write("email: " + u.email + '\n');
            Console.Write("\n");

            //  Book.series
            //  Book.issueNumber
            //  Book.cover
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL 
            for (int j = 0; j < u.books.Count; ++j)
            {
                Console.Write("series: " + u.books[j].series + '\n');
                Console.Write("issueNumber: " + u.books[j].issueNumber + '\n');
                Console.Write("cover: " + u.books[j].cover + '\n');
                Console.Write("grade: " + u.books[j].grade + '\n');
                Console.Write("grader: " + u.books[j].grader + '\n');
                Console.Write("value: " + u.books[j].value + '\n');
                Console.Write("imageURL: " + u.books[j].imageURL + '\n');
                for (int k = 0; k < u.books[j].artists.Count; ++k)
                    Console.Write("artist: " + u.books[j].artists[k] + '\n');
                for (int k = 0; k < u.books[j].coverArtists.Count; ++k)
                    Console.Write("coverArtist: " + u.books[j].coverArtists[k] + '\n');
                for (int k = 0; k < u.books[j].writers.Count; ++k)
                    Console.Write("writer: " + u.books[j].writers[k] + '\n');
                for (int k = 0; k < u.books[j].signatures.Count; ++k)
                    Console.Write("signature: " + u.books[j].signatures[k] + '\n');
                Console.Write("\n");
            }

            for (int i = 0; i < DAL.users.Count; ++i)
            {
                //  User.id
                //  User.firstName
                //  User.lastName
                //  User.email
                Console.Write("id: " + DAL.users[i].id + '\n');
                Console.Write("firstName: " + DAL.users[i].firstName + '\n');
                Console.Write("lastName: " + DAL.users[i].lastName + '\n');
                Console.Write("email: " + DAL.users[i].email + '\n');
                Console.Write("\n");

                //  Book.series
                //  Book.issueNumber
                //  Book.cover
                //  Book.grade
                //  Book.grader
                //  Book.value
                //  Book.imageURL 
                for (int j = 0; j < DAL.users[i].books.Count; ++j)
                {
                    Console.Write("series: " + DAL.users[i].books[j].series + '\n');
                    Console.Write("issueNumber: " + DAL.users[i].books[j].issueNumber + '\n');
                    Console.Write("cover: " + DAL.users[i].books[j].cover + '\n');
                    Console.Write("grade: " + DAL.users[i].books[j].grade + '\n');
                    Console.Write("grader: " + DAL.users[i].books[j].grader + '\n');
                    Console.Write("value: " + DAL.users[i].books[j].value + '\n');
                    Console.Write("imageURL: " + DAL.users[i].books[j].imageURL + '\n');
                    for(int k = 0; k < DAL.users[i].books[j].artists.Count; ++k)
                        Console.Write("artist: " + DAL.users[i].books[j].artists[k] + '\n');
                    for (int k = 0; k < DAL.users[i].books[j].coverArtists.Count; ++k)
                        Console.Write("coverArtist: " + DAL.users[i].books[j].coverArtists[k] + '\n');
                    for (int k = 0; k < DAL.users[i].books[j].writers.Count; ++k)
                        Console.Write("writer: " + DAL.users[i].books[j].writers[k] + '\n');
                    for (int k = 0; k < DAL.users[i].books[j].signatures.Count; ++k)
                        Console.Write("signature: " + DAL.users[i].books[j].signatures[k] + '\n');
                    Console.Write("\n");
                }
            }
#endif // Show Detailed Books

            Console.ReadLine();
        }
    }
}
