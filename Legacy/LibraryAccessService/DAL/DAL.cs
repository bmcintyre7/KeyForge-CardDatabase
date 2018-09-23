using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LibraryAccessService
{
    public class DAL
    {
        private MySqlConnection conn;
        private static string connString;
        private MySqlCommand command;
        //public static List<User> users = new List<User>();
        Dictionary<String, String> identifierQueries = new Dictionary<String, String>();
        Dictionary<String, String> valueQueries = new Dictionary<String, String>();
        //private static List<Employee.Employee> empList;
        //private ErrorHandler.ErrorHandler err;

        public DAL(string _connString)
        {
            //err = new ErrorHandler.ErrorHandler();
            connString = _connString;

            // Possible Identifiers:
            //  signature
            //      - string name specified
            //  writer
            //      - string name specified
            //  artist
            //      - string name specified
            // coverartist
            //      - string name specified
            //  grade
            //      - float grade specified
            //  grader
            //      - string grader specified
            //  series
            //      - string series specified
            //  issue
            //      - float issue number specified


            identifierQueries.Add("grade", "userbook.grade");
            identifierQueries.Add("grader", "grader.name");
            identifierQueries.Add("series", "series.name");
            identifierQueries.Add("issue", "book.issuenumber");
            identifierQueries.Add("count", "count(*)");
            identifierQueries.Add("id", "userbook.id");

            valueQueries.Add("series", "series.name");
            valueQueries.Add("signatures", "creator.name");
            valueQueries.Add("books", "count(*)");
            valueQueries.Add("users", "count(*)");
            valueQueries.Add("newest", "concat(firstname, ' ', lastname)");
        }

        /*
        public void GetUsers()
        {
            conn = new MySqlConnection(connString);

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";
            users.Clear();

            string sqlSelectString = "select user.id, firstname, lastname, email, signupdate from user";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL
            //  Book.issueNumber
            //  Book.cover
            while (reader.Read())
            {
                User user = new User();
                user.id = Convert.ToInt32(reader[0]);
                user.firstName = reader[1].ToString();
                user.lastName = reader[2].ToString();
                user.email = reader[3].ToString();
                user.signupDate = Convert.ToDateTime(reader[4].ToString());

                //Book book = new Book();
                //book.series = reader[4].ToString();
                //book.grade = (float)Convert.ToDecimal(reader[5]);
                //book.grader = reader[6].ToString();
                //book.value = (float)Convert.ToDecimal(reader[7]);
                //book.imageURL = reader[8].ToString();
                //book.issueNumber = Convert.ToInt32(reader[9]);
                //book.cover = reader[10].ToString()[0];
                //user.books.Add(book);

                users.Add(user);
            }
            command.Connection.Close();
            conn.Close();
        }

        //public void GetUser(String _email)
        //{
        //    conn = new MySqlConnection(connString);

        //    //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
        //    //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
        //    //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
        //    //    "series on series.id = book.series join grader on grader.id = userbook.grader;";
        //    users.Clear();

        //    string sqlSelectString = "select user.id, firstname, lastname, email, signupdate from user where email = '" + _email + "';";
        //    command = new MySqlCommand(sqlSelectString, conn);
        //    //if (newConnection)
        //    command.Connection.Open();

        //    MySqlDataReader reader = command.ExecuteReader();
        //    // Order:
        //    //  User.id
        //    //  User.firstName
        //    //  User.lastName
        //    //  User.email
        //    //
        //    //  Book.series
        //    //  Book.grade
        //    //  Book.grader
        //    //  Book.value
        //    //  Book.imageURL
        //    //  Book.issueNumber
        //    //  Book.cover
        //    while (reader.Read())
        //    {
        //        User user = new User();
        //        user.id = Convert.ToInt32(reader[0]);
        //        user.firstName = reader[1].ToString();
        //        user.lastName = reader[2].ToString();
        //        user.email = reader[3].ToString();
        //        user.signupDate = Convert.ToDateTime(reader[4].ToString());

        //        //Book book = new Book();
        //        //book.series = reader[4].ToString();
        //        //book.grade = (float)Convert.ToDecimal(reader[5]);
        //        //book.grader = reader[6].ToString();
        //        //book.value = (float)Convert.ToDecimal(reader[7]);
        //        //book.imageURL = reader[8].ToString();
        //        //book.issueNumber = Convert.ToInt32(reader[9]);
        //        //book.cover = reader[10].ToString()[0];
        //        //user.books.Add(book);

        //        users.Add(user);
        //    }
        //    command.Connection.Close();
        //}

        public String AddBook(int _userID, Book _b)
        {
            conn = new MySqlConnection(connString);
            string ret = "";

            #region SERIES
            string sqlString;
            MySqlDataReader reader;
            if (!exists("series", _b.series))
            {
                command.Connection.Close();
                // Insert the series if necessary
                sqlString = "insert into series (name) values ('" + _b.series + "');";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            command.Connection.Close();
            #endregion

            #region CREATORS
            // Insert any new creators
            List<String> allNew = new List<String>();
            for (int i = 0; i < _b.coverArtists.Count; i++)
                allNew.Add(_b.coverArtists[i]);
            //sqlString += "('" + _b.coverArtists[i] + "'), ";
            for (int i = 0; i < _b.writers.Count; i++)
                allNew.Add(_b.writers[i]);
            //sqlString += "('" + _b.writers[i] + "'), ";
            for (int i = 0; i < _b.artists.Count; i++)
                allNew.Add(_b.artists[i]);
            //sqlString += "('" + _b.artists[i] + "'), ";
            for (int i = 0; i < _b.signatures.Count; i++)
                allNew.Add(_b.signatures[i]);
            for (int i = 0; i < allNew.Count; ++i)
            {
                if (exists("creator", allNew[i]))
                    continue;

                sqlString = "insert into creator (name) values ('" + allNew[i] + "');";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            #endregion

            #region BOOK
            // Series ID
            // Issue Number
            // Cover (A, B, C, etc.)
            // Image URL
            if(!bookExists(_b))
            {
                //ret += " - BOOK DOESN'T EXIST - ";
                sqlString = "insert into book (series, issuenumber, cover, imageurl) values ((select id from series where name = '" + _b.series + "'), " + _b.issueNumber + ", '" + _b.cover + "', '" + _b.imageURL + "');";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            #endregion

            #region USERBOOK
            int userbookID = -1;
            if (!userbookExists(_userID, _b, ref userbookID))
            {
                sqlString = "select id from series where name = '" + _b.series + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int seriesID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                //ret += " - BOOK DOESN'T EXIST - ";
                sqlString = "insert into userbook (`book`, `grade`, `gradecode`, `grader`, `value`, `owner`, `isbn`, `publisher`, `pagecount`) values (" +
                    "(select id from book where series = '" + seriesID + "' and issuenumber = '" + _b.issueNumber + "' and cover = '" + _b.cover + "' and imageurl = '" + _b.imageURL + "'), '" + 
                    _b.grade + "', '" + _b.gradeCode + "', (select id from grader where name = '" + _b.grader + "'), '" + _b.value + "', '" + _userID + "', '" + _b.isbn + "', '" + _b.publisher + "', '" + _b.pageCount + "');";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                userbookID = (int)command.LastInsertedId;
                command.Connection.Close();
            }
            ret += userbookID;
            #endregion

            #region COVERARTISTS
            for (int i = 0; i < _b.coverArtists.Count; i++)
            {
                sqlString = "select id from creator where name = '" + _b.coverArtists[i] + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int creatorID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                sqlString = "select count(*) from bookcoverartist where creator = '" + creatorID + "' and userbook = '" + userbookID + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() != "0")
                {
                    command.Connection.Close();
                    continue;
                }
                command.Connection.Close();

                sqlString = "insert into bookcoverartist (creator, userbook) values ((select id from creator where name = '" + _b.coverArtists[i] + "'), " + userbookID + ");";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }
            #endregion

            #region WRITERS
            ret += "WRITERS";
            for (int i = 0; i < _b.writers.Count; i++)
            {
                sqlString = "select id from creator where name = '" + _b.writers[i] + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int creatorID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                sqlString = "select count(*) from bookwriter where creator = '" + creatorID + "' and userbook = '" + userbookID + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() != "0")
                {
                    command.Connection.Close();
                    continue;
                }
                command.Connection.Close();

                sqlString = "insert into bookwriter (creator, userbook) values ((select id from creator where name = '" + _b.writers[i] + "'), " + userbookID + ");";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }
            #endregion

            #region ARTISTS
            ret += "ARTISTS";
            for (int i = 0; i < _b.artists.Count; i++)
            {
                sqlString = "select id from creator where name = '" + _b.artists[i] + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int creatorID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                sqlString = "select count(*) from bookartist where creator = '" + creatorID + "' and userbook = '" + userbookID + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() != "0")
                {
                    command.Connection.Close();
                    continue;
                }
                command.Connection.Close();

                sqlString = "insert into bookartist (creator, userbook) values ((select id from creator where name = '" + _b.artists[i] + "'), " + userbookID + ");";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }
            #endregion

            #region SIGNATURES
            ret += "SIGS";
            for (int i = 0; i < _b.signatures.Count; i++)
            {
                sqlString = "select id from creator where name = '" + _b.signatures[i] + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int creatorID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                sqlString = "select count(*) from booksignature where creator = '" + creatorID + "' and userbook = '" + userbookID + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() != "0")
                {
                    command.Connection.Close();
                    continue;
                }
                command.Connection.Close();

                sqlString = "insert into booksignature (creator, userbook) values ((select id from creator where name = '" + _b.signatures[i] + "'), " + userbookID + ");";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }
            #endregion
            conn.Close();
            return ret;
        }

        public String AddBooks(int _userID, BulkAdd _b)
        {
            conn = new MySqlConnection(connString);
            string ret = "";

            #region SERIES
            string sqlString;
            MySqlDataReader reader;
            if (!exists("series", _b.series))
            {
                command.Connection.Close();
                // Insert the series if necessary
                sqlString = "insert into series (name) values ('" + _b.series + "');";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            command.Connection.Close();
            #endregion

            List<Decimal> allIssues = new List<Decimal>();
            try
            {
                allIssues = _b.IssueList;
            }
            catch(IssueListException e)
            {
                conn.Close();
                throw new IssueListException(e.Message);
            }
            
            for (int i = 0; i < allIssues.Count; ++i)
            {
                Book b = new Book();
                b.issueNumber = allIssues[i];
                b.cover = 'A';
                b.series = _b.series;

                #region BOOK
                // Series ID
                // Issue Number
                // Cover (A, B, C, etc.)
                // Image URL
                if (!bookExists(b))
                {
                    //ret += " - BOOK DOESN'T EXIST - ";
                    sqlString = "insert into book (series, issuenumber, cover, imageurl) values ((select id from series where name = '" + b.series + "'), " + b.issueNumber + ", '" + b.cover + "', '" + b.imageURL + "');";
                    //ret += sqlString;
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
                #endregion

                #region USERBOOK
                int userbookID = -1;
                if (!userbookExists(_userID, b, ref userbookID))
                {
                    sqlString = "select id from series where name = '" + _b.series + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    reader.Read();
                    int seriesID = Convert.ToInt32(reader[0].ToString());
                    command.Connection.Close();

                    //ret += " - BOOK DOESN'T EXIST - ";
                    sqlString = "insert into userbook (`book`, `grade`, `gradecode`, `grader`, `value`, `owner`, `isbn`, `publisher`, `pagecount`) values (" +
                        "(select id from book where series = '" + seriesID + "' and issuenumber = '" + b.issueNumber + "' and cover = '" + b.cover + "' and imageurl = '" + b.imageURL + "'), '" +
                        b.grade + "', '" + b.gradeCode + "', (select id from grader where name = '" + b.grader + "'), '" + b.value + "', '" + _userID + "', '" + b.isbn + "', '" + b.publisher + "', '" + b.pageCount + "');";
                    //ret += sqlString;
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    command.ExecuteScalar();
                    userbookID = (int)command.LastInsertedId;
                    command.Connection.Close();
                }
                ret += userbookID;
                #endregion
            }
            conn.Close();
            return ret;
        }

        public String UpdateBook(int _userID, Book _b)
        {
            conn = new MySqlConnection(connString);
            string ret = "";

            Book oldBook = GetBook(_b.id);
            string sqlString;
            MySqlDataReader reader;

            int bookID = -1;
            #region SERIES
            if (_b.series != oldBook.series)
            {
                int seriesID = -1;
                if (!exists("series", _b.series))
                {
                    ret += "Series Added  -  ";
                    command.Connection.Close();
                    // Insert the series if necessary
                    sqlString = "insert into series (name) values ('" + _b.series + "');";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();

                    int newSeriesID = 0;

                    sqlString = "select id from series where series.name = '" + _b.series + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        newSeriesID = Convert.ToInt32(reader[0]);
                        seriesID = newSeriesID;
                        ret += " newSeriesID = " + newSeriesID + "  -  ";
                    }
                    command.Connection.Close();
                }

                sqlString = "select book from userbook where userbook.id = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bookID = Convert.ToInt32(reader[0]);
                    ret += " bookID = " + bookID + "  -  ";
                }
                command.Connection.Close();

                if (seriesID == -1)
                {
                    sqlString = "select id from series where series.name = '" + _b.series + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        seriesID = Convert.ToInt32(reader[0]);
                        ret += " seriesID = " + bookID + "  -  ";
                    }
                    command.Connection.Close();
                }

                sqlString = "update book set series = " + seriesID + " where book.id = " + bookID + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            #endregion

            #region CREATORS
            List<String> allCreators = new List<String>();
            for (int i = 0; i < _b.writers.Count; ++i)
                allCreators.Add(_b.writers[i]);
            for (int i = 0; i < _b.coverArtists.Count; ++i)
                allCreators.Add(_b.coverArtists[i]);
            for (int i = 0; i < _b.artists.Count; ++i)
                allCreators.Add(_b.artists[i]);
            for (int i = 0; i < _b.signatures.Count; ++i)
                allCreators.Add(_b.signatures[i]);

            for (int i = 0; i < allCreators.Count; ++i)
                if (!exists("creator", allCreators[i]))
                {
                    sqlString = "insert into creator (name) values ('" + allCreators[i] + "');";
                    //ret += sqlString;
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
            #endregion

            #region BOOK
            // Series ID
            // Issue Number
            // Cover (A, B, C, etc.)
            // Image URL
            if (_b.cover != oldBook.cover)
            {
                if (bookID == -1)
                {
                    sqlString = "select book from userbook where userbook.id = " + _b.id + ";";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bookID = Convert.ToInt32(reader[0]);
                        ret += " bookID = " + bookID + "  -  ";
                    }
                    command.Connection.Close();
                }

                //ret += " - BOOK DOESN'T EXIST - ";
                sqlString = "update book set cover = '" + _b.cover + "' where book.id = " + bookID + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if (_b.imageURL != oldBook.imageURL)
            {
                if (bookID == -1)
                {
                    sqlString = "select book from userbook where userbook.id = " + _b.id + ";";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bookID = Convert.ToInt32(reader[0]);
                        ret += " bookID = " + bookID + "  -  ";
                    }
                    command.Connection.Close();
                }

                //ret += " - BOOK DOESN'T EXIST - ";
                sqlString = "update book set imageURL = '" + _b.imageURL + "' where book.id = " + bookID + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if (_b.issueNumber != oldBook.issueNumber)
            {
                if (bookID == -1)
                {
                    sqlString = "select book from userbook where userbook.id = " + _b.id + ";";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bookID = Convert.ToInt32(reader[0]);
                        ret += " bookID = " + bookID + "  -  ";
                    }
                    command.Connection.Close();
                }

                //ret += " - BOOK DOESN'T EXIST - ";
                sqlString = "update book set issuenumber = " + _b.issueNumber + " where book.id = " + bookID + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            #endregion

            #region USERBOOK
            if (_b.value != oldBook.value)
            {
                sqlString = "update userbook set value = '" + _b.value + "' where userbook.id = " + _b.id + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if (_b.grader != oldBook.grader)
            {
                int graderID = -1;
                sqlString = "select id from grader where name = '" + _b.grader + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    graderID = Convert.ToInt32(reader[0]);
                    ret += " graderID = " + graderID + "  -  ";
                }
                command.Connection.Close();

                sqlString = "update userbook set grader = '" + graderID + "' where userbook.id = " + _b.id + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if(_b.isbn != oldBook.isbn)
            {
                sqlString = "update userbook set isbn = '" + _b.isbn + "' where userbook.id = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if(_b.publisher != oldBook.publisher)
            {
                sqlString = "update userbook set publisher = '" + _b.publisher + "' where userbook.id = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if(_b.pageCount != oldBook.pageCount)
            {
                sqlString = "update userbook set pagecount = '" + _b.pageCount + "' where userbook.id = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            if (_b.grader != "Unassigned")
            {
                if (_b.gradeCode != oldBook.gradeCode)
                {
                    sqlString = "update userbook set gradecode = '" + _b.gradeCode + "' where userbook.id = " + _b.id + ";";
                    //ret += sqlString;
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
                if (_b.grade != oldBook.grade)
                {
                    sqlString = "update userbook set grade = '" + _b.grade + "' where userbook.id = " + _b.id + ";";
                    //ret += sqlString;
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
            }
            else
            {
                sqlString = "update userbook set gradecode = '' where userbook.id = " + _b.id + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();

                sqlString = "update userbook set grade = 0 where userbook.id = " + _b.id + ";";
                //ret += sqlString;
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                command.Connection.Close();
            }
            //sqlString = "select id from series where name = '" + _b.series + "';";
            //command = new MySqlCommand(sqlString, conn);
            //command.Connection.Open();
            //reader = command.ExecuteReader();
            //reader.Read();
            //int seriesID = Convert.ToInt32(reader[0].ToString());
            //command.Connection.Close();

            ////ret += " - BOOK DOESN'T EXIST - ";
            //sqlString = "insert into userbook (`book`, `grade`, `gradecode`, `grader`, `value`, `owner`) values (" +
            //    "(select id from book where series = '" + seriesID + "' and issuenumber = '" + _b.issueNumber + "' and cover = '" + _b.cover + "' and imageurl = '" + _b.imageURL + "'), '" +
            //    _b.grade + "', '" + _b.gradeCode + "', (select id from grader where name = '" + _b.grader + "'), '" + _b.value + "', '" + _userID + "');";
            ////ret += sqlString;
            //command = new MySqlCommand(sqlString, conn);
            //command.Connection.Open();
            //command.ExecuteScalar();
            //userbookID = (int)command.LastInsertedId;
            //command.Connection.Close();

            #endregion

            #region COVERARTISTS
            { 
                // Select all current listed Cover Artists for this book.
                sqlString = "select creator.id, name from creator join bookcoverartist on bookcoverartist.creator = creator.id where userbook = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                List<KeyValuePair<int, String>> currCoverArtists = new List<KeyValuePair<int, String>>();
                while (reader.Read())
                {
                    int key = Convert.ToInt32(reader[0]);
                    String value = reader[1].ToString();
                    currCoverArtists.Add(new KeyValuePair<int, String>(key, value));
                }
                command.Connection.Close();

                // See which Cover Artists are already listed for this book.
                bool[] alreadyIn = new bool[_b.coverArtists.Count];
                for (int i = 0; i < alreadyIn.Length; ++i)
                    alreadyIn[i] = false;

                for (int i = 0; i < _b.coverArtists.Count; ++i)
                {
                    for (int j = 0; j < currCoverArtists.Count; ++j)
                        if (_b.coverArtists[i] == currCoverArtists[j].Value)
                        {
                            alreadyIn[i] = true;
                            break;
                        }
                }

                // See which ones aren't in the updated list.
                List<int> needRemoved = new List<int>();
                for (int i = 0; i < currCoverArtists.Count; ++i)
                {
                    bool found = false;
                    for (int j = 0; j < _b.coverArtists.Count; ++j)
                    {
                        if (_b.coverArtists[j] == currCoverArtists[i].Value)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        needRemoved.Add(currCoverArtists[i].Key);
                }

                // Insert any that aren't in.
                for (int i = 0; i < _b.coverArtists.Count; ++i)
                {
                    if (alreadyIn[i])
                        continue;

                    sqlString = "insert into bookcoverartist (creator, userbook) values ((select id from creator where name = '" + _b.coverArtists[i] + "'), " + _b.id + ");";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    command.ExecuteScalar();
                    command.Connection.Close();
                }

                // Delete any that should no longer be in.
                for (int i = 0; i < needRemoved.Count; ++i)
                {
                    sqlString = "delete from bookcoverartist where userbook = " + _b.id + " and creator = '" + needRemoved[i] + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
            }
            #endregion

            #region WRITERS
            {
                ret += "WRITERS";
                sqlString = "select creator.id, name from creator join bookwriter on bookwriter.creator = creator.id where userbook = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                List<KeyValuePair<int, String>> currWriters = new List<KeyValuePair<int, String>>();
                while (reader.Read())
                {
                    int key = Convert.ToInt32(reader[0]);
                    String value = reader[1].ToString();
                    currWriters.Add(new KeyValuePair<int, String>(key, value));
                }
                command.Connection.Close();

                // See which Cover Artists are already listed for this book.
                bool[] alreadyIn = new bool[_b.writers.Count];
                for (int i = 0; i < alreadyIn.Length; ++i)
                    alreadyIn[i] = false;

                for (int i = 0; i < _b.writers.Count; ++i)
                {
                    for (int j = 0; j < currWriters.Count; ++j)
                        if (_b.writers[i] == currWriters[j].Value)
                        {
                            alreadyIn[i] = true;
                            break;
                        }
                }

                // See which ones aren't in the updated list.
                List<int> needRemoved = new List<int>();
                for (int i = 0; i < currWriters.Count; ++i)
                {
                    bool found = false;
                    for (int j = 0; j < _b.writers.Count; ++j)
                    {
                        if (_b.writers[j] == currWriters[i].Value)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        needRemoved.Add(currWriters[i].Key);
                }

                // Insert any that aren't in.
                for (int i = 0; i < _b.writers.Count; ++i)
                {
                    if (alreadyIn[i])
                        continue;

                    sqlString = "insert into bookwriter (creator, userbook) values ((select id from creator where name = '" + _b.writers[i] + "'), " + _b.id + ");";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    command.ExecuteScalar();
                    command.Connection.Close();
                }

                // Delete any that should no longer be in.
                for (int i = 0; i < needRemoved.Count; ++i)
                {
                    sqlString = "delete from bookwriter where userbook = " + _b.id + " and creator = '" + needRemoved[i] + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
            }
            #endregion

            #region ARTISTS
            {
                ret += "ARTISTS";
            
                // Select all current listed Cover Artists for this book.
                sqlString = "select creator.id, name from creator join bookartist on bookartist.creator = creator.id where userbook = " + _b.id + ";";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                List<KeyValuePair<int, String>> currArtists = new List<KeyValuePair<int, String>>();
                while (reader.Read())
                {
                    int key = Convert.ToInt32(reader[0]);
                    String value = reader[1].ToString();
                    currArtists.Add(new KeyValuePair<int, String>(key, value));
                }
                command.Connection.Close();

                // See which Cover Artists are already listed for this book.
                bool[] alreadyIn = new bool[_b.artists.Count];
                for (int i = 0; i < alreadyIn.Length; ++i)
                    alreadyIn[i] = false;

                for (int i = 0; i < _b.artists.Count; ++i)
                {
                    for (int j = 0; j < currArtists.Count; ++j)
                        if (_b.artists[i] == currArtists[j].Value)
                        {
                            alreadyIn[i] = true;
                            break;
                        }
                }

                // See which ones aren't in the updated list.
                List<int> needRemoved = new List<int>();
                for (int i = 0; i < currArtists.Count; ++i)
                {
                    bool found = false;
                    for (int j = 0; j < _b.artists.Count; ++j)
                    {
                        if (_b.artists[j] == currArtists[i].Value)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        needRemoved.Add(currArtists[i].Key);
                }

                // Insert any that aren't in.
                for (int i = 0; i < _b.artists.Count; ++i)
                {
                    if (alreadyIn[i])
                        continue;

                    sqlString = "insert into bookartist (creator, userbook) values ((select id from creator where name = '" + _b.artists[i] + "'), " + _b.id + ");";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    command.ExecuteScalar();
                    command.Connection.Close();
                }

                // Delete any that should no longer be in.
                for (int i = 0; i < needRemoved.Count; ++i)
                {
                    sqlString = "delete from bookartist where userbook = " + _b.id + " and creator = '" + needRemoved[i] + "';";
                    command = new MySqlCommand(sqlString, conn);
                    command.Connection.Open();
                    reader = command.ExecuteReader();
                    command.Connection.Close();
                }
            }
            #endregion
            
            // TODO
            #region SIGNATURES
            /*
            ret += "SIGS";
            for (int i = 0; i < _b.signatures.Count; i++)
            {
                sqlString = "select id from creator where name = '" + _b.signatures[i] + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                int creatorID = Convert.ToInt32(reader[0].ToString());
                command.Connection.Close();

                sqlString = "select count(*) from booksignature where creator = '" + creatorID + "' and userbook = '" + userbookID + "';";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                if (reader[0].ToString() != "0")
                {
                    command.Connection.Close();
                    continue;
                }
                command.Connection.Close();

                sqlString = "insert into booksignature (creator, userbook) values ((select id from creator where name = '" + _b.signatures[i] + "'), " + userbookID + ");";
                command = new MySqlCommand(sqlString, conn);
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }
            #endregion
            
            conn.Close();
            return ret;
        }

        bool userExists(String _email) {
            string sqlString = "select count(*) from user where email = '" + _email + "';";
            //ret += sqlString;
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool ret = true;
            if (reader[0].ToString() == "0")
                ret = false;

            command.Connection.Close();
            return ret;
        }

        bool exists(String _table, String _entry)
        {
            string sqlString = "select count(*) from " + _table + " where name = '" + _entry + "';";
            //ret += sqlString;
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bool ret = true;
            if (reader[0].ToString() == "0")
                ret = false;

            command.Connection.Close();
            return ret;
        }

        bool bookExists(Book _b)
        {
            // Series ID
            // Issue Number
            // Cover (A, B, C, etc.)
            // Image URL

            string sqlString = "select id from series where name = '" + _b.series + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int seriesID = Convert.ToInt32(reader[0].ToString());
            command.Connection.Close();

            sqlString = "select count(*) from book where series = " + seriesID + " and issuenumber = " + _b.issueNumber + " and cover = '" + _b.cover + "' and imageurl = '" + _b.imageURL + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            bool ret = true;
            if (reader[0].ToString() == "0")
                ret = false;
            command.Connection.Close();

            return ret;
        }

        bool userbookExists(int _userID, Book _b, ref int _bookID)
        {
            string sqlString = "select id from series where name = '" + _b.series + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int seriesID = Convert.ToInt32(reader[0].ToString());
            command.Connection.Close();

            sqlString = "select id from book where series = " + seriesID + " and issuenumber = " + _b.issueNumber + " and cover = '" + _b.cover + "' and imageurl = '" + _b.imageURL + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            int bookID = Convert.ToInt32(reader[0].ToString());
            command.Connection.Close();

            sqlString = "select id from grader where name = '" + _b.grader + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            int graderID = Convert.ToInt32(reader[0].ToString());
            command.Connection.Close();

            sqlString = "select count(*), id from userbook where book = '" + bookID + "' and grade = '" + _b.grade + "' and gradecode = '" + _b.gradeCode + "' and grader = '" + graderID + "' and owner = '" + _userID + "' and value = '" + _b.value + "' and isbn = '" + _b.isbn + "' and publisher = '" + _b.publisher + "' and pagecount = '" + _b.pageCount + "';";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            bool ret = true;
            if (reader[0].ToString() == "0")
                ret = false;
            else
                _bookID = Convert.ToInt32(reader[1].ToString());

            command.Connection.Close();

            return ret;
        }

        public String DeleteBook(int _id)
        {
            conn = new MySqlConnection(connString);
            String ret = "DeleteBook - " + _id;

            string sqlString = "delete from bookwriter where userbook = " + _id + ";";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            command.ExecuteScalar();
            command.Connection.Close();

            sqlString = "delete from bookartist where userbook = " + _id + ";";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            command.ExecuteScalar();
            command.Connection.Close();

            sqlString = "delete from bookcoverartist where userbook = " + _id + ";";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            command.ExecuteScalar();
            command.Connection.Close();

            sqlString = "delete from booksignature where userbook = " + _id + ";";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            command.ExecuteScalar();
            command.Connection.Close();

            sqlString = "delete from userbook where id = " + _id + ";";
            command = new MySqlCommand(sqlString, conn);
            command.Connection.Open();
            command.ExecuteScalar();
            command.Connection.Close();

            ret += "DeleteBook Done";
            conn.Close();
            return ret;
        }

        public User GetUser(String _email)
        {
            conn = new MySqlConnection(connString);

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";
            //users.Clear();

            if(!userExists(_email)) {
                string addUserString = "insert into user (email, signupdate) values ('" + _email + "', now())";
                command = new MySqlCommand(addUserString, conn);
                //if (newConnection)
                command.Connection.Open();
                command.ExecuteScalar();
                command.Connection.Close();
            }

            string sqlSelectString = "select user.id, firstname, lastname, email, signupdate from user where email = '" + _email + "';";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL
            //  Book.issueNumber
            //  Book.cover
            User user = new User();

            while (reader.Read())
            {
                user.id = Convert.ToInt32(reader[0]);
                user.firstName = reader[1].ToString();
                user.lastName = reader[2].ToString();
                user.email = reader[3].ToString();
                user.signupDate = Convert.ToDateTime(reader[4].ToString());

                //Book book = new Book();
                //book.series = reader[4].ToString();
                //book.grade = (float)Convert.ToDecimal(reader[5]);
                //book.grader = reader[6].ToString();
                //book.value = (float)Convert.ToDecimal(reader[7]);
                //book.imageURL = reader[8].ToString();
                //book.issueNumber = Convert.ToInt32(reader[9]);
                //book.cover = reader[10].ToString()[0];
                //user.books.Add(book);
            }
            command.Connection.Close();
            conn.Close();
            return user;
        }

        public void GetUsersBooks(User _user)
        {
            conn = new MySqlConnection(connString);

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";

            string sqlSelectString = "select userbook.id, series.name, book.issuenumber, book.cover, userbook.grade, grader.name, userbook.value, userbook.isbn, userbook.publisher, userbook.pagecount, book.imageurl from book join series on series.id = book.series join userbook on " + 
                "userbook.book = book.id join grader on grader.id = userbook.grader where userbook.owner = " + _user.id + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.issueNumber
            //  Book.cover
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL 
            while (reader.Read())
            {
                //User user = new User();
                //user.id = Convert.ToInt32(reader[0]);
                //user.firstName = reader[1].ToString();
                //user.lastName = reader[2].ToString();
                //user.email = reader[3].ToString();
                //user.signupDate = Convert.ToDateTime(reader[4].ToString());

                Book book = new Book();
                book.id = Convert.ToInt32(reader[0]);
                book.series = reader[1].ToString();
                book.issueNumber = Convert.ToInt32(reader[2]);
                book.cover = reader[3].ToString()[0];
                book.grade = Convert.ToDecimal(reader[4]);
                book.grader = reader[5].ToString();
                book.value = Convert.ToDecimal(reader[6]);
                book.isbn = reader[7].ToString();
                book.publisher = reader[8].ToString();
                book.pageCount = Convert.ToInt32(reader[9]);
                book.imageURL = reader[10].ToString();

                _user.Books.Add(book);
            }
            command.Connection.Close();
            conn.Close();
            for(int i = 0; i < _user.Books.Count; ++i)
            {
                GetBooksArtists(_user.Books[i]);
                GetBooksCoverArtists(_user.Books[i]);
                GetBooksWriters(_user.Books[i]);
                GetBooksSignatures(_user.Books[i]);
            }
        }

        public Book GetBook(int _bookID)
        {
            conn = new MySqlConnection(connString);

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";

            string sqlSelectString = "select userbook.id, series.name, book.issuenumber, book.cover, userbook.grade, grader.name, userbook.value, userbook.isbn, userbook.publisher, userbook.pagecount, book.imageurl from book join series on series.id = book.series join userbook on " +
                "userbook.book = book.id join grader on grader.id = userbook.grader where userbook.id = " + _bookID + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.issueNumber
            //  Book.cover
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL
            Book book = new Book();
            while (reader.Read())
            {
                //User user = new User();
                //user.id = Convert.ToInt32(reader[0]);
                //user.firstName = reader[1].ToString();
                //user.lastName = reader[2].ToString();
                //user.email = reader[3].ToString();
                //user.signupDate = Convert.ToDateTime(reader[4].ToString());

                //Book book = new Book();
                book.id = Convert.ToInt32(reader[0]);
                book.series = reader[1].ToString();
                book.issueNumber = Convert.ToInt32(reader[2]);
                book.cover = reader[3].ToString()[0];
                book.grade = Convert.ToDecimal(reader[4]);
                book.grader = reader[5].ToString();
                book.value = Convert.ToDecimal(reader[6]);
                book.isbn = reader[7].ToString();
                book.publisher = reader[8].ToString();
                book.pageCount = Convert.ToInt32(reader[9]);
                book.imageURL = reader[10].ToString();
            }
            command.Connection.Close();
            conn.Close();

            GetBooksArtists(book);
            GetBooksCoverArtists(book);
            GetBooksWriters(book);
            GetBooksSignatures(book);
            return book;
        }

        public String GetUsersFilteredBooks(User _user, List<String> _identifiers, List<String> _values)
        {
            conn = new MySqlConnection(connString);

            // Possible Identifiers:
            //  signature
            //      - string name specified
            //  writer
            //      - string name specified
            //  artist
            //      - string name specified
            // coverartist
            //      - string name specified
            //  grade
            //      - float grade specified
            //  grader
            //      - string grader specified
            //  series
            //      - string series specified
            //  issue
            //      - float issue number specified
            //  

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";

            string sqlSelectString = "select userbook.id, series.name, book.issuenumber, book.cover, userbook.grade, grader.name, userbook.gradecode, userbook.value, userbook.isbn, userbook.publisher, userbook.pagecount, book.imageurl from book join series on series.id = book.series join userbook on " +
                "userbook.book = book.id join grader on grader.id = userbook.grader where userbook.owner = " + _user.id;
            //int index = -1;
            for(int i = 0; i < _identifiers.Count; ++i)
                if(identifierQueries.ContainsKey(_identifiers[i]))
                    sqlSelectString += " and " + identifierQueries[_identifiers[i]] + " = '" + _values[i] + "' ";

            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.issueNumber
            //  Book.cover
            //  Book.grade
            //  Book.grader
            //  Book.gradeCode
            //  Book.value
            //  Book.imageURL 
            while (reader.Read())
            {
                //User user = new User();
                //user.id = Convert.ToInt32(reader[0]);
                //user.firstName = reader[1].ToString();
                //user.lastName = reader[2].ToString();
                //user.email = reader[3].ToString();
                //user.signupDate = Convert.ToDateTime(reader[4].ToString());

                Book book = new Book();
                book.id = Convert.ToInt32(reader[0]);
                book.series = reader[1].ToString();
                book.issueNumber = Convert.ToDecimal(reader[2]);
                book.cover = reader[3].ToString()[0];
                book.grade = Convert.ToDecimal(reader[4]);
                book.grader = reader[5].ToString();
                book.gradeCode = reader[6].ToString();
                book.value = Convert.ToDecimal(reader[7]);
                book.isbn = reader[8].ToString();
                book.publisher = reader[9].ToString();
                book.pageCount = Convert.ToInt32(reader[10]);
                book.imageURL = reader[11].ToString();

                _user.Books.Add(book);
            }
            command.Connection.Close();

            for (int i = 0; i < _user.Books.Count; ++i)
            {
                GetBooksArtists(_user.Books[i]);
                GetBooksCoverArtists(_user.Books[i]);
                GetBooksWriters(_user.Books[i]);
                GetBooksSignatures(_user.Books[i]);
            }
            conn.Close();
            return sqlSelectString;
        }

        public String GetInfo(List<String> _identifiers, List<String> _values)
        {
            conn = new MySqlConnection(connString);
            string ret = "";

            // TODO: Genericize™ this some.
            for (int i = 0; i < _identifiers.Count; ++i)
            {
                switch (_identifiers[i])
                {
                    case "count":
                        {
                            string sqlSelectString = "";
                            switch (_values[i])
                            {
                                case "series":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + " from userbook join book on book.id = userbook.book join series on series.id = book.series group by userbook.book order by count(*) desc limit 1;";
                                    break;
                                case "signatures":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + " from userbook join booksignature on booksignature.userbook = userbook.id join creator on booksignature.creator = creator.id group by creator.name order by count(*) desc limit 1;";
                                    break;
                                case "books":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + " from userbook;";
                                    break;
                                case "collection":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + ", count(*) as c from userbook join user on userbook.owner = user.id group by owner order by c desc limit 1;";
                                    break;
                                case "users":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + " from user;";
                                    break;
                                case "allGraded":
                                    sqlSelectString = "select count(*) from userbook where grader != 6;";
                                    break;
                                case "mostGraded":
                                    sqlSelectString = "select grader.name, count(*) as c from userbook join grader on grader.id = userbook.grader where grader.name != 'Unassigned' group by userbook.grader order by c desc;";
                                    break;
                                case "popularSeries":
                                    sqlSelectString = "select series.name, count(*) as c from userbook join book on book.id = userbook.book join series on series.id = book.series group by series.id order by c desc limit 5";
                                    break;
                            }
                           
                            command = new MySqlCommand(sqlSelectString, conn);
                            //if (newConnection)
                            command.Connection.Open();

                            MySqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                ret += reader[0].ToString(); //  + " " + (_values[i] == "collection" ? "(" + reader[1].ToString() + ") " : "");
                                if (_values[i] == "popularSeries")
                                    ret += "|" + reader[1].ToString() + "\n";
                                if (_values[i] == "mostGraded")
                                    break;
                            }

                            break;
                        }
                    case "user":
                        {
                            string sqlSelectString = "";
                            switch (_values[i])
                            {
                                case "newest":
                                    sqlSelectString = "select " + valueQueries[_values[i]] + " from user order by signupdate desc limit 1;";
                                    break;
                            }

                            command = new MySqlCommand(sqlSelectString, conn);
                            //if (newConnection)
                            command.Connection.Open();

                            MySqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                                ret += reader[0].ToString();
                            break;
                        }
                    default:
                        break;
                }
            }
            conn.Close();
            return ret;
        }

        private bool GetIndex(List<String> _identifiers, String _identifier, out int _index)
        {
            _index = -1;
            if (_identifiers.Contains(_identifier) && Convert.ToBoolean((_index = _identifiers.IndexOf(_identifier))))
                return true;

            return false;
        }

        public List<Book> GetUsersBooksList(User _user)
        {
            conn = new MySqlConnection(connString);

            //string sqlSelectString = "select user.id, firstname, lastname, email, series.name, grade, " +
            //    "grader.name, value, book.imageurl, book.issuenumber, book.cover from user join " +
            //    "userbook on user.id = userbook.owner join book on book.id = userbook.book join " +
            //    "series on series.id = book.series join grader on grader.id = userbook.grader;";

            string sqlSelectString = "select userbook.id, series.name from book join series on series.id = book.series join userbook on " +
                "userbook.book = book.id where userbook.owner = " + _user.id + " group by series.name;";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();
            // Order:
            //  User.id
            //  User.firstName
            //  User.lastName
            //  User.email
            //
            //  Book.series
            //  Book.issueNumber
            //  Book.cover
            //  Book.grade
            //  Book.grader
            //  Book.value
            //  Book.imageURL 
            List<Book> books = new List<Book>();
            while (reader.Read())
            {
                //User user = new User();
                //user.id = Convert.ToInt32(reader[0]);
                //user.firstName = reader[1].ToString();
                //user.lastName = reader[2].ToString();
                //user.email = reader[3].ToString();
                //user.signupDate = Convert.ToDateTime(reader[4].ToString());

                Book book = new Book();
                book.id = Convert.ToInt32(reader[0]);
                book.series = reader[1].ToString();
                //book.issueNumber = Convert.ToInt32(reader[2]);
                //book.cover = reader[3].ToString()[0];
                //book.grade = (float)Convert.ToDecimal(reader[4]);
                //book.grader = reader[5].ToString();
                //book.value = (float)Convert.ToDecimal(reader[6]);
                //book.imageURL = reader[7].ToString();


                books.Add(book);
            }
            command.Connection.Close();
            conn.Close();
            return books;
            //for (int i = 0; i < _user.Books.Count; ++i)
            //{
            //    GetBooksArtists(_user.Books[i]);
            //    GetBooksCoverArtists(_user.Books[i]);
            //    GetBooksWriters(_user.Books[i]);
            //    GetBooksSignatures(_user.Books[i]);
            //}
        }

        private void GetBooksArtists(Book _book)
        {
            string sqlSelectString = "select creator.name from creator join bookartist on bookartist.creator = creator.id where bookartist.userbook = " + _book.id + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.artists.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksCoverArtists(Book _book)
        {
            string sqlSelectString = "select creator.name from creator join bookcoverartist on bookcoverartist.creator = creator.id where bookcoverartist.userbook = " + _book.id + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.coverArtists.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksWriters(Book _book)
        {
            string sqlSelectString = "select creator.name from creator join bookwriter on bookwriter.creator = creator.id where bookwriter.userbook = " + _book.id + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.writers.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksSignatures(Book _book)
        {
            string sqlSelectString = "select creator.name from creator join booksignature on booksignature.creator = creator.id where booksignature.userbook = " + _book.id + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.signatures.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksArtists(Book _book, String _filter)
        {
            string sqlSelectString = "select creator.name from creator join bookartist on bookartist.creator = creator.id where bookartist.userbook = " + _book.id + " and creator.name = " + _filter + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.artists.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksCoverArtists(Book _book, String _filter)
        {
            string sqlSelectString = "select creator.name from creator join bookcoverartist on bookcoverartist.creator = creator.id where bookcoverartist.userbook = " + _book.id + " and creator.name = " + _filter + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.coverArtists.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksWriters(Book _book, String _filter)
        {
            string sqlSelectString = "select creator.name from creator join bookwriter on bookwriter.creator = creator.id where bookwriter.userbook = " + _book.id + " and creator.name = " + _filter + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.writers.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        private void GetBooksSignatures(Book _book, String _filter)
        {
            string sqlSelectString = "select creator.name from creator join booksignature on booksignature.creator = creator.id where booksignature.userbook = " + _book.id + " and creator.name = " + _filter + ";";
            command = new MySqlCommand(sqlSelectString, conn);
            //if (newConnection)
            command.Connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _book.signatures.Add(reader[0].ToString());
            }
            command.Connection.Close();
        }

        */
    }
}
