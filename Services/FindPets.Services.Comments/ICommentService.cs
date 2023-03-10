namespace FindPets.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetComments(int offset = 0, int limit = 10);
    Task<CommentModel> AddComment(AddCommentModel model);

    //Task<BookModel> GetBook(int bookId);
    //Task UpdateBook(int id, UpdateBookModel model);
    //Task DeleteBook(int bookId);
}