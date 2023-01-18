using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.EntityLayer.Concrete;
using System.Threading.Tasks;
using System;
using RealEstate.BusinessLayer.Concrete;
using RealEstate.DataAccessLayer.EntityFramework;
using RealEstate.DataAccessLayer.Concrete;
using System.Linq;

namespace RealEstate.PresentationLayer.Controllers
{

        public class MessageController : Controller
        {
            MessageManager wmm = new MessageManager(new EfMessageDal());
            private readonly UserManager<AppUser> _userManager;

            public MessageController(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<IActionResult> ReceiverMessage(string p)
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);
                p = values.Email;
                var messageList = wmm.GetListReceiverMessage(p);
                return View(messageList);
            }
       
            public async Task<IActionResult> SenderMessage(string p)
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);
                p = values.Email;
                var messageList = wmm.GetListSenderMessage(p);
                return View(messageList);
            }
 
            public IActionResult MessageDetails(int id)
            {
                Message message = wmm.TGetByID(id);
                return View(message);
            }
            public IActionResult ReceiverMessageDetails(int id)
            {
                Message message = wmm.TGetByID(id);
                return View(message);
            }
            [HttpGet]
          
            public IActionResult SendMessage()
            {
                return View();
            }
            [HttpPost]
          
            public async Task<IActionResult> SendMessage(Message p)
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);
                string mail = values.Email;
                string name = values.Name + " " + values.Surname;
                p.Sender = mail;
                p.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                p.SenderName = name;
                Context c = new Context();
                var usernamesurname = c.Users.Where(x => x.Email == p.Receiver).Select(y => y.Name + " " + y.Surname).FirstOrDefault();
                p.ReceiverName = usernamesurname;
                wmm.TInsert(p);
                return RedirectToAction("SenderMessage");
            }
        }
    }


