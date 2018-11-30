using CrudRestAspNetCore.Model.Base;
using CrudRestAspNetCore.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudRestAspNetCore.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected MySqlContext context;
        private DbSet<T> dataset;

        public GenericRepository(MySqlContext context)
        {
            this.context = context;
            this.dataset = context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                this.dataset.Add(item);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public void Delete(int id)
        {
            var result = dataset.SingleOrDefault(p => p.id.Equals(id));

            try
            {
                if (result != null)
                {
                    this.dataset.Remove(result);
                    this.context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool Exist(int? id)
        {
            return dataset.Any(p => p.id.Equals(id));
        }

        public List<T> FindAll()
        {
            return this.dataset.ToList();
        }

        public T FindById(int id)
        {
            return dataset.SingleOrDefault(p => p.id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataset.FromSql<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            return dataset.FromSql<T>(query).Count();
        }

        public T Update(T item)
        {
            if (!Exist(item.id)) return null;
            var result = dataset.SingleOrDefault(p => p.id.Equals(item.id));
            try
            {
                this.context.Entry(result).CurrentValues.SetValues(item);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return item;
        }
    }
}
