
using BookManagement.DAL.Interfaces.Repositories;
using BookManagement.DAL.Repositories;
using BookManagement.DAL.Repositories.AuthorRepository;
using BookManagement.DAL.Repositories.BookRepository;
using BookManagement.DAL.Repositories.GenreRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.DAL.IoC
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IBookRepository,BookRepository>();
            services.AddScoped<IBookImageRepository,BookImageRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IBookUserRepository, BookUserRepository>();
        }
    }
}
