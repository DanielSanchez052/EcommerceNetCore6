using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async  Task<IReadOnlyList<Product>> GetAllProucts()
        { 
            return await _context.Product.ToListAsync();
        }

        public async Task<Product> GetProuctByIdAsync(int Id)
        {
            return await _context.Product.FindAsync(Id);
        }
    }
}
