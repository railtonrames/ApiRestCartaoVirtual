using ApiRestCartaoVirtual.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual.Data
{
    public interface IARCV_InterfaceRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangeAsync();

        Task<Email[]> GetAllEmails();
    }
}
