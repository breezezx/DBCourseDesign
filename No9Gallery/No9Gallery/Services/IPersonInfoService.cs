using Microsoft.AspNetCore.Http;
using No9Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace No9Gallery.Services
{
    public interface IPersonInfoService
    {
        Task<PersonInfo> GetPersonInfoAsync(string UserID,string ViewID);
        Task<string> ReviseInfo(ReviseInfo reviseinfo);
        Task<string> Uploadavatar(string id,List<IFormFile> files);
        Task<List<Chargelist>> GetChargeListAsync(string id);
        Task<int> GetPointsAsync(string id);
        Task<string> ChargeSubmit(string order_no, string id, int amount, string ConTent, string chargetime,int points);
        Task<string> ChangeFollow(string visitid, string id);
        Task<string> UploadWork(string Userid,IFormFile file, string Workname, string WorkType,string Introduction,int NewWorkPoint);
        Task<string> Upgrade(string id,string Level,string Points, string order_no, string upgradetime);

        //新加
        Task<List<MessageList>> GetReportAsync();
        Task<string> PostMassage(string adminID, string ReceiverID, string Content,string Time);
        Task<string> ChangeMessage();
        Task<List<MessageReceivelist>> GetMessageAsync(string id);
        Task<string> setRead(string msgid);
        Task<string> PostAllMessage(string adminID,string Content,string Time);
    }
}
