//This Class Is Auto Generated with Backender, For get more Information check https://github.com/MahdiHoseinpoor/Backender
using GoBlog.Core.Dtos;
using GoBlog.Core.Domains;
using GoBlog.Core.Dtos.Catalog;
using GoBlog.Core.Domains.Catalog;
namespace GoBlog.Services.Catalog
{
    public class CatalogDtosFactory
    {
        public  CategoryService _categoryService ;
        public  PostService _postService ;
        public CatalogDtosFactory(CategoryService CategoryService,PostService PostService)
        {
            _categoryService = CategoryService;
            _postService = PostService;
        }
        public PostDto PreparePostDto(Post post)
        {
            var postDto = new PostDto()
            {
                Title = post.Title,
                Author = post.Author,
                Content = post.Content
            };
            postDto.CategoryDto = PrepareCategoryDto(_categoryService.GetCategoryById(post.CategoryId));
            return postDto;
        }
        public List<PostDto> PreparePostDto(List<Post> posts)
        {
            var postDtos = new List<PostDto>();
            foreach (var post in posts)
            {
                postDtos.Add(PreparePostDto(post));
            }
            return postDtos;
        }
        public CommentDto PrepareCommentDto(Comment comment)
        {
            var commentDto = new CommentDto()
            {
                Content = comment.Content,
                Name = comment.Name,
                CommentStatus = comment.CommentStatus,
                Email = comment.Email
            };
            commentDto.PostDto = PreparePostDto(_postService.GetPostById(comment.PostId));
            return commentDto;
        }
        public List<CommentDto> PrepareCommentDto(List<Comment> comments)
        {
            var commentDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {
                commentDtos.Add(PrepareCommentDto(comment));
            }
            return commentDtos;
        }
        public CategoryDto PrepareCategoryDto(Category category)
        {
            var categoryDto = new CategoryDto()
            {
                Title = category.Title,
                Description = category.Description
            };
            return categoryDto;
        }
        public List<CategoryDto> PrepareCategoryDto(List<Category> categorys)
        {
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in categorys)
            {
                categoryDtos.Add(PrepareCategoryDto(category));
            }
            return categoryDtos;
        }
    }
}