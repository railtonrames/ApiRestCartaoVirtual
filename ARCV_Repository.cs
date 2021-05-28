using ApiRestCartaoVirtual.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestCartaoVirtual
{
    public class ARCV_Repository : IARCV_InterfaceRepository
    {
        public void Add<T>(T entity) where T : class
        {
            var contexto = new EmailContext();
            contexto.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            var contexto = new EmailContext();
            contexto.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            var contexto = new EmailContext();
            contexto.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            var contexto = new EmailContext();
            return (await contexto.SaveChangesAsync()) > 0;
        }

    }
}
