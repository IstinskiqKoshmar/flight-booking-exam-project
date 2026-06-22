using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Data.Repositories;

public class EfRepository<T>(FlightBookingDbContext context) : IRepository<T> where T : class
{
    public async Task<IReadOnlyList<T>> GetAllAsync() => await context.Set<T>().AsNoTracking().ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);
    public async Task<T> AddAsync(T entity) { context.Add(entity); await context.SaveChangesAsync(); return entity; }
    public async Task UpdateAsync(T entity) { context.Update(entity); await context.SaveChangesAsync(); }
    public async Task DeleteAsync(int id) { var entity = await context.Set<T>().FindAsync(id); if (entity is not null) { context.Remove(entity); await context.SaveChangesAsync(); } }
}
