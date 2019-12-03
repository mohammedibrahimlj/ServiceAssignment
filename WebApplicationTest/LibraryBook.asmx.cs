using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplicationTest.Common;
using WebApplicationTest.ServiceInterface;

namespace WebApplicationTest
{
    /// <summary>
    /// Summary description for LibraryBook
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [Serializable]
    public class LibraryBook : System.Web.Services.WebService,ServiceInterface.LibraryBookInterface
    {
        List<Common.Book> AllBooks;
        [WebMethod(EnableSession = true)]
        public List<Common.Book> AuthorByKeyword(string Author)
        {
            if (Session["UserName"] != null)
            {
                AllBooks = new List<Common.Book>();
                try
                {
                    AllBooks = Common.BookListClass.AllBooks.Where(w => w.Author.Contains(Author)).ToList();
                }
                catch { }
            }
            else
            {
                AllBooks = new List<Common.Book>();
            }
            return AllBooks;
        }
        [WebMethod(EnableSession = true)]
        public List<Book> Title(string Title)
        {
            AllBooks = new List<Common.Book>();
            if (Session["UserName"] != null)
            {
                try
                {
                    AllBooks = Common.BookListClass.AllBooks.Where(w => w.Title.Contains(Title)).ToList();
                }
                catch { }
            }
            return AllBooks;
        }
    }

    
}
