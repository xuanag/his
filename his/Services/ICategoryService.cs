using his.Models;

namespace his.Services
{
    public interface ICategoryService: IMongoService<Category>
    {
        Task<List<Category>> CategoriesByType(string type);
    }
}
