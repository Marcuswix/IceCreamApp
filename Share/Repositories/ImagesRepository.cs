using Share.Contexts;
using Share.Entities;
namespace Share.Repositories
{
    public class ImagesRepository : BaseRepository<ImagesEntity>
    {
        private readonly DataContext _context;

        public ImagesRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
