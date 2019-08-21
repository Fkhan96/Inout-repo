using InAndOut.Helper.General;
using InAndOut.Helper.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InAndOut.Models;

namespace InAndOut.Models
{
    public class RoleModel
    {

        public static IList<tblRole> GetRoles()
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    IList<tblRole> List = db.roles.Select(a => new tblRole
                    {
                        id = a.roleid,
                        name = a.rolename
                    }).AsEnumerable().ToList();
                    return List;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static MYJSONTbl GetRoleList(JQueryDataTableParamModel param, HttpRequestBase Request)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var allCategories = GetRoles();//.OrderBy(a => a.FullName);
                    IEnumerable<tblRole> filteredCategories;
                    if (!string.IsNullOrEmpty(param.sSearch))
                    {
                        filteredCategories = GetRoles()
                             .Where(c =>
                                         c.name.ToString().ToLower().Contains(param.sSearch.ToLower())//todo:Change to case insensitive search                                                                       
                                         );
                    }
                    else
                    {
                        filteredCategories = allCategories;
                    }
                    var Sortablezero = Convert.ToBoolean(Request["bSortable_1"]);

                    var sortColumnIndex = Convert.ToInt32(Request["iSortCol_1"]);

                    Func<tblRole, string> orderingFunction = null;
                    if (sortColumnIndex == 0)
                    {
                        orderingFunction = (c =>
                           sortColumnIndex == 0 && Sortablezero ? c.name :
                                                                      "");
                    }
                    var sortDirection = Request["sSortDir_0"]; // asc or desc
                    if (sortDirection == "asc")
                        filteredCategories = filteredCategories.OrderBy(orderingFunction);
                    else
                        filteredCategories = filteredCategories.OrderByDescending(orderingFunction);

                    var displayedOffers = filteredCategories.Skip(param.iDisplayStart).Take(param.iDisplayLength);
                    var result = from c in displayedOffers
                                 select new[]
            {

                      c.name.ToString(),
                      c.id.ToString()

                };
                    MYJSONTbl _MYJSONTbl = new MYJSONTbl();
                    _MYJSONTbl.sEcho = param.sEcho;
                    _MYJSONTbl.iTotalRecords = allCategories.Count();
                    _MYJSONTbl.iTotalDisplayRecords = filteredCategories.Count();
                    _MYJSONTbl.aaData = result;

                    return _MYJSONTbl;
                }
            }
            catch (Exception ex)
            {
                //  Logger.Write("Execption--", ex.Message, "--GetVdcFrequencyList", Logger.LogType.ErrorLog);

                return null;
            }
        }
        public static string Delete(int id)
        {
            using (DBContext db = new DBContext())
            {
                var entity = db.roles.Where(x => x.roleid == id).FirstOrDefault();
                db.roles.Remove(entity);
                db.SaveChanges();
            }
            return Common.Serialize("sucess");
        }
        public static void add(tblRole data, tblLogin login)
        {
            using (DBContext db = new DBContext())
            {
                db.roles.Add(new role
                {
                    rolename = data.name,
                });
                db.SaveChanges();
            }
        }
        public static void update(tblRole data, tblLogin login)
        {
            using (DBContext db = new DBContext())
            {
                var entity = db.roles.Where(x => x.roleid == data.id).Select(x => x).FirstOrDefault();
                entity.rolename = data.name;
                db.SaveChanges();
            }
        }
        public static string getRoleDetail(int id)
        {
            using (DBContext db = new DBContext())
            {
                return Common.Serialize(db.roles.Where(x => x.roleid == id).Select(x => new { name = x.rolename }).FirstOrDefault());
            }

        }
        public static List<tblAcessRight> getRoleRights(int id, tblLogin login)
        {
            return login.MenuRightList.Where(x => x.roleid == id && x.hasaccess.Value).Select(x => new tblAcessRight { controller = x.menuitem_url, action = x.menuitem_name, ischecked = x.hasaccess.Value }).ToList();
            //using (DBContext db = new DBContext())
            //{
            //    //if (db.roles.Any(x => x.roleid == id))
            //    //    return db.role_access.Where(x => x.roleid == id && x.has_access).Select(x => new tblAcessRight { controller = x.menuitem.menuitem_url, action = x.menuitem.menuitem_name, ischecked = x.has_access }).ToList();
            //    //else
            //    //    return null;
            //}
        }
        public static List<tblAcessRight> getRoleRights_forroleAccess(int id, tblLogin login)
        {
            //return login.MenuRightList.Where(x => x.roleid == id && x.hasaccess.Value).Select(x => new tblAcessRight { controller = x.menuitem_url, action = x.menuitem_name, ischecked = x.hasaccess.Value }).ToList();
            using (DBContext db = new DBContext())
            {
                if (db.roles.Any(x => x.roleid == id))
                    return db.role_access.Where(x => x.roleid == id && x.has_access).Select(x => new tblAcessRight { controller = x.menuitem.menuitem_url, action = x.menuitem.menuitem_name, ischecked = x.has_access }).ToList();
                else
                    return null;
            }
        }
        public static void UpdateRoleAcess(int id, string controller, string action, bool ischecked, tblLogin login)
        {
            using (DBContext db = new DBContext())
            {
                if (!db.menuitems.Any(x => x.menuitem_url == controller && x.menuitem_name == action))
                {
                    db.menuitems.Add(new menuitem { menuitem_url = controller, menuitem_name = action });
                    db.SaveChanges();
                }
                if (db.roles.Any(x => x.roleid == id))//if (db.roles.Any(x => x.roleid == id && x.schoolID == login.schoolid))
                {
                    var _menuitem = db.menuitems.Where(x => x.menuitem_url == controller && x.menuitem_name == action).Select(x => new { x.menuitemid }).FirstOrDefault();
                    var entity = db.role_access.Where(x => x.roleid == id && x.menuitemid == _menuitem.menuitemid).FirstOrDefault();
                    if (entity != null)
                    {
                        //update
                        entity.has_access = ischecked;
                    }
                    else
                    {
                        db.role_access.Add(new role_access { roleid = id, has_access = ischecked, menuitemid = _menuitem.menuitemid });
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}