using Share.Contexts;
using Share.Entities;

namespace Share.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity>
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
