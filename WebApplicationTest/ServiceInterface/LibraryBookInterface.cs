using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationTest.ServiceInterface
{
    interface LibraryBookInterface
    {
        List<Common.Book> Title(string Title);
        List<Common.Book> AuthorByKeyword(string Author);
    }
}
