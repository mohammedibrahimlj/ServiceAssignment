using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTest.ServiceInterface
{
    interface UserInterface
    {
        List<Common.Book> SearchBook(string BookTitle);
        List<Common.BookBorrow> GetBookBorrowing();
        List<Common.BookReturning> GetBookReturining();
        string Login(string username, string password);
    }
}
