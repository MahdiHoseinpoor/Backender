using Hotelino.Core.Domains.Blog;
using Hotelino.Core.Dtos.Blog;
using Hotelino.Services.Rooms;
using Hotelino.Services.Administrative;
using Hotelino.Services.Financial;


namespace Hotelino.Services.Blog
{
    public class BlogDtosFactory
    {
        #region PrepareMethods

            #region Post 

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
        public List<PostDto> PreparePostDto(List<Post> Posts)
        {
            var postDtos = new List<PostDto>();
            foreach (var post in Posts)
            {
                    postDtos.Add(PreparePostDto(post));
            }
            return postDtos;
        }
            

           #endregion

            #region Comment 

        public CommentDto PrepareCommentDto(Comment comment)
        {
            var commentDto = new CommentDto()
            {

                Name = comment.Name,

                CommentStatus = comment.CommentStatus,

                Email = comment.Email,

                Content = comment.Content
            };

                        commentDto.PostDto = PreparePostDto(_postService.GetPostById(comment.PostId));
                                
            return commentDto;
        }
        public List<CommentDto> PrepareCommentDto(List<Comment> Comments)
        {
            var commentDtos = new List<CommentDto>();
            foreach (var comment in Comments)
            {
                    commentDtos.Add(PrepareCommentDto(comment));
            }
            return commentDtos;
        }
            

           #endregion

            #region Category 

        public CategoryDto PrepareCategoryDto(Category category)
        {
            var categoryDto = new CategoryDto()
            {

                Title = category.Title,

                Description = category.Description
            };
            
            return categoryDto;
        }
        public List<CategoryDto> PrepareCategoryDto(List<Category> Categories)
        {
            var categoryDtos = new List<CategoryDto>();
            foreach (var category in Categories)
            {
                    categoryDtos.Add(PrepareCategoryDto(category));
            }
            return categoryDtos;
        }
            

           #endregion
        #endregion
        #region fields
        public CategoryService _categoryService;
        public PostService _postService;

        #endregion

        #region ctor

        public BlogDtosFactory(CategoryService CategoryService,PostService PostService)
        {
        _categoryService = CategoryService;
        _postService = PostService;
        }
          
        #endregion
        
    }
}