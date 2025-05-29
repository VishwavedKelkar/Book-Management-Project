namespace BookManagement.BL.Interfaces.Services.GenreInterface
{
    public interface IDeleteGenreService
    {
        Task DeleteGenreAsync(int genreId);
    }
}
