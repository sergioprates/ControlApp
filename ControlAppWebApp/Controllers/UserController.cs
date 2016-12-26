using ControlApp.Application.Interfaces;
using ControlApp.Resources;
using ControlAppWebApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;

namespace ControlAppWebApp.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserAppService _service;

        public UserController(IUserAppService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        [Route("api/user/{login}")]
        public Task<HttpResponseMessage> GetByLogin(string login)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                AssertionConcern.AssertArgumentNotNull(login, "Login inválido");

                var user = _service.GetByLogin(login);

                AssertionConcern.AssertArgumentNotNull(user, "Usuário não encontrado.");

                response = Request.CreateResponse(HttpStatusCode.OK, new { user = new RegisterUserModel(user) });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Authorize]
        [Route("api/user")]
        public Task<HttpResponseMessage> GetAllWithoutPassword()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                IEnumerable<ControlApp.Dominio.Model.User> users = _service.GetAllActive();

                AssertionConcern.AssertArgumentNotNull(users, "Nenhum usuário encontrado.");

                List<ControlApp.Dominio.Model.User> typedUsers = users.OrderBy(x=> x.Name).ToList();

                List<object> itens = new List<object>();

                for (int i = 0; i < typedUsers.Count; i++)
                {
                    itens.Add(new
                    {
                        Login = typedUsers[i].Login,
                        Email = typedUsers[i].Email,
                        Active = typedUsers[i].Active,
                        Name = typedUsers[i].Name
                    });
                }

                response = Request.CreateResponse(HttpStatusCode.OK, new { users = itens });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Authorize]
        [Route("api/user")]
        public Task<HttpResponseMessage> Register(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.Register(model.Name, model.Login, model.Email, model.Password, model.ConfirmPassword, model.Active);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Authorize]       
        [Route("api/user/update")]
        public Task<HttpResponseMessage> Update(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                ControlApp.Dominio.Model.User user = _service.GetByLogin(model.Login);

                AssertionConcern.AssertArgumentNotNull(user, "Usuário inexistente.");

                if (model.Password != model.GetHash())
                {
                    user.SetPassword(model.Password, model.ConfirmPassword);
                }

                user.SetActivation(model.Active);
                user.ChangeEmail(model.Email);
                user.Validate();

                _service.Update(user);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Usuário alterado com sucesso!" });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpGet]
        [Authorize]        
        [Route("api/user/search/{user}")]
        public Task<HttpResponseMessage> Search(string user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                var users = _service.Search(user);
                response = Request.CreateResponse(HttpStatusCode.OK, new { users });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        
        [HttpGet]
        [Authorize]        
        [Route("api/user/delete/{user}")]
        public Task<HttpResponseMessage> Delete(string user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            //Pegando usuario
            //User.Identity.Name;

            try
            {
                _service.Delete(user);
                response = Request.CreateResponse(HttpStatusCode.OK, new { msg = "Usuário excluído com sucesso." });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

    }
}
