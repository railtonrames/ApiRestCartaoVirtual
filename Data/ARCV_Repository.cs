using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiRestCartaoVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestCartaoVirtual.Data
{
    public class ARCV_Repository : IARCV_InterfaceRepository
    {
        private readonly EmailContext _context;

        public ARCV_Repository(EmailContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Email[]> GetAllEmails()
        {
            IQueryable<Email> query = _context.Email
                .Include(h => h.Endereco)
                .Include(h => h.Cartoes);

            query = query.AsNoTracking();

            return await query.ToArrayAsync();
        }
    }
}
