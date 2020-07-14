using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMSProject.Models.DB;
using CMSProject.Models.ViewModel;

namespace CMSProject.Models.EntityManager
{
    [System.Runtime.InteropServices.Guid("4F1872D7-B7DF-4385-8D57-4023CB77A92A")]
    public class UserManager
    {
        public void AddUserAccount(UserSignUpView user)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                //User Table
                User U = new User();
                U.Username = user.Username;
                U.Password = user.Password;
                db.Users.Add(U);
                db.SaveChanges();

                //UserProfile Table
                UserProfile UP = new UserProfile();
                UP.UserID = U.UserID;
                UP.FirstName = user.FirstName;
                UP.LastName = user.LastName;
                UP.Gender = user.Gender;
                UP.BirthDate = user.BirthDate;
                UP.Mobile = user.Mobile;
                UP.Email = user.Email;
                UP.RoleID = 3;
                db.UserProfiles.Add(UP);
                db.SaveChanges();

                CustomerUser CU = new CustomerUser();
                CU.UserID = U.UserID;
                db.CustomerUsers.Add(CU);
                db.SaveChanges();
            }
        }

        public void AddEmployeeAccount(UserProfileView user)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                //User Table
                User U = new User();
                U.Username = user.Username;
                U.Password = user.Password;
                db.Users.Add(U);
                db.SaveChanges();

                //UserProfile Table
                UserProfile UP = new UserProfile();
                UP.UserID = U.UserID;
                UP.FirstName = user.FirstName;
                UP.LastName = user.LastName;
                UP.Gender = user.Gender;
                UP.BirthDate = Convert.ToDateTime(user.BirthDate);
                UP.Mobile = user.Mobile;
                UP.Email = user.Email;
                UP.RoleID = 2;
                db.UserProfiles.Add(UP);
                db.SaveChanges();
            }
        }

        public bool IsUserNameExist(string userName)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                return db.Users.Where(o => o.Username.Equals(userName)).Any();
            }
        }

        public bool IsEmailExist(string eMail)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                return db.UserProfiles.Where(i => i.Email.Equals(eMail)).Any();
            }
        }

        public string GetUserPassword(string userName)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var user = db.Users.Where(o => o.Username.ToLower().Equals(userName));
                if (user.Any())
                    return user.FirstOrDefault().Password;
                else
                    return string.Empty;
            }
        }
        public bool IsUserInRole(string userName, string roleName)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                User U = db.Users.Where(o => o.Username.ToLower().Equals(userName))?.FirstOrDefault();
                if (U != null)
                {
                    var roles = from q in db.UserProfiles
                                join u in db.UserRoles on q.RoleID equals
                                u.RoleID
                                where u.RoleName.Equals(roleName) &&
                                q.UserID.Equals(U.UserID)
                                select u.RoleName;
                    if (roles != null)
                    {
                        return roles.Any();
                    }
                }
                return false;
            }
        }

        public List<Branches> GetAllBranches()
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var branches = db.BranchLists.Select(o => new Branches
                {
                    BranchID = o.BranchID
                }).ToList();
                return branches;
            }
        }

        public int GetUserID(string userName)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                var user = db.Users.Where(o => o.Username.Equals(userName));
                if (user.Any())
                    return user.FirstOrDefault().UserID;
            }
            return 0;
        }

        public List<UserProfileView> GetAllUserProfiles()
        {
            List<UserProfileView> profiles = new List<UserProfileView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                UserProfileView UPV;
                var users = db.Users.ToList();
                foreach (User u in db.Users)
                {
                    UPV = new UserProfileView();
                    UPV.UserID = u.UserID;
                    UPV.Username = u.Username;
                    UPV.Password = u.Password;
                    var UP = db.UserProfiles.Find(u.UserID);
                    if (UP != null)
                    {
                        UPV.UserProfileID = UP.UserProfileID;
                        UPV.FirstName = UP.FirstName;
                        UPV.LastName = UP.LastName;
                        UPV.Gender = UP.Gender;
                        UPV.BirthDate = Convert.ToDateTime(UP.BirthDate).ToString("yyyy-MM-dd");
                        UPV.Mobile = UP.Mobile;
                        UPV.Email = UP.Email;
                    }
                    var SUR = db.UserRoles.Where(o => o.RoleID.Equals(UP.RoleID));
                    if (SUR.Any())
                    {
                        var userRole = SUR.FirstOrDefault();
                        UPV.RoleID = userRole.RoleID;
                        UPV.RoleName = userRole.RoleName;
                    }
                    if (UPV.RoleName.Equals("Employee"))
                        profiles.Add(UPV);
                }
            }
            return profiles;
        }

        public List<CustomerProfileView> GetAllCustomerProfiles()
        {
            List<CustomerProfileView> profiles = new List<CustomerProfileView>();
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                CustomerProfileView CPV;
                var users = db.Users.ToList();
                foreach (User u in db.Users)
                {
                    CPV = new CustomerProfileView();
                    CPV.UserID = u.UserID;
                    CPV.Username = u.Username;
                    CPV.Password = u.Password;
                    var UP = db.UserProfiles.Find(u.UserID);
                    if (UP != null)
                    {
                        CPV.UserProfileID = UP.UserProfileID;
                        CPV.FirstName = UP.FirstName;
                        CPV.LastName = UP.LastName;
                        CPV.Gender = UP.Gender;
                        CPV.BirthDate = Convert.ToDateTime(UP.BirthDate).ToString("yyyy-MM-dd");
                        CPV.Mobile = UP.Mobile;
                        CPV.Email = UP.Email;
                    }
                    var SUR = db.UserRoles.Where(o => o.RoleID.Equals(UP.RoleID));
                    if (SUR.Any())
                    {
                        var userRole = SUR.FirstOrDefault();
                        CPV.RoleID = userRole.RoleID;
                        CPV.RoleName = userRole.RoleName;
                    }
                    if (CPV.RoleName.Equals("Customer"))
                    {
                        var CU = db.CustomerUsers.Where(o => o.UserID.Equals(CPV.UserID)).FirstOrDefault();
                        CPV.Active = CU.Active;
                        profiles.Add(CPV);
                    }
                }
            }
            return profiles;
        }


        public UserProfileView GetUserProfile(int userprofileid)
        {
            UserProfileView profile = new UserProfileView();
            profile = GetAllUserProfiles().Where(o => o.UserProfileID.Equals(userprofileid)).FirstOrDefault();
            return profile;
        }

        public void UpdateUserAccount(UserProfileView user)
        {
            using (CMSProjectEntities db = new CMSProjectEntities())
            {
                //User Table
                User U = new User();
                U.UserID = user.UserID;
                U.Username = user.Username;
                U.Password = user.Password;
                db.Entry(U).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //UserProfile Table
                UserProfile UP = new UserProfile();
                UP.UserProfileID = user.UserProfileID;
                UP.UserID = U.UserID;
                UP.FirstName = user.FirstName;
                UP.LastName = user.LastName;
                UP.Gender = user.Gender;
                UP.BirthDate = Convert.ToDateTime(user.BirthDate);
                UP.Mobile = user.Mobile;
                UP.Email = user.Email;
                UP.RoleID = user.RoleID;
                db.Entry(UP).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}