using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebApplicationTest.Common;

namespace WebApplicationTest
{
    /// <summary>
    /// Summary description for LibraryUser
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [Serializable()]
    public class LibraryUser : System.Web.Services.WebService, ServiceInterface.UserInterface
    {
        private string statusMessage = "";

        [WebMethod(EnableSession =true)]
        public string Login(string username, string password)
        {
            try
            {
                statusMessage = string.Empty;
                _ = BookListClass.userList;
                var user = Common.BookListClass.AllUser.Where(w => w.UserEmail == username.Trim() && w.Password == password.Trim().ToString()).FirstOrDefault();
                if(user==null)
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
        [WebMethod(EnableSession = true)]
        public List<Book> SearchBook(string BookTitle)
        {
            throw new NotImplementedException();
        }
        [WebMethod(EnableSession = true)]
        public List<Common.BookBorrow> GetBookBorrowing()
        {
            statusMessage = string.Empty;
            List<Common.BookBorrow> BookRoowsList = null;
            if (Session["UserName"] != null)
            {
                BookRoowsList = Common.BookListClass.UserBookBorrow.Where(w=>w.BorrowUser.UserEmail== Session["UserName"].ToString()).ToList();
            }
            else
            {
                statusMessage = "Please Login";
            }
            return BookRoowsList;
        }
        [WebMethod(EnableSession = true)]
        public List<Common.BookReturning> GetBookReturining()
        {
            statusMessage = string.Empty;

            List<Common.BookReturning> BookReturnList = null;
            if (Session["UserName"] != null)
            {
                BookReturnList = Common.BookListClass.UserBookReturning.Where(s=>s.ReturningUser.UserEmail== Session["UserName"].ToString()).ToList();
            }
            else
            {
                statusMessage = "Please Login";
            }
            return BookReturnList;
        }
    }
}
