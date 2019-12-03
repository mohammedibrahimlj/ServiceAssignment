using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace WebApplicationTest.Common
{
    public static class BookListClass
    {
        public static List<Book> AllBooks = new List<Book>();
        public static List<User> AllUser = new List<User>();
        public static List<BookBorrow> UserBookBorrow = new List<BookBorrow>();
        public static List<BookReturning> UserBookReturning = new List<BookReturning>();
        public static CreateUserList userList = new CreateUserList();

        private static string FromAddress = ConfigurationManager.AppSettings["FromMail"].ToString(), Password = ConfigurationManager.AppSettings["Password"].ToString()
            , smpt = ConfigurationManager.AppSettings["SMPT"].ToString(), Port = ConfigurationManager.AppSettings["Port"].ToString();
        public static void MailTrigger()
        {
            try
            {
                foreach(var DueList in UserBookBorrow.Where(w=>w.IsMailSentDate==null))
                {
                    if(DateTime.Now> DueList.BookDueDate)
                    {

                        MailMessage message = new MailMessage(FromAddress, DueList.BorrowUser.UserEmail);

                        string mailbody = "Hi "+ DueList.BorrowUser.UserEmail+", Your Borrowed book "+ DueList.BookDetails.Title+" is due on "+ DueList.BookDueDate.ToString();
                        message.Subject = "Reg: Borrowed Book "+ DueList.BookDetails.Title+" due on "+ DueList.BookDueDate.ToString();
                        message.Body = mailbody;
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient(smpt, int.Parse(Port.ToString())); 
                        System.Net.NetworkCredential basicCredential1 = new
                        System.Net.NetworkCredential(FromAddress, Password);
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicCredential1;
                        try
                        {
                            client.Send(message);
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        DueList.IsMailSentDate = DateTime.Now;
                    }
                }
            }
            catch { throw new Exception(); }
        }
    }
    public class CreateUserList
    {
        public CreateUserList()
        {
            if(BookListClass.AllUser.Count()==0)
            {
                BookListClass.AllUser.Add(new User { UserEmail = "Student@gmail.com", Password = "123", UserRole = UserRole.Student });
                BookListClass.AllUser.Add(new User { UserEmail = "Teacher@gmail.com", Password = "123", UserRole = UserRole.Teacher });
                BookListClass.AllUser.Add(new User { UserEmail = "Librarian@gmail.com", Password = "123", UserRole = UserRole.Librarian });
            }
        }
    }
    public class BookBorrow
    {
        public User BorrowUser { get; set; }
        public Book BookDetails { get; set; }
        public DateTime BookBorrowDate { get; set; }
        public DateTime BookDueDate { get; set; }
        public DateTime? IsMailSentDate { get; set; }
    }
    public class BookReturning
    {
        public User ReturningUser { get; set; }
        public Book BookDetails { get; set; }
        public DateTime BookDueDate { get; set; }
        public DateTime BookReturningDate { get; set; }
        public string userQuery { get; set; }
    }
    public class User
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
    }
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Press { get; set; }
        public string PublicationTime { get; set; }
        public string ClassificationCode { get; set; }
        public string Keyword { get; set; }
    }
    public enum UserRole { Student=0,
    Teacher=1,
    Librarian=2}
}