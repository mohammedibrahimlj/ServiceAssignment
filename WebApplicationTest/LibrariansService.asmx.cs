using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplicationTest.Common;

namespace WebApplicationTest
{
    /// <summary>
    /// Summary description for LibrariansService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [Serializable()]
    public class LibrariansService : System.Web.Services.WebService, ServiceInterface.LibrariansInterface
    {
        string statusMessage = "";
        [WebMethod(EnableSession = true)]
        public string AddBooks(string Title,string Author,string Press,string PublicationTime,string ClassificationCode,string Keyword)
        {
            statusMessage = string.Empty;
            if (Session["UserName"] != null)
            {
                
                try
                {
                    Common.BookListClass.AllBooks.Add(new Common.Book { Title = Title, Author = Author, Press = Press, PublicationTime = PublicationTime, ClassificationCode = ClassificationCode, Keyword = Keyword });
                    statusMessage = "Book Added Sucessfully";
                }
                catch { statusMessage = "Book Added Failed..."; };
            }
            else
            {
                statusMessage = "Please Login";
            }
            return statusMessage;
        }
        [WebMethod(EnableSession = true)]
        public string DeleteBook(string Title)
        {
            statusMessage = string.Empty;
            if (Session["UserName"] != null)
            {
                try
                {
                    var removeBook = Common.BookListClass.AllBooks.Where(s => s.Title == Title).FirstOrDefault();
                    if (removeBook == null)
                    {
                        statusMessage = "Book Not Found...";
                    }
                    else
                    {
                        Common.BookListClass.AllBooks.Remove(removeBook);
                        statusMessage = "Book Removed Sucessfully";
                    }
                }
                catch { statusMessage = "Book Added Failed..."; };
            }
            else
            {
                statusMessage = "Please Login";
            }
            return statusMessage;
        }
        [WebMethod(EnableSession = true)]
        public string ReturnBook(string BookTitle, string username)
        {
            statusMessage = string.Empty;
            if (Session["UserName"] != null)
            {
                var userdetails = Common.BookListClass.AllUser.Where(w => w.UserEmail == username.ToString().Trim()).FirstOrDefault();
                var BookDetails = Common.BookListClass.AllBooks.Where(w => w.Title == BookTitle).FirstOrDefault();
                if (BookDetails != null)
                {
                    var bookingdata = BookListClass.UserBookBorrow.Where(w => w.BookDetails == BookDetails && w.BorrowUser == userdetails).FirstOrDefault();
                    BookListClass.UserBookBorrow.Remove(bookingdata);
                    if (bookingdata != null)
                    {
                        BookListClass.UserBookReturning.Add(new BookReturning { ReturningUser = userdetails, BookDetails = BookDetails, BookDueDate = bookingdata.BookDueDate, BookReturningDate = DateTime.Now });
                    }
                    else
                    {
                        statusMessage = "Book Borrow status not found.";
                    }
                }
                else
                {
                    statusMessage = "Book Details not found.";
                }
            }
            else
            {
                statusMessage = "Please Login";
            }
            return statusMessage;
        }
        [WebMethod(EnableSession = true)]
        public string BorrowBook(string BookTitle, string username)
        {
            statusMessage = string.Empty;
            if (Session["UserName"] != null)
            {
                var userdetails = Common.BookListClass.AllUser.Where(w => w.UserEmail == username.ToString().Trim()).FirstOrDefault();
                var BookDetails = Common.BookListClass.AllBooks.Where(w => w.Title == BookTitle).FirstOrDefault();
                if (BookDetails != null)
                {
                    var bookingdata = BookListClass.UserBookBorrow.Where(w => w.BookDetails == BookDetails && w.BorrowUser == userdetails).FirstOrDefault();

                    if (bookingdata == null)
                    {
                        if (userdetails.UserRole == UserRole.Student)
                        {
                            BookListClass.UserBookBorrow.Add(new BookBorrow { BorrowUser = userdetails, BookDetails = BookDetails, BookDueDate = DateTime.Now.AddDays(10), BookBorrowDate = DateTime.Now });
                        }
                        else if (userdetails.UserRole == UserRole.Teacher)
                        {
                            BookListClass.UserBookBorrow.Add(new BookBorrow { BorrowUser = userdetails, BookDetails = BookDetails, BookDueDate = DateTime.Now.AddDays(20), BookBorrowDate = DateTime.Now });
                        }
                        statusMessage = "Book Borrowed Sucessfully";
                    }
                    else
                    {
                        statusMessage = "Book Already Borrowed";
                    }
                }
                else
                {
                    statusMessage = "Book Details not found.";
                }
            }
            else
            {
                statusMessage = "Please Login";
            }
            return statusMessage;
        }
        [WebMethod(EnableSession = true)]
        public string MailTrigger()
        {
            string statusMessage = string.Empty;
            try
            {
                BookListClass.MailTrigger();
                statusMessage = "Mail Triggered Successfully";
            }
            catch { statusMessage = "Mail Trigger Failed."; }
            return statusMessage;
        }

        [WebMethod(EnableSession = true)]
        public string Login(string username, string password)
        {
            try
            {
                statusMessage = string.Empty;
                _ = BookListClass.userList;
                var user = Common.BookListClass.AllUser.Where(w => w.UserEmail == username.Trim() && w.Password == password.Trim().ToString()).FirstOrDefault();
                if (user == null)
                {
                    statusMessage = "Invalid UserName/Password";
                }
                else
                {
                    statusMessage = "LoginIn Sucessfully";
                    Session["UserName"] = username;
                }

            }
            catch { }
            return statusMessage;
        }
    }
}
