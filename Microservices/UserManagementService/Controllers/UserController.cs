using UserManagementService.Models;
using UserManagementService.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authService;

        public UserController(UserService userService, AuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            // ユーザーの検証（実際のパスワードハッシュ検証を実装すること）
            var user = await _userService.GetUserByUsernameAsync(request.Username);
            if (user == null || user.Password != request.Password) // パスワード検証ロジックを適切に実装
            {
                return Unauthorized("Invalid username or password.");
            }

            // セッション作成
            var session = await _authService.CreateSessionAsync(user.UserId);

            // トークン生成 (簡易的な例としてGUIDを使用)
            var token = session.Token;

            var response = new LoginResponse
            {
                Token = token
            };

            return Ok(response);
        }
    }
}