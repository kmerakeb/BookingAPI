using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class PageFactory
    {
        public PageFactory()
        {

        }
        public Page CreatePage(DAO.Page page)
        {


            return new Page()
            {

            Id = page.Id,
            PageURL = page.PageURL,
            BusinessId = page.BusinessId,
            CreateDate = page.CreateDate,
            UpdateDate = page.UpdateDate,
            IsActive = page.IsActive,
            PublishDate = page.PublishDate


    };
        }
        public DAO.Page CreatePage(Page page)
        {
            return new DAO.Page()
            {

                Id = page.Id,
                PageURL = page.PageURL,
                BusinessId = page.BusinessId,
                CreateDate = page.CreateDate,
                UpdateDate = page.UpdateDate,
                IsActive = page.IsActive,
                PublishDate = page.PublishDate

            };
        }
    }
}
