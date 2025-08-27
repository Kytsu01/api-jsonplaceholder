using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JsonPlaceholderImporter.Controllers.Imports
{
    [Route("api/import/all")]
    [ApiController]
    public class ImportAllController : ControllerBase
    {
        private readonly UsersImportController _users;
        private readonly PostsImporterController _posts;
        private readonly CommentsImportController _comments;
        private readonly AlbumsImportController _albums;
        private readonly PhotosImportController _photos;
        private readonly TodosImportController _todos;

        public ImportAllController(UsersImportController users, PostsImporterController posts, 
                                    CommentsImportController comments, AlbumsImportController albums, 
                                    PhotosImportController photos, TodosImportController todos)
        {
            _users = users;
            _posts = posts;
            _comments = comments;
            _albums = albums;
            _photos = photos;
            _todos = todos;
        }

        [HttpPost]
        public async Task<IActionResult> ImportAll()
        {
            var users = await _users.Import();
            var posts = await _posts.Import();
            var comments = await _comments.Import();
            var albums = await _albums.Import();
            var photos = await _photos.Import();
            var todos = await _todos.Import();

            return Ok(new { users, posts, comments, albums, photos, todos });
        }
    }
}
