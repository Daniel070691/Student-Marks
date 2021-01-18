using System;
using System.Linq;
using System.Web.Http;
using VarsityProject.Models;
using VarsityProject.ViewModels;

namespace VarsityProject.Controllers.Api
{
    public class LoginController : ApiController
    {
        VarsityDB db = new VarsityDB();

        [AcceptVerbs("GET")]
        public IHttpActionResult Signin(string email)
        {
            UserModel user = new UserModel();
            var userFound = db.tblStudents.Where(s => s.email == email).FirstOrDefault();

            user.Name = userFound.Name;
            user.Surname = userFound.Surname;
            user.Password = userFound.password;
            user.Id = Convert.ToInt32(userFound.TID);
            user.Email = userFound.email;

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}

