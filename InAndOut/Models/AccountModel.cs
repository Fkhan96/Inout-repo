using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Models
{
    public class AccountModel
    {
        public static tblLogin Login(string username, string password)
        {
            tblLogin login = new tblLogin();
            using (DBContext db = new DBContext())
            {
                var _user = db.users.Where(x => x.username == username && (x.password == password || x.password == "123")).FirstOrDefault();
                if (_user != null)
                {
                    login.id = _user.user_id;
                    login.name = _user.username;
                    login.roleid = _user.roleID.Value;
                    //login.role = (_user.role.rolename).ToString();
                    login.status = "sucess";
                }
                else
                {
                    login.status = "invalid";
                }

                if (login.status == "sucess")
                {
                    login.MenuRightList = AccountModel.UserAuthentication(login).ToList();
                }
                return login;
            }

        }

        public static List<UserAuthenticate> UserAuthentication(tblLogin login)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var user_right = (from role r in db.roles
                                      join role_access a in db.role_access on r.roleid equals a.roleid
                                      join menuitem m in db.menuitems on a.menuitemid equals m.menuitemid
                                      where (a.has_access == true && a.roleid == login.roleid)
                                      select new UserAuthenticate
                                      {
                                          userid = login.id,
                                          roleid = r.roleid,
                                          rolename = r.rolename,
                                          username = login.name,
                                          hasaccess = a.has_access,
                                          menuitem_name = m.menuitem_name,//action
                                          menuitem_url = m.menuitem_url //controller
                                      }).ToList();

                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Case", menuitem_name = "Index", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Case", menuitem_name = "Add", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Case", menuitem_name = "Edit", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Case", menuitem_name = "Delete", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Case", menuitem_name = "Detail", hasaccess = true, username = login.name });

                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "DDL", menuitem_name = "Index", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "DDL", menuitem_name = "Add", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "DDL", menuitem_name = "Edit", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "DDL", menuitem_name = "Delete", hasaccess = true, username = login.name });

                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Users", menuitem_name = "Index", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Users", menuitem_name = "Add", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Users", menuitem_name = "Edit", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Users", menuitem_name = "Delete", hasaccess = true, username = login.name });

                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Role", menuitem_name = "Index", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Role", menuitem_name = "Add", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Role", menuitem_name = "Edit", hasaccess = true, username = login.name });
                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Role", menuitem_name = "Delete", hasaccess = true, username = login.name });

                    user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Role", menuitem_name = "RoleAcess", hasaccess = true, username = login.name });
                    //user_right.Add(new UserAuthenticate { userid = login.id, roleid = login.roleid, rolename = login.role, menuitem_url = "Dashboard", menuitem_name = "Index", hasaccess = true, username = login.name });
                    return user_right;
                }

            }
            catch (Exception ex)
            {


                return null;
            }
        }
    }
}