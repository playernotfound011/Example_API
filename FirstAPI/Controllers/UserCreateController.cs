using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstAPI.Models;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreateController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
            {
                var query = (from a in db.UserWs
                             orderby a.IdUser
                             select a).ToList();

                return Ok(query);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Models.Request.UserRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                    {
                        var exist = (from a in db.UserWs
                                     where a.Username == model.userName
                                     select a).FirstOrDefault();

                        if (exist == null)
                        {
                            Models.DB.UserW insUser = new Models.DB.UserW();
                            insUser.Username = model.userName;
                            insUser.Email = model.email;
                            insUser.UserPass = Encrypt.GetSHA256(model.password);
                            insUser.TypeUser = model.typeUser;
                            insUser.CreationDate = DateTime.Now;

                            db.UserWs.Add(insUser);
                            db.SaveChanges();

                            return Ok( new { Message = "User created!", User_ID = insUser.IdUser});
                        }
                        else
                        {
                            return Ok( new { Error = "User already exist!"});
                        }
                    }
                }
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] Models.Request.UserEditRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                    {
                        var exist = (from a in db.UserWs
                                     where a.IdUser == model.userId
                                     select a).FirstOrDefault();

                        if (exist != null)
                        {
                            Models.DB.UserW updUser = db.UserWs.Find(model.userId);
                            updUser.Username = model.userName;
                            updUser.UserPass = Encrypt.GetSHA256(model.password);
                            updUser.Email = model.email;
                            updUser.TypeUser = model.typeUser;
                            db.Entry(updUser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            db.SaveChanges();

                            return Ok(new { Message = "User ID: " + updUser.IdUser + " modified" });
                        }
                        else
                        {
                            return Ok(new { Error = "User not found!" });
                        }
                    }
                }
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                   
        }

        [HttpDelete]

        public ActionResult Delete([FromBody] Models.Request.UserEditRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (Models.DB.WEBSITEContext db = new Models.DB.WEBSITEContext())
                    {
                        var exist = (from a in db.UserWs
                                     where a.IdUser == model.userId
                                     select a).FirstOrDefault();

                        if (exist != null)
                        {
                            Models.DB.UserW updUser = db.UserWs.Find(model.userId);
                            db.UserWs.Remove(updUser);
                            db.SaveChanges();

                            return Ok(new { Message = "User ID: " + updUser.IdUser + " Deleted!" });
                        }
                        else
                        {
                            return Ok(new { Error = "User not found!" });
                        }

                    }
                }
                else
                {
                    return ValidationProblem();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        } 
    }
}
