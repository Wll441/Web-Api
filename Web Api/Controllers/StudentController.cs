using Superpower.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Umbraco.Core.Models.Membership;
using Web_Api.Models;
using Web_Api.Tools;

namespace Web_Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/students")]
    public class StudentController : ApiController
    {
        [Route("login")]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.LoginName == "admin@163.com")
                {
                    return Ok(
                            new JwtTools().JwtEncode(new Dictionary<string, object>()
                            {
                                {"loginname",model.LoginName},
                                { "LoginPwd",model.LoginPwd}
                            })
                    );
                }
            }
            return InternalServerError(new Exception("账号密码有误"));
        }
        [Route("UserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            var token = Request.Headers.GetValues("aa").FirstOrDefault();
            if (string.IsNullOrEmpty(token))
                return InternalServerError(new Exception("token不存在"));
            return Ok(new JwtTools().JwtDecode(token));
        } 
    }
}
