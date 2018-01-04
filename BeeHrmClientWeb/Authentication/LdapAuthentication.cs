using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace BeeHrmClientWeb.Authentication
{
   
        public class LdapAuthentication
        {
            private string _path;
            private string _filterAttribute;

            /// <summary>
            /// Contruct domain path to be used in domain user verification.
            /// </summary>
            /// <param name="path">path for domain. Eg: LDAP://spi.com</param>
            public LdapAuthentication(string path)
            {
                _path = path;
            }

            /// <summary>
            /// Authenticate user against domain constructed.
            /// </summary>
            /// <param name="domain">Name of domain.</param>
            /// <param name="username">Login user name.</param>
            /// <param name="pwd">Password of a domain user.</param>
            /// <returns>Returns true if user is valid against domain else returns false.</returns>
            public bool IsAuthenticated(string domain, string username, string pwd)
            {
                bool authenticated = false;
                try
                {

                    DirectoryEntry entry = new DirectoryEntry(_path, username, pwd);
                    object nativeObject = entry.NativeObject;
                    authenticated = true;

                }
                catch (DirectoryServicesCOMException cex)
                {
                    //not authenticated; reason why is in cex
                }
                catch (Exception ex)
                {
                    throw new Exception("Error authenticating user. " + ex.Message);
                }

                return authenticated;
            }
        }
    }