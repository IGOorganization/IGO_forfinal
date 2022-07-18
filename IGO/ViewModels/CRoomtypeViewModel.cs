using IGO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CRoomtypeViewModel
    {
        DemoIgoContext _dbIgo;
        TTicketAndProduct _ticketandproduct = new TTicketAndProduct();
        public CRoomtypeViewModel(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public int? TicketAndProductId
        {
            get { return _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FProductId == ProductId && n.FTicketId == ticketId).FTicketAndProductId; }
        }
        public int? ProductId
        {
            get { return _ticketandproduct.FProductId; }
            set { _ticketandproduct.FProductId = value; }
        }
        public string ProductName
        {
            get { return _dbIgo.TProducts.FirstOrDefault(n=>n.FProductId==ProductId).FProductName; }
        }
        public int? ticketId
        {
            get { return _ticketandproduct.FTicketId; }
            set { _ticketandproduct.FTicketId = value; }
        }
        public decimal? dataPrice
        {
            get { return _dbIgo.TTicketAndProducts.FirstOrDefault(n => n.FProductId == ProductId && n.FTicketId == ticketId).FPrice; }
        }
        public int? Quantity
        {
            get { return _dbIgo.TProducts.FirstOrDefault(n=>n.FProductId==ProductId).FQuantity; }
        }
        public string Introduction
        {
            get { return _dbIgo.TProducts.FirstOrDefault(n => n.FProductId == ProductId).FIntroduction; }
        }
        public string roomtype
        {
            get { return _dbIgo.TTicketTypes.FirstOrDefault(n => n.FTicketId == ticketId).FTicketName; }
        }
    }
}
