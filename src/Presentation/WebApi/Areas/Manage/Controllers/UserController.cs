using Application.Extentions;
using Application.Interfaces.Identity;
using Application.Models;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Areas.Manage.Models;

namespace WebApi.Areas.Manage.Controllers
{
    [ApiController]
    [Area("Manage")]
    [Route("[area]/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        #region DI
        private readonly IUserService userService;
        #endregion

        [HttpPost]
        [ModelStateValidate]
        public async Task<ApiResult<object>> Create([FromBody] CreateUserDto model, CancellationToken cancellationToken = new CancellationToken())
        {
            var newUser = new User(model.username, model.phoneNumber, model.email, model.name, model.surname, false, false);
            var createUserResult = await userService.CreateAsync(newUser, model.password, cancellationToken);
            if (createUserResult.Succeeded)
                return Ok(new CreateUserVM(newUser.Id));
            return BadRequest(createUserResult.Errors);
        }

        [HttpGet]
        public async Task<ApiResult<object>> GetAll(string keyword, int page = 1, CancellationToken cancellationToken = new CancellationToken())
        {
            int pageSize = 20;
            var users = await userService.GetAllAsync<UserThumbailVM>(keyword: keyword, page: page, pageSize: pageSize);
            if (users.totalCount > 0)
                return Ok(users);
            return NotFound(users);
        }


        [HttpPost]
        public async Task<ApiResult<object>> Edit([FromRoute] string id, [FromBody] EditUserDto model, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await userService.FindByIdAsync(id);
            if (user != null)
            {
                bool hasChanged = false;

                #region Validate
                if (model.username.ToLower() != user.UserName.ToLower())
                {
                    hasChanged = true;
                    user.UserName = model.username;
                }

                if (model.phoneNumber.ToLower() != user.PhoneNumber.ToLower())
                {
                    hasChanged = true;
                    user.PhoneNumber = model.phoneNumber;
                }

                if (model.email.ToLower() != user.Email.ToLower())
                {
                    hasChanged = true;
                    user.Email = model.email;
                }

                if (model.name.ToLower() != user.Name.ToLower())
                {
                    hasChanged = true;
                    user.Name = model.name;
                }

                if (model.surname.ToLower() != user.Surname.ToLower())
                {
                    hasChanged = true;
                    user.Surname = model.surname;
                }
                #endregion

                if (hasChanged)
                {
                    var updateResult = await userService.UpdateAsync(user, cancellationToken);
                    if (updateResult.Succeeded)
                        return Ok();
                    return BadRequest(updateResult.Errors);
                }
                return BadRequest("هیچ تغییراتی صورت نگرفته است!");
            }
            return NotFound("کاربر مورد نظر پیدا نشد.");
        }

        [HttpDelete]
        public async Task<ApiResult<object>> Delete([FromRoute] string id, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await userService.FindByIdAsync(id);
            if(user != null)
            {
                var deleteResult = await userService.DeleteAsync(user, cancellationToken);
                if (deleteResult.Succeeded)
                    return Ok("کاربر " + user.UserName + " با موفقیت حذف گردید.");
                return BadRequest(deleteResult.Errors);
            }
            return NotFound("کاربر مورد نظر پیدا نشد.");
        }
    }
}
