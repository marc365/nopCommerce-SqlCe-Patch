using Nop.Core;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Nop.Data
{
    /// <summary>
    /// Sql Compact override Entity Framework repository
    /// </summary>
    public partial class SqlCeEfRepository<T> : EfRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public SqlCeEfRepository(IDbContext context) : base (context)
        {
            this._context = context;
        }

        /// <summary>
        /// SqlCe override insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public override void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                //don't throw a double exception if SqlCe errors becuase it can't save text over 4000 characters
                if (!dbEx.EntityValidationErrors.FirstOrDefault().ValidationErrors.FirstOrDefault().ErrorMessage.Contains("4000"))
                    throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

    }
}