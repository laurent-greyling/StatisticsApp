using Nfield.Stats.Entities;
using Nfield.Stats.Models;
using Nfield.Stats.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;

namespace Nfield.Stats.Services
{
    public class NfieldServer : INfieldServer
    {
        readonly ISqliteService<ServerEntity> _sqliteService;
        public NfieldServer()
        {
            _sqliteService = DependencyService.Get<ISqliteService<ServerEntity>>();
        }

        public void ClearAll()
        {
            _sqliteService.DeleteAll();
        }

        public ServerEntity Get()
        {
            return _sqliteService.Get().FirstOrDefault();
        }

        public void Set(string serverSelected)
        {
            var server = new ServerEntity();
            switch (serverSelected)
            {
                case AppConst.America:
                    server.NfieldServer = AppConst.UrlAmerica;
                    server.ServerName = AppConst.America;
                    break;
                case AppConst.Asia:
                    server.NfieldServer = AppConst.UrlAsia;
                    server.ServerName = AppConst.Asia;
                    break;
                case AppConst.Blue:
                    server.NfieldServer = AppConst.UrlBlue;
                    server.ServerName = AppConst.Blue;
                    break;
                case AppConst.China:
                    server.NfieldServer = AppConst.UrlChina;
                    server.ServerName = AppConst.China;
                    break;
                case AppConst.Europe:
                    server.NfieldServer = AppConst.UrlEurope;
                    server.ServerName = AppConst.Europe;
                    break;
                case AppConst.Orange:
                    server.NfieldServer = AppConst.UrlOrange;
                    server.ServerName = AppConst.Orange;
                    break;
                case AppConst.Purple:
                    server.NfieldServer = AppConst.UrlPurple;
                    server.ServerName = AppConst.Purple;
                    break;
                case AppConst.Red:
                    server.NfieldServer = AppConst.UrlRed;
                    server.ServerName = AppConst.Red;
                    break;
                case AppConst.White:
                    server.NfieldServer = AppConst.UrlWhite;
                    server.ServerName = AppConst.White;
                    break;
                case AppConst.Yellow:
                    server.NfieldServer = AppConst.UrlYellow;
                    server.ServerName = AppConst.Yellow;
                    break;
                case AppConst.Rc:
                    server.NfieldServer = AppConst.UrlRc;
                    server.ServerName = AppConst.Rc;
                    break;
                default:
                    break;
            }

            var exists = Get();

            if (exists == null)
            {
                _sqliteService.Add(server);
            }
            else
            {
                _sqliteService.Update(server);
            }
        }
    }
}
