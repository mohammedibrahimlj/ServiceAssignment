using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTest.ServiceInterface
{
    interface LibrariansInterface
    {
        string AddBooks(string Title, string Author, string Press, string PublicationTime, string ClassificationCode, string Keyword);
        string DeleteBook(string Title);
        string ReturnBook(string BookTitle,string username);
        string BorrowBook(string BookTitle,string username);
        string Login(string username, string password);
        string MailTrigger();

    }
}
