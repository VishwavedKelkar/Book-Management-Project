
using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.BL.Interfaces.Services.BookUserInterface;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.BL.Interfaces.Services.JwtTokenService;
using BookManagement.BL.Interfaces.Services.SeedData;
using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.BL.Services.AuthorService;
using BookManagement.BL.Services.BookImageServices;
using BookManagement.BL.Services.BookService;
using BookManagement.BL.Services.BookUserServices;
using BookManagement.BL.Services.GenreService;
using BookManagement.BL.Services.UserServices;
using BookManagement.Services.SeedData;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.BL.IoC
{
    public static class ServiceExtension
    {
        public static void AddAuthorServices(this IServiceCollection services)
        {
            //AuthorServices
            services.AddScoped<ICreateAuthorService, CreateAuthorService>();
            services.AddScoped<IGetAuthorService, GetAuthorService>();
            services.AddScoped<IGetAllAuthorsService, GetAllAuthorsService>();
            services.AddScoped<IUpdateAuthorService, UpdateAuthorService>();
            services.AddScoped<IDeleteAuthorService, DeleteAuthorService>();


            //BookServices
            services.AddScoped<ICreateBookService, CreateBookService>();
            services.AddScoped<IGetAllBooksService, GetAllBooksService>();
            services.AddScoped<IGetBookService, GetBookService>();
            services.AddScoped<IUpdateBookService, UpdateBookService>();
            services.AddScoped<IDeleteBookService, DeleteBookService>();


            //GenreService
            services.AddScoped<ICreateGenreService, CreateGenreService>();
            services.AddScoped<IGetGenreService, GetGenreService>();
            services.AddScoped<IGetAllGenresService, GetAllGenresService>();
            services.AddScoped<IUpdateGenreService, UpdateGenreService>();
            services.AddScoped<IDeleteGenreService, DeleteGenreService>();

            //BookImageService
            services.AddScoped<IAddBookImageService, AddBookImageService>();
            services.AddScoped<IGetAllBookImagesService, GetAllBookImagesService>();
            services.AddScoped<IGetBookImageService, GetBookImageService>();
            services.AddScoped<IUpdateBookImageService, UpdateBookImageService>();
            services.AddScoped<IDeleteBookImageService, DeleteBookImageService>();

            //UserService
            services.AddScoped<IAddUserService,AddUserService>();
            services.AddScoped<IGetAllUsersService,GetAllUsersService>();
            services.AddScoped<IGetUserService,GetUserService>();
            services.AddScoped<IUpdateUserService,UpdateUserService>();
            services.AddScoped<IDeleteUserService,DeleteUserService>();

            //BookUserService
            services.AddScoped<IAssignBookToUserService, AssignBookToUserService>();
            services.AddScoped<IGetUserBooksService, GetUserBooksService>();
            services.AddScoped<IRemoveBookFromUserService, RemoveBookFromUserService>();

            //JwtService
            services.AddScoped<IJwtService, JwtService>();

            //DataSeederService
            services.AddScoped<IDataSeeder, RolesSeederService>();

        }
    }
}
