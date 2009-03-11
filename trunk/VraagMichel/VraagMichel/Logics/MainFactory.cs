using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTO;
using Exceptions;

namespace Logics
{
    public class MainFactory
    {
        // Properties
        private UserLogic _UserLogic { get; set; }
        public User _User { get; set; }
        public string ErrorMsg { get; private set; }


        #region Singleton Pattern

        private static readonly object syncroot = new Object();
        private static MainFactory instance;

        private MainFactory()
        {
            _UserLogic = new UserLogic();
        }

        public static MainFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncroot)
                    {
                        if (instance == null)
                            instance = new MainFactory();
                    }
                }

                return instance;
            }
        }

        #endregion


        //***************************************************//
        public bool Login(string userbarcode)
        {
            if (userbarcode != null)
            {
                var user = _UserLogic.GetUserLocal(userbarcode);

                if (user == null)
                {
                    try
                    {
                        _UserLogic.SynchronizeBackend(userbarcode);
                    }
                    catch (FB_Exception ex)
                    {
                        ErrorMsg = ex.EntityMessage;
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = string.Format("Exception in Login:\r\n{0}", ex);
                    }
                    user = _UserLogic.GetUserLocal(userbarcode);
                }

                if (user != null)
                {

                    _User = user;
                    SelectRole();
                    return true;
                }
                ErrorMsg = "User not found";
                return false;
            }
            ErrorMsg = "Userbarcode is null";
            return false;
      }

        //***************************************************//
        public bool Login2(string userbarcode)
        {
            try
            {
                var user = _UserLogic.GetUserLocal(userbarcode);

                if (user == null)
                {
                    _UserLogic.SynchronizeBackend(userbarcode);
                    user = _UserLogic.GetUserLocal(userbarcode);
                }

                if (user != null)
                {
                    _User = user;
                    SelectRole();
                    return true;
                }
                ErrorMsg = "User Not Found";
            }
            catch (FB_Exception ex)
            {
                ErrorMsg = ex.EntityMessage;
            }
            catch (Exception ex)
            {
                ErrorMsg = string.Format("Exception in Login:\r\n{0}", ex);
            }

            return false;
        }


        //***************************************************//
        public bool Login3(string userbarcode)
        {
            if (string.IsNullOrEmpty(userbarcode))
            {
                ErrorMsg = "Userbarcode is null";
                return false;
            }

            _User = _UserLogic.GetUserLocal(userbarcode) ?? GetUserFromBackEnd(userbarcode);
            if (_User == null)
            {
                ErrorMsg = "User not found";
                return false;
            }
            SelectRole();
            return true;

        }

        private User GetUserFromBackEnd(string userbarcode)
        {
            try
            {
                _UserLogic.SynchronizeBackend(userbarcode);
            }
            catch (FB_Exception ex)
            {
                ErrorMsg = ex.EntityMessage;
            }
            catch (Exception ex)
            {
                ErrorMsg = string.Format("Exception in Login:\r\n{0}", ex);
            }
            return _UserLogic.GetUserLocal(userbarcode);

        }







        private void SelectRole()
        {

        }

    }

}