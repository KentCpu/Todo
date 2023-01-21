using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.DTO;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("registration")]
        public async Task<ActionResult> registration(FormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registeredUser = _dataContext.Users.FirstOrDefault(user => user.email == model.email);
                    if (registeredUser != null) 
                    {
                        return BadRequest();
                    }

                    User user = new User(model.email, BCrypt.Net.BCrypt.HashPassword(model.password));
                    await _dataContext.Users.AddAsync(user);
                    await _dataContext.SaveChangesAsync();

                    AuthResponse responseData = new AuthResponse(user.id, TokenController.generateToken(user.email));
                    return Ok(responseData);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка при регистрации пользователя", e);
            }

            return StatusCode(400);
        }


        [HttpPost("login")]
        public async Task<ActionResult> login(FormModel formModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registeredUser = await _dataContext.Users.FirstOrDefaultAsync(user => user.email == formModel.email);
                    if (registeredUser != null && BCrypt.Net.BCrypt.Verify(formModel.password, registeredUser.password))
                    {
                        AuthResponse responseData = new AuthResponse(registeredUser.id, TokenController.generateToken(registeredUser.email));
                        return Ok(responseData);
                    }
                    else
                    {
                        return BadRequest();
                    }
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка при входе в аккаунт", e);
            }
            return StatusCode(400);
        }
    }
}