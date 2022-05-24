using Frexco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Frexco.Services.Exceptions;

namespace Frexco.Services
{
    public class StockService
    {
        private readonly FrexcoContext _context;

        public StockService(FrexcoContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> FindAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }

        public async Task InsertAsync(Stock obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock> FindByIdAsync(int id)
        {
            return await _context.Stock.Include(obj => obj.Product).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Stock.FindAsync(id);
                _context.Stock.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task UpdateAsync(Stock obj)
        {
            bool hasAny = await _context.Stock.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}