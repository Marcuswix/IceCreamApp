using Microsoft.EntityFrameworkCore;
using Share.Contexts;
using Share.Entities;

namespace Share.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEntity>
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override CategoryEntity Update(CategoryEntity entity)
        {
            return base.Update(entity);
        }
    }
}
