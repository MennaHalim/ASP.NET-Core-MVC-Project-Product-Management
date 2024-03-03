using Ecommerc.MVC.Data;
using Ecommerce.Application.Contracts;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class BookRepository : Repository<Book, int> , IBookRepository
    {
        public BookRepository(ApplicationDbContext ecommerceContext) : base(ecommerceContext)
        {
            
        }
    }
}
